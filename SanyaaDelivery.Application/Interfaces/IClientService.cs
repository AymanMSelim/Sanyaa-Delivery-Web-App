using App.Global.DTOs;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IClientService
    {
        ClientDto GetAllClients();

        Task<ClientT> GetAsync(int id, bool includePhone = false, bool includeAddress = false, bool getDefaultOnly = false, bool trackingEnabled = false);
        Task<int> GetPointAsync(int clientId);

        Task<List<ClientT>> GetListAsync(string searchValue);

        Task<ClientT> GetByPhone(string phone);

        Task<List<ClientT>> GetByName(string name);

        Task<int> Add(ClientT client);

        Task<int> UpdateAsync(ClientT client);

        Task<int> UpdateNameAsync(int clientId, string name);
        
        Task<int> UpdateEmailAsync(int clientId, string email);

        Task<int> UpdateBranchByCityAsync(int clientId, int cityId);

        Task<int> UpdateBranchByRegionAsync(int clientId, int regionId);

        Task<int> UpdateBranchAsync(int clientId, int brnachId);

        Task<int> AddPointAsync(int clientId, int points);

        Task<int> WidthrawPointAsync(int clientId, int points);

        Task<List<AddressT>> GetAddressListAsync(int clientId, bool? excludeDeleted = true);
        
        Task<AddressT> GetAddress(int addressId);

        Task<int> AddAddress(AddressT address);

        Task<int> UpdateAddress(AddressT address);

        Task<Result<AddressT>> DeleteAddress(int addressId);
        
        Task<int> SetDefaultAddressAsync(int addressId, int clientId);

        Task<AddressT> GetDefaultAddressAsync(int clientId);
        Task<int?> GetDefaultAddressCityIdAsync(int clientId);
        Task<ClientPhonesT> GetDefaultPhoneAsync(int clientId);

        Task<int> GetDefaultCityIdAsync(int clientId);
        Task<CityT> GetDefaultCityAsync(int clientId);

        Task<List<ClientPhonesT>> GetPhoneListAsync(int clientId, bool? getDeleted = false); 
        
        Task<ClientPhonesT> GetPhone(int phoneId);

        Task<int> AddPhone(ClientPhonesT phone);

        Task<int> UpdatePhone(ClientPhonesT phone);

        Task<Result<ClientPhonesT>> DeletePhone(int phoneId);

    }
}
