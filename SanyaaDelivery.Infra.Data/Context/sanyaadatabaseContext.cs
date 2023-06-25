using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SanyaaDelivery.Domain.Models;

namespace SanyaaDelivery.Infra.Data.Context
{
    public partial class SanyaaDatabaseContext : DbContext
    {
        public SanyaaDatabaseContext()
        {
        }

        public SanyaaDatabaseContext(DbContextOptions<SanyaaDatabaseContext> options)
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



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountRoleT>(entity =>
            {
                entity.HasKey(e => e.AccountRoleId).HasName("PRIMARY");

                entity.ToTable("account_role_t");

                entity.HasIndex(e => e.RoleId, "account_role_role_idx");

                entity.HasIndex(e => e.AccountId, "fk_account_role_account_idx");

                entity.Property(e => e.AccountRoleId)
                    .HasColumnType("int(11)")
                    .HasColumnName("account_role_id");
                entity.Property(e => e.AccountId)
                    .HasColumnType("int(11)")
                    .HasColumnName("account_id");
                entity.Property(e => e.IsAcive)
                    .HasDefaultValueSql("b'1'")
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_acive");
                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .HasColumnName("role_id");

                entity.HasOne(d => d.Account).WithMany(p => p.AccountRoleT)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("fk_account_role_account");

                entity.HasOne(d => d.Role).WithMany(p => p.AccountRoleT)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_account_type_role_role");
            });

