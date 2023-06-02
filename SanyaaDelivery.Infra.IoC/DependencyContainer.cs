using App.Global.DateTimeHelper;
using App.Global.Fawry;
using App.Global.Interfaces;
using App.Global.SMS;
using App.Global.Translation;
using App.Global.WhatsApp;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Application.Services;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Domain.OtherModels;
using SanyaaDelivery.Infra.Data;
using SanyaaDelivery.Infra.Data.Repositories;
using System.Linq;

namespace SanyaaDelivery.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingClass());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IRepository<ClientT>, ClientRepository>();
            services.AddScoped<IRepository<RequestT>, RequestRepository>();
            services.AddScoped<IRepository<LoginT>, EmployeeApplicationAccountRepository>();
            services.AddScoped<IRepository<EmployeeT>, EmployeeRepository>();
            services.AddScoped<IRepository<DepartmentEmployeeT>, EmployeeDeptartmentRepository>();
            services.AddScoped<IRepository<SystemUserT>, SystemUserRepository>();
            services.AddScoped<IRepository<Cleaningsubscribers>, SubscribeRepository>();
            services.AddScoped<IRepository<AccountT>, AccountRepository>();
            services.AddScoped<IRepository<AccountRoleT>, AccountRoleRepository>();
            services.AddScoped<IRepository<MessagesT>, MessageRepository>();
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
            services.AddScoped<IRepository<LandingScreenItemDetailsT>, LandingScreenItemDetailsRepository>();
            services.AddScoped<IRepository<CityT>, CityRepository>();
            services.AddScoped<IRepository<RegionT>, RegionRepository>();
            services.AddScoped<IRepository<AddressT>, AddressRepository>();
            services.AddScoped<IRepository<ClientPhonesT>, ClientPhoneRepository>();
            services.AddScoped<IRepository<EmployeeWorkplacesT>, EmployeeWorkplaceRepository>();
            services.AddScoped<IRepository<EmployeeSubscriptionT>, EmployeeSubscriptionRepository>();
            services.AddScoped<IRepository<InsurancePaymentT>, EmployeeInsurancePaymentRepository>();
            services.AddScoped<IRepository<BranchT>, BranchRepository>();
            services.AddScoped<IRepository<TranslatorT>, TranslatorRepository>();
            services.AddScoped<IRepository<VacationT>, VacationRepository>();
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
            services.AddScoped<IRepository<TranslatorT>, TranslationRepository>();
            services.AddScoped<IRepository<RequestStatusT>, RequestStatusRepository>();
            services.AddScoped<IRepository<FavouriteEmployeeT>, FavouriteEmployeeRepository>();
            services.AddScoped<IRepository<SiteT>, SiteRepository>();
            services.AddScoped<IRepository<SubscriptionServiceT>, SubscriptionServiceRepository>();
            services.AddScoped<IRepository<RequestCanceledT>, RequestCanceledRepository>();
            services.AddScoped<IRepository<RequestDelayedT>, RequestDelayedRepository>();
            services.AddScoped<IRepository<RequestComplaintT>, RequestComplaintRepository>();
            services.AddScoped<IRepository<RequestServicesT>, RequestServiceRepository>();
            services.AddScoped<IRepository<ClientPointT>, ClientPointRepository>();
            services.AddScoped<IRepository<RequestStatusGroupT>, RequestStatusGroupRepository>();
            services.AddScoped<IRepository<PaymentT>, PaymentRepository>();
            services.AddScoped<IRepository<OpreationT>, OpreationRepository>();
            services.AddScoped<IRepository<BroadcastRequestT>, BroadcastRequestRepository>();
            services.AddScoped<IRepository<RejectRequestT>, RequestRejectedRepository>();
            services.AddScoped<IRepository<AppNotificationT>, AppNotificationRepository>();


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
            services.AddScoped<IRequestStatusService, RequestStatusService>();
            services.AddScoped<IFavouriteEmployeeService, FavouriteEmployeeService>();
            services.AddScoped<WhatsAppService, WhatsAppService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRequestHelperService, RequestHelperService>();
            services.AddScoped<IClientPointService, ClientPointService>();
            services.AddScoped<ISubscriptionRequestService, SubscriptionRequestService>();
            services.AddScoped<IEmployeeRequestService, EmployeeRequestService>();
            services.AddScoped<IRequestUtilityService, RequestUtiliyService>();
            services.AddScoped<ILookupService, LookupService>();
            services.AddScoped<IOperationService, OperationService>();
            services.AddScoped<IVacationService, VacationService>();
            services.AddScoped<IEmployeeInsuranceService, EmployeeInsuranceService>();
            services.AddScoped<ISMSService, SMSMisrService>();
            services.AddScoped<INotificatonService, NotificatonService>();

            services.AddSingleton<Translator, Translator>();
            services.AddSingleton<IFawryAPIService, FawryAPIService>();
            services.AddSingleton<IFawryChargeService, FawryChargeService>();
            services.AddSingleton<ITranslationService, TranslationService>();
            services.AddSingleton<DateTimeHelperService, DateTimeHelperService>();
            services.AddScoped<Application.IGeneralSetting, Application.GeneralSetting>();

        }


    }

    public class MappingClass : Profile
    {
        public MappingClass()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<EmployeeT, AppEmployeeDto>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.EmployeeName))
                .ForMember(dest => dest.Phone, act => act.MapFrom(src => src.EmployeePhone))
                .ForMember(dest => dest.Image, act => act.MapFrom(src => src.EmployeeImageUrl))
                .ForMember(dest => dest.Rate, act => act.MapFrom(src => src.EmployeeReviewT.Sum(d => d.Rate) % 5))
                .ForMember(dest => dest.IsFavourite, act => act.MapFrom(src => src.FavouriteEmployeeT.Any()))
                .ForMember(dest => dest.EmployeeReviews, act => act.MapFrom(src => src.EmployeeReviewT))
                .ForMember(dest => dest.DepartmentId, act => act.MapFrom(src => src.DepartmentEmployeeT.FirstOrDefault().DepartmentId));

            CreateMap<EmployeeReviewT, EmployeeReviewDto>()
                .ForMember(dest => dest.ClientName, act => act.MapFrom(src => src.Client.ClientName));

            CreateMap<RequestStatusT, RequestStatusDto>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.RequestStatusId))
                .ForMember(dest => dest.StatusName, act => act.MapFrom(src => App.Global.Translation.Translator.STranlate(src.RequestStatusName)));


            CreateMap<RequestStatusGroupT, RequestGroupStatusDto>()
              .ForMember(dest => dest.Id, act => act.MapFrom(src => src.RequestStatusGroupId))
              .ForMember(dest => dest.StatusName, act => act.MapFrom(src => App.Global.Translation.Translator.STranlate(src.GroupName)));

            CreateMap<ClientPointT, ClientPointDto>()
             .ForMember(dest => dest.CreationDate, act => act.MapFrom(src => src.CreationDate.Value.ToString("dd-MM-yyyy")))
             .ForMember(dest => dest.PointTypeDescription, act => act.MapFrom(src => ((Domain.Enum.ClientPointType)src.PointType).ToString()));



            CreateMap<RequestT, AppRequestDto>()
                .ForMember(dest => dest.Date, act => act.MapFrom(src => src.RequestTimestamp.Value.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.Time, act => act.MapFrom(src => src.RequestTimestamp.Value.ToString("hh:mm tt")))
                .ForMember(dest => dest.RequestId, act => act.MapFrom(src => src.RequestId))
                .ForMember(dest => dest.RequestStatus, act => act.MapFrom(src => App.Global.Translation.Translator.STranlate(src.RequestStatusNavigation.RequestStatusName)))
                .ForMember(dest => dest.DayOfWeek, act => act.MapFrom(src => App.Global.Translation.Translator.STranlate(src.RequestTimestamp.Value.DayOfWeek.ToString())))
                .ForMember(dest => dest.Department, act => act.MapFrom(src => src.Department.DepartmentName))
                .ForMember(dest => dest.Services, act => act.MapFrom(src => string.Join(", ", src.RequestServicesT.Select(d => d.Service.ServiceName).ToList())))
                .ForMember(dest => dest.RequestCaption, act => act.MapFrom(src => "طلب رقم #" + src.RequestId));

            CreateMap<RequestT, RequestDto>()
                .ForMember(dest => dest.BranchName, act => act.MapFrom(src => src.Branch.BranchName))
                .ForMember(dest => dest.CityId, act => act.MapFrom(src => src.RequestedAddress.CityId))
                .ForMember(dest => dest.ClientAddress, act => act.MapFrom(src => $"{src.RequestedAddress.AddressGov}, {src.RequestedAddress.AddressCity}, {src.RequestedAddress.AddressRegion}"))
                .ForMember(dest => dest.ClientName, act => act.MapFrom(src => src.Client.ClientName))
                .ForMember(dest => dest.ClientPhone, act => act.MapFrom(src => src.RequestedPhone.ClientPhone))
                .ForMember(dest => dest.DeparmentName, act => act.MapFrom(src => src.Department.DepartmentName))
                .ForMember(dest => dest.EmployeeName, act => act.MapFrom(src => src.Employee.EmployeeName))
                .ForMember(dest => dest.RequestStatusName, act => act.MapFrom(src => App.Global.Translation.Translator.STranlate(src.RequestStatusNavigation.RequestStatusName)))
                .ForMember(dest => dest.ServicesNames, act => act.MapFrom(src => string.Join(", ", src.RequestServicesT.Select(d => d.Service.ServiceName).ToList())))
                .ForMember(dest => dest.SubscriptionName, act => act.MapFrom(src => src.Subscription.SubscriptionName))
                .ForMember(dest => dest.SystemUserName, act => act.MapFrom(src => src.SystemUser.SystemUserUsername))
                .ForMember(dest => dest.EmployeeAccountState, act => act.MapFrom(src => src.Employee.LoginT.LoginAccountState));

            CreateMap<RequestServicesT, RequestServiceDto>()
                .ForMember(dest => dest.RequestServiceId, act => act.MapFrom(src => src.RequestServiceId))
                .ForMember(dest => dest.ServiceName, act => act.MapFrom(src => src.Service.ServiceName))
                .ForMember(dest => dest.ServiceDescription, act => act.MapFrom(src => src.Service.ServiceDes))
                .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.RequestServicesQuantity))
                .ForMember(dest => dest.NetPrice, act => act.MapFrom(src => src.ServicePrice - src.ServiceDiscount))
                .ForMember(dest => dest.Discount, act => act.MapFrom(src => src.ServiceDiscount))
                .ForMember(dest => dest.Price, act => act.MapFrom(src => src.ServicePrice));

            CreateMap<RequestT, AppRequestDetailsDto>()
                .ForMember(dest => dest.Date, act => act.MapFrom(src => src.RequestTimestamp.Value.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.Time, act => act.MapFrom(src => src.RequestTimestamp.Value.ToString("hh:mm tt")))
                .ForMember(dest => dest.RequestId, act => act.MapFrom(src => src.RequestId))
                .ForMember(dest => dest.RequestStatus, act => act.MapFrom(src => App.Global.Translation.Translator.STranlate(src.RequestStatusNavigation.RequestStatusName)))
                .ForMember(dest => dest.DayOfWeek, act => act.MapFrom(src => App.Global.Translation.Translator.STranlate(src.RequestTimestamp.Value.DayOfWeek.ToString())))
                .ForMember(dest => dest.RequestServiceList, act => act.MapFrom(src => src.RequestServicesT))
                .ForMember(dest => dest.RequestCaption, act => act.MapFrom(src => "طلب رقم #" + src.RequestId))
                .ForMember(dest => dest.CityName, act => act.MapFrom(src => src.RequestedAddress.AddressCity))
                .ForMember(dest => dest.RegionName, act => act.MapFrom(src => src.RequestedAddress.AddressRegion))
                .ForMember(dest => dest.StreetName, act => act.MapFrom(src => src.RequestedAddress.AddressStreet))
                .ForMember(dest => dest.Latitude, act => act.MapFrom(src => src.RequestedAddress.Latitude))
                .ForMember(dest => dest.Longitude, act => act.MapFrom(src => src.RequestedAddress.Longitude))
                .ForMember(dest => dest.FlatNumber, act => act.MapFrom(src => src.RequestedAddress.AddressFlatNum))
                .ForMember(dest => dest.FloorNumber, act => act.MapFrom(src => src.RequestedAddress.AddressBlockNum))
                .ForMember(dest => dest.Location, act => act.MapFrom(src => src.RequestedAddress.Location))
                .ForMember(dest => dest.Phone, act => act.MapFrom(src => src.RequestedPhone.ClientPhone))
                .ForMember(dest => dest.Employee, act => act.MapFrom(src => src.Employee))
                .ForMember(dest => dest.DepartmentName, act => act.MapFrom(src => src.Department.DepartmentName))
                .ForMember(dest => dest.DepartmentId, act => act.MapFrom(src => src.Department.DepartmentId));

            CreateMap<SubscriptionT, SubscriptionDto>()
                .ForMember(dest => dest.DepartmentName, act => act.MapFrom(src => src.Department.DepartmentName))
                .ForMember(dest => dest.PriceCaption, act => act.MapFrom(d => "تبدأ من"))
                .ForMember(dest => dest.StartFromPriceC, act => act.MapFrom(src => src.StartFromPrice + " LE"));

            CreateMap<SubscriptionServiceT, SubscriptionServiceDto>()
              .ForMember(dest => dest.ServiceName, act => act.MapFrom(src => src.Service.ServiceName))
              .ForMember(dest => dest.ServiceDescription, act => act.MapFrom(src => src.Service.ServiceDes))
              .ForMember(dest => dest.TotalPrice, act => act.MapFrom(src => src.TotalPricePerMonth))
              .ForMember(dest => dest.NetPrice, act => act.MapFrom(src => src.TotalPricePerMonth - src.Discount))
              .ForMember(dest => dest.SubscriptionServiceId, act => act.MapFrom(src => src.SubscriptionServiceId))
              .ForMember(dest => dest.Info, act => act.MapFrom(src => src.Info));


            CreateMap<ClientSubscriptionT, ClientSubscriptionDto>()
              .ForMember(dest => dest.ServiceName, act => act.MapFrom(src => src.SubscriptionService.Service.ServiceName))
              .ForMember(dest => dest.ServiceId, act => act.MapFrom(src => src.SubscriptionService.ServiceId))
              .ForMember(dest => dest.Phone, act => act.MapFrom(src => src.Phone.ClientPhone))
              .ForMember(dest => dest.Address, act => act.MapFrom(src => src.Address.AddressCity + ", " + src.Address.AddressRegion))
              .ForMember(dest => dest.VisitCountDescription, act => act.MapFrom(src => $"{src.Subscription.RequestNumberPerMonth} زيارات شهريا"))
              .ForMember(dest => dest.VisitTime, act => act.MapFrom(src => src.VisitTime.HasValue ? src.VisitTime.Value.ToString("hh:mm tt") : ""))
              .ForMember(dest => dest.SubscriptionName, act => act.MapFrom(src => src.Subscription.SubscriptionName))
              .ForMember(dest => dest.DepartmentId, act => act.MapFrom(src => src.Subscription.DepartmentId));
        }
    }
}
