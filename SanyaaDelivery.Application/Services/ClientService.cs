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
        private readonly ICityService cityService;
        private readonly IRegionService regionService;

        public ClientService(IUnitOfWork unitOfWork, IRepository<ClientT> clientRepository, 
            IRepository<AddressT> addressRepository, IRepository<ClientPhonesT> phoneRepository,
            ICityService cityService, IRegionService regionService)
        {
            this.unitOfWork = unitOfWork;
            this.clientRepository = clientRepository;
            this.addressRepository = addressRepository;
            this.phoneRepository = phoneRepository;
            this.cityService = cityService;
            this.regionService = regionService;
        }
        public ClientDto GetAllClients()
        {
            //return new ClientDto{ Clients = clientRepository.GetAll() };
            return null;
        }

        public Task<ClientT> GetAsync(int id, bool includePhone = false, bool includeAddress = false)
        {
            var query = clientRepository.Where(d => d.ClientId == id);
            if (includeAddress)
            {
                query = query.Include(d => d.AddressT);
            }
            if (includePhone)
            {
                query = query.Include(d => d.ClientPhonesT);
            }
            return query.FirstOrDefaultAsync();
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

        public async Task<Result<AddressT>> DeleteAddress(int addressId)
        {
            unitOfWork.StartTransaction();
            var address = await GetAddress(addressId);
            if (address.IsNull())
            {
                return ResultFactory<AddressT>.CreateErrorResponse(null, App.Global.Enums.ResultStatusCode.NotFound, "AddressNotFound");
            }
            var addressList = await GetAddressListAsync(address.ClientId.Value);
            if (addressList.Count == 1)
            {
                return ResultFactory<AddressT>.CreateErrorResponse(null, App.Global.Enums.ResultStatusCode.DeleteFailed, "OneAddressFound");
            }
            address.IsDeleted = true;
            await UpdateAddress(address);
            if (address.IsDefault)
            {
                await SelectDefaultAddressAutoAsync(address.ClientId.Value);
            }
            await unitOfWork.CommitAsync();
            return ResultFactory<AddressT>.CreateSuccessResponse(null, App.Global.Enums.ResultStatusCode.RecordDeletedSuccessfully); ;
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

        public Task<List<AddressT>> GetAddressListAsync(int clientId, bool? excludeDeleted = true)
        {
            var query = addressRepository.Where(d => d.ClientId == clientId);
            if (excludeDeleted.HasValue)
            {
                if (excludeDeleted.Value)
                {
                    query = query.Where(d => d.IsDeleted == false);
                }
                else
                {
                    query = query.Where(d => d.IsDeleted);
                }
            }
            return query.ToListAsync();
        }

        public Task<List<ClientPhonesT>> GetPhoneListAsync(int clientId, bool? getDeleted = false)
        {
            var query = phoneRepository.Where(d => d.ClientId == clientId);
            if (getDeleted.HasValue)
            {
                if (getDeleted.Value)
                {
                    query = query.Where(d => d.IsDeleted);
                }
                else
                {
                    query = query.Where(d => d.IsDeleted == false);
                }
            }
            return query.ToListAsync();
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
            //var subscription = await subscriptionservice.getasync(subscriptionid);
            //var clientsubscriptionlist = await clientsubscriptionservice.getlistasync(clientid, null, true);
            //var samedepartmentsubscription = clientsubscriptionlist.where(d => d.subscription.departmentid == subscription.departmentid).firstordefault();
            //if (samedepartmentsubscription.isnotnull())
            //{
            //    _ = await clientsubscriptionservice.deletetasync(samedepartmentsubscription.clientsubscriptionid);
            //}
            //return await clientsubscriptionservice.addasync(new clientsubscriptiont
            //{
            //    clientid = clientid,
            //    subscriptionid = subscriptionid,
            //    creationtime = datetime.now,
            //    systemuserid = 500
            //});
            return default;
        }

        public async Task<int> UnSubscripe(int subscriptionId, int clientId)
        {
            //var clientSubscriptionList = await clientSubscriptionService.GetListAsync(clientId);
            //var subscription = clientSubscriptionList.Where(d => d.SubscriptionId == subscriptionId).FirstOrDefault();
            //if (subscription.IsNotNull())
            //{
            //    return await clientSubscriptionService.DeletetAsync(subscription.ClientSubscriptionId);
            //}
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
            client.ClientPoints -= points;
            return await UpdateAsync(client);
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
                return ((int)App.Global.Enums.ResultStatusCode.NotFound);
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

        public async Task<ClientPhonesT> GetDefaultPhoneAsync(int clientId)
        {
            ClientPhonesT defaultPhone = null;
            var phoneList = await GetPhoneListAsync(clientId);
            if (phoneList.IsEmpty())
            {
                return null;
            }
            if (phoneList.Count == 1)
            {
                defaultPhone = phoneList.FirstOrDefault();
                if (defaultPhone.IsDefault == false)
                {
                    defaultPhone.IsDefault = true;
                    await UpdatePhone(defaultPhone);
                }
                return defaultPhone;
            }
            defaultPhone = phoneList.FirstOrDefault(d => d.IsDefault);
            if (defaultPhone.IsNull())
            {
                var firstPhone = phoneList.OrderBy(d => d.ClientPhoneId).FirstOrDefault();
                firstPhone.IsDefault = true;
                await UpdatePhone(firstPhone);
                return firstPhone;
            }
            else
            {
                return defaultPhone;
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

        public Task<List<ClientT>> GetListAsync(string searchValue)
        {
            return clientRepository
                .Where(d => d.ClientName.Contains(searchValue) ||
                d.ClientEmail.Contains(searchValue) ||
                d.ClientPhonesT.Any(p => p.ClientPhone.Contains(searchValue))).ToListAsync();
        }

        public async Task<Result<ClientPhonesT>> DeletePhone(int phoneId)
        {
            unitOfWork.StartTransaction();
            var phone = await GetPhone(phoneId);
            if (phone.IsNull())
            {
                return ResultFactory<ClientPhonesT>.CreateNotFoundResponse();
            }
            var clientPhoneList = await GetPhoneListAsync(phone.ClientId.Value);
            if(clientPhoneList.Count == 1)
            {
                return ResultFactory<ClientPhonesT>.CreateErrorResponseMessageFD("Can't remove this single phone, please add another phone number and try again");
            }
            phone.IsDeleted = true;
            clientPhoneList.RemoveAll(d => d.ClientPhoneId == phoneId);
            if (phone.IsDefault)
            {
                phone.IsDefault = false;
            }
            var defaultPhone = clientPhoneList.FirstOrDefault();
            defaultPhone.IsDefault = true;
            await UpdatePhone(phone);
            await UpdatePhone(defaultPhone);
            var affectedRow = await unitOfWork.CommitAsync();
            return ResultFactory<ClientPhonesT>.CreateAffectedRowsResult(affectedRow);
        }
    }
}
