using App.Global.DTOs;
using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Domain.OtherModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class RuntimeDataService : IRuntimeDataService
    {
        private readonly IRepository<BranchT> branchRepository;
        private readonly IRepository<ClientT> clientRepository;
        private readonly IRepository<EmployeeT> employeeRepository;
        private readonly IRepository<DepartmentT> departmentRepository;
        private readonly IRepository<DepartmentSub0T> departmentSub0Repository;
        private readonly IRepository<DepartmentSub1T> departmentSub1Repository;
        private readonly IRepository<ServiceT> serviceRepository;
        private readonly IRepository<CityT> cityRepository;
        private readonly IRepository<GovernorateT> governorateRepository;
        private readonly IRepository<RegionT> regionRepository;
        private readonly IRepository<EmployeeSubscriptionT> employeeSubscriptionRepository;
        private readonly IRepository<ClientPhonesT> phoneRepository;
        private readonly IRepository<TranslatorT> translatorRepository;
        private readonly IRepository<EmployeeTypeT> employeeTypeRepository;
        private readonly IRepository<CountryT> countryRepository;
        private readonly IRepository<ServiceRatioT> serviceRatioRepository;
        private readonly IRepository<SystemUserT> systemUserRepository;
        private readonly IRepository<SubscriptionT> subscriptionRepository;
        private readonly IRepository<PromocodeT> promocodeRepository;
        private readonly IRepository<SiteT> siteRepository;
        private readonly IRepository<DepartmentEmployeeT> employeeDepartmentRepository;
        private readonly IRepository<EmployeeWorkplacesT> employeeBranchRepository;
        private readonly IRepository<FiredStaffT> firedEmployeeRepository;

        public RuntimeDataService(IRepository<BranchT> branchRepository, IRepository<ClientT> clientRepository,
            IRepository<EmployeeT> employeeRepository, IRepository<DepartmentT> departmentRepository,
            IRepository<DepartmentSub0T> departmentSub0Repository, IRepository<DepartmentSub1T> departmentSub1Repository,
            IRepository<ServiceT> serviceRepository, IRepository<CityT> cityRepository,
            IRepository<GovernorateT> governorateRepository, IRepository<RegionT> regionRepository, 
            IRepository<EmployeeSubscriptionT> employeeSubscriptionRepository, IRepository<ClientPhonesT> phoneRepository,
            IRepository<TranslatorT> translatorRepository, IRepository<EmployeeTypeT> employeeTypeRepository, 
            IRepository<CountryT> countryRepository, IRepository<ServiceRatioT> serviceRatioRepository,
            IRepository<SystemUserT> systemUserRepository, IRepository<SubscriptionT> subscriptionRepository, 
            IRepository<PromocodeT> promocodeRepository, IRepository<SiteT> siteRepository, IRepository<DepartmentEmployeeT> employeeDepartmentRepository,
            IRepository<EmployeeWorkplacesT> employeeBranchRepository, IRepository<FiredStaffT> firedEmployeeRepository)
        {
            this.branchRepository = branchRepository;
            this.clientRepository = clientRepository;
            this.employeeRepository = employeeRepository;
            this.departmentRepository = departmentRepository;
            this.departmentSub0Repository = departmentSub0Repository;
            this.departmentSub1Repository = departmentSub1Repository;
            this.serviceRepository = serviceRepository;
            this.cityRepository = cityRepository;
            this.governorateRepository = governorateRepository;
            this.regionRepository = regionRepository;
            this.employeeSubscriptionRepository = employeeSubscriptionRepository;
            this.phoneRepository = phoneRepository;
            this.translatorRepository = translatorRepository;
            this.employeeTypeRepository = employeeTypeRepository;
            this.countryRepository = countryRepository;
            this.serviceRatioRepository = serviceRatioRepository;
            this.systemUserRepository = systemUserRepository;
            this.subscriptionRepository = subscriptionRepository;
            this.promocodeRepository = promocodeRepository;
            this.siteRepository = siteRepository;
            this.employeeDepartmentRepository = employeeDepartmentRepository;
            this.employeeBranchRepository = employeeBranchRepository;
            this.firedEmployeeRepository = firedEmployeeRepository;
        }
        public async Task<RuntimeData> GetAsync()
        {
            try
            {
                var data = new RuntimeData
                {
                    BranchLightList = await branchRepository.DbSet.Select(d => new Domain.LightModels.BranchLight
                    {
                        BranchId = d.BranchId,
                        BranchName = d.BranchName
                    }).ToListAsync(),
                    CityLightList = await cityRepository.DbSet.Select(d => new Domain.LightModels.CityLight
                    {
                        CityId = d.CityId,
                        CityName = d.CityName,
                        GovernorateId = d.GovernorateId,
                        BranchId = d.BranchId
                    }).ToListAsync(),
                    ClientLightList = await clientRepository.DbSet.Select(d => new Domain.LightModels.ClientLight
                    {
                        ClientId = d.ClientId,
                        ClientName = d.ClientName,
                        BranchId = d.BranchId
                    }).ToListAsync(),
                    DepartmentLightList = await departmentRepository.DbSet.Select(d => new Domain.LightModels.DepartmentLight
                    {
                        DepartmentId = d.DepartmentId,
                        DepartmentName = d.DepartmentName,
                        Percentage = d.DepartmentPercentage
                    }).ToListAsync(),
                    DepartmentSub0LightList = await departmentSub0Repository.DbSet.Select(d => new Domain.LightModels.DepartmentSub0Light
                    {
                        DepartmentName = d.DepartmentSub0,
                        DepartmentSub0Id = d.DepartmentSub0Id,
                        DepartmentId = d.DepartmentId
                    }).ToListAsync(),
                    DepartmentSub1LightList = await departmentSub1Repository.DbSet.Select(d => new Domain.LightModels.DepartmentSub1Light
                    {
                        DepartmentSub1Id = d.DepartmentId,
                        DepartmentName = d.DepartmentSub1,
                        DepartmentSub0Id = d.DepartmentSub0Id
                    }).ToListAsync(),
                    EmployeeLightList = await employeeRepository.DbSet.Where(d => d.IsApproved).Select(d => new Domain.LightModels.EmployeeLight
                    {
                        EmployeeId = d.EmployeeId,
                        EmployeeName = d.EmployeeName,
                        SubscriptionId = d.SubscriptionId,
                        PhoneNumber = d.EmployeePhone,
                        Address = d.EmployeeCity
                    }).ToListAsync(),
                    EmployeeSubscriptionList = await employeeSubscriptionRepository.GetListAsync(),
                    CountryLightList = await countryRepository.DbSet.Select(d => new Domain.LightModels.CountryLight
                    {
                        CountryId = d.CountryId,
                        CountryName = d.CountryName
                    }).ToListAsync(),
                    GovernorateLightList = await governorateRepository.DbSet.Select(d => new Domain.LightModels.GovernorateLight
                    {
                        GovernorateId = d.GovernorateId,
                        GovernorateName = d.GovernorateName,
                        CountryId = d.CountryId
                    }).ToListAsync(),
                    RegionLightList = await regionRepository.DbSet.Select(d => new Domain.LightModels.RegionLight
                    {
                        RegionId = d.RegionId,
                        RegionName = d.RegionName,
                        CityId = d.CityId
                    }).ToListAsync(),
                    ServiceList = await serviceRepository.GetListAsync(),
                    PhoneLightList = await phoneRepository.DbSet.Select(d => new Domain.LightModels.PhoneLight
                    {
                        ClientId = d.ClientId,
                        Phone = d.ClientPhone,
                        ClientPhoneId = d.ClientPhoneId
                    }).ToListAsync(),
                    TranslatorList = await translatorRepository.GetListAsync(),
                    EmployeeTypeList = await employeeTypeRepository.GetListAsync(),
                    ServiceRatioList = await serviceRatioRepository.DbSet.Include(d => d.ServiceRatioDetailsT).ToListAsync(),
                    SystemUserLightList = await systemUserRepository.DbSet.Select(d => new Domain.LightModels.SystemUserLight
                    {
                        SystemUserId = d.SystemUserId,
                        SystemUserName = d.SystemUserUsername,
                    }).ToListAsync(),
                    PromocodeLightList = await promocodeRepository.DbSet.Select(d => new Domain.LightModels.PromocodeLight
                    {
                        Promocode = d.Promocode,
                        PromocodeId = d.PromocodeId
                    }).ToListAsync(),
                    SubsctiptionLighttList = await subscriptionRepository.DbSet.Select(d => new Domain.LightModels.SubscriptionLight
                    {
                        DepartmentId = d.DepartmentId,
                        SubscriptionId = d.SubscriptionId,
                        SubscriptionName = d.SubscriptionName
                    }).ToListAsync(),
                    SiteLighttList = await siteRepository.DbSet.Select(d => new Domain.LightModels.SiteLight
                    {
                        SiteId = d.SiteId,
                        SiteName = d.SiteName
                    }).ToListAsync(),
                    EmployeeDepartmentLightList = await employeeDepartmentRepository.DbSet.Select(d => new Domain.LightModels.EmployeeDepartmentLight
                    {
                        DepartmentId = d.DepartmentId.Value,
                        EmployeeId = d.EmployeeId
                    }).ToListAsync(),
                    EmployeeWorkplaceLightList = await employeeBranchRepository.DbSet.Select(d => new Domain.LightModels.EmployeeWorkplaceLight
                    {
                        EmployeeId = d.EmployeeId,
                        BranchId = d.BranchId
                    }).ToListAsync(),
                    FiredEmployeeIdList = await firedEmployeeRepository.DbSet.Select(d => d.EmployeeId).ToListAsync(),
                    RequestStatusList = GeneralSetting.RequestStatusList
                };
                return data;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<object> Get1()
        {
            //var branchSelect = await branchRepository.DbSet.Select(d => new SelectList
            //{
            //    Id = d.BranchId.ToString(),
            //    Name = d.BranchName
            //}).ToListAsync();

            //var citySelect = await cityRepository.DbSet.Select(d => new SelectList
            //{
            //    Id = d.CityId.ToString(),
            //    Name = d.CityName,
            //    FkId = d.GovernorateId.ToString()
            //}).ToListAsync();

            //var departmenSelect = await departmentRepository.DbSet.Select(d => new SelectList
            //{
            //    Id = d.DepartmentId.ToString(),
            //    Name = d.DepartmentName
            //}).ToListAsync();

            //var departmenSub0Select = await departmentSub0Repository.DbSet.Select(d => new SelectList
            //{
            //    Id = d.DepartmentSub0Id.ToString(),
            //    Name = d.DepartmentSub0,
            //    FkId = d.DepartmentId.ToString()
            //}).ToListAsync();

            //var departmenSub1Select = await departmentSub1Repository.DbSet.Select(d => new SelectList
            //{
            //    Id = d.DepartmentId.ToString(),
            //    Name = d.DepartmentSub1,
            //    FkId = d.DepartmentSub0Id.ToString()
            //}).ToListAsync();

            var clientSelect = await clientRepository.DbSet.Select(d => new SelectList
            {
                Id = d.ClientId.ToString(),
                Name = d.ClientName,
                FkId = d.BranchId.ToString()
            }).ToListAsync();

            //var employeeSelect = await employeeRepository.DbSet.Select(d => new SelectList
            //{
            //    Id = d.EmployeeId.ToString(),
            //    Name = d.EmployeeName
            //}).ToListAsync();


            return clientSelect;
        }
    }
}
