using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SanyaaDelivery.Infra.Data.Models;

public partial class Sane3erySanyaadatabaseContext : DbContext
{
    public Sane3erySanyaadatabaseContext()
    {
    }

    public Sane3erySanyaadatabaseContext(DbContextOptions<Sane3erySanyaadatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountRoleT> AccountRoleT { get; set; }

    public virtual DbSet<AccountT> AccountT { get; set; }

    public virtual DbSet<AccountTypeT> AccountTypeT { get; set; }

    public virtual DbSet<AddressT> AddressT { get; set; }

    public virtual DbSet<AppLandingScreenItemT> AppLandingScreenItemT { get; set; }

    public virtual DbSet<AppNotificationT> AppNotificationT { get; set; }

    public virtual DbSet<AppSettingT> AppSettingT { get; set; }

    public virtual DbSet<AttachmentT> AttachmentT { get; set; }

    public virtual DbSet<BillDetailsT> BillDetailsT { get; set; }

    public virtual DbSet<BillNumberT> BillNumberT { get; set; }

    public virtual DbSet<BranchT> BranchT { get; set; }

    public virtual DbSet<BroadcastRequestT> BroadcastRequestT { get; set; }

    public virtual DbSet<Cart> Cart { get; set; }

    public virtual DbSet<CartDetailsT> CartDetailsT { get; set; }

    public virtual DbSet<CartT> CartT { get; set; }

    public virtual DbSet<CityT> CityT { get; set; }

    public virtual DbSet<Cleaningsubscribers> Cleaningsubscribers { get; set; }

    public virtual DbSet<ClientPhonesT> ClientPhonesT { get; set; }

    public virtual DbSet<ClientPointT> ClientPointT { get; set; }

    public virtual DbSet<ClientSubscriptionT> ClientSubscriptionT { get; set; }

    public virtual DbSet<ClientT> ClientT { get; set; }

    public virtual DbSet<CountryT> CountryT { get; set; }

    public virtual DbSet<DayWorkingTimeT> DayWorkingTimeT { get; set; }

    public virtual DbSet<DepartmentEmployeeT> DepartmentEmployeeT { get; set; }

    public virtual DbSet<DepartmentSub0T> DepartmentSub0T { get; set; }

    public virtual DbSet<DepartmentSub1T> DepartmentSub1T { get; set; }

    public virtual DbSet<DepartmentT> DepartmentT { get; set; }

    public virtual DbSet<DiscountT> DiscountT { get; set; }

    public virtual DbSet<DiscountTypeT> DiscountTypeT { get; set; }

    public virtual DbSet<EmployeeApproval> EmployeeApproval { get; set; }

    public virtual DbSet<EmployeeLocation> EmployeeLocation { get; set; }

    public virtual DbSet<EmployeeReviewT> EmployeeReviewT { get; set; }

    public virtual DbSet<EmployeeSubscriptionT> EmployeeSubscriptionT { get; set; }

    public virtual DbSet<EmployeeT> EmployeeT { get; set; }

    public virtual DbSet<EmployeeTypeT> EmployeeTypeT { get; set; }

    public virtual DbSet<EmployeeWorkplacesT> EmployeeWorkplacesT { get; set; }

    public virtual DbSet<EmploymentApplicationsT> EmploymentApplicationsT { get; set; }

    public virtual DbSet<FavouriteEmployeeT> FavouriteEmployeeT { get; set; }

    public virtual DbSet<FavouriteServiceT> FavouriteServiceT { get; set; }

    public virtual DbSet<FawryChargeRequestT> FawryChargeRequestT { get; set; }

    public virtual DbSet<FawryChargeT> FawryChargeT { get; set; }

    public virtual DbSet<FirebaseCloudT> FirebaseCloudT { get; set; }

    public virtual DbSet<FiredStaffT> FiredStaffT { get; set; }

    public virtual DbSet<FollowUpT> FollowUpT { get; set; }

    public virtual DbSet<GovernorateT> GovernorateT { get; set; }

    public virtual DbSet<Hangfireaggregatedcounter> Hangfireaggregatedcounter { get; set; }

    public virtual DbSet<Hangfirecounter> Hangfirecounter { get; set; }

    public virtual DbSet<Hangfiredistributedlock> Hangfiredistributedlock { get; set; }

    public virtual DbSet<Hangfirehash> Hangfirehash { get; set; }

    public virtual DbSet<Hangfirejob> Hangfirejob { get; set; }

    public virtual DbSet<Hangfirejobparameter> Hangfirejobparameter { get; set; }

    public virtual DbSet<Hangfirejobqueue> Hangfirejobqueue { get; set; }

    public virtual DbSet<Hangfirejobstate> Hangfirejobstate { get; set; }

    public virtual DbSet<Hangfirelist> Hangfirelist { get; set; }

    public virtual DbSet<Hangfireserver> Hangfireserver { get; set; }

    public virtual DbSet<Hangfireset> Hangfireset { get; set; }

    public virtual DbSet<Hangfirestate> Hangfirestate { get; set; }

    public virtual DbSet<IncreaseDiscountT> IncreaseDiscountT { get; set; }

    public virtual DbSet<InsurancePaymentT> InsurancePaymentT { get; set; }

    public virtual DbSet<LandingScreenItemDetailsT> LandingScreenItemDetailsT { get; set; }

    public virtual DbSet<LoginT> LoginT { get; set; }

    public virtual DbSet<MessagesT> MessagesT { get; set; }

    public virtual DbSet<Notifications> Notifications { get; set; }

    public virtual DbSet<OpeningSoonDepartmentT> OpeningSoonDepartmentT { get; set; }

    public virtual DbSet<OpreationT> OpreationT { get; set; }

    public virtual DbSet<PartinerCartT> PartinerCartT { get; set; }

    public virtual DbSet<PartinerPaymentRequestT> PartinerPaymentRequestT { get; set; }

    public virtual DbSet<PartinerPaymentT> PartinerPaymentT { get; set; }

    public virtual DbSet<PaymentT> PaymentT { get; set; }

    public virtual DbSet<Poll> Poll { get; set; }

    public virtual DbSet<ProductReceiptT> ProductReceiptT { get; set; }

    public virtual DbSet<ProductSoldT> ProductSoldT { get; set; }

    public virtual DbSet<ProductT> ProductT { get; set; }

    public virtual DbSet<Promocode> Promocode { get; set; }

    public virtual DbSet<PromocodeCityT> PromocodeCityT { get; set; }

    public virtual DbSet<PromocodeDepartmentT> PromocodeDepartmentT { get; set; }

    public virtual DbSet<PromocodeT> PromocodeT { get; set; }

    public virtual DbSet<QuantityHistoryT> QuantityHistoryT { get; set; }

    public virtual DbSet<RegestrationT> RegestrationT { get; set; }

    public virtual DbSet<RegionT> RegionT { get; set; }

    public virtual DbSet<RejectRequestT> RejectRequestT { get; set; }

    public virtual DbSet<RequestCanceledT> RequestCanceledT { get; set; }

    public virtual DbSet<RequestComplaintT> RequestComplaintT { get; set; }

    public virtual DbSet<RequestDelayedT> RequestDelayedT { get; set; }

    public virtual DbSet<RequestDiscountT> RequestDiscountT { get; set; }

    public virtual DbSet<RequestServicesT> RequestServicesT { get; set; }

    public virtual DbSet<RequestStagesT> RequestStagesT { get; set; }

    public virtual DbSet<RequestStatusGroupT> RequestStatusGroupT { get; set; }

    public virtual DbSet<RequestStatusT> RequestStatusT { get; set; }

    public virtual DbSet<RequestT> RequestT { get; set; }

    public virtual DbSet<RoleT> RoleT { get; set; }

    public virtual DbSet<ServiceRatioDetailsT> ServiceRatioDetailsT { get; set; }

    public virtual DbSet<ServiceRatioT> ServiceRatioT { get; set; }

    public virtual DbSet<ServiceT> ServiceT { get; set; }

    public virtual DbSet<SettingT> SettingT { get; set; }

    public virtual DbSet<SiteContractT> SiteContractT { get; set; }

    public virtual DbSet<SiteT> SiteT { get; set; }

    public virtual DbSet<SubscriptionSequenceT> SubscriptionSequenceT { get; set; }

    public virtual DbSet<SubscriptionServiceT> SubscriptionServiceT { get; set; }

    public virtual DbSet<SubscriptionT> SubscriptionT { get; set; }

    public virtual DbSet<SystemUserT> SystemUserT { get; set; }

    public virtual DbSet<TimetableT> TimetableT { get; set; }

    public virtual DbSet<TokenT> TokenT { get; set; }

    public virtual DbSet<TransactionT> TransactionT { get; set; }

    public virtual DbSet<TranslatorT> TranslatorT { get; set; }

    public virtual DbSet<VacationT> VacationT { get; set; }

    public virtual DbSet<VersionT> VersionT { get; set; }

    public virtual DbSet<WorkingAreaT> WorkingAreaT { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=192.185.6.199;database=sane3ery_sanyaadatabase;uid=sane3_dbuser;password=Sanyaa@dbuser.live");

}