            modelBuilder.Entity<AccountT>(entity =>
            {
                entity.HasKey(e => e.AccountId).HasName("PRIMARY");

                entity.ToTable("account_t");

                entity.HasIndex(e => e.AccountTypeId, "fk_account_account_type_t_idx");

                entity.HasIndex(e => e.SystemUserId, "fk_account_system_user_idx");

                entity.Property(e => e.AccountId)
                    .HasColumnType("int(11)")
                    .HasColumnName("account_id");
                entity.Property(e => e.AccountHashSlat)
                    .IsRequired()
                    .HasColumnName("account_hash_slat");
                entity.Property(e => e.AccountPassword)
                    .IsRequired()
                    .HasColumnName("account_password");
                entity.Property(e => e.AccountReferenceId)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("account_reference_id");
                entity.Property(e => e.AccountSecurityCode)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("account_security_code");
                entity.Property(e => e.AccountTypeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("account_type_id");
                entity.Property(e => e.AccountUsername)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("account_username");
                entity.Property(e => e.CreationDate)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date");
                entity.Property(e => e.Description)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("description");
                entity.Property(e => e.EmailOtpCode)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("email_otp_code");
                entity.Property(e => e.FcmToken)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("fcm_token");
                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("b'1'")
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_active");
                entity.Property(e => e.IsDeleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_deleted");
                entity.Property(e => e.IsEmailVerfied)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_email_verfied");
                entity.Property(e => e.IsMobileVerfied)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_mobile_verfied");
                entity.Property(e => e.IsPasswordReseted)
                    .HasDefaultValueSql("b'0'")
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_password_reseted");
                entity.Property(e => e.LastOtpCreationTime)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("last_otp_creation_time");
                entity.Property(e => e.LastResetPasswordRequestTime)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("last_reset_password_request_time");
                entity.Property(e => e.MobileOtpCode)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("mobile_otp_code");
                entity.Property(e => e.OtpCountWithinDay)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("otp_count_within_day");
                entity.Property(e => e.PasswordResetCountWithinDay)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("password_reset_count_within_day");
                entity.Property(e => e.ResetPasswordToken)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("reset_password_token");
                entity.Property(e => e.SystemUserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");

                entity.HasOne(d => d.AccountType).WithMany(p => p.AccountT)
                    .HasForeignKey(d => d.AccountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_account_account_type_t");

                entity.HasOne(d => d.SystemUser).WithMany(p => p.AccountT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_account_system_user");
            });

            modelBuilder.Entity<AccountTypeT>(entity =>
            {
                entity.HasKey(e => e.AccountTypeId).HasName("PRIMARY");

                entity.ToTable("account_type_t");

                entity.Property(e => e.AccountTypeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("account_type_id");
                entity.Property(e => e.AccountTypeDes)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("account_type_des");
                entity.Property(e => e.AccountTypeName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("account_type_name");
                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("b'1'")
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_active");
            });

            modelBuilder.Entity<AddressT>(entity =>
            {
                entity.HasKey(e => e.AddressId).HasName("PRIMARY");

                entity.ToTable("address_t");

                entity.HasIndex(e => e.CityId, "fk_address_t_city_t_idx");

                entity.HasIndex(e => e.ClientId, "fk_address_t_client_t");

                entity.HasIndex(e => e.GovernorateId, "fk_address_t_governorate_t_idx");

                entity.HasIndex(e => e.RegionId, "fk_address_t_region_t_idx");

                entity.Property(e => e.AddressId)
                    .HasColumnType("int(11)")
                    .HasColumnName("address_id");
                entity.Property(e => e.AddressBlockNum)
                    .HasDefaultValueSql("'0'")
                    .HasColumnType("smallint(6)")
                    .HasColumnName("address_block_num");
                entity.Property(e => e.AddressCity)
                    .HasMaxLength(75)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("address_city");
                entity.Property(e => e.AddressDes)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("address_des");
                entity.Property(e => e.AddressFlatNum)
                    .HasDefaultValueSql("'0'")
                    .HasColumnType("smallint(6)")
                    .HasColumnName("address_flat_num");
                entity.Property(e => e.AddressGov)
                    .HasMaxLength(75)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("address_gov");
                entity.Property(e => e.AddressRegion)
                    .HasMaxLength(75)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("address_region");
                entity.Property(e => e.AddressStreet)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("address_street");
                entity.Property(e => e.CityId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("city_id");
                entity.Property(e => e.ClientId)
                    .HasColumnType("int(11)")
                    .HasColumnName("client_id");
                entity.Property(e => e.GovernorateId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("governorate_id");
                entity.Property(e => e.IsDefault)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_default");
                entity.Property(e => e.IsDeleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_deleted");
                entity.Property(e => e.Latitude)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("latitude");
                entity.Property(e => e.Location)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("location");
                entity.Property(e => e.Longitude)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("longitude");
                entity.Property(e => e.RegionId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("region_id");

                entity.HasOne(d => d.City).WithMany(p => p.AddressT)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("fk_address_t_city_t");

                entity.HasOne(d => d.Client).WithMany(p => p.AddressT)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("fk_address_t_client_t");

                entity.HasOne(d => d.Governorate).WithMany(p => p.AddressT)
                    .HasForeignKey(d => d.GovernorateId)
                    .HasConstraintName("fk_address_t_governorate_t");

                entity.HasOne(d => d.Region).WithMany(p => p.AddressT)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("fk_address_t_region_t");
            });

            modelBuilder.Entity<AppLandingScreenItemT>(entity =>
            {
                entity.HasKey(e => e.ItemId).HasName("PRIMARY");

                entity.ToTable("app_landing_screen_item_t");

                entity.Property(e => e.ItemId)
                    .HasColumnType("int(11)")
                    .HasColumnName("item_id");
                entity.Property(e => e.ActionLink)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("action_link");
                entity.Property(e => e.Caption)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("caption");
                entity.Property(e => e.DepartmentId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("department_id");
                entity.Property(e => e.HavePackage)
                    .HasColumnType("bit(1)")
                    .HasColumnName("have_package");
                entity.Property(e => e.ImagePath)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("image_path");
                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_active");
                entity.Property(e => e.ItemType)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("item_type");
            });

            modelBuilder.Entity<AppNotificationT>(entity =>
            {
                entity.HasKey(e => e.NotificationId).HasName("PRIMARY");

                entity.ToTable("app_notification_t");

                entity.HasIndex(e => e.AccountId, "fk_app_notification_t_account_t_idx");

                entity.Property(e => e.NotificationId)
                    .HasColumnType("int(11)")
                    .HasColumnName("notification_id");
                entity.Property(e => e.AccountId)
                    .HasColumnType("int(11)")
                    .HasColumnName("account_id");
                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("body");
                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_time");
                entity.Property(e => e.Image)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("image");
                entity.Property(e => e.Link)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("link");
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<AppSettingT>(entity =>
            {
                entity.HasKey(e => e.SettingId).HasName("PRIMARY");

                entity.ToTable("app_setting_t");

                entity.Property(e => e.SettingId)
                    .HasColumnType("int(11)")
                    .HasColumnName("setting_id");
                entity.Property(e => e.CreationDate)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date");
                entity.Property(e => e.IsAppSetting)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_app_setting");
                entity.Property(e => e.SettingDatatype)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("setting_datatype");
                entity.Property(e => e.SettingKey)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("setting_key");
                entity.Property(e => e.SettingValue)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("setting_value");
                entity.Property(e => e.SystemUserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");
            });

            modelBuilder.Entity<AttachmentT>(entity =>
            {
                entity.HasKey(e => e.AttachmentId).HasName("PRIMARY");

                entity.ToTable("attachment_t");

                entity.Property(e => e.AttachmentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("attachment_id");
                entity.Property(e => e.AttachmentType)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("attachment_type");
                entity.Property(e => e.CreationTime)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("creation_time");
                entity.Property(e => e.FileName)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("file_name");
                entity.Property(e => e.FilePath)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("file_path");
                entity.Property(e => e.ReferenceId)
                    .HasMaxLength(14)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("reference_id");
            });

            modelBuilder.Entity<BillDetailsT>(entity =>
            {
                entity.HasKey(e => new { e.BillNumber, e.BillType, e.BillCost }).HasName("PRIMARY");

                entity.ToTable("bill_details_t");

                entity.Property(e => e.BillNumber)
                    .HasMaxLength(45)
                    .HasColumnName("bill_number");
                entity.Property(e => e.BillType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("bill_type");
                entity.Property(e => e.BillCost).HasColumnName("bill_cost");
                entity.Property(e => e.BillIo)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("bill_io");
                entity.Property(e => e.BillNote)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("bill_note");

                entity.HasOne(d => d.BillNumberNavigation).WithMany(p => p.BillDetailsT)
                    .HasForeignKey(d => d.BillNumber)
                    .HasConstraintName("fk_bill_details_t_bill_number_t1");
            });

            modelBuilder.Entity<BillNumberT>(entity =>
            {
                entity.HasKey(e => e.BillNumber).HasName("PRIMARY");

                entity.ToTable("bill_number_t");

                entity.HasIndex(e => e.RequestId, "fk_bill_number_t_request_t1_idx");

                entity.HasIndex(e => e.SystemUserId, "fk_bill_number_t_system_user_t1_idx");

                entity.Property(e => e.BillNumber)
                    .HasMaxLength(45)
                    .HasColumnName("bill_number");
                entity.Property(e => e.BillTimestamp)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("bill_timestamp");
                entity.Property(e => e.RequestId)
                    .HasColumnType("int(11)")
                    .HasColumnName("request_id");
                entity.Property(e => e.SystemUserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");

                entity.HasOne(d => d.Request).WithMany(p => p.BillNumberT)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("fk_bill_number_t_request_t1");

                entity.HasOne(d => d.SystemUser).WithMany(p => p.BillNumberT)
                    .HasForeignKey(d => d.SystemUserId)
                    .HasConstraintName("fk_bill_number_t_system_user_t1");
            });

            modelBuilder.Entity<BranchT>(entity =>
            {
                entity.HasKey(e => e.BranchId).HasName("PRIMARY");

                entity.ToTable("branch_t");

                entity.HasIndex(e => e.BranchName, "branch_name_UNIQUE").IsUnique();

                entity.HasIndex(e => e.BranchPhone, "branch_phone_UNIQUE").IsUnique();

                entity.Property(e => e.BranchId)
                    .HasColumnType("int(11)")
                    .HasColumnName("branch_id");
                entity.Property(e => e.BranchBlockNum)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("branch_block_num");
                entity.Property(e => e.BranchCity)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("branch_city");
                entity.Property(e => e.BranchDes)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("branch_des");
                entity.Property(e => e.BranchFlatNum)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("branch_flat_num");
                entity.Property(e => e.BranchGov)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("branch_gov");
                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("branch_name");
                entity.Property(e => e.BranchPhone)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("branch_phone");
                entity.Property(e => e.BranchRegion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("branch_region");
                entity.Property(e => e.BranchStreet)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("branch_street");
            });

            modelBuilder.Entity<BroadcastRequestT>(entity =>
            {
                entity.HasKey(e => e.BroadcastRequestId).HasName("PRIMARY");

                entity.ToTable("broadcast_request_t");

                entity.HasIndex(e => e.EmployeeId, "fk_broadcast_request_t_employee_t_idx");

                entity.HasIndex(e => e.RequestId, "fk_broadcast_request_t_request_t_idx");

                entity.Property(e => e.BroadcastRequestId)
                    .HasColumnType("int(11)")
                    .HasColumnName("broadcast_request_id");
                entity.Property(e => e.ActionTime)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("action_time");
                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_time");
                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(14)
                    .HasColumnName("employee_id");
                entity.Property(e => e.IsListed)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_listed");
                entity.Property(e => e.IsSeen)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_seen");
                entity.Property(e => e.RequestId)
                    .HasColumnType("int(11)")
                    .HasColumnName("request_id");
                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("status");

                entity.HasOne(d => d.Employee).WithMany(p => p.BroadcastRequestT)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_broadcast_request_t_employee_t");

                entity.HasOne(d => d.Request).WithMany(p => p.BroadcastRequestT)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("fk_broadcast_request_t_request_t");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("cart");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                entity.Property(e => e.Barcode)
                    .IsRequired()
                    .HasColumnType("text");
                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasColumnType("text");
                entity.Property(e => e.Qte)
                    .HasColumnType("int(11)")
                    .HasColumnName("QTE");
                entity.Property(e => e.UserId).HasColumnType("int(11)");
            });

            modelBuilder.Entity<CartDetailsT>(entity =>
            {
                entity.HasKey(e => e.CartDetailsId).HasName("PRIMARY");

                entity.ToTable("cart_details_t");

                entity.HasIndex(e => e.CartId, "fk_cart_details_t_cart_t_idx");

                entity.HasIndex(e => e.ServiceId, "fk_cart_details_t_service_t_idx");

                entity.Property(e => e.CartDetailsId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cart_details_id");
                entity.Property(e => e.CartId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cart_id");
                entity.Property(e => e.CreationTime)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("creation_time");
                entity.Property(e => e.ServiceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("service_id");
                entity.Property(e => e.ServiceQuantity)
                    .HasColumnType("int(11)")
                    .HasColumnName("service_quantity");

                entity.HasOne(d => d.Cart).WithMany(p => p.CartDetailsT)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("fk_cart_details_t_cart_t");

                entity.HasOne(d => d.Service).WithMany(p => p.CartDetailsT)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cart_details_t_service_t");
            });

            modelBuilder.Entity<CartT>(entity =>
            {
                entity.HasKey(e => e.CartId).HasName("PRIMARY");

                entity.ToTable("cart_t");

                entity.HasIndex(e => e.ClientId, "fk_cart_t_client_t_idx");

                entity.HasIndex(e => e.DepartmentId, "fk_cart_t_department_t_idx");

                entity.HasIndex(e => e.PromocodeId, "fk_cart_t_promocode_t_idx");

                entity.Property(e => e.CartId)
                    .HasColumnType("int(11)")
                    .HasColumnName("cart_id");
                entity.Property(e => e.ClientId)
                    .HasColumnType("int(11)")
                    .HasColumnName("client_id");
                entity.Property(e => e.CreationTime)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("creation_time");
                entity.Property(e => e.DepartmentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("department_id");
                entity.Property(e => e.HaveRequest)
                    .HasColumnType("bit(1)")
                    .HasColumnName("have_request");
                entity.Property(e => e.IsViaApp)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_via_app");
                entity.Property(e => e.ModificationTime)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("modification_time");
                entity.Property(e => e.Note)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("note");
                entity.Property(e => e.PromocodeId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("promocode_id");
                entity.Property(e => e.UsePoint)
                    .HasColumnType("bit(1)")
                    .HasColumnName("use_point");

                entity.HasOne(d => d.Client).WithMany(p => p.CartT)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("fk_cart_t_client_t");

                entity.HasOne(d => d.Department).WithMany(p => p.CartT)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cart_t_department_t");

                entity.HasOne(d => d.Promocode).WithMany(p => p.CartT)
                    .HasForeignKey(d => d.PromocodeId)
                    .HasConstraintName("fk_cart_t_promocode_t");
            });

            modelBuilder.Entity<CityT>(entity =>
            {
                entity.HasKey(e => e.CityId).HasName("PRIMARY");

                entity.ToTable("city_t");

                entity.HasIndex(e => e.BranchId, "fk_city_t_branch_t_idx");

                entity.HasIndex(e => e.GovernorateId, "fk_city_t_governorate_t_idx");

                entity.Property(e => e.CityId)
                    .HasColumnType("int(11)")
                    .HasColumnName("city_id");
                entity.Property(e => e.BranchId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("branch_Id");
                entity.Property(e => e.CityName)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("city_name");
                entity.Property(e => e.DeliveryPrice)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("smallint(6)")
                    .HasColumnName("delivery_price");
                entity.Property(e => e.GovernorateId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("governorate_id");
                entity.Property(e => e.LoactionLat)
                    .HasMaxLength(25)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("loaction_lat");
                entity.Property(e => e.LocationLang)
                    .HasMaxLength(25)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("location_lang");
                entity.Property(e => e.LocationUrl)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("location_url");
                entity.Property(e => e.MinimumCharge)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("smallint(6)")
                    .HasColumnName("minimum_charge");

                entity.HasOne(d => d.Branch).WithMany(p => p.CityT)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("fk_city_t_branch_t");

                entity.HasOne(d => d.Governorate).WithMany(p => p.CityT)
                    .HasForeignKey(d => d.GovernorateId)
                    .HasConstraintName("fk_city_t_governorate_t");
            });

            modelBuilder.Entity<Cleaningsubscribers>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("cleaningsubscribers");

                entity.HasIndex(e => e.ClientId, "IX_CleaningSubscribers_ClientId");

                entity.HasIndex(e => e.SystemUserId, "IX_CleaningSubscribers_SystemUserId");

                entity.Property(e => e.Id).HasColumnType("int(11)");
                entity.Property(e => e.ClientId).HasColumnType("int(11)");
                entity.Property(e => e.Package).HasColumnType("int(11)");
                entity.Property(e => e.SubscribeDate).HasMaxLength(6);
                entity.Property(e => e.SystemUserId).HasColumnType("int(11)");

                entity.HasOne(d => d.Client).WithMany(p => p.Cleaningsubscribers)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_CleaningSubscribers_client_t_ClientId");

                entity.HasOne(d => d.SystemUser).WithMany(p => p.Cleaningsubscribers)
                    .HasForeignKey(d => d.SystemUserId)
                    .HasConstraintName("FK_CleaningSubscribers_system_user_t_SystemUserId");
            });

            modelBuilder.Entity<ClientPhonesT>(entity =>
            {
                entity.HasKey(e => e.ClientPhoneId).HasName("PRIMARY");

                entity.ToTable("client_phones_t");

                entity.HasIndex(e => e.ClientPhone, "client_phone_UNIQUE").IsUnique();

                entity.HasIndex(e => e.ClientId, "fk_client_phones_t_client_t_idx");

                entity.Property(e => e.ClientPhoneId)
                    .HasColumnType("int(11)")
                    .HasColumnName("client_phone_id");
                entity.Property(e => e.Active)
                    .HasDefaultValueSql("'0'")
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("active");
                entity.Property(e => e.ClientId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("client_id");
                entity.Property(e => e.ClientPhone)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("client_phone");
                entity.Property(e => e.Code)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("''''''")
                    .HasColumnName("code");
                entity.Property(e => e.IsDefault)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_default");
                entity.Property(e => e.IsDeleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_deleted");
                entity.Property(e => e.PwdUsr)
                    .HasMaxLength(40)
                    .HasDefaultValueSql("''''''")
                    .HasColumnName("pwd_usr");

                entity.HasOne(d => d.Client).WithMany(p => p.ClientPhonesT)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_client_phones_t_client_t");
            });

            modelBuilder.Entity<ClientPointT>(entity =>
            {
                entity.HasKey(e => e.ClientPointId).HasName("PRIMARY");

                entity.ToTable("client_point_t");

                entity.HasIndex(e => e.ClientId, "fk_client_t_idx");

                entity.HasIndex(e => e.SystemUserId, "fk_system_user_t_idx");

                entity.Property(e => e.ClientPointId)
                    .HasColumnType("int(11)")
                    .HasColumnName("client_point_id");
                entity.Property(e => e.ClientId)
                    .HasColumnType("int(11)")
                    .HasColumnName("client_id");
                entity.Property(e => e.CreationDate)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("creation_date");
                entity.Property(e => e.PointType)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("point_type");
                entity.Property(e => e.Points)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("points");
                entity.Property(e => e.Reason)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("reason");
                entity.Property(e => e.RequestId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("request_id");
                entity.Property(e => e.SystemUserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");

                entity.HasOne(d => d.Client).WithMany(p => p.ClientPointT)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_client_t");
            });

            modelBuilder.Entity<ClientSubscriptionT>(entity =>
            {
                entity.HasKey(e => e.ClientSubscriptionId).HasName("PRIMARY");

                entity.ToTable("client_subscription_t");

                entity.HasIndex(e => e.ClientId, "client_subscription_t_client_t_idx");

                entity.HasIndex(e => e.SubscriptionId, "client_subscription_t_subscribtion_t_idx");

                entity.HasIndex(e => e.SystemUserId, "client_subscription_t_system_user_t_idx");

                entity.HasIndex(e => e.AddressId, "fk_client_subscription_t_address_t_idx");

                entity.HasIndex(e => e.PhoneId, "fk_client_subscription_t_client_phones_t_idx");

                entity.HasIndex(e => e.EmployeeId, "fk_client_subscription_t_employee_t_idx");

                entity.HasIndex(e => e.SubscriptionServiceId, "fk_client_subscription_t_subscription_service_t_idx");

                entity.Property(e => e.ClientSubscriptionId)
                    .HasColumnType("int(11)")
                    .HasColumnName("client_subscription_id");
                entity.Property(e => e.AddressId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("address_id");
                entity.Property(e => e.AutoRenew)
                    .HasColumnType("bit(1)")
                    .HasColumnName("auto_renew");
                entity.Property(e => e.ClientId)
                    .HasColumnType("int(11)")
                    .HasColumnName("client_id");
                entity.Property(e => e.CreationTime)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("creation_time");
                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(14)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("employee_id");
                entity.Property(e => e.ExpireDate)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("expire_date");
                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_active");
                entity.Property(e => e.IsCanceled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_canceled");
                entity.Property(e => e.PhoneId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("phone_id");
                entity.Property(e => e.SubscriptionId)
                    .HasColumnType("int(11)")
                    .HasColumnName("subscription_id");
                entity.Property(e => e.SubscriptionServiceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("subscription_service_id");
                entity.Property(e => e.SystemUserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");
                entity.Property(e => e.VisitTime)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("visit_time");

                entity.HasOne(d => d.Address).WithMany(p => p.ClientSubscriptionT)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("fk_client_subscription_t_address_t");

                entity.HasOne(d => d.Client).WithMany(p => p.ClientSubscriptionT)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("client_subscription_t_client_t");

                entity.HasOne(d => d.Employee).WithMany(p => p.ClientSubscriptionT)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("fk_client_subscription_t_employee_t");

                entity.HasOne(d => d.Phone).WithMany(p => p.ClientSubscriptionT)
                    .HasForeignKey(d => d.PhoneId)
                    .HasConstraintName("fk_client_subscription_t_client_phones_t");

                entity.HasOne(d => d.Subscription).WithMany(p => p.ClientSubscriptionT)
                    .HasForeignKey(d => d.SubscriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("client_subscription_t_subscribtion_t");

                entity.HasOne(d => d.SubscriptionService).WithMany(p => p.ClientSubscriptionT)
                    .HasForeignKey(d => d.SubscriptionServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_client_subscription_t_subscription_service_t");

                entity.HasOne(d => d.SystemUser).WithMany(p => p.ClientSubscriptionT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("client_subscription_t_system_user_t");
            });

            modelBuilder.Entity<ClientT>(entity =>
            {
                entity.HasKey(e => e.ClientId).HasName("PRIMARY");

                entity.ToTable("client_t");

                entity.HasIndex(e => e.SystemUserId, "fk_client_systemuser_idx");

                entity.HasIndex(e => e.BranchId, "fk_client_t_branch_t1_idx");

                entity.HasIndex(e => e.ClientName, "name_index");

                entity.Property(e => e.ClientId)
                    .HasColumnType("int(11)")
                    .HasColumnName("client_id");
                entity.Property(e => e.BranchId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("branch_id");
                entity.Property(e => e.ClientEmail)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("client_email");
                entity.Property(e => e.ClientKnowUs)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("client_know_us");
                entity.Property(e => e.ClientName)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("client_name");
                entity.Property(e => e.ClientNotes)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("client_notes");
                entity.Property(e => e.ClientPoints)
                    .HasColumnType("int(11)")
                    .HasColumnName("client_points");
                entity.Property(e => e.ClientRegDate)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("client_reg_date");
                entity.Property(e => e.CurrentAddress)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("current_address");
                entity.Property(e => e.CurrentPhone)
                    .HasMaxLength(11)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("current_phone");
                entity.Property(e => e.IsGuest)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_guest");
                entity.Property(e => e.SystemUserId)
                    .HasDefaultValueSql("'500'")
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");

                entity.HasOne(d => d.Branch).WithMany(p => p.ClientT)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("fk_client_t_branch_t1");

                entity.HasOne(d => d.SystemUser).WithMany(p => p.ClientT)
                    .HasForeignKey(d => d.SystemUserId)
                    .HasConstraintName("fk_client_systemuser");
            });

            modelBuilder.Entity<CountryT>(entity =>
            {
                entity.HasKey(e => e.CountryId).HasName("PRIMARY");

                entity.ToTable("country_t");

                entity.Property(e => e.CountryId)
                    .HasColumnType("int(11)")
                    .HasColumnName("country_id");
                entity.Property(e => e.CountryName)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("country_name");
                entity.Property(e => e.LocationLang)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("location_lang");
                entity.Property(e => e.LocationLat)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("location_lat");
                entity.Property(e => e.LocationUrl)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("location_url");
            });

            modelBuilder.Entity<DayWorkingTimeT>(entity =>
            {
                entity.HasKey(e => e.DayWorkingTimeId).HasName("PRIMARY");

                entity.ToTable("day_working_time_t");

                entity.Property(e => e.DayWorkingTimeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("day_working_time_id");
                entity.Property(e => e.DayNameInEnglish)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("day_name_in_english");
                entity.Property(e => e.DayOfTheWeekIndex)
                    .HasColumnType("int(11)")
                    .HasColumnName("day_of_the_week_index");
                entity.Property(e => e.EndTime)
                    .HasColumnType("time")
                    .HasColumnName("end_time");
                entity.Property(e => e.StartTime)
                    .HasColumnType("time")
                    .HasColumnName("start_time");
            });

            modelBuilder.Entity<DepartmentEmployeeT>(entity =>
            {
                entity.HasKey(e => e.DepartmentEmployeeId).HasName("PRIMARY");

                entity.ToTable("department_employee_t");

                entity.HasIndex(e => e.EmployeeId, "fk_department_t_has_employee_t_employee_t1_idx");

                entity.HasIndex(e => e.DepartmentId, "fk_employee_t_department_t_idx");

                entity.Property(e => e.DepartmentEmployeeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("department_employee_id");
                entity.Property(e => e.DepartmentId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("department_id");
                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("department_name");
                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(14)
                    .HasColumnName("employee_id");
                entity.Property(e => e.Percentage)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("percentage");

                entity.HasOne(d => d.Department).WithMany(p => p.DepartmentEmployeeT)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("fk_employee__department_t_department_t");

                entity.HasOne(d => d.Employee).WithMany(p => p.DepartmentEmployeeT)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("fk_department_t_has_employee_t_employee_t1");
            });

            modelBuilder.Entity<DepartmentSub0T>(entity =>
            {
                entity.HasKey(e => e.DepartmentSub0Id).HasName("PRIMARY");

                entity.ToTable("department_sub0_t");

                entity.HasIndex(e => e.DepartmentId, "fk_department_sub0_t_department_t_idx");

                entity.Property(e => e.DepartmentSub0Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("department_sub0_id");
                entity.Property(e => e.DepartmentId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("department_id");
                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("department_name");
                entity.Property(e => e.DepartmentSub0)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("department_sub0");

                entity.HasOne(d => d.Department).WithMany(p => p.DepartmentSub0T)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("fk_department_sub0_t_department_t");
            });

            modelBuilder.Entity<DepartmentSub1T>(entity =>
            {
                entity.HasKey(e => e.DepartmentId).HasName("PRIMARY");

                entity.ToTable("department_sub1_t");

                entity.HasIndex(e => new { e.DepartmentName, e.DepartmentSub0, e.DepartmentSub1 }, "dept").IsUnique();

                entity.HasIndex(e => e.DepartmentSub0Id, "fk_department_sub1_t_department_sub0_t_idx");

                entity.Property(e => e.DepartmentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("department_id");
                entity.Property(e => e.DepartmentDes)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("department_des");
                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("department_name");
                entity.Property(e => e.DepartmentSub0)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("department_sub0");
                entity.Property(e => e.DepartmentSub0Id)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("department_sub0_id");
                entity.Property(e => e.DepartmentSub1)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("department_sub1");

                entity.HasOne(d => d.DepartmentSub0Navigation).WithMany(p => p.DepartmentSub1T)
                    .HasForeignKey(d => d.DepartmentSub0Id)
                    .HasConstraintName("fk_department_sub1_t_department_sub0_t");
            });

            modelBuilder.Entity<DepartmentT>(entity =>
            {
                entity.HasKey(e => e.DepartmentId).HasName("PRIMARY");

                entity.ToTable("department_t");

                entity.Property(e => e.DepartmentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("department_id");
                entity.Property(e => e.DepartmentDes)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("department_des");
                entity.Property(e => e.DepartmentImage)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("department_image");
                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("department_name");
                entity.Property(e => e.DepartmentPercentage)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("department_percentage");
                entity.Property(e => e.IncludeDeliveryPrice)
                    .HasColumnType("bit(1)")
                    .HasColumnName("include_delivery_price");
                entity.Property(e => e.MaximumDiscountPercentage)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("maximum_discount_percentage");
                entity.Property(e => e.Terms)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("terms");
            });

            modelBuilder.Entity<DiscountT>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("discount_t");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                entity.Property(e => e.Discount2)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("discount2");
                entity.Property(e => e.Discount3)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("discount3");
                entity.Property(e => e.Discount4)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("discount4");
                entity.Property(e => e.DiscountMore)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("discount_more");
            });

            modelBuilder.Entity<DiscountTypeT>(entity =>
            {
                entity.HasKey(e => e.DiscountTypeId).HasName("PRIMARY");

                entity.ToTable("discount_type_t");

                entity.Property(e => e.DiscountTypeId)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("discount_type_id");
                entity.Property(e => e.DiscountTypeDes)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("discount_type_des");
                entity.Property(e => e.DiscountTypeName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("discount_type_name");
                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("b'1'")
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_active");
            });

            modelBuilder.Entity<EmployeeApproval>(entity =>
            {
                entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

                entity.ToTable("employee_approval");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(14)
                    .HasColumnName("employee_id");
                entity.Property(e => e.Approval)
                    .HasMaxLength(11)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("approval");
            });

            modelBuilder.Entity<EmployeeLocation>(entity =>
            {
                entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

                entity.ToTable("employee_location");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(14)
                    .HasColumnName("employee_id");
                entity.Property(e => e.Latitude)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text");
                entity.Property(e => e.Location)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text");
                entity.Property(e => e.Longitude)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text");

                entity.HasOne(d => d.Employee).WithOne(p => p.EmployeeLocation)
                    .HasForeignKey<EmployeeLocation>(d => d.EmployeeId)
                    .HasConstraintName("fk_employee_location");
            });

            modelBuilder.Entity<EmployeeReviewT>(entity =>
            {
                entity.HasKey(e => e.EmployeeReviewId).HasName("PRIMARY");

                entity.ToTable("employee_review_t");

                entity.HasIndex(e => e.ClientId, "fk_employee_review_t_client_t_idx");

                entity.HasIndex(e => e.EmployeeId, "fk_employee_review_t_emploee_t_idx");

                entity.HasIndex(e => e.RequestId, "fk_employee_review_t_request_t_idx");

                entity.Property(e => e.EmployeeReviewId)
                    .HasColumnType("int(11)")
                    .HasColumnName("employee_review_id");
                entity.Property(e => e.ClientId)
                    .HasColumnType("int(11)")
                    .HasColumnName("client_id");
                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_time");
                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(14)
                    .HasColumnName("employee_id");
                entity.Property(e => e.Rate)
                    .HasColumnType("int(11)")
                    .HasColumnName("rate");
                entity.Property(e => e.RequestId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("request_id");
                entity.Property(e => e.Review)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("review");

                entity.HasOne(d => d.Client).WithMany(p => p.EmployeeReviewT)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_employee_review_t_client_t");

                entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeReviewT)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_employee_review_t_emploee_t");

                entity.HasOne(d => d.Request).WithMany(p => p.EmployeeReviewT)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("fk_employee_review_t_request_t");
            });

            modelBuilder.Entity<EmployeeSubscriptionT>(entity =>
            {
                entity.HasKey(e => e.SubscriptionId).HasName("PRIMARY");

                entity.ToTable("employee_subscription_t");

                entity.Property(e => e.SubscriptionId)
                    .HasColumnType("int(11)")
                    .HasColumnName("subscription_id");
                entity.Property(e => e.Description)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("description");
                entity.Property(e => e.InsuranceAmount)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("insurance_amount");
                entity.Property(e => e.MaxRequestCount)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("max_request_count");
                entity.Property(e => e.MaxRequestPrice)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("max_request_price");
               entity.Property(e => e.MaxUnPaidAmount)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("max_unpaid_amount");
            });

            modelBuilder.Entity<EmployeeT>(entity =>
            {
                entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

                entity.ToTable("employee_t");

                entity.HasIndex(e => e.EmployeeId, "employee_national_id_UNIQUE").IsUnique();

                entity.HasIndex(e => e.SubscriptionId, "fk_employee_subscription_t_idx");

                entity.HasIndex(e => e.SystemId, "fk_employee_t_system_user_t_idx");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(14)
                    .HasColumnName("employee_id");
                entity.Property(e => e.EmployeeBlockNum)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("employee_block_num");
                entity.Property(e => e.EmployeeCity)
                    .HasMaxLength(75)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("employee_city");
                entity.Property(e => e.EmployeeDes)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("employee_des");
                entity.Property(e => e.EmployeeFileNum)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("employee_file_num");
                entity.Property(e => e.EmployeeFlatNum)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("employee_flat_num");
                entity.Property(e => e.EmployeeGov)
                    .HasMaxLength(75)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("employee_gov");
                entity.Property(e => e.EmployeeHireDate)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("employee_hire_date");
                entity.Property(e => e.EmployeeImageUrl)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("employee_image_url");
                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(75)
                    .HasColumnName("employee_name");
                entity.Property(e => e.EmployeePercentage)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("employee_percentage");
                entity.Property(e => e.EmployeePhone)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("employee_phone");
                entity.Property(e => e.EmployeePhone1)
                    .HasMaxLength(11)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("employee_phone1");
                entity.Property(e => e.EmployeeRegion)
                    .HasMaxLength(75)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("employee_region");
                entity.Property(e => e.EmployeeRelativeName)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("employee_relative_name");
                entity.Property(e => e.EmployeeRelativePhone)
                    .HasMaxLength(11)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("employee_relative_phone");
                entity.Property(e => e.EmployeeStreet)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("employee_street");
                entity.Property(e => e.EmployeeType)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("employee_type");
                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_active");
                entity.Property(e => e.IsApproved)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_approved");
                entity.Property(e => e.IsDataComplete)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_data_complete");
                entity.Property(e => e.IsFired)
                    .HasDefaultValueSql("b'0'")
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_fired");
                entity.Property(e => e.IsNewEmployee)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_new_employee");
                entity.Property(e => e.SubscriptionId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("subscription_id");
                entity.Property(e => e.SystemId)
                    .HasDefaultValueSql("'1'")
                    .HasColumnType("int(11)")
                    .HasColumnName("system_id");
                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("title");

                entity.HasOne(d => d.Subscription).WithMany(p => p.EmployeeT)
                    .HasForeignKey(d => d.SubscriptionId)
                    .HasConstraintName("fk_employee_subscription_t");

                entity.HasOne(d => d.System).WithMany(p => p.EmployeeT)
                    .HasForeignKey(d => d.SystemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_employee_t_system_user_t");
            });

            modelBuilder.Entity<EmployeeTypeT>(entity =>
            {
                entity.HasKey(e => e.EmployeeTypeId).HasName("PRIMARY");

                entity.ToTable("employee_type_t");

                entity.Property(e => e.EmployeeTypeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("employee_type_id");
                entity.Property(e => e.Description)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("description");
                entity.Property(e => e.EmployeeTypeName)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("employee_type_name");
            });

            modelBuilder.Entity<EmployeeWorkplacesT>(entity =>
            {
                entity.HasKey(e => e.EmployeeWorkplaceId).HasName("PRIMARY");

                entity.ToTable("employee_workplaces_t");

                entity.HasIndex(e => e.BranchId, "fk_branch_t_has_employee_t_branch_t1_idx");

                entity.HasIndex(e => e.EmployeeId, "fk_branch_t_has_employee_t_employee_t1_idx");

                entity.Property(e => e.EmployeeWorkplaceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("employee_workplace_id");
                entity.Property(e => e.BranchId)
                    .HasColumnType("int(11)")
                    .HasColumnName("branch_id");
                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(14)
                    .HasColumnName("employee_id");

                entity.HasOne(d => d.Branch).WithMany(p => p.EmployeeWorkplacesT)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_branch_t_has_employee_t_branch_t1");

                entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeWorkplacesT)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("fk_branch_t_has_employee_t_employee_t1");
            });

            modelBuilder.Entity<EmploymentApplicationsT>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("employment_applications_t");

                entity.HasIndex(e => e.NationalId, "employee_national_id_UNIQUE").IsUnique();

                entity.HasIndex(e => e.EmployeePhone, "employee_phone1_UNIQUE").IsUnique();

                entity.HasIndex(e => e.EmployeeRelativePhone, "employee_relative_num_UNIQUE").IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                entity.Property(e => e.ApplicationStatus)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("'''جديد'''")
                    .HasColumnName("application_status");
                entity.Property(e => e.BranchId)
                    .HasColumnType("int(11)")
                    .HasColumnName("branch_id");
                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("department");
                entity.Property(e => e.EmployeeBlockNum)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("employee_block_num");
                entity.Property(e => e.EmployeeDes)
                    .HasMaxLength(100)
                    .HasDefaultValueSql("'''null'''")
                    .HasColumnName("employee_des");
                entity.Property(e => e.EmployeeFlatNum)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("employee_flat_num");
                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("employee_name");
                entity.Property(e => e.EmployeePhone)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("employee_phone");
                entity.Property(e => e.EmployeeRelativeName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("employee_relative_name");
                entity.Property(e => e.EmployeeRelativePhone)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("employee_relative_phone");
                entity.Property(e => e.LocationLangitude)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("location_langitude");
                entity.Property(e => e.LocationLatitude)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("location_latitude");
                entity.Property(e => e.LocationText)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("location_text");
                entity.Property(e => e.NationalId)
                    .IsRequired()
                    .HasMaxLength(14)
                    .HasColumnName("national_id");
                entity.Property(e => e.Timestamp)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("timestamp");
            });

            modelBuilder.Entity<FavouriteEmployeeT>(entity =>
            {
                entity.HasKey(e => e.FavouriteEmployeeId).HasName("PRIMARY");

                entity.ToTable("favourite_employee_t");

                entity.HasIndex(e => e.ClientId, "favourite_employee_t_client_t_idx");

                entity.HasIndex(e => e.EmployeeId, "fk_favourite_employee_t_employee_t_idx");

                entity.Property(e => e.FavouriteEmployeeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("favourite_employee_id");
                entity.Property(e => e.ClientId)
                    .HasColumnType("int(11)")
                    .HasColumnName("client_id");
                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(14)
                    .HasColumnName("employee_id");

                entity.HasOne(d => d.Client).WithMany(p => p.FavouriteEmployeeT)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("favourite_employee_t_client_t");

                entity.HasOne(d => d.Employee).WithMany(p => p.FavouriteEmployeeT)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_favourite_employee_t_employee_t");
            });

            modelBuilder.Entity<FavouriteServiceT>(entity =>
            {
                entity.HasKey(e => e.FavouriteServiceId).HasName("PRIMARY");

                entity.ToTable("favourite_service_t");

                entity.HasIndex(e => e.ClientId, "favourite_service_t_client_t_idx");

                entity.HasIndex(e => e.ServiceId, "favourite_service_t_service_t_idx");

                entity.Property(e => e.FavouriteServiceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("favourite_service_id");
                entity.Property(e => e.ClientId)
                    .HasColumnType("int(11)")
                    .HasColumnName("client_id");
                entity.Property(e => e.CreationTime)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("creation_time");
                entity.Property(e => e.ServiceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("service_id");

                entity.HasOne(d => d.Client).WithMany(p => p.FavouriteServiceT)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("favourite_service_t_client_t");

                entity.HasOne(d => d.Service).WithMany(p => p.FavouriteServiceT)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("favourite_service_t_service_t");
            });

            modelBuilder.Entity<FawryChargeRequestT>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("fawry_charge_request_t");

                entity.HasIndex(e => e.ChargeId, "fk_fawry_charge_idx");

                entity.HasIndex(e => e.RequestId, "fk_request_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                entity.Property(e => e.ChargeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("charge_id");
                entity.Property(e => e.RequestId)
                    .HasColumnType("int(11)")
                    .HasColumnName("request_id");

                entity.HasOne(d => d.Charge).WithMany(p => p.FawryChargeRequestT)
                    .HasForeignKey(d => d.ChargeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_fawry_charge");

                entity.HasOne(d => d.Request).WithMany(p => p.FawryChargeRequestT)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_request");
            });

            modelBuilder.Entity<FawryChargeT>(entity =>
            {
                entity.HasKey(e => e.SystemId).HasName("PRIMARY");

                entity.ToTable("fawry_charge_t");

                entity.HasIndex(e => e.EmployeeId, "fk_fawry_charge_t_employee_t_idx");

                entity.HasIndex(e => e.FawryRefNumber, "fk_fawry_charge_t_idx");

                entity.Property(e => e.SystemId)
                    .HasColumnType("int(11)")
                    .HasColumnName("system_id");
                entity.Property(e => e.ChargeAmount)
                    .HasPrecision(10)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("charge_amount");
                entity.Property(e => e.ChargeExpireDate)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("charge_expire_date");
                entity.Property(e => e.ChargeStatus)
                    .HasMaxLength(40)
                    .HasDefaultValueSql("'''NEW'''")
                    .HasColumnName("charge_status");
                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(14)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("employee_id");
                entity.Property(e => e.FawryRefNumber)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("bigint(20)")
                    .HasColumnName("fawry_ref_number");
                entity.Property(e => e.IsConfirmed)
                    .HasDefaultValueSql("b'0'")
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_confirmed");
                entity.Property(e => e.RecordTimestamp)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("record_timestamp");

                entity.HasOne(d => d.Employee).WithMany(p => p.FawryChargeT)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("fk_fawry_charge_t_employee_t");
            });

            modelBuilder.Entity<FirebaseCloudT>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("firebase_cloud_t");

                entity.HasIndex(e => e.AccountId, "fk_firebase_cloud_t_account_t_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                entity.Property(e => e.AccountId)
                    .HasColumnType("int(11)")
                    .HasColumnName("account_id");
                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_time");
                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("token");

                entity.HasOne(d => d.Account).WithMany(p => p.FirebaseCloudT)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_firebase_cloud_t_account_t");
            });

            modelBuilder.Entity<FiredStaffT>(entity =>
            {
                entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

                entity.ToTable("fired_staff_t");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(14)
                    .HasColumnName("employee_id");
                entity.Property(e => e.FiredDate)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("fired_date");
                entity.Property(e => e.FiredReasons)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("fired_reasons");

                entity.HasOne(d => d.Employee).WithOne(p => p.FiredStaffT)
                    .HasForeignKey<FiredStaffT>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_fired_staff_t_employee_t1");
            });

            modelBuilder.Entity<FollowUpT>(entity =>
            {
                entity.HasKey(e => new { e.RequestId, e.Timestamp }).HasName("PRIMARY");

                entity.ToTable("follow_up_t");

                entity.HasIndex(e => e.SystemUserId, "system_user_fk_idx");

                entity.Property(e => e.RequestId)
                    .HasColumnType("int(11)")
                    .HasColumnName("request_id");
                entity.Property(e => e.Timestamp)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("timestamp");
                entity.Property(e => e.Behavior)
                    .HasMaxLength(15)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("behavior");
                entity.Property(e => e.Cleaness)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("cleaness");
                entity.Property(e => e.Paid)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("paid");
                entity.Property(e => e.Prices)
                    .HasMaxLength(15)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("prices");
                entity.Property(e => e.Product)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("product");
                entity.Property(e => e.ProductCost)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("product_cost");
                entity.Property(e => e.Rate)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("rate");
                entity.Property(e => e.Reason)
                    .HasMaxLength(15)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("reason");
                entity.Property(e => e.Review)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("review");
                entity.Property(e => e.SystemUserId)
                    .HasDefaultValueSql("'1'")
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");
                entity.Property(e => e.Time)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("time");
                entity.Property(e => e.Tps)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("tps");

                entity.HasOne(d => d.Request).WithMany(p => p.FollowUpT)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("request_fk");

                entity.HasOne(d => d.SystemUser).WithMany(p => p.FollowUpT)
                    .HasForeignKey(d => d.SystemUserId)
                    .HasConstraintName("system_user_fk");
            });

            modelBuilder.Entity<GovernorateT>(entity =>
            {
                entity.HasKey(e => e.GovernorateId).HasName("PRIMARY");

                entity.ToTable("governorate_t");

                entity.HasIndex(e => e.CountryId, "fk_governorate_t_country_t_idx");

                entity.Property(e => e.GovernorateId)
                    .HasColumnType("int(11)")
                    .HasColumnName("governorate_id");
                entity.Property(e => e.CountryId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("country_id");
                entity.Property(e => e.GovernorateName)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("governorate_name");
                entity.Property(e => e.LocationLang)
                    .HasMaxLength(25)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("location_lang");
                entity.Property(e => e.LocationLat)
                    .HasMaxLength(25)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("location_lat");
                entity.Property(e => e.LocationUrl)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("location_url");

                entity.HasOne(d => d.Country).WithMany(p => p.GovernorateT)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("fk_governorate_t_country_t");
            });

            modelBuilder.Entity<IncreaseDiscountT>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.Timestamp, e.IncreaseDiscountReason }).HasName("PRIMARY");

                entity.ToTable("increase_discount_t");

                entity.HasIndex(e => e.SystemUserId, "fk_increase_discount_t_system_user_t1_idx");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(14)
                    .HasColumnName("employee_id");
                entity.Property(e => e.Timestamp)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("timestamp");
                entity.Property(e => e.IncreaseDiscountReason)
                    .HasMaxLength(45)
                    .HasColumnName("increase_discount_reason");
                entity.Property(e => e.IncreaseDiscountType)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("increase_discount_type");
                entity.Property(e => e.IncreaseDiscountValue)
                    .HasColumnType("smallint(6)")
                    .HasColumnName("increase_discount_value");
                entity.Property(e => e.SystemUserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");

                entity.HasOne(d => d.Employee).WithMany(p => p.IncreaseDiscountT)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("fk_increase_discount_t_employee_t1");

                entity.HasOne(d => d.SystemUser).WithMany(p => p.IncreaseDiscountT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_increase_discount_t_system_user_t1");
            });

            modelBuilder.Entity<InsurancePaymentT>(entity =>
            {
                entity.HasKey(e => e.InsurancePaymentId).HasName("PRIMARY");

                entity.ToTable("insurance_payment_t");

                entity.HasIndex(e => e.EmployeeId, "fk_insurance_payment_t_employee_t_idx");

                entity.HasIndex(e => e.SystemUserId, "fk_insurance_payment_t_system_user_t_idx");

                entity.Property(e => e.InsurancePaymentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("insurance_payment_id");
                entity.Property(e => e.Amount)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("amount");
                entity.Property(e => e.CreationTime)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("creation_time");
                entity.Property(e => e.Description)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("description");
                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(14)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("employee_id");
                entity.Property(e => e.ReferenceId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("reference_id");
                entity.Property(e => e.ReferenceType)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("reference_type");
                entity.Property(e => e.SystemUserId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");

                entity.HasOne(d => d.Employee).WithMany(p => p.InsurancePaymentT)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("fk_insurance_payment_t_employee_t");

                entity.HasOne(d => d.SystemUser).WithMany(p => p.InsurancePaymentT)
                    .HasForeignKey(d => d.SystemUserId)
                    .HasConstraintName("fk_insurance_payment_t_system_user_t");
            });

            modelBuilder.Entity<LandingScreenItemDetailsT>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("landing_screen_item_details_t");

                entity.HasIndex(e => e.DapartmentSub0Id, "fk_landing_screen_item_details_t_department_sub0_t_idx");

                entity.HasIndex(e => e.DepartmentId, "fk_landing_screen_item_details_t_department_t_idx");

                entity.HasIndex(e => e.ItemId, "fk_landing_screen_item_details_t_item_t_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                entity.Property(e => e.DapartmentSub0Id)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("dapartment_sub0_id");
                entity.Property(e => e.DepartmentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("department_id");
                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("department_name");
                entity.Property(e => e.Description)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("description");
                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("image_url");
                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_active");
                entity.Property(e => e.ItemId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("item_id");

                entity.HasOne(d => d.DapartmentSub0).WithMany(p => p.LandingScreenItemDetailsT)
                    .HasForeignKey(d => d.DapartmentSub0Id)
                    .HasConstraintName("fk_landing_screen_item_details_t_department_sub0_t");

                entity.HasOne(d => d.Department).WithMany(p => p.LandingScreenItemDetailsT)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_landing_screen_item_details_t_department_t");

                entity.HasOne(d => d.Item).WithMany(p => p.LandingScreenItemDetailsT)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("fk_landing_screen_item_details_t_item_t");
            });

            modelBuilder.Entity<LoginT>(entity =>
            {
                entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

                entity.ToTable("login_t");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(14)
                    .HasColumnName("employee_id");
                entity.Property(e => e.LastActiveTimestamp)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("last_active_timestamp");
                entity.Property(e => e.LoginAccountDeactiveMessage)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("login_account_deactive_message");
                entity.Property(e => e.LoginAccountState)
                    .HasColumnType("bit(1)")
                    .HasColumnName("login_account_state");
                entity.Property(e => e.LoginAvailability)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'''فارغ'''")
                    .HasColumnName("login_availability");
                entity.Property(e => e.LoginMessage)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("login_message");
                entity.Property(e => e.LoginPassword)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("login_password");

                entity.HasOne(d => d.Employee).WithOne(p => p.LoginT)
                    .HasForeignKey<LoginT>(d => d.EmployeeId)
                    .HasConstraintName("fk_login_t_employee_t1");
            });

            modelBuilder.Entity<MessagesT>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("messages_t");

                entity.HasIndex(e => e.EmployeeId, "fk_message_t_employee_t_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                entity.Property(e => e.Body)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("body");
                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(14)
                    .HasColumnName("employee_id");
                entity.Property(e => e.IsRead)
                    .HasDefaultValueSql("'0'")
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("is_read");
                entity.Property(e => e.MessageTimestamp)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("message_timestamp");
                entity.Property(e => e.Title)
                    .HasMaxLength(30)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("title");

                entity.HasOne(d => d.Employee).WithMany(p => p.MessagesT)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("fk_employee_id");
            });

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("notifications");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("body");
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(75)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<OpeningSoonDepartmentT>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("opening_soon_department_t");

                entity.HasIndex(e => e.CityId, "fk_opening_soon_department_t_city_t_idx");

                entity.HasIndex(e => e.DepartmentId, "fk_opening_soon_department_t_department_t_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                entity.Property(e => e.CityId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("city_id");
                entity.Property(e => e.DepartmentId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("department_id");

                entity.HasOne(d => d.City).WithMany(p => p.OpeningSoonDepartmentT)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("fk_opening_soon_department_t_city_t");

                entity.HasOne(d => d.Department).WithMany(p => p.OpeningSoonDepartmentT)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("fk_opening_soon_department_t_department_t");
            });

            modelBuilder.Entity<OpreationT>(entity =>
            {
                entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

                entity.ToTable("opreation_t");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(14)
                    .HasColumnName("employee_Id");
                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_active");
                entity.Property(e => e.LastActiveTime)
                    .HasColumnType("datetime")
                    .HasColumnName("last_active_time");
                entity.Property(e => e.OpenVacation)
                    .HasColumnType("bit(1)")
                    .HasColumnName("open_vacation");
                entity.Property(e => e.PreferredWorkingEndHour)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("preferred_working_end_hour");
                entity.Property(e => e.PreferredWorkingStartHour)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("preferred_working_start_hour");

                entity.HasOne(d => d.Employee).WithOne(p => p.OpreationT)
                    .HasForeignKey<OpreationT>(d => d.EmployeeId)
                    .HasConstraintName("fk_opreation_t_employee_t");
            });

            modelBuilder.Entity<PartinerCartT>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("partiner_cart_t");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                entity.Property(e => e.ServiceCount)
                    .HasColumnType("int(11)")
                    .HasColumnName("service_count");
                entity.Property(e => e.ServiceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("service_id");
                entity.Property(e => e.SystemUsername)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("system_username");
            });

            modelBuilder.Entity<PartinerPaymentRequestT>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("partiner_payment_request_t");

                entity.HasIndex(e => e.PaymentId, "partiner_payment_fk_idx");

                entity.HasIndex(e => e.RequestId, "partiner_request_fk_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                entity.Property(e => e.PaymentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("payment_id");
                entity.Property(e => e.RequestId)
                    .HasColumnType("int(11)")
                    .HasColumnName("request_id");

                entity.HasOne(d => d.Payment).WithMany(p => p.PartinerPaymentRequestT)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("partiner_payment_fk");

                entity.HasOne(d => d.Request).WithMany(p => p.PartinerPaymentRequestT)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("partiner_request_fk");
            });

            modelBuilder.Entity<PartinerPaymentT>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("partiner_payment_t");

                entity.HasIndex(e => e.SystemUserId, "partiner_systemuser_fk_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                entity.Property(e => e.Amount)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("amount");
                entity.Property(e => e.DateFrom)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("date_from");
                entity.Property(e => e.DateTo)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("date_to");
                entity.Property(e => e.RecordTimestamp)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("record_timestamp");
                entity.Property(e => e.SystemUserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");

                entity.HasOne(d => d.SystemUser).WithMany(p => p.PartinerPaymentT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("partiner_systemuser_fk");
            });

            modelBuilder.Entity<PaymentT>(entity =>
            {
                entity.HasKey(e => e.RequestId).HasName("PRIMARY");

                entity.ToTable("payment_t");

                entity.HasIndex(e => e.RequestId, "fk_payment_t_request_t1_idx");

                entity.HasIndex(e => e.SystemUserId, "fk_payment_t_system_user_t1_idx");

                entity.Property(e => e.RequestId)
                    .HasColumnType("int(11)")
                    .HasColumnName("request_id");
                entity.Property(e => e.Payment).HasColumnName("payment");
                entity.Property(e => e.PaymentTimestamp)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("payment_timestamp");
                entity.Property(e => e.SystemUserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");

                entity.HasOne(d => d.Request).WithOne(p => p.PaymentT)
                    .HasForeignKey<PaymentT>(d => d.RequestId)
                    .HasConstraintName("fk_payment_t_request_t1");

                entity.HasOne(d => d.SystemUser).WithMany(p => p.PaymentT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_payment_t_system_user_t1");
            });

            modelBuilder.Entity<Poll>(entity =>
            {
                entity.HasKey(e => e.RequestId).HasName("PRIMARY");

                entity.ToTable("poll");

                entity.Property(e => e.RequestId)
                    .HasColumnType("int(11)")
                    .HasColumnName("request_id");
                entity.Property(e => e.Employee)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("employee");
                entity.Property(e => e.Employee2)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("employee2");
                entity.Property(e => e.Knowme)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("knowme");
                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("note");
                entity.Property(e => e.Place)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("place");
                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("price");
                entity.Property(e => e.Time)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("time");
                entity.Property(e => e.Vote)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("vote");
            });

            modelBuilder.Entity<ProductReceiptT>(entity =>
            {
                entity.HasKey(e => e.ReceiptId).HasName("PRIMARY");

                entity.ToTable("product_receipt_t");

                entity.Property(e => e.ReceiptId)
                    .HasColumnType("int(11)")
                    .HasColumnName("receipt_id");
                entity.Property(e => e.BranchId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("branch_id");
                entity.Property(e => e.ProductReceiptPaid)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("product_receipt_paid");
                entity.Property(e => e.ReceiptEmployeeBuyer)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("receipt_employee_buyer");
                entity.Property(e => e.ReceiptTimestamp)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("receipt_timestamp");
                entity.Property(e => e.SystemUsername)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("system_username");
            });

            modelBuilder.Entity<ProductSoldT>(entity =>
            {
                entity.HasKey(e => new { e.ReceiptId, e.ProductId, e.ProductSoldNote }).HasName("PRIMARY");

                entity.ToTable("product_sold_t");

                entity.HasIndex(e => e.ReceiptId, "fk_product_receipt_t_has_product_t_product_receipt_t1_idx");

                entity.HasIndex(e => e.ProductId, "fk_product_receipt_t_has_product_t_product_t1_idx");

                entity.Property(e => e.ReceiptId)
                    .HasColumnType("int(11)")
                    .HasColumnName("receipt_id");
                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("product_id");
                entity.Property(e => e.ProductSoldNote)
                    .HasMaxLength(5)
                    .HasDefaultValueSql("''''''")
                    .HasColumnName("product_sold_note");
                entity.Property(e => e.ProductSoldPrice)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("product_sold_price");
                entity.Property(e => e.ProductSoldQuantity)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("smallint(6)")
                    .HasColumnName("product_sold_quantity");

                entity.HasOne(d => d.Product).WithMany(p => p.ProductSoldT)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_product_receipt_t_has_product_t_product_t1");

                entity.HasOne(d => d.Receipt).WithMany(p => p.ProductSoldT)
                    .HasForeignKey(d => d.ReceiptId)
                    .HasConstraintName("fk_product_receipt_t_has_product_t_product_receipt_t1");
            });

            modelBuilder.Entity<ProductT>(entity =>
            {
                entity.HasKey(e => e.ProductId).HasName("PRIMARY");

                entity.ToTable("product_t");

                entity.HasIndex(e => e.BranchId, "fk_products_t_branch_t1_idx");

                entity.HasIndex(e => e.ProductName, "product_name_UNIQUE").IsUnique();

                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("product_id");
                entity.Property(e => e.BranchId)
                    .HasColumnType("int(11)")
                    .HasColumnName("branch_id");
                entity.Property(e => e.ProductCustomerPrice).HasColumnName("product_customer_price");
                entity.Property(e => e.ProductDepartment)
                    .HasMaxLength(25)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("product_department");
                entity.Property(e => e.ProductDes)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("product_des");
                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("product_name");
                entity.Property(e => e.ProductPriceBuy).HasColumnName("product_price_buy");
                entity.Property(e => e.ProductPriceSell).HasColumnName("product_price_sell");
                entity.Property(e => e.ProductQuantity)
                    .HasColumnType("smallint(6)")
                    .HasColumnName("product_quantity");

                entity.HasOne(d => d.Branch).WithMany(p => p.ProductT)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_products_t_branch_t1");
            });

            modelBuilder.Entity<PromocodeCityT>(entity =>
            {
                entity.HasKey(e => e.PromocodeCityId).HasName("PRIMARY");

                entity.ToTable("promocode_city_t");

                entity.HasIndex(e => e.CityId, "fk_promocode_ciry_t_city_t_idx");

                entity.HasIndex(e => e.PromocodeId, "fk_promocode_city_t_promocode_t_idx");

                entity.Property(e => e.PromocodeCityId)
                    .HasColumnType("int(11)")
                    .HasColumnName("promocode_city_id");
                entity.Property(e => e.CityId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("city_id");
                entity.Property(e => e.PromocodeId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("promocode_id");

                entity.HasOne(d => d.City).WithMany(p => p.PromocodeCityT)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("fk_promocode_city_t_city_t");

                entity.HasOne(d => d.Promocode).WithMany(p => p.PromocodeCityT)
                    .HasForeignKey(d => d.PromocodeId)
                    .HasConstraintName("fk_promocode_city_t_promocode_t");
            });

            modelBuilder.Entity<PromocodeDepartmentT>(entity =>
            {
                entity.HasKey(e => e.PromocodeDepartmentJd).HasName("PRIMARY");

                entity.ToTable("promocode_department_t");

                entity.HasIndex(e => e.DepartmentId, "fk_promocode_department_t_depart,emt_t_idx");

                entity.HasIndex(e => e.PromocodeId, "fk_promocode_department_t_promocode_t_idx");

                entity.Property(e => e.PromocodeDepartmentJd)
                    .HasColumnType("int(11)")
                    .HasColumnName("promocode_department_jd");
                entity.Property(e => e.DepartmentId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("department_id");
                entity.Property(e => e.PromocodeId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("promocode_id");

                entity.HasOne(d => d.Department).WithMany(p => p.PromocodeDepartmentT)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("fk_promocode_department_t_department_t");

                entity.HasOne(d => d.Promocode).WithMany(p => p.PromocodeDepartmentT)
                    .HasForeignKey(d => d.PromocodeId)
                    .HasConstraintName("fk_promocode_department_t_promocode_t");
            });

            modelBuilder.Entity<PromocodeT>(entity =>
            {
                entity.HasKey(e => e.PromocodeId).HasName("PRIMARY");

                entity.ToTable("promocode_t", tb => tb.HasComment("				"));

                entity.HasIndex(e => e.SystemUserId, "fk_promocode_t_sysyem_user_t_idx");

                entity.Property(e => e.PromocodeId)
                    .HasColumnType("int(11)")
                    .HasColumnName("promocode_id");
                entity.Property(e => e.AutoApply)
                    .HasColumnType("bit(1)")
                    .HasColumnName("auto_apply");
                entity.Property(e => e.CompanyDiscountPercentage)
                    .HasPrecision(10)
                    .HasColumnName("company_discount_percentage");
                entity.Property(e => e.Description)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("description");
                entity.Property(e => e.ExpireTime)
                    .HasColumnType("datetime")
                    .HasColumnName("expire_time");
                entity.Property(e => e.MaxUsageCount)
                    .HasColumnType("int(11)")
                    .HasColumnName("max_usage_count");
                entity.Property(e => e.MinimumCharge)
                    .HasColumnType("int(11)")
                    .HasColumnName("minimum_charge");
                entity.Property(e => e.Promocode)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("promocode");
                entity.Property(e => e.SystemUserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");
                entity.Property(e => e.Type)
                    .HasColumnType("int(11)")
                    .HasColumnName("type");
                entity.Property(e => e.UsageCount)
                    .HasColumnType("int(11)")
                    .HasColumnName("usage_count");
                entity.Property(e => e.Value)
                    .HasColumnType("int(11)")
                    .HasColumnName("value");

                entity.HasOne(d => d.SystemUser).WithMany(p => p.PromocodeT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_promocode_t_sysyem_user_t");
            });

            modelBuilder.Entity<QuantityHistoryT>(entity =>
            {
                entity.HasKey(e => new { e.QuantityTimestamp, e.ProductId }).HasName("PRIMARY");

                entity.ToTable("quantity_history_t");

                entity.HasIndex(e => e.ProductId, "fk_quantity_history_t_product_t1_idx");

                entity.Property(e => e.QuantityTimestamp)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("quantity_timestamp");
                entity.Property(e => e.ProductId)
                    .HasColumnType("int(11)")
                    .HasColumnName("product_id");
                entity.Property(e => e.QuantityHistory)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("smallint(6)")
                    .HasColumnName("quantity_history");
                entity.Property(e => e.SystemUsername)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("system_username");

                entity.HasOne(d => d.Product).WithMany(p => p.QuantityHistoryT)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_quantity_history_t_product_t1");
            });

            modelBuilder.Entity<RegestrationT>(entity =>
            {
                entity.HasKey(e => new { e.RegestrationName, e.RegestrationPhone }).HasName("PRIMARY");

                entity.ToTable("regestration_t");

                entity.HasIndex(e => e.RegestrationPhone, "regestration_phone_UNIQUE").IsUnique();

                entity.Property(e => e.RegestrationName)
                    .HasMaxLength(45)
                    .HasColumnName("regestration_name");
                entity.Property(e => e.RegestrationPhone)
                    .HasMaxLength(11)
                    .HasColumnName("regestration_phone");
                entity.Property(e => e.RegestrationAge)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("regestration_age");
                entity.Property(e => e.RegestrationCity)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("regestration_city");
                entity.Property(e => e.RegestrationDepartment)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("regestration_department");
                entity.Property(e => e.RegestrationExperience)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("regestration_experience");
                entity.Property(e => e.RegestrationGov)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("regestration_gov");
                entity.Property(e => e.RegestrationPassword)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("regestration_password");
                entity.Property(e => e.RegestrationTimestamep)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("regestration_timestamep");
                entity.Property(e => e.RegestrationTransport)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("regestration_transport");
                entity.Property(e => e.RegestrationView)
                    .HasMaxLength(6)
                    .HasDefaultValueSql("'''لا'''")
                    .HasColumnName("regestration_view");
            });

            modelBuilder.Entity<RegionT>(entity =>
            {
                entity.HasKey(e => e.RegionId).HasName("PRIMARY");

                entity.ToTable("region_t");

                entity.HasIndex(e => e.CityId, "fk_region_t_city_t_idx");

                entity.Property(e => e.RegionId)
                    .HasColumnType("int(11)")
                    .HasColumnName("region_id");
                entity.Property(e => e.CityId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("city_id");
                entity.Property(e => e.DeliveryPrice)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("smallint(6)")
                    .HasColumnName("delivery_price");
                entity.Property(e => e.IsDeliveryPriceActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_delivery_price_active");
                entity.Property(e => e.IsMinimumChargeActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_minimum_charge_active");
                entity.Property(e => e.LocationLang)
                    .HasMaxLength(25)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("location_lang");
                entity.Property(e => e.LocationLat)
                    .HasMaxLength(25)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("location_lat");
                entity.Property(e => e.LocationUrl)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("location_url");
                entity.Property(e => e.MinimumCharge)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("smallint(6)")
                    .HasColumnName("minimum_charge");
                entity.Property(e => e.RegionName)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("region_name");

                entity.HasOne(d => d.City).WithMany(p => p.RegionT)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("fk_region_t_city_t");
            });

            modelBuilder.Entity<RejectRequestT>(entity =>
            {
                entity.HasKey(e => e.RejectRequestId).HasName("PRIMARY");

                entity.ToTable("reject_request_t");

                entity.HasIndex(e => e.EmployeeId, "fk_reject_request_t_employee_t1_idx");

                entity.HasIndex(e => e.RequestId, "fk_reject_request_t_request_t1_idx");

                entity.Property(e => e.RejectRequestId)
                    .HasColumnType("int(11)")
                    .HasColumnName("reject_request_id");
                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(14)
                    .HasColumnName("employee_id");
                entity.Property(e => e.RejectRequestTimestamp)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("reject_request_timestamp");
                entity.Property(e => e.RequestId)
                    .HasColumnType("int(11)")
                    .HasColumnName("request_id");

                entity.HasOne(d => d.Employee).WithMany(p => p.RejectRequestT)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("fk_reject_request_t_employee_t1");

                entity.HasOne(d => d.Request).WithMany(p => p.RejectRequestT)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_reject_request_t_request_t1");
            });

            modelBuilder.Entity<RequestCanceledT>(entity =>
            {
                entity.HasKey(e => new { e.RequestId, e.CancelRequestTimestamp }).HasName("PRIMARY");

                entity.ToTable("request_canceled_t");

                entity.HasIndex(e => e.SystemUserId, "fk_cancel_request_t_system_user_t1_idx");

                entity.Property(e => e.RequestId)
                    .HasColumnType("int(11)")
                    .HasColumnName("request_id");
                entity.Property(e => e.CancelRequestTimestamp)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("cancel_request_timestamp");
                entity.Property(e => e.CancelRequestReason)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("cancel_request_reason");
                entity.Property(e => e.SystemUserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");

                entity.HasOne(d => d.Request).WithMany(p => p.RequestCanceledT)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("fk_cancel_requests_t_requests_t1");

                entity.HasOne(d => d.SystemUser).WithMany(p => p.RequestCanceledT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cancel_request_t_system_user_t1");
            });

            modelBuilder.Entity<RequestComplaintT>(entity =>
            {
                entity.HasKey(e => new { e.RequestId, e.ComplaintTimestamp }).HasName("PRIMARY");

                entity.ToTable("request_complaint_t");

                entity.HasIndex(e => new { e.RequestId, e.NewRequestId }, "fk_request_complaint_t_request_t1_idx");

                entity.HasIndex(e => e.SystemUserId, "fk_request_complaint_t_system_users_t1_idx");

                entity.Property(e => e.RequestId)
                    .HasColumnType("int(11)")
                    .HasColumnName("request_id");
                entity.Property(e => e.ComplaintTimestamp)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("complaint_timestamp");
                entity.Property(e => e.ComplaintDes)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("complaint_des");
                entity.Property(e => e.ComplaintIsSolved)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasDefaultValueSql("'''لا'''")
                    .HasColumnName("complaint_is_solved");
                entity.Property(e => e.IsSolved)
                    .HasDefaultValueSql("b'0'")
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_solved");
                entity.Property(e => e.NewRequestId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("new_request_id");
                entity.Property(e => e.SystemUserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");

                entity.HasOne(d => d.Request).WithMany(p => p.RequestComplaintT)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("fk_request_complaint_t_request_t1");

                entity.HasOne(d => d.SystemUser).WithMany(p => p.RequestComplaintT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_request_complaint_t_system_users_t1");
            });

            modelBuilder.Entity<RequestDelayedT>(entity =>
            {
                entity.HasKey(e => e.RequestDelayedId).HasName("PRIMARY");

                entity.ToTable("request_delayed_t");

                entity.HasIndex(e => e.SystemUserId, "fk_delay_request_t_system_user_t1_idx");

                entity.HasIndex(e => e.RequestId, "fk_delay_requests_t_requests_t1_idx");

                entity.Property(e => e.RequestDelayedId)
                    .HasColumnType("int(11)")
                    .HasColumnName("request_delayed_id");
                entity.Property(e => e.DelayRequestNewTimestamp)
                    .HasColumnType("datetime")
                    .HasColumnName("delay_request_new_timestamp");
                entity.Property(e => e.DelayRequestReason)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("delay_request_reason");
                entity.Property(e => e.DelayRequestTimestamp)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("delay_request_timestamp");
                entity.Property(e => e.RequestId)
                    .HasColumnType("int(11)")
                    .HasColumnName("request_id");
                entity.Property(e => e.SystemUserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");

                entity.HasOne(d => d.Request).WithMany(p => p.RequestDelayedT)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("fk_delay_requests_t_requests_t1");

                entity.HasOne(d => d.SystemUser).WithMany(p => p.RequestDelayedT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_delay_request_t_system_user_t1");
            });

            modelBuilder.Entity<RequestDiscountT>(entity =>
            {
                entity.HasKey(e => e.RequestDiscountId).HasName("PRIMARY");

                entity.ToTable("request_discount_t");

                entity.HasIndex(e => e.DiscountTypeId, "fk_discount_type_t_idx");

                entity.HasIndex(e => e.SystemUserId, "fk_request_discount_t_system_user_t_idx");

                entity.HasIndex(e => e.RequestId, "fk_request_t_idx");

                entity.Property(e => e.RequestDiscountId)
                    .HasColumnType("int(11)")
                    .HasColumnName("request_discount_id");
                entity.Property(e => e.CompanyPercentage)
                    .HasPrecision(10)
                    .HasColumnName("company_percentage");
                entity.Property(e => e.CreationTime)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("creation_time");
                entity.Property(e => e.Description)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("description");
                entity.Property(e => e.DiscountTypeId)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("discount_type_id");
                entity.Property(e => e.DiscountValue)
                    .HasPrecision(10)
                    .HasColumnName("discount_value");
                entity.Property(e => e.RequestId)
                    .HasColumnType("int(11)")
                    .HasColumnName("request_id");
                entity.Property(e => e.SystemUserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");

                entity.HasOne(d => d.DiscountType).WithMany(p => p.RequestDiscountT)
                    .HasForeignKey(d => d.DiscountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_request_discount_t_discount_type_t");

                entity.HasOne(d => d.Request).WithMany(p => p.RequestDiscountT)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_request_t");

                entity.HasOne(d => d.SystemUser).WithMany(p => p.RequestDiscountT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_request_discount_t_system_user_t");
            });

            modelBuilder.Entity<RequestServicesT>(entity =>
            {
                entity.HasKey(e => e.RequestServiceId).HasName("PRIMARY");

                entity.ToTable("request_services_t");

                entity.HasIndex(e => e.SystemUserId, "fk_request_services_t_system_user_t_idx");

                entity.HasIndex(e => e.RequestId, "fk_requests_t_has_service_t_requests_t1_idx");

                entity.HasIndex(e => e.ServiceId, "fk_requests_t_has_service_t_service_t1_idx");

                entity.Property(e => e.RequestServiceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("request_service_id");
                entity.Property(e => e.AddTimestamp)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("add_timestamp");
                entity.Property(e => e.RequestId)
                    .HasColumnType("int(11)")
                    .HasColumnName("request_id");
                entity.Property(e => e.RequestServicesQuantity)
                    .HasDefaultValueSql("'1'")
                    .HasColumnType("int(11)")
                    .HasColumnName("request_services_quantity");
                entity.Property(e => e.ServiceDiscount)
                    .HasPrecision(10)
                    .HasColumnName("service_discount");
                entity.Property(e => e.ServiceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("service_id");
                entity.Property(e => e.ServiceMaterialCost)
                    .HasPrecision(10)
                    .HasColumnName("service_material_cost");
                entity.Property(e => e.ServicePoint)
                    .HasColumnType("int(11)")
                    .HasColumnName("service_point");
                entity.Property(e => e.ServicePrice)
                    .HasPrecision(10)
                    .HasColumnName("service_price");
                entity.Property(e => e.SystemUserId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");

                entity.HasOne(d => d.Request).WithMany(p => p.RequestServicesT)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("fk_requests_t_has_service_t_requests_t1");

                entity.HasOne(d => d.Service).WithMany(p => p.RequestServicesT)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_requests_t_service_t");

                entity.HasOne(d => d.SystemUser).WithMany(p => p.RequestServicesT)
                    .HasForeignKey(d => d.SystemUserId)
                    .HasConstraintName("fk_request_services_t_system_user_t");
            });

            modelBuilder.Entity<RequestStagesT>(entity =>
            {
                entity.HasKey(e => e.RequestId).HasName("PRIMARY");

                entity.ToTable("request_stages_t");

                entity.HasIndex(e => e.RequestId, "fk_request_stages_request_t1_idx");

                entity.Property(e => e.RequestId)
                    .HasColumnType("int(11)")
                    .HasColumnName("request_id");
                entity.Property(e => e.AcceptTimestamp)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("accept_timestamp");
                entity.Property(e => e.Cost)
                    .HasPrecision(10)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("cost");
                entity.Property(e => e.FinishTimestamp)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("finish_timestamp");
                entity.Property(e => e.PaymentFlag)
                    .HasDefaultValueSql("b'0'")
                    .HasColumnType("bit(1)")
                    .HasColumnName("payment_flag");
                entity.Property(e => e.ReceiveTimestamp)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("receive_timestamp");
                entity.Property(e => e.SentTimestamp)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("sent_timestamp");

                entity.HasOne(d => d.Request).WithOne(p => p.RequestStagesT)
                    .HasForeignKey<RequestStagesT>(d => d.RequestId)
                    .HasConstraintName("fk_request_stages_request_t1");
            });

            modelBuilder.Entity<RequestStatusGroupT>(entity =>
            {
                entity.HasKey(e => e.RequestStatusGroupId).HasName("PRIMARY");

                entity.ToTable("request_status_group_t");

                entity.Property(e => e.RequestStatusGroupId)
                    .HasColumnType("int(11)")
                    .HasColumnName("request_status_group_id");
                entity.Property(e => e.Description)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("description");
                entity.Property(e => e.GroupName)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("group_name");
            });

            modelBuilder.Entity<RequestStatusT>(entity =>
            {
                entity.HasKey(e => e.RequestStatusId).HasName("PRIMARY");

                entity.ToTable("request_status_t");

                entity.HasIndex(e => e.RequestStatusGroupId, "fk_request_status_t_request_status_group_t_idx");

                entity.Property(e => e.RequestStatusId)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("request_status_id");
                entity.Property(e => e.RequestStatusDes)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("request_status_des");
                entity.Property(e => e.Color)
                 .HasMaxLength(45)
                 .HasDefaultValueSql("'NULL'")
                 .HasColumnName("color");
                entity.Property(e => e.RequestStatusGroupId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("request_status_group_id");
                entity.Property(e => e.RequestStatusName)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("request_status_name");

                entity.HasOne(d => d.RequestStatusGroup).WithMany(p => p.RequestStatusT)
                    .HasForeignKey(d => d.RequestStatusGroupId)
                    .HasConstraintName("fk_request_status_t_request_status_group_t");
            });

            modelBuilder.Entity<RequestT>(entity =>
            {
                entity.HasKey(e => e.RequestId).HasName("PRIMARY");

                entity.ToTable("request_t");

                entity.HasIndex(e => e.BranchId, "fk_request_t_branch_t1_idx");

                entity.HasIndex(e => e.CartId, "fk_request_t_cart_t_idx");

                entity.HasIndex(e => e.ClientSubscriptionId, "fk_request_t_client_subscription_t_idx");

                entity.HasIndex(e => e.EmployeeId, "fk_request_t_employee_t1_idx");

                entity.HasIndex(e => e.PromocodeId, "fk_request_t_promocode_t_idx");

                entity.HasIndex(e => e.SiteId, "fk_request_t_site_t_idx");

                entity.HasIndex(e => e.SubscriptionId, "fk_request_t_subscription_t_idx");

                entity.HasIndex(e => e.SystemUserId, "fk_request_t_system_user_t1_idx");

                entity.HasIndex(e => e.RequestedAddressId, "fk_requests_t_address_t1_idx");

                entity.HasIndex(e => e.RequestedPhoneId, "fk_requests_t_clients_phones_t1_idx");

                entity.HasIndex(e => e.ClientId, "fk_requests_t_clients_t1_idx");

                entity.HasIndex(e => e.DepartmentId, "fk_requests_t_department_t1_idx");

                entity.HasIndex(e => e.RequestStatus, "fk_requests_t_request_status_t1_idx");

                entity.HasIndex(e => e.RequestTimestamp, "timestamp_index");

                entity.Property(e => e.RequestId)
                    .HasColumnType("int(11)")
                    .HasColumnName("request_id");
                entity.Property(e => e.BranchId)
                    .HasColumnType("int(11)")
                    .HasColumnName("branch_id");
                entity.Property(e => e.CartId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("cart_id");
                entity.Property(e => e.ClientId)
                    .HasColumnType("int(11)")
                    .HasColumnName("client_id");
                entity.Property(e => e.ClientSubscriptionId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("client_subscription_id");
                entity.Property(e => e.CompanyPercentageAmount)
                    .HasPrecision(10)
                    .HasColumnName("company_percentage_amount");
                entity.Property(e => e.CustomerPrice)
                    .HasPrecision(10)
                    .HasColumnName("customer_price");
                entity.Property(e => e.DeliveryPrice)
                    .HasPrecision(10)
                    .HasColumnName("delivery_price");
                entity.Property(e => e.DepartmentId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("department_id");
                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(14)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("employee_id");
                entity.Property(e => e.EmployeePercentageAmount)
                    .HasPrecision(10)
                    .HasColumnName("employee_percentage_amount");
                entity.Property(e => e.IsCanceled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_canceled");
                entity.Property(e => e.IsCompleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_completed");
                entity.Property(e => e.IsConfirmed)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_confirmed");
                entity.Property(e => e.IsFollowed)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_followed");
                entity.Property(e => e.IsPaid)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_paid");
                entity.Property(e => e.IsReviewed)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_reviewed");
                entity.Property(e => e.MaterialCost)
                    .HasPrecision(10)
                    .HasColumnName("material_cost");
                entity.Property(e => e.NetPrice)
                    .HasPrecision(10)
                    .HasColumnName("net_price");
                entity.Property(e => e.PromocodeId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("promocode_id");
                entity.Property(e => e.RequestCurrentTimestamp)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("request_current_timestamp");
                entity.Property(e => e.RequestNote)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("request_note");
                entity.Property(e => e.RequestPoints)
                    .HasColumnType("int(11)")
                    .HasColumnName("request_points");
                entity.Property(e => e.RequestStatus)
                    .HasDefaultValueSql("'1'")
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("request_status");
                entity.Property(e => e.RequestTimestamp)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("request_timestamp");
                entity.Property(e => e.RequestedAddressId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("requested_address_id");
                entity.Property(e => e.RequestedPhoneId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("requested_phone_id");
                entity.Property(e => e.SiteId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("site_id");
                entity.Property(e => e.SubscriptionId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("subscription_id");
                entity.Property(e => e.SystemUserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");
                entity.Property(e => e.TotalDiscount)
                    .HasPrecision(10)
                    .HasColumnName("total_discount");
                entity.Property(e => e.TotalPrice)
                    .HasPrecision(10)
                    .HasColumnName("total_price");
                entity.Property(e => e.UsedPoints)
                    .HasColumnType("int(11)")
                    .HasColumnName("used_points");

                entity.HasOne(d => d.Branch).WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_request_t_branch_t1");

                entity.HasOne(d => d.Cart).WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("fk_request_t_cart_t");

                entity.HasOne(d => d.Client).WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("fk_requests_t_clients_t1");

                entity.HasOne(d => d.ClientSubscription).WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.ClientSubscriptionId)
                    .HasConstraintName("fk_request_t_client_subscription_t");

                entity.HasOne(d => d.Department).WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("fk_requests_t_department_t1");

                entity.HasOne(d => d.Employee).WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("fk_request_t_employee_t1");

                entity.HasOne(d => d.Promocode).WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.PromocodeId)
                    .HasConstraintName("fk_request_t_promocode_t");

                entity.HasOne(d => d.RequestStatusNavigation).WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.RequestStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_requests_t_request_status_t1");

                entity.HasOne(d => d.RequestedAddress).WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.RequestedAddressId)
                    .HasConstraintName("fk_requests_t_address_t1");

                entity.HasOne(d => d.RequestedPhone).WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.RequestedPhoneId)
                    .HasConstraintName("fk_requests_t_clients_phones_t1");

                entity.HasOne(d => d.Site).WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.SiteId)
                    .HasConstraintName("fk_request_t_site_t");

                entity.HasOne(d => d.Subscription).WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.SubscriptionId)
                    .HasConstraintName("fk_request_t_subscription_t");

                entity.HasOne(d => d.SystemUser).WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_request_t_system_user_t1");
            });

            modelBuilder.Entity<RoleT>(entity =>
            {
                entity.HasKey(e => e.RoleId).HasName("PRIMARY");

                entity.ToTable("role_t");

                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .HasColumnName("role_id");
                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("b'1'")
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_active");
                entity.Property(e => e.RoleDes)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("role_des");
                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<ServiceRatioDetailsT>(entity =>
            {
                entity.HasKey(e => e.ServiceRatioDetailsId).HasName("PRIMARY");

                entity.ToTable("service_ratio_details_t");

                entity.HasIndex(e => e.CityId, "fk_service_ratio_city_t_idx");

                entity.HasIndex(e => e.DepartmentId, "fk_service_ratio_department_t_idx");

                entity.HasIndex(e => e.ServiceRatioId, "fk_service_ratio_detatils_t_service_ratio_t_idx");

                entity.Property(e => e.ServiceRatioDetailsId)
                    .HasColumnType("int(11)")
                    .HasColumnName("service_ratio_details_id");
                entity.Property(e => e.CityId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("city_id");
                entity.Property(e => e.DepartmentId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("department_id");
                entity.Property(e => e.ServiceRatioId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("service_ratio_id");

                entity.HasOne(d => d.City).WithMany(p => p.ServiceRatioDetailsT)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("fk_service_ratio_city_t");

                entity.HasOne(d => d.Department).WithMany(p => p.ServiceRatioDetailsT)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("fk_service_ratio_department_t");

                entity.HasOne(d => d.ServiceRatio).WithMany(p => p.ServiceRatioDetailsT)
                    .HasForeignKey(d => d.ServiceRatioId)
                    .HasConstraintName("fk_service_ratio_detatils_t_service_ratio_t");
            });

            modelBuilder.Entity<ServiceRatioT>(entity =>
            {
                entity.HasKey(e => e.ServiceRatioId).HasName("PRIMARY");

                entity.ToTable("service_ratio_t");

                entity.Property(e => e.ServiceRatioId)
                    .HasColumnType("int(11)")
                    .HasColumnName("service_ratio_id");
                entity.Property(e => e.Description)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("description");
                entity.Property(e => e.IsActive)
                    .HasDefaultValueSql("b'1'")
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_active");
                entity.Property(e => e.Ratio)
                    .HasPrecision(10)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("ratio");
            });

            modelBuilder.Entity<ServiceT>(entity =>
            {
                entity.HasKey(e => e.ServiceId).HasName("PRIMARY");

                entity.ToTable("service_t");

                entity.HasIndex(e => e.DepartmentId, "fk_service_t_department_sub1t1_idx");

                entity.Property(e => e.ServiceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("service_id");
                entity.Property(e => e.CompanyDiscountPercentage)
                    .HasPrecision(10)
                    .HasDefaultValueSql("'100.00'")
                    .HasColumnName("company_discount_percentage");
                entity.Property(e => e.DepartmentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("department_id");
                entity.Property(e => e.DiscountServiceCount)
                    .HasDefaultValueSql("'1'")
                    .HasColumnType("int(11)")
                    .HasColumnName("discount_service_count");
                entity.Property(e => e.MaterialCost)
                    .HasPrecision(10)
                    .HasDefaultValueSql("'0.00'")
                    .HasColumnName("material_cost");
                entity.Property(e => e.NoDiscount)
                    .HasColumnType("bit(1)")
                    .HasColumnName("no_discount");
                entity.Property(e => e.NoMinimumCharge)
                    .HasColumnType("bit(1)")
                    .HasColumnName("no_minimum_charge");
                entity.Property(e => e.NoPointDiscount)
                    .HasColumnType("bit(1)")
                    .HasColumnName("no_point_discount");
                entity.Property(e => e.NoPromocodeDiscount)
                    .HasColumnType("bit(1)")
                    .HasColumnName("no_promocode_discount");
                entity.Property(e => e.ServiceCost)
                    .HasPrecision(10)
                    .HasColumnName("service_cost");
                entity.Property(e => e.ServiceDes)
                    .HasMaxLength(150)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("service_des");
                entity.Property(e => e.ServiceDiscount)
                    .HasPrecision(10, 8)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("service_discount");
                entity.Property(e => e.ServiceDuration)
                    .HasPrecision(10)
                    .HasColumnName("service_duration");
                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("service_name");
                entity.Property(e => e.ServicePoints)
                    .HasDefaultValueSql("'0'")
                    .HasColumnType("int(11)")
                    .HasColumnName("service_points");

                entity.HasOne(d => d.Department).WithMany(p => p.ServiceT)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("fk_service_t_department_sub1t1");
            });

            modelBuilder.Entity<SettingT>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("setting_t");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                entity.Property(e => e.FawryAutoUpdateStausFlag)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("bit(1)")
                    .HasColumnName("fawry_auto_update_staus_flag");
                entity.Property(e => e.FawryAutopayFlag)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("bit(1)")
                    .HasColumnName("fawry_autopay_flag");
                entity.Property(e => e.FawryPaySendDate)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("fawry_pay_send_date");
            });

            modelBuilder.Entity<SiteContractT>(entity =>
            {
                entity.HasKey(e => e.ContractId).HasName("PRIMARY");

                entity.ToTable("site_contract_t");

                entity.HasIndex(e => e.SiteId, "fk_contract_t_stie_t_idx");

                entity.HasIndex(e => e.ModificationSystemUserId, "fk_contract_t_system_user_t_1_idx");

                entity.HasIndex(e => e.SystemUserId, "fk_contract_t_system_user_t_idx");

                entity.Property(e => e.ContractId)
                    .HasColumnType("int(11)")
                    .HasColumnName("contract_id");
                entity.Property(e => e.Amount)
                    .HasPrecision(10)
                    .HasColumnName("amount");
                entity.Property(e => e.ContractDesception)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("contract_desception");
                entity.Property(e => e.CreationTime)
                    .HasDefaultValueSql("'current_timestamp()'")
                    .HasColumnType("datetime")
                    .HasColumnName("creation_time");
                entity.Property(e => e.ModificationSystemUserId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("modification_system_user_id");
                entity.Property(e => e.ModificationTime)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("modification_time");
                entity.Property(e => e.PaidAmount)
                    .HasPrecision(10)
                    .HasColumnName("paid_amount");
                entity.Property(e => e.RemainAmount)
                    .HasPrecision(10)
                    .HasColumnName("remain_amount");
                entity.Property(e => e.SiteId)
                    .HasColumnType("int(11)")
                    .HasColumnName("site_id");
                entity.Property(e => e.SystemUserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");

                entity.HasOne(d => d.ModificationSystemUser).WithMany(p => p.SiteContractTModificationSystemUser)
                    .HasForeignKey(d => d.ModificationSystemUserId)
                    .HasConstraintName("fk_contract_t_system_user_t_1");

                entity.HasOne(d => d.Site).WithMany(p => p.SiteContractT)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_contract_t_stie_t");

                entity.HasOne(d => d.SystemUser).WithMany(p => p.SiteContractTSystemUser)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_contract_t_system_user_t");
            });

            modelBuilder.Entity<SiteT>(entity =>
            {
                entity.HasKey(e => e.SiteId).HasName("PRIMARY");

                entity.ToTable("site_t");

                entity.HasIndex(e => e.ClientId, "fk_site_t_client_t_idx");

                entity.HasIndex(e => e.SiteEngineer, "fk_site_t_employee_t_idx");

                entity.HasIndex(e => e.SystemUserId, "fk_site_t_system_user_t_idx");

                entity.Property(e => e.SiteId)
                    .HasColumnType("int(11)")
                    .HasColumnName("site_id");
                entity.Property(e => e.ClientId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("client_id");
                entity.Property(e => e.CompleteTime)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("complete_time");
                entity.Property(e => e.CreationTime)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("creation_time");
                entity.Property(e => e.IsComplete)
                    .HasDefaultValueSql("b'0'")
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_complete");
                entity.Property(e => e.SiteDescreption)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("site_descreption");
                entity.Property(e => e.SiteEngineer)
                    .HasMaxLength(14)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("site_engineer");
                entity.Property(e => e.SiteName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("site_name");
                entity.Property(e => e.SystemUserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");

                entity.HasOne(d => d.Client).WithMany(p => p.SiteT)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("fk_site_t_client_t");

                entity.HasOne(d => d.SiteEngineerNavigation).WithMany(p => p.SiteT)
                    .HasForeignKey(d => d.SiteEngineer)
                    .HasConstraintName("fk_site_t_employee_t");

                entity.HasOne(d => d.SystemUser).WithMany(p => p.SiteT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_site_t_system_user_t");
            });

            modelBuilder.Entity<SubscriptionSequenceT>(entity =>
            {
                entity.HasKey(e => e.ClientSubscriptionSequenceId).HasName("PRIMARY");

                entity.ToTable("subscription_sequence_t");

                entity.HasIndex(e => e.SubscriptionServiceId, "fk_subscription_service_t_subscription_sequence_t_idx");

                entity.Property(e => e.ClientSubscriptionSequenceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("client_subscription_sequence_id");
                entity.Property(e => e.CompanyDiscountPercentage).HasColumnName("company_discount_percentage");
                entity.Property(e => e.DiscountPercentage).HasColumnName("discount_percentage");
                entity.Property(e => e.Sequence)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("sequence");
                entity.Property(e => e.SubscriptionServiceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("subscription_service_id");

                entity.HasOne(d => d.SubscriptionService).WithMany(p => p.SubscriptionSequenceT)
                    .HasForeignKey(d => d.SubscriptionServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_subscription_service_t_subscription_sequence_t");
            });

            modelBuilder.Entity<SubscriptionServiceT>(entity =>
            {
                entity.HasKey(e => e.SubscriptionServiceId).HasName("PRIMARY");

                entity.ToTable("subscription_service_t");

                entity.HasIndex(e => e.ServiceId, "fk_subscription_service_t_service_t_idx");

                entity.HasIndex(e => e.SubscriptionId, "fk_subscription_service_t_subscription_t_idx");

                entity.Property(e => e.SubscriptionServiceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("subscription_service_id");
                entity.Property(e => e.Discount)
                    .HasPrecision(10)
                    .HasColumnName("discount");
                entity.Property(e => e.Info)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("info");
                entity.Property(e => e.ServiceId)
                    .HasColumnType("int(11)")
                    .HasColumnName("service_id");
                entity.Property(e => e.SubscriptionId)
                    .HasColumnType("int(11)")
                    .HasColumnName("subscription_id");
                entity.Property(e => e.TotalPricePerMonth)
                    .HasPrecision(10)
                    .HasColumnName("total_price_per_month");

                entity.HasOne(d => d.Service).WithMany(p => p.SubscriptionServiceT)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_subscription_service_t_service_t");

                entity.HasOne(d => d.Subscription).WithMany(p => p.SubscriptionServiceT)
                    .HasForeignKey(d => d.SubscriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_subscription_service_t_subscription_t");
            });

            modelBuilder.Entity<SubscriptionT>(entity =>
            {
                entity.HasKey(e => e.SubscriptionId).HasName("PRIMARY");

                entity.ToTable("subscription_t");

                entity.HasIndex(e => e.DepartmentId, "fk_client_subscription_t_department_t_idx");

                entity.Property(e => e.SubscriptionId)
                    .HasColumnType("int(11)")
                    .HasColumnName("subscription_id");
                entity.Property(e => e.Condition)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("condition");
                entity.Property(e => e.DepartmentId)
                    .HasColumnType("int(11)")
                    .HasColumnName("department_id");
                entity.Property(e => e.Description)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("description");
                entity.Property(e => e.IgnoreServiceDiscount)
                    .HasColumnType("bit(1)")
                    .HasColumnName("ignore_service_discount");
                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_active");
                entity.Property(e => e.IsContract)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_contract");
                entity.Property(e => e.NumberOfMonth)
                    .HasDefaultValueSql("'1'")
                    .HasColumnType("int(11)")
                    .HasColumnName("number_of_month");
                entity.Property(e => e.RequestNumberPerMonth)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("request_number_per_month");
                entity.Property(e => e.StartFromPrice)
                    .HasPrecision(10)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("start_from_price");
                entity.Property(e => e.SubscriptionName)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("subscription_name");

                entity.HasOne(d => d.Department).WithMany(p => p.SubscriptionT)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_client_subscription_t_department_t");
            });

            modelBuilder.Entity<SystemUserT>(entity =>
            {
                entity.HasKey(e => e.SystemUserId).HasName("PRIMARY");

                entity.ToTable("system_user_t");

                entity.HasIndex(e => e.BranchId, "fk_system_users_t_branch_t1_idx");

                entity.HasIndex(e => e.EmployeeId, "fk_system_users_t_employee_t1_idx");

                entity.HasIndex(e => e.SystemUserUsername, "system_user_username_UNIQUE").IsUnique();

                entity.Property(e => e.SystemUserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");
                entity.Property(e => e.BranchId)
                    .HasColumnType("int(11)")
                    .HasColumnName("branch_id");
                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(14)
                    .HasColumnName("employee_id");
                entity.Property(e => e.SystemUserLevel)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("system_user_level");
                entity.Property(e => e.SystemUserPass)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("system_user_pass");
                entity.Property(e => e.SystemUserUsername)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("system_user_username");

                entity.HasOne(d => d.Branch).WithMany(p => p.SystemUserT)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_system_users_t_branch_t1");

                entity.HasOne(d => d.Employee).WithMany(p => p.SystemUserT)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_system_users_t_employee_t1");
            });

            modelBuilder.Entity<TimetableT>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.TimetableDate }).HasName("PRIMARY");

                entity.ToTable("timetable_t");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(14)
                    .HasColumnName("employee_id");
                entity.Property(e => e.TimetableDate)
                    .HasColumnType("date")
                    .HasColumnName("timetable_date");
                entity.Property(e => e.Timetable1)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("timetable_1");
                entity.Property(e => e.Timetable10)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("timetable_10");
                entity.Property(e => e.Timetable4)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("timetable_4");
                entity.Property(e => e.Timetable7)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("timetable_7");
                entity.Property(e => e.Timetable9)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("timetable_9");

                entity.HasOne(d => d.Employee).WithMany(p => p.TimetableT)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_timetable_t_employee_t1");
            });

            modelBuilder.Entity<TokenT>(entity =>
            {
                entity.HasKey(e => e.TokenId).HasName("PRIMARY");

                entity.ToTable("token_t");

                entity.HasIndex(e => e.AccountId, "fk_token_t_account_t_idx");

                entity.Property(e => e.TokenId)
                    .HasColumnType("int(11)")
                    .HasColumnName("token_id");
                entity.Property(e => e.AccountId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("account_id");
                entity.Property(e => e.CreationTime)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("creation_time");
                entity.Property(e => e.Token)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("token");

                entity.HasOne(d => d.Account).WithMany(p => p.TokenT)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_token_t_account_t");
            });

            modelBuilder.Entity<TransactionT>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("transaction_t");

                entity.HasIndex(e => e.SystemUserId, "fk_transaction_t_system_user_t_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");
                entity.Property(e => e.Amount)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("amount");
                entity.Property(e => e.CreationTime)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("creation_time");
                entity.Property(e => e.Date)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("datetime")
                    .HasColumnName("date");
                entity.Property(e => e.Description)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("description");
                entity.Property(e => e.IsCanceled)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_canceled");
                entity.Property(e => e.ReferenceId)
                    .HasMaxLength(14)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("reference_id");
                entity.Property(e => e.ReferenceType)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("reference_type");
                entity.Property(e => e.SystemUserId)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");

                entity.HasOne(d => d.SystemUser).WithMany(p => p.TransactionT)
                    .HasForeignKey(d => d.SystemUserId)
                    .HasConstraintName("fk_transaction_t_system_user_t");
            });

            modelBuilder.Entity<TranslatorT>(entity =>
            {
                entity.HasKey(e => e.TranslatorId).HasName("PRIMARY");

                entity.ToTable("translator_t");

                entity.Property(e => e.TranslatorId)
                    .HasColumnType("int(11)")
                    .HasColumnName("translator_id");
                entity.Property(e => e.Key)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("key");
                entity.Property(e => e.LangId)
                    .HasDefaultValueSql("'1'")
                    .HasColumnType("int(11)")
                    .HasColumnName("lang_id");
                entity.Property(e => e.ReferenceId)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnName("reference_id");
                entity.Property(e => e.ReferenceType)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("reference_type");
                entity.Property(e => e.Value)
                    .HasDefaultValueSql("'NULL'")
                    .HasColumnType("text")
                    .HasColumnName("value");
            });

            modelBuilder.Entity<VacationT>(entity =>
            {
                entity.HasKey(e => e.VacationId).HasName("PRIMARY");

                entity.ToTable("vacation_t");

                entity.HasIndex(e => e.EmployeeId, "fk_vacation_t_employee_t_idx");

                entity.HasIndex(e => e.SystemUserId, "fk_vacation_t_system_user_t_idx");

                entity.Property(e => e.VacationId)
                    .HasColumnType("int(11)")
                    .HasColumnName("vacation_id");
                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_time");
                entity.Property(e => e.Day)
                    .HasColumnType("date")
                    .HasColumnName("day");
                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasMaxLength(14)
                    .HasColumnName("employee_id");
                entity.Property(e => e.SystemUserId)
                    .HasColumnType("int(11)")
                    .HasColumnName("system_user_id");

                entity.HasOne(d => d.Employee).WithMany(p => p.VacationT)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_vacation_t_employee_t");

                entity.HasOne(d => d.SystemUser).WithMany(p => p.VacationT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_vacation_t_system_user_t");
            });

            modelBuilder.Entity<VersionT>(entity =>
            {
                entity.HasKey(e => e.VersionNumber).HasName("PRIMARY");

                entity.ToTable("version_t");

                entity.Property(e => e.VersionNumber)
                    .HasColumnType("int(11)")
                    .HasColumnName("version_number");
            });

            modelBuilder.Entity<WorkingAreaT>(entity =>
            {
                entity.HasKey(e => e.WorkingAreaId).HasName("PRIMARY");

                entity.ToTable("working_area_t");

                entity.HasIndex(e => e.BranchId, "fk_work_areas_branch_t1_idx");

                entity.HasIndex(e => new { e.WorkingAreaGov, e.WorkingAreaCity, e.WorkingAreaRegion }, "unique_area").IsUnique();

                entity.Property(e => e.WorkingAreaId)
                    .HasColumnType("int(11)")
                    .HasColumnName("working_area_id");
                entity.Property(e => e.BranchId)
                    .HasColumnType("int(11)")
                    .HasColumnName("branch_id");
                entity.Property(e => e.WorkingAreaCity)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("working_area_city");
                entity.Property(e => e.WorkingAreaGov)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("working_area_gov");
                entity.Property(e => e.WorkingAreaRegion)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("working_area_region");

                entity.HasOne(d => d.Branch).WithMany(p => p.WorkingAreaT)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_work_areas_branch_t1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
