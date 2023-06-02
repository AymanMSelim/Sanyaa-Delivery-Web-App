using App.Global.DTOs;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static App.Global.Enums;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface ICartService
    {
        Task<int> AddAsync(CartT cart);
        Task<CartT> AddOrReturnExistAsync(int clientId, int departmentId, bool isViaApp);
        Task<int> AddDetailsAsync(CartDetailsT cartDetails);
        Task<List<CartT>> GetListAsync(DateTime? startDate = null, DateTime? endDate = null, bool? isViaApp = false);
        Task<List<CartDetailsT>> GetDetailsListAsync(int cartId);
        Task<List<CartDetailsT>> GetDetailsListByClientIdAsync(int clientId, bool haveRequest = false);
        Task<CartT> GetAsync(int id, bool includeDetails = false, bool includeService = false, bool includeDepartment = false);
        Task<CartT> GetCurrentByClientIdAsync(int clientId, bool? isViaApp = null, bool includeDetails = false, bool includeService = false);
        Task<CartDetailsT> GetDetailsAsync(int id);
        Task<Domain.DTOs.CartDto> GetCartForAppAsync(int cartId, int? cityId = null, int? clientSubscriptionId = null, DateTime? requestTime = null, RequestT request = null);
        Task<int> DeleteAsync(int id);
        Task<int> DeletetDetailsAsync(int id);
        Task<int> DeletetByServiceIdAsync(int cartId, int serviceId);
        Task<int> UpdateAsync(CartT cart);
        Task<int> UpdateDetailsAsync(CartDetailsT cartDetails);
        Task<Result<CartDetailsT>> AddUpdateServiceAsync(int clientId, bool isViaApp, int serviceId, int quantity);
        Task<Result<CartDetailsT>> AddUpdateServiceAsync(int cartId, int serviceId, int quantity);
        Task<int> UpdateNoteAsync(int cartId, string note);
        Task<Result<PromocodeT>> ApplyPromocodeAsync(int cartId, string code);
        Task<int> CancelPromocodeAsync(int cartId);
        Task<Result<object>> ChangeUsePointStatusAsync(int cartId);
    }
}
