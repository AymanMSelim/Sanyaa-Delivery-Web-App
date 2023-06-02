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

        public async Task<ClientT> GetAsync(int id, bool includePhone = false, bool includeAddress = false, bool getDefaultOnly = false, bool trackingEnabled = false)
        {
            var client = await clientRepository.Where(d => d.ClientId == id).FirstOrDefaultAsync();
            //if (getDefaultOnly)
            //{
            //    client.AddressT = await addressRepository.AsTracking(trackingEnabled).Where(d => d.IsDefault && d.ClientId == id).ToListAsync();
            //    client.ClientPhonesT = await phoneRepository.AsTracking(trackingEnabled).Where(d => d.IsDefault && d.ClientId == id).ToListAsync();
            //}
            //else
            //{
            if (includeAddress)
            {
                client.AddressT = await addressRepository.Where(d => d.ClientId == id).ToListAsync();
            }
            if (includePhone)
            {
                client.ClientPhonesT = await phoneRepository.Where(d => d.ClientId == id).ToListAsync();
            }
            //}
            return client;
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
            address.AddressId = 0;
            await addressRepository.AddAsync(address);
            return await addressRepository.SaveAsync();
        }

        public async Task<int> UpdateAddress(AddressT address)
        {
            unitOfWork.StartTransaction();
            addressRepository.Update(address.AddressId, address);
            await SelectDefaultAddressAutoAsync(address.ClientId);
            return await unitOfWork.CommitAsync();
        }

        public async Task<Result<AddressT>> DeleteAddress(int addressId)
        {
            unitOfWork.StartTransaction();
            var address = await GetAddress(addressId);
            if (address.IsNull())
            {
                return ResultFactory<AddressT>.CreateErrorResponse(null, App.Global.Enums.ResultStatusCode.NotFound, "AddressNotFound");
            }
            var addressList = await GetAddressListAsync(address.ClientId);
            if (addressList.Count == 1)
            {
                return ResultFactory<AddressT>.CreateErrorResponse(null, App.Global.Enums.ResultStatusCode.DeleteFailed, "OneAddressFound");
            }
            address.IsDeleted = true;
            address.IsDefault = false;
            await UpdateAddress(address);
            await unitOfWork.SaveAsync();
            if (address.IsDefault)
            {
                await SelectDefaultAddressAutoAsync(address.ClientId);
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

        public async Task<int> AddPointAsync(int clientId, int points)
        {
            var client = await GetAsync(clientId);
            client.ClientPoints += points;
            return await UpdateAsync(client);
        }

        public async Task<int> WidthrawPointAsync(int clientId, int points)
        {
            var client = await GetAsync(clientId);
            client.ClientPoints -= points;
            return await UpdateAsync(client);
        }

        public async Task<int> UpdateBranchByCityAsync(int clientId, int cityId)
        {
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                var city = await cityService.GetAsync(cityId, true);
                var client = await GetAsync(clientId);
                if (city.IsNull() || client.IsNull())
                {
                    return (int)App.Global.Enums.ResultStatusCode.Failed;
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
                if (isRootTransaction)
                {
                    return await unitOfWork.CommitAsync(false);
                }
                return (int)App.Global.Enums.ResultStatusCode.Success;
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                App.Global.Logging.LogHandler.PublishException(ex);
                return (int)App.Global.Enums.ResultStatusCode.Exception;
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }

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
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                var addressList = await GetAddressListAsync(clientId);
                if (addressList.IsEmpty())
                {
                    return ((int)App.Global.Enums.ResultStatusCode.Failed);
                }
                if (!addressList.Any(d => d.AddressId == addressId))
                {
                    return ((int)App.Global.Enums.ResultStatusCode.Failed);
                }
                foreach (var address in addressList)
                {
                    address.IsDefault = false;
                    if (address.AddressId == addressId)
                    {
                        address.IsDefault = true;
                    }
                    addressRepository.Update(address.AddressId, address);
                }
                if (isRootTransaction)
                {
                    return await unitOfWork.CommitAsync(false);
                }
                return (int)App.Global.Enums.ResultStatusCode.Success;
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                App.Global.Logging.LogHandler.PublishException(ex);
                return (int)App.Global.Enums.ResultStatusCode.Exception;
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }
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
            var defaultAddressList = addressList.Where(d => d.IsDefault).ToList();
            if (defaultAddressList.IsEmpty())
            {
                address = addressList.OrderBy(d => d.AddressId).LastOrDefault();
                address.IsDefault = true;
                return await UpdateAddress(address);
            }
            else if (defaultAddressList.Count == 1)
            {
                return ((int)App.Global.Enums.ResultStatusCode.Success);
            }
            else
            {
                unitOfWork.StartTransaction();
                addressList.ForEach(d => d.IsDefault = false);
                address = addressList.OrderBy(d => d.AddressId).FirstOrDefault();
                address.IsDefault = true;
                foreach (var item in addressList)
                {
                    await UpdateAddress(item);
                }
                return await unitOfWork.CommitAsync();
            }
        }

        public async Task<AddressT> GetDefaultAddressAsync(int clientId)
        {
            AddressT defaultAddress = await addressRepository.Where(d => d.ClientId == clientId && d.IsDeleted == false && d.IsDefault)
                .FirstOrDefaultAsync();
            if (defaultAddress.IsNotNull())
            {
                return defaultAddress;
            }
            defaultAddress = await addressRepository.Where(d => d.ClientId == clientId && d.IsDeleted == false)
                .FirstOrDefaultAsync();
            if (defaultAddress.IsNotNull())
            {
                defaultAddress.IsDefault = true;
                await UpdateAddress(defaultAddress);
                return defaultAddress;
            }
            return null;
            //var addressList = await GetAddressListAsync(clientId);
            //if (addressList.IsEmpty())
            //{
            //    return null;
            //}
            //if(addressList.Count == 1)
            //{
            //    defaultAddress = addressList.FirstOrDefault();
            //    if (defaultAddress.IsDefault == false)
            //    {
            //        defaultAddress.IsDefault = true;
            //        await UpdateAddress(defaultAddress);
            //    }
            //    return defaultAddress;
            //}
            //defaultAddress = addressList.FirstOrDefault(d => d.IsDefault);
            //if (defaultAddress.IsNull())
            //{
            //    var firstAddress = addressList.OrderBy(d => d.AddressId).FirstOrDefault();
            //    firstAddress.IsDefault = true;
            //    await UpdateAddress(firstAddress);
            //    return firstAddress;
            //}
            //else
            //{
            //    return defaultAddress;
            //}
        }

        public async Task<int?> GetDefaultAddressCityIdAsync(int clientId)
        {
            var defaultCity = await addressRepository.Where(d => d.ClientId == clientId && d.IsDeleted == false && d.IsDefault)
                .Select(d => d.CityId)
                .FirstOrDefaultAsync();
            if (defaultCity.IsNotNull())
            {
                return defaultCity;
            }
            defaultCity = await addressRepository.Where(d => d.ClientId == clientId && d.IsDeleted == false)
                 .Select(d => d.CityId)
                .FirstOrDefaultAsync();
            if (defaultCity.IsNotNull())
            {
                return defaultCity;
            }
            return null;
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
            bool isRootTransaction = false;
            try
            {
                isRootTransaction = unitOfWork.StartTransaction();
                var region = await regionService.GetAsync(regionId);
                if (region.IsNull())
                {
                    return (int)App.Global.Enums.ResultStatusCode.Failed;
                }
                CityT city = await cityService.GetAsync(region.CityId.Value, true);
                var client = await GetAsync(clientId);
                if (city.IsNull() || client.IsNull())
                {
                    return (int)App.Global.Enums.ResultStatusCode.Failed;
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
                if (isRootTransaction)
                {
                    return await unitOfWork.CommitAsync(false);
                }
                return (int)App.Global.Enums.ResultStatusCode.Success;
            }
            catch (Exception ex)
            {
                unitOfWork.RollBack();
                App.Global.Logging.LogHandler.PublishException(ex);
                return (int)App.Global.Enums.ResultStatusCode.Exception;
            }
            finally
            {
                if (isRootTransaction)
                {
                    unitOfWork.DisposeTransaction(false);
                }
            }
          
        }

        public async Task<int> GetDefaultCityIdAsync(int clientId)
        {
            var defaultCity = await GetDefaultAddressCityIdAsync(clientId);
            if (defaultCity.IsNotNull())
            {
                return defaultCity.Value;
            }
            return GeneralSetting.DefaultCityId;
        }

        public async Task<CityT> GetDefaultCityAsync(int clientId)
        {
            var defaultCity = await addressRepository.Where(d => d.ClientId == clientId && d.IsDeleted == false && d.IsDefault)
                  .Select(d => d.City)
                  .FirstOrDefaultAsync();
            if (defaultCity.IsNotNull())
            {
                return defaultCity;
            }
            defaultCity = await addressRepository.Where(d => d.ClientId == clientId && d.IsDeleted == false)
                 .Select(d => d.City)
                .FirstOrDefaultAsync();
            if (defaultCity.IsNotNull())
            {
                return defaultCity;
            }
            return null;
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

        public Task<int> GetPointAsync(int clientId)
        {
            return clientRepository.Where(d => d.ClientId == clientId)
                .Select(d => d.ClientPoints).FirstOrDefaultAsync();
        }
    }
}
