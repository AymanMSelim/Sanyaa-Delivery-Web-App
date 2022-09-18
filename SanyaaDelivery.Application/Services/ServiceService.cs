using App.Global.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IRepository<ServiceT> serviceRepository;
        private readonly IRepository<FavouriteServiceT> favouriteServiceRepository;
        private readonly ICartService cartService;
        private readonly IServiceRatioService serviceRatioService;
        private readonly IClientService clientService;

        public ServiceService(IRepository<ServiceT> serviceRepository, IRepository<FavouriteServiceT> favouriteServiceRepository, 
            ICartService cartService, IServiceRatioService serviceRatioService, 
            IClientService clientService)
        {
            this.serviceRepository = serviceRepository;
            this.favouriteServiceRepository = favouriteServiceRepository;
            this.cartService = cartService;
            this.serviceRatioService = serviceRatioService;
            this.clientService = clientService;
        }
        public async Task<int> AddAsync(ServiceT service)
        {
            await serviceRepository.AddAsync(service);
            return await serviceRepository.SaveAsync();
        }

        public async Task<int> DeleteAsync(int serviceId)
        {
            await serviceRepository.DeleteAsync(serviceId);
            return await serviceRepository.SaveAsync();
        }

        public Task<ServiceT> GetAsync(int serviceId, bool includeDepartment = false)
        {
            var query = serviceRepository.DbSet.Where(d => d.ServiceId == serviceId);
            if (includeDepartment)
            {
                query = query
                    .Include(d => d.Department)
                    .ThenInclude(d => d.DepartmentSub0Navigation)
                    .ThenInclude(d => d.Department);
            }
            return query.FirstOrDefaultAsync();
        }

        public Task<List<ServiceT>> GetAsync(string serviceName)
        {
            return serviceRepository
                .Where(d => d.ServiceName.Contains(serviceName))
                .ToListAsync();
        }

        public Task<List<ServiceT>> GetListByDepartmentSub1Async(int departmentSub1Id)
        {
            return serviceRepository
                .Where(d => d.DepartmentId == departmentSub1Id)
                .ToListAsync();
        }

        public Task<List<ServiceT>> GetListByDeparmentSub0Async(int departmentSub0Id)
        {
            return serviceRepository
                .Where(d => d.Department.DepartmentSub0Id == departmentSub0Id)
                .ToListAsync();
        }

        public Task<List<ServiceT>> GetListByMainDeparmentAsync(int departmentId)
        {
            return serviceRepository
                .Where(d => d.Department.DepartmentSub0Navigation.DepartmentId == departmentId)
                .ToListAsync();
        }

        public Task<List<ServiceT>> GetOfferListByDepartmentSub1Async(int departmentSub1Id)
        {
            return serviceRepository
                .Where(d => d.DepartmentId == departmentSub1Id && d.ServiceDiscount > 0)
                .ToListAsync();
        }

        public Task<List<ServiceT>> GetOfferListByDeparmentSub0Async(int departmentSub0Id)
        {
            return serviceRepository
                .Where(d => d.Department.DepartmentSub0Id == departmentSub0Id && d.ServiceDiscount > 0 && d.NoDiscount == false)
                .ToListAsync();
        }

        public Task<List<ServiceT>> GetOfferListByMainDeparmentAsync(int departmentId)
        {
            return serviceRepository
                .Where(d => d.Department.DepartmentSub0Navigation.Department.DepartmentId == departmentId && d.ServiceDiscount > 0 && d.NoDiscount == false)
                .ToListAsync();
        }

        public Task<int> UpdateAsync(ServiceT service)
        {
            serviceRepository.Update(service.ServiceId, service);
            return serviceRepository.SaveAsync();
        }

        public Task<DepartmentT> GetServiceMainDepartmentAsync(int serviceId)
        {
            return serviceRepository.DbSet
                .Include(d => d.Department)
                .ThenInclude(d => d.DepartmentSub0Navigation)
                .ThenInclude(d => d.Department)
                .Where(d => d.ServiceId == serviceId)
                .Select(d => d.Department.DepartmentSub0Navigation.Department)
                .FirstOrDefaultAsync();
        }

        public async Task<List<ServiceCustom>> ConvertServiceToCustom(List<ServiceT> serviceList, int clientId)
        {

            int departmentId;
            int cityId;
            decimal ratio = 1;
            if (serviceList.IsNull() || serviceList.Count == 0)
            {
                return new List<ServiceCustom>();
            }
            var service = await GetAsync(serviceList.FirstOrDefault().ServiceId, true);
            departmentId = service.Department.DepartmentSub0Navigation.DepartmentId.Value;
            cityId = await clientService.GetDefaultCityIdAsync(clientId);
            var serviceIdList = serviceList.Select(d => d.ServiceId).ToList();
            var favouriteServiceList = await favouriteServiceRepository.Where(d => d.ClientId == clientId && serviceIdList.Contains(d.ServiceId)).ToListAsync();
            var inCartServiceList = await cartService.GetDetailsListByClientIdAsync(clientId);
            var ratioList = await serviceRatioService.GetListAsync(cityId, departmentId, true);
            if (ratioList.HasItem())
            {
                ratio = 1 + (ratioList.FirstOrDefault().Ratio.Value / 100);
            }
            if (favouriteServiceList.IsNull())
            {
                favouriteServiceList = new List<FavouriteServiceT>();
            }
            if (inCartServiceList.IsNull())
            {
                inCartServiceList = new List<CartDetailsT>();
            }
            serviceList.ForEach(d => d.ServiceCost = (short)((decimal)d.ServiceCost * ratio));
            return serviceList.Select(d => new ServiceCustom
            {
                ServiceId = d.ServiceId,
                CompanyDiscountPercentage = d.CompanyDiscountPercentage,
                DepartmentId = d.DepartmentId,
                DiscountServiceCount = d.DiscountServiceCount,
                IsFavourite = favouriteServiceList.Any(f => f.ServiceId == d.ServiceId),
                IsInCart = inCartServiceList.Any(c => c.ServiceId == d.ServiceId),
                CartQuantity = inCartServiceList.Any(c => c.ServiceId == d.ServiceId) ? inCartServiceList.FirstOrDefault(c => c.ServiceId == d.ServiceId).ServiceQuantity.Value : 0,
                MaterialCost = d.MaterialCost,
                NoDiscount = d.NoDiscount,
                ServiceCost = d.ServiceCost,
                ServiceDiscount = d.NoDiscount.HasValue ? d.NoDiscount.Value ? 0 : d.ServiceDiscount : 0,
                ServiceDuration = d.ServiceDuration,
                ServiceName = d.ServiceName,
                ServicePoints = d.ServicePoints,
                HasDiscount = d.NoDiscount == false && d.ServiceDiscount > 0,
                NetServiceCost = GetNetServiceCost(d),
                ServiceDes = d.ServiceDes
            }).ToList();
        }

        public async Task<List<ServiceCustom>> ConvertServiceToCustomMultiDepartment(List<ServiceT> serviceList, int clientId)
        {

            if (serviceList.IsNull() || serviceList.Count == 0)
            {
                return new List<ServiceCustom>();
            }
            var cityId = await clientService.GetDefaultCityIdAsync(clientId);
            var ratioList = await serviceRatioService.GetListAsync(cityId, null, true, true);
            var serviceIdList = serviceList.Select(d => d.ServiceId).ToList();
            var favouriteServiceList = await favouriteServiceRepository.Where(d => d.ClientId == clientId && serviceIdList.Contains(d.ServiceId)).ToListAsync();
            var inCartServiceList = await cartService.GetDetailsListByClientIdAsync(clientId);
            await GetServiceMainDepartmentAsync(serviceIdList);
            if (favouriteServiceList.IsNull())
            {
                favouriteServiceList = new List<FavouriteServiceT>();
            }
            if (inCartServiceList.IsNull())
            {
                inCartServiceList = new List<CartDetailsT>();
            }
            foreach (var item in serviceList)
            {
                var departmentRatio = ratioList.Where(d => d.ServiceRatioDetailsT.Any(t => t.DepartmentId == item.Department.DepartmentSub0Navigation.DepartmentId)).FirstOrDefault();
                if (departmentRatio.IsNotNull())
                {
                    item.ServiceCost = (short)(item.ServiceCost + (short)((departmentRatio.Ratio / 100) * (decimal)item.ServiceCost));
                }
            }
            return serviceList.Select(d => new ServiceCustom
            {
                ServiceId = d.ServiceId,
                CompanyDiscountPercentage = d.CompanyDiscountPercentage,
                DepartmentId = d.DepartmentId,
                DiscountServiceCount = d.DiscountServiceCount,
                IsFavourite = favouriteServiceList.Any(f => f.ServiceId == d.ServiceId),
                IsInCart = inCartServiceList.Any(c => c.ServiceId == d.ServiceId),
                CartQuantity = inCartServiceList.Any(c => c.ServiceId == d.ServiceId) ? inCartServiceList.FirstOrDefault(c => c.ServiceId == d.ServiceId).ServiceQuantity.Value : 0,
                MaterialCost = d.MaterialCost,
                NoDiscount = d.NoDiscount,
                ServiceCost = d.ServiceCost,
                ServiceDiscount = d.NoDiscount.HasValue ? d.NoDiscount.Value ? 0 : d.ServiceDiscount : 0,
                ServiceDuration = d.ServiceDuration,
                ServiceName = d.ServiceName,
                ServicePoints = d.ServicePoints,
                HasDiscount = d.NoDiscount == false && d.ServiceDiscount > 0,
                NetServiceCost = GetNetServiceCost(d),
                ServiceDes = d.ServiceDes
            }).ToList();
        }


        private short GetNetServiceCost(ServiceT service)
        {
            if((service.NoDiscount.HasValue && service.NoDiscount.Value) || service.ServiceDiscount.HasValue == false || service.ServiceDiscount <= 0)
            {
                return service.ServiceCost;
            }
            if (service.DiscountServiceCount > 0)
            {
                service.ServiceDes = $"اطلب {service.DiscountServiceCount} خدمة واحصل على الخصم";
            }
            return (short)((decimal)service.ServiceCost - ((decimal)service.ServiceDiscount.Value / 100 * (decimal)service.ServiceCost));

        }
        public Task<List<ServiceT>> GetServiceList(int? mainDepartmentId = null, int? departmentSub0Id = null, int? departmentSub1Id = null, bool? getOffer = null)
        {
            var query = serviceRepository.DbSet.AsQueryable();
            if (mainDepartmentId.HasValue)
            {
                query = query.Where(d => d.Department.DepartmentSub0Navigation.Department.DepartmentId == mainDepartmentId);
            }
            if (departmentSub0Id.HasValue)
            {
                query = query.Where(d => d.Department.DepartmentSub0Id == departmentSub0Id);
            }
            if (departmentSub1Id.HasValue)
            {
                query = query.Where(d => d.DepartmentId == departmentSub1Id);
            }
            if (getOffer.HasValue)
            {
                query = query.Where(d => d.NoDiscount == false && d.ServiceDiscount > 0);
            }
            return query.ToListAsync();
        }


        public async Task<List<ServiceCustom>> GetCustomServiceList(int clientId, int? mainDepartmentId = null, int? departmentSub0Id = null, int? departmentSub1Id = null, bool? getOffer = null)
        {
            var serviceList = await GetServiceList(mainDepartmentId, departmentSub0Id, departmentSub1Id, getOffer);
            return await ConvertServiceToCustom(serviceList, clientId);
        }

        public Task<List<ServiceT>> GetServiceMainDepartmentAsync(List<int> serviceIdList)
        {
            return serviceRepository.DbSet
                          .Include(d => d.Department)
                          .ThenInclude(d => d.DepartmentSub0Navigation)
                          .ThenInclude(d => d.Department)
                          .Where(d => serviceIdList.Contains(d.ServiceId))
                          .ToListAsync();
        }
    }
}
