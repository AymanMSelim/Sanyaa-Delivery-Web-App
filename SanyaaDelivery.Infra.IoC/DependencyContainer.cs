using App.Global.Fawry;
using App.Global.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Application.Services;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Infra.Data;
using SanyaaDelivery.Infra.Data.Repositories;

namespace SanyaaDelivery.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IRepository<ClientT>, ClientRepository>();
            services.AddScoped<IRepository<RequestT>, OrderRepository>();
            services.AddScoped<IRepository<LoginT>, EmployeeApplicationAccountRepository>();
            services.AddScoped<IRepository<EmployeeT>, EmployeeRepository>();
            services.AddScoped<IRepository<DepartmentEmployeeT>, EmployeeDeptartmentRepository>();
            services.AddScoped<IRepository<SystemUserT>, SystemUserRepository>();
            services.AddScoped<IRepository<Cleaningsubscribers>, SubscribeRepository>();
            services.AddScoped<IRepository<AccountT>, AccountRepository>();
            services.AddScoped<IRepository<AccountRoleT>, AccountRoleRepository>();
            services.AddScoped<IRepository<AccountTypeT>, AccountTypeRepository>();
            services.AddScoped<IRepository<RoleT>, RoleRepository>();
            services.AddScoped<IRepository<WorkingAreaT>, WorkingAreaRepository>();
            services.AddScoped<IRepository<AppLandingScreenItemT>, AppLandingScreenItemRepository>();
            services.AddScoped<IRepository<ServiceT>, ServiceRepository>();
            services.AddScoped<IRepository<DepartmentT>, DepartmentRepository>();
            services.AddScoped<IRepository<DepartmentSub0T>, DepartmentSub0Repository>();
            services.AddScoped<IRepository<DepartmentSub1T>, DepartmentSub1Repository>();
            services.AddScoped<IRepository<CountryT>, CountryRepository>();
            services.AddScoped<IRepository<GovernorateT>, GovernorateRepository>();
            services.AddScoped<IRepository<CityT>, CityRepository>();
            services.AddScoped<IRepository<RegionT>, RegionRepository>();
            services.AddScoped<IRepository<AddressT>, AddressRepository>();
            services.AddScoped<IRepository<ClientPhonesT>, ClientPhoneRepository>();
            services.AddScoped<IRepository<EmployeeWorkplacesT>, EmployeeWorkplaceRepository>();
            services.AddScoped<IRepository<EmployeeSubscriptionT>, EmployeeSubscriptionRepository>();
            services.AddScoped<IRepository<BranchT>, BranchRepository>();
            services.AddScoped<IRepository<TranslatorT>, TranslatorRepository>();
            services.AddScoped<IRepository<EmploymentApplicationsT>, EmployementApplicationRepository>();
            services.AddScoped<IRepository<AppSettingT>, AppSettingRepository>();
            services.AddScoped<IRepository<TokenT>, TokenRepository>();
            services.AddScoped<IRepository<EmployeeTypeT>, EmployeeTypeRepository>();
            services.AddScoped<IRepository<ServiceRatioT>, ServiceRatioRepository>();
            services.AddScoped<IRepository<ServiceRatioDetailsT>, ServiceRatioDetailsRepository>();
            services.AddScoped<IRepository<OpeningSoonDepartmentT>, OpeningSoonDepartmentRepository>();
            services.AddScoped<IRepository<TransactionT>, TransactionRepository>();
            services.AddScoped<IRepository<CartT>, CartRepository>();
            services.AddScoped<IRepository<CartDetailsT>, CartDetailsRepository>();
            services.AddScoped<IRepository<SubscriptionT>, SubscriptionRepository>();
            services.AddScoped<IRepository<SubscriptionSequenceT>, SubscriptionSequenceRepository>();
            services.AddScoped<IRepository<ClientSubscriptionT>, ClientSubscriptionRepository>();
            services.AddScoped<IRepository<FavouriteServiceT>, FavouriteServiceRepository>();
            services.AddScoped<IRepository<PromocodeT>, PromocodeRepository>();
            services.AddScoped<IRepository<DayWorkingTimeT>, DayWorkingTimeRepository>();
            services.AddScoped<IRepository<AttachmentT>, AttatchmentRepository>();
            services.AddScoped<IRepository<FollowUpT>, FollowUpRepository>();
            services.AddScoped<IRepository<FawryChargeT>, FawryChargeRepository>();
            services.AddScoped<IRepository<FawryChargeRequestT>, FawryChargeRequestRepository>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IEmployeeAppAccountService, EmployeeAppAccountService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmpDeptService, EmpDeptService>();
            services.AddScoped<ISystemUserService, SystemUserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICleaningSubscriberService, CleaningSubscriberService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountTypeService, AccountTypeService>();
            services.AddScoped<IAccountRoleService, AccountRoleService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IFawryService, FawryService>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IWorkingAreaService, WorkingAreaService>();
            services.AddScoped<IAppLandingScreenService, AppLandingScreenService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IGovernorateService, GovernorateService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<IRuntimeDataService, RuntimeDataService>();
            services.AddScoped<IAppSettingService, AppSettingService>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDeparmentSub0Service, DepartmentSub0Service>();
            services.AddScoped<IDeparmentSub1Service, DepartmentSub1Service>();
            services.AddScoped<IServiceRatioService, ServiceRatioService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<IClientSubscriptionService, ClientSubscriptionService>();
            services.AddScoped<IFavouriteServiceService, FavouriteServiceService>();
            services.AddScoped<IPromocodeService, PromocodeService>();
            services.AddScoped<IHelperService, HelperService>();
            services.AddScoped<IDayWorkingTimeService, DayWorkingTimeService>();
            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddSingleton<IFawryAPIService, FawryAPIService>();
            services.AddSingleton<IFawryChargeService, FawryChargeService>();
            services.AddScoped<Application.IGeneralSetting, Application.GeneralSetting>();

        }
    }
}
