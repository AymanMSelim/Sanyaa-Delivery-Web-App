using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using App.Global.ExtensionMethods;
using System.Transactions;
using App.Global.DTOs;
using SanyaaDelivery.Infra.Data;

namespace SanyaaDelivery.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<ClientT> clientRepository;
        private readonly IRepository<AddressT> addressRepository;
        private readonly IRepository<ClientPhonesT> phoneRepository;
        private readonly IClientSubscriptionService clientSubscriptionService;
        private readonly ISubscriptionService subscriptionService;
        private readonly ICityService cityService;
        private readonly IRegionService regionService;

        public ClientService(IUnitOfWork unitOfWork, IRepository<ClientT> clientRepository, 
            IRepository<AddressT> addressRepository, IRepository<ClientPhonesT> phoneRepository,
            IClientSubscriptionService clientSubscriptionService, ISubscriptionService subscriptionService,
            ICityService cityService, IRegionService regionService)
        {
            this.unitOfWork = unitOfWork;
            this.clientRepository = clientRepository;
            this.addressRepository = addressRepository;
            this.phoneRepository = phoneRepository;
            this.clientSubscriptionService = clientSubscriptionService;
            this.subscriptionService = subscriptionService;
            this.cityService = cityService;
            this.regionService = regionService;
        }
        public ClientDto GetAllClients()
        {
            //return new ClientDto{ Clients = clientRepository.GetAll() };
            return null;
        }

        public Task<ClientT> GetAsync(int id)
        {
            return clientRepository.GetAsync(id);
        }

        public Task<List<ClientT>> GetByName(string name)
        {
            return clientRepository.Where(c => c.ClientName.Contains(name)).ToListAsync();
        }

        public Task<ClientT> GetByPhone(string phone)
        {
            return clientRepository.Where(c => c.ClientPhonesT.Where(p => p.ClientPhone == phone).Any())
                .Include(d => d.ClientPhonesT)
                .FirstOrDefaultAsync();
        }

        public async Task<int> Add(ClientT client)
        {
            await clientRepository.AddAsync(client);
            return await clientRepository.SaveAsync();
        }

        public Task<int> UpdateAsync(ClientT client)
        {
            clientRepository.Update(client.ClientId, client);
            return clientRepository.SaveAsync();
        }

        public async Task<int> AddAddress(AddressT address)
        {
            await addressRepository.AddAsync(address);
            return await addressRepository.SaveAsync();
        }

        public Task<int> UpdateAddress(AddressT address)
        {
            addressRepository.Update(address.AddressId, address);
            return addressRepository.SaveAsync();
        }

        public async Task<OpreationResultMessage<AddressT>> DeleteAddress(int addressId)
        {
            unitOfWork.StartTransaction();
            var address = await GetAddress(addressId);
            if (address.IsNull())
            {
                return OpreationResultMessageFactory<AddressT>.CreateErrorResponse(null, App.Global.Enums.OpreationResultStatusCode.NotFound, "AddressNotFound");
            }
            var addressList = await GetAddressListAsync(address.ClientId);
            if (addressList.Count == 1)
            {
                return OpreationResultMessageFactory<AddressT>.CreateErrorResponse(null, App.Global.Enums.OpreationResultStatusCode.DeleteFailed, "OneAddressFound");
            }
            address.IsDeleted = true;
            await UpdateAddress(address);
            if (address.IsDefault)
            {
                await SelectDefaultAddressAutoAsync(address.ClientId);
            }
            await unitOfWork.CommitAsync();
            return OpreationResultMessageFactory<AddressT>.CreateSuccessResponse(null, App.Global.Enums.OpreationResultStatusCode.RecordDeletedSuccessfully); ;
        }

        public async Task<int> AddPhone(ClientPhonesT phone)
        {
            await phoneRepository.AddAsync(phone);
            return await phoneRepository.SaveAsync();
        }

        public Task<int> UpdatePhone(ClientPhonesT phone)
        {
            phoneRepository.Update(phone.ClientPhoneId, phone);
            return phoneRepository.SaveAsync();
        }

        public Task<List<AddressT>> GetAddressListAsync(int clientId, bool getDeleted = false)
        {
            if (getDeleted)
            {
                return addressRepository.Where(d => d.ClientId == clientId).ToListAsync();
            }
            else
            {
                return addressRepository.Where(d => d.ClientId == clientId && d.IsDeleted == false).ToListAsync();
            }
        }

        public Task<List<ClientPhonesT>> GetPhoneList(int clientId)
        {
            return phoneRepository.Where(d => d.ClientId == clientId).ToListAsync();
        }

        public Task<AddressT> GetAddress(int addressId)
        {
            return addressRepository.GetAsync(addressId);
        }

        public Task<ClientPhonesT> GetPhone(int phoneId)
        {
            return phoneRepository.GetAsync(phoneId);
        }

        public async Task<int> Subscripe(int subscriptionId, int clientId)
        {
            var subscription = await subscriptionService.GetAsync(subscriptionId);
            var clientSubscriptionList = await clientSubscriptionService.GetListAsync(clientId, null, true);
            var sameDepartmentSubscription = clientSubscriptionList.Where(d => d.Subscription.DepartmentId == subscription.DepartmentId).FirstOrDefault();
            if (sameDepartmentSubscription.IsNotNull())
            {
                _ = await clientSubscriptionService.DeletetAsync(sameDepartmentSubscription.ClientSubscriptionId);
            }
            return await clientSubscriptionService.AddAsync(new ClientSubscriptionT
            {
                ClientId = clientId,
                SubscriptionId = subscriptionId,
                CreationTime = DateTime.Now,
                SystemUserId = 500
            });

        }

        public async Task<int> UnSubscripe(int subscriptionId, int clientId)
        {
            var clientSubscriptionList = await clientSubscriptionService.GetListAsync(clientId);
            var subscription = clientSubscriptionList.Where(d => d.SubscriptionId == subscriptionId).FirstOrDefault();
            if (subscription.IsNotNull())
            {
                return await clientSubscriptionService.DeletetAsync(subscription.ClientSubscriptionId);
            }
            return 0;
        }

        public async Task<int> AddPointAsync(int clientId, int points)
        {
            var client = await GetAsync(clientId);
            if (client.IsNotNull())
            {
                client.ClientPoints += points;
                return await UpdateAsync(client);
            }
            return 1;
        }

        public async Task<int> WidthrawPointAsync(int clientId, int points)
        {
            var client = await GetAsync(clientId);
            if (client.IsNotNull())
            {
                client.ClientPoints -= points;
                return await UpdateAsync(client);
            }
            return 1;
        }

        public async Task<int> UpdateBranchByCityAsync(int clientId, int cityId)
        {
            unitOfWork.StartTransaction();
            var city = await cityService.GetAsync(cityId, true);
            var client = await GetAsync(clientId);
            if (city.IsNull() || client.IsNull())
            {
                return -1;
            }
            client.BranchId = city.BranchId;
            var clientAddressList = await GetAddressListAsync(clientId);
            if (clientAddressList.IsEmpty())
            {
                var address = new AddressT
                {
                    ClientId = clientId,
                    AddressGov = city.Governorate.GovernorateName,
                    AddressCity = city.CityName,
                    CityId = cityId,
                    IsDefault = true
                };
                await AddAddress(address);
            }
            await UpdateAsync(client);
            return await unitOfWork.CommitAsync();
        }

        public async Task<int> UpdateBranchAsync(int clientId, int brnachId)
        {
            var client = await GetAsync(clientId);
            if (client.IsNull())
            {
                return -1;
            }
            client.BranchId = brnachId;
            return await UpdateAsync(client);
        }

        public async Task<int> SetDefaultAddressAsync(int addressId, int clientId)
        {
            unitOfWork.StartTransaction();
            var addressList = await GetAddressListAsync(clientId);
            if (addressList.IsEmpty())
            {
                return -1;
            }
            if(!addressList.Any(d => d.AddressId == addressId))
            {
                return -1;
            }
            foreach (var address in addressList)
            {
                address.IsDefault = false;
                if(address.AddressId == addressId)
                {
                    address.IsDefault = true;
                }
                await UpdateAddress(address);
            }
            return await unitOfWork.CommitAsync();
        }

        public async Task<int> SelectDefaultAddressAutoAsync(int clientId, List<AddressT> addressList = null)
        {
            AddressT address = null;
            if (addressList.IsEmpty())
            {
                addressList = await GetAddressListAsync(clientId);
            }
            if (addressList.IsEmpty())
            {
                return ((int)App.Global.Enums.OpreationResultStatusCode.NotFound);
            }
            address = addressList.OrderBy(d => d.AddressId).LastOrDefault();
            address.IsDefault = true;
            return await UpdateAddress(address);
        }

        public async Task<AddressT> GetDefaultAddressAsync(int clientId)
        {
            AddressT defaultAddress = null;
            var addressList = await GetAddressListAsync(clientId);
            if (addressList.IsEmpty())
            {
                return null;
            }
            if(addressList.Count == 1)
            {
                defaultAddress = addressList.FirstOrDefault();
                if (defaultAddress.IsDefault == false)
                {
                    defaultAddress.IsDefault = true;
                    await UpdateAddress(defaultAddress);
                }
                return defaultAddress;
            }
            defaultAddress = addressList.FirstOrDefault(d => d.IsDefault);
            if (defaultAddress.IsNull())
            {
                var firstAddress = addressList.OrderBy(d => d.AddressId).FirstOrDefault();
                firstAddress.IsDefault = true;
                await UpdateAddress(firstAddress);
                return firstAddress;
            }
            else
            {
                return defaultAddress;
            }
        }

        public async Task<int> UpdateNameAsync(int clientId, string name)
        {
            var client = await GetAsync(clientId);
            client.ClientName = name;
            return await UpdateAsync(client);
        }

        public async Task<int> UpdateEmailAsync(int clientId, string email)
        {
            var client = await GetAsync(clientId);
            client.ClientEmail = email;
            return await UpdateAsync(client);
        }

        public async Task<int> UpdateBranchByRegionAsync(int clientId, int regionId)
        {
            unitOfWork.StartTransaction();
            var region = await regionService.GetAsync(regionId);
            if (region.IsNull())
            {
                return -1;
            }
            CityT city = await cityService.GetAsync(region.CityId.Value, true);
            var client = await GetAsync(clientId);
            if (city.IsNull() || client.IsNull())
            {
                return -1;
            }
            var clientAddressList = await GetAddressListAsync(clientId);
            if (clientAddressList.IsEmpty())
            {
                var address = new AddressT
                {
                    ClientId = clientId,
                    AddressGov = city.Governorate.GovernorateName,
                    AddressCity = city.CityName,
                    CityId = region.CityId,
                    RegionId = region.RegionId,
                    AddressRegion = region.RegionName,
                    IsDefault = true
                };
                await AddAddress(address);
            }
            client.BranchId = city.BranchId;
            await UpdateAsync(client);
            return await unitOfWork.CommitAsync();
        }

        public async Task<int> GetDefaultCityIdAsync(int clientId)
        {
            var defaultAddress = await GetDefaultAddressAsync(clientId);
            if (defaultAddress.IsNotNull() && defaultAddress.CityId.HasValue)
            {
                return defaultAddress.CityId.Value;
            }
            return GeneralSetting.DefaultCityId;
        }
    }
}
