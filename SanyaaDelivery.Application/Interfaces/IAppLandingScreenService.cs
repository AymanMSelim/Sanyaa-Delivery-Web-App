using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IAppLandingScreenService
    {
        Task<List<Domain.Models.AppLandingScreenItemT>> GetBannerListAsync();
        Task<List<Domain.Models.AppLandingScreenItemT>> GetOfferListAsync();
        Task<List<Domain.Models.AppLandingScreenItemT>> GetDepartmentListAsync(int? clientId);
        Task<List<Domain.Models.AppLandingScreenItemT>> GetListAsync(List<int> typeList);
        Task<List<LandingScreenItemDetailsT>> GetDetailsItemListAsync(int itemId);
        Task<AppLandingScreenItemT> GetAsync(int itemId);
    }
}
