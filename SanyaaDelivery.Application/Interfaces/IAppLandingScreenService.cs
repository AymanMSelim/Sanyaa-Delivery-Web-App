using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IAppLandingScreenService
    {
        Task<List<Domain.Models.AppLandingScreenItemT>> GetPictureListAsync();
        Task<List<Domain.Models.AppLandingScreenItemT>> GetOfferListAsync();
        Task<List<Domain.Models.AppLandingScreenItemT>> GetDepartmentListAsync();
        Task<List<Domain.Models.AppLandingScreenItemT>> GetListAsync(int type);
    }
}
