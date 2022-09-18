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
        public virtual DbSet<AppSettingT> AppSettingT { get; set; }
        public virtual DbSet<BillDetailsT> BillDetailsT { get; set; }
        public virtual DbSet<BillNumberT> BillNumberT { get; set; }
        public virtual DbSet<BranchT> BranchT { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CartDetailsT> CartDetailsT { get; set; }
        public virtual DbSet<CartT> CartT { get; set; }
        public virtual DbSet<CityT> CityT { get; set; }
        public virtual DbSet<Cleaningsubscribers> Cleaningsubscribers { get; set; }
        public virtual DbSet<ClientPhonesT> ClientPhonesT { get; set; }
        public virtual DbSet<ClientPointT> ClientPointT { get; set; }
        public virtual DbSet<ClientSubscriptionT> ClientSubscriptionT { get; set; }
        public virtual DbSet<ClientT> ClientT { get; set; }
        public virtual DbSet<ContractPaymentT> ContractPaymentT { get; set; }
        public virtual DbSet<CountryT> CountryT { get; set; }
        public virtual DbSet<CouponCityT> CouponCityT { get; set; }
        public virtual DbSet<CouponDepartmentT> CouponDepartmentT { get; set; }
        public virtual DbSet<CouponT> CouponT { get; set; }
        public virtual DbSet<DayWorkingTimeT> DayWorkingTimeT { get; set; }
        public virtual DbSet<DepartmentEmployeeT> DepartmentEmployeeT { get; set; }
        public virtual DbSet<DepartmentSub0T> DepartmentSub0T { get; set; }
        public virtual DbSet<DepartmentSub1T> DepartmentSub1T { get; set; }
        public virtual DbSet<DepartmentT> DepartmentT { get; set; }
        public virtual DbSet<DiscountT> DiscountT { get; set; }
        public virtual DbSet<DiscountTypeT> DiscountTypeT { get; set; }
        public virtual DbSet<EmployeeApproval> EmployeeApproval { get; set; }
        public virtual DbSet<EmployeeLocation> EmployeeLocation { get; set; }
        public virtual DbSet<EmployeeSubscriptionT> EmployeeSubscriptionT { get; set; }
        public virtual DbSet<EmployeeT> EmployeeT { get; set; }
        public virtual DbSet<EmployeeTypeT> EmployeeTypeT { get; set; }
        public virtual DbSet<EmployeeWorkplacesT> EmployeeWorkplacesT { get; set; }
        public virtual DbSet<EmploymentApplicationsT> EmploymentApplicationsT { get; set; }
        public virtual DbSet<FavouriteServiceT> FavouriteServiceT { get; set; }
        public virtual DbSet<FawryChargeRequestT> FawryChargeRequestT { get; set; }
        public virtual DbSet<FawryChargeT> FawryChargeT { get; set; }
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
        public virtual DbSet<RequestStatusT> RequestStatusT { get; set; }
        public virtual DbSet<RequestT> RequestT { get; set; }
        public virtual DbSet<RoleT> RoleT { get; set; }
        public virtual DbSet<ServiceRatioDetailsT> ServiceRatioDetailsT { get; set; }
        public virtual DbSet<ServiceRatioT> ServiceRatioT { get; set; }
        public virtual DbSet<ServiceT> ServiceT { get; set; }
        public virtual DbSet<SubscriptionSequenceT> SubscriptionSequenceT { get; set; }
        public virtual DbSet<SubscriptionT> SubscriptionT { get; set; }
        public virtual DbSet<SystemUserT> SystemUserT { get; set; }
        public virtual DbSet<TimetableT> TimetableT { get; set; }
        public virtual DbSet<TokenT> TokenT { get; set; }
        public virtual DbSet<TransactionT> TransactionT { get; set; }
        public virtual DbSet<TranslatorT> TranslatorT { get; set; }
        public virtual DbSet<VersionT> VersionT { get; set; }
        public virtual DbSet<WorkingAreaT> WorkingAreaT { get; set; }
        public virtual DbSet<AttachmentT> AttachmentT { get; set; }

        // Unable to generate entity type for table 'promocode'. Please see the warning messages.
        // Unable to generate entity type for table 'promocode'. Please see the warning messages.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;database=sanyaadatabase;uid=user;password=user@5100");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountRoleT>(entity =>
            {
                entity.HasKey(e => e.AccountRoleId);

                entity.ToTable("account_role_t");

                entity.HasIndex(e => e.AccountId)
                    .HasName("fk_account_role_account_idx");

                entity.HasIndex(e => e.RoleId)
                    .HasName("account_role_role_idx");

                entity.Property(e => e.AccountRoleId)
                    .HasColumnName("account_role_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsAcive)
                    .HasColumnName("is_acive")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountRoleT)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("fk_account_role_account");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AccountRoleT)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_account_type_role_role");
            });

            modelBuilder.Entity<AttachmentT>(entity =>
            {
                entity.HasKey(e => e.AttachmentId);

                entity.ToTable("attachment_t");

                entity.Property(e => e.AttachmentId)
                    .HasColumnName("attachment_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AttachmentType)
                    .HasColumnName("attachment_type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creation_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.FileName)
                    .HasColumnName("file_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.FilePath)
                    .HasColumnName("file_path")
                    .HasColumnType("text");

                entity.Property(e => e.ReferenceId)
                    .HasColumnName("reference_id")
                    .HasColumnType("varchar(14)");
            });
            modelBuilder.Entity<AccountT>(entity =>
            {
                entity.HasKey(e => e.AccountId);

                entity.ToTable("account_t");

                entity.HasIndex(e => e.AccountTypeId)
                    .HasName("fk_account_account_type_t_idx");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("fk_account_system_user_idx");

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AccountHashSlat)
                    .IsRequired()
                    .HasColumnName("account_hash_slat")
                    .HasColumnType("longtext");

                entity.Property(e => e.AccountPassword)
                    .IsRequired()
                    .HasColumnName("account_password")
                    .HasColumnType("longtext");

                entity.Property(e => e.AccountReferenceId)
                    .HasColumnName("account_reference_id")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.AccountSecurityCode)
                    .HasColumnName("account_security_code")
                    .HasColumnType("longtext");

                entity.Property(e => e.AccountTypeId)
                    .HasColumnName("account_type_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AccountUsername)
                    .IsRequired()
                    .HasColumnName("account_username")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creation_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.EmailOtpCode)
                    .HasColumnName("email_otp_code")
                    .HasColumnType("varchar(6)");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''");

                entity.Property(e => e.IsEmailVerfied)
                    .HasColumnName("is_email_verfied")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsMobileVerfied)
                    .HasColumnName("is_mobile_verfied")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsPasswordReseted)
                    .HasColumnName("is_password_reseted")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.LastOtpCreationTime)
                    .HasColumnName("last_otp_creation_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.LastResetPasswordRequestTime)
                    .HasColumnName("last_reset_password_request_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.MobileOtpCode)
                    .HasColumnName("mobile_otp_code")
                    .HasColumnType("varchar(6)");

                entity.Property(e => e.OtpCountWithinDay)
                    .HasColumnName("otp_count_within_day")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.PasswordResetCountWithinDay)
                    .HasColumnName("password_reset_count_within_day")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ResetPasswordToken)
                    .HasColumnName("reset_password_token")
                    .HasColumnType("longtext");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.AccountT)
                    .HasForeignKey(d => d.AccountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_account_account_type_t");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.AccountT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_account_system_user");
            });

            modelBuilder.Entity<AccountTypeT>(entity =>
            {
                entity.HasKey(e => e.AccountTypeId);

                entity.ToTable("account_type_t");

                entity.Property(e => e.AccountTypeId)
                    .HasColumnName("account_type_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AccountTypeDes)
                    .HasColumnName("account_type_des")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.AccountTypeName)
                    .IsRequired()
                    .HasColumnName("account_type_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''");
            });

            modelBuilder.Entity<AddressT>(entity =>
            {
                entity.HasKey(e => e.AddressId);

                entity.ToTable("address_t");

                entity.HasIndex(e => e.CityId)
                    .HasName("fk_address_t_city_t_idx");

                entity.HasIndex(e => e.ClientId)
                    .HasName("fk_address_t_client_t_idx");

                entity.HasIndex(e => e.RegionId)
                    .HasName("fk_address_t_region_t_idx");

                entity.Property(e => e.AddressId)
                    .HasColumnName("address_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddressBlockNum)
                    .HasColumnName("address_block_num")
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.AddressCity)
                    .HasColumnName("address_city")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.AddressDes)
                    .HasColumnName("address_des")
                    .HasColumnType("text");

                entity.Property(e => e.AddressFlatNum)
                    .HasColumnName("address_flat_num")
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.AddressGov)
                    .HasColumnName("address_gov")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.AddressRegion)
                    .HasColumnName("address_region")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.AddressStreet)
                    .HasColumnName("address_street")
                    .HasColumnType("text");

                entity.Property(e => e.CityId)
                    .HasColumnName("city_id")
                    .HasColumnType("int(11)"); 

                entity.Property(e => e.GovernorateId)
                    .HasColumnName("governorate_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsDefault)
                    .HasColumnName("is_default")
                    .HasColumnType("bit(1)");   
                
                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Latitude).HasColumnType("varchar(75)");

                entity.Property(e => e.Location).HasColumnType("text");

                entity.Property(e => e.Longitude).HasColumnType("varchar(75)");

                entity.Property(e => e.RegionId)
                    .HasColumnName("region_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.AddressT)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("fk_address_t_city_t");

                entity.HasOne(d => d.Governorate)
                   .WithMany(p => p.AddressT)
                   .HasForeignKey(d => d.GovernorateId)
                   .HasConstraintName("fk_address_t_governorate_t");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.AddressT)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("fk_address_t_client_t");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.AddressT)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("fk_address_t_region_t");
            });

            modelBuilder.Entity<AppLandingScreenItemT>(entity =>
            {
                entity.HasKey(e => e.ItemId);

                entity.ToTable("app_landing_screen_item_t");

                entity.Property(e => e.ItemId)
                    .HasColumnName("item_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ActionLink)
                    .HasColumnName("action_link")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.Caption)
                    .HasColumnName("caption")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.HavePackage)
                    .IsRequired()
                    .HasColumnName("have_package")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.ImagePath)
                    .HasColumnName("image_path")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''");

                entity.Property(e => e.ItemType)
                    .HasColumnName("item_type")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<AppSettingT>(entity =>
            {
                entity.HasKey(e => e.SettingId);

                entity.ToTable("app_setting_t");

                entity.Property(e => e.SettingId)
                    .HasColumnName("setting_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creation_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.IsAppSetting)
                    .HasColumnName("is_app_setting")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.SettingDatatype)
                    .HasColumnName("setting_datatype")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.SettingKey)
                    .IsRequired()
                    .HasColumnName("setting_key")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.SettingValue)
                    .IsRequired()
                    .HasColumnName("setting_value")
                    .HasColumnType("text");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<BillDetailsT>(entity =>
            {
                entity.HasKey(e => new { e.BillNumber, e.BillType, e.BillCost });

                entity.ToTable("bill_details_t");

                entity.Property(e => e.BillNumber)
                    .HasColumnName("bill_number")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.BillType)
                    .HasColumnName("bill_type")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.BillCost).HasColumnName("bill_cost");

                entity.Property(e => e.BillIo)
                    .HasColumnName("bill_io")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.BillNote)
                    .HasColumnName("bill_note")
                    .HasColumnType("varchar(20)");

                entity.HasOne(d => d.BillNumberNavigation)
                    .WithMany(p => p.BillDetailsT)
                    .HasForeignKey(d => d.BillNumber)
                    .HasConstraintName("fk_bill_details_t_bill_number_t1");
            });

            modelBuilder.Entity<BillNumberT>(entity =>
            {
                entity.HasKey(e => e.BillNumber);

                entity.ToTable("bill_number_t");

                entity.HasIndex(e => e.RequestId)
                    .HasName("fk_bill_number_t_request_t1_idx");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("fk_bill_number_t_system_user_t1_idx");

                entity.Property(e => e.BillNumber)
                    .HasColumnName("bill_number")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.BillTimestamp)
                    .HasColumnName("bill_timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.RequestId)
                    .HasColumnName("request_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.BillNumberT)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("fk_bill_number_t_request_t1");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.BillNumberT)
                    .HasForeignKey(d => d.SystemUserId)
                    .HasConstraintName("fk_bill_number_t_system_user_t1");
            });

            modelBuilder.Entity<BranchT>(entity =>
            {
                entity.HasKey(e => e.BranchId);

                entity.ToTable("branch_t");

                entity.HasIndex(e => e.BranchName)
                    .HasName("branch_name_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.BranchPhone)
                    .HasName("branch_phone_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.BranchId)
                    .HasColumnName("branch_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BranchBlockNum)
                    .HasColumnName("branch_block_num")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BranchCity)
                    .IsRequired()
                    .HasColumnName("branch_city")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.BranchDes)
                    .HasColumnName("branch_des")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.BranchFlatNum)
                    .HasColumnName("branch_flat_num")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BranchGov)
                    .IsRequired()
                    .HasColumnName("branch_gov")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasColumnName("branch_name")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.BranchPhone)
                    .IsRequired()
                    .HasColumnName("branch_phone")
                    .HasColumnType("varchar(11)");

                entity.Property(e => e.BranchRegion)
                    .IsRequired()
                    .HasColumnName("branch_region")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.BranchStreet)
                    .IsRequired()
                    .HasColumnName("branch_street")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("cart");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Barcode)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Qte)
                    .HasColumnName("QTE")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserId).HasColumnType("int(11)");
            });

            modelBuilder.Entity<CartDetailsT>(entity =>
            {
                entity.HasKey(e => e.CartDetailsId);

                entity.ToTable("cart_details_t");

                entity.HasIndex(e => e.CartId)
                    .HasName("fk_cart_details_t_cart_t_idx");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("fk_cart_details_t_service_t_idx");

                entity.Property(e => e.CartDetailsId)
                    .HasColumnName("cart_details_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CartId)
                    .HasColumnName("cart_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creation_time")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("service_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ServiceQuantity)
                    .HasColumnName("service_quantity")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CartDetailsT)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cart_details_t_cart_t");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.CartDetailsT)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cart_details_t_service_t");
            });

            modelBuilder.Entity<CartT>(entity =>
            {
                entity.HasKey(e => e.CartId);

                entity.ToTable("cart_t");

                entity.HasIndex(e => e.ClientId)
                    .HasName("fk_cart_t_client_t_idx");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("fk_cart_t_department_t_idx");

                entity.HasIndex(e => e.PromocodeId)
                    .HasName("fk_cart_t_promocode_t_idx");

                entity.Property(e => e.CartId)
                    .HasColumnName("cart_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creation_time")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsViaApp)
                    .HasColumnName("is_via_app")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ModificationTime)
                    .HasColumnName("modification_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.PromocodeId)
                    .HasColumnName("promocode_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UsePoint)
                    .HasColumnName("use_point")
                    .HasColumnType("bit(1)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.CartT)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cart_t_client_t");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.CartT)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cart_t_department_t");

                entity.HasOne(d => d.Promocode)
                    .WithMany(p => p.CartT)
                    .HasForeignKey(d => d.PromocodeId)
                    .HasConstraintName("fk_cart_t_promocode_t");
            });

            modelBuilder.Entity<CityT>(entity =>
            {
                entity.HasKey(e => e.CityId);

                entity.ToTable("city_t");

                entity.HasIndex(e => e.BranchId)
                    .HasName("fk_city_t_branch_t_idx");

                entity.HasIndex(e => e.GovernorateId)
                    .HasName("fk_city_t_governorate_t_idx");

                entity.Property(e => e.CityId)
                    .HasColumnName("city_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BranchId)
                    .HasColumnName("branch_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CityName)
                    .HasColumnName("city_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.DeliveryPrice)
                    .HasColumnName("delivery_price")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.GovernorateId)
                    .HasColumnName("governorate_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LoactionLat)
                    .HasColumnName("loaction_lat")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.LocationLang)
                    .HasColumnName("location_lang")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.LocationUrl)
                    .HasColumnName("location_url")
                    .HasColumnType("text");

                entity.Property(e => e.MinimumCharge)
                    .HasColumnName("minimum_charge")
                    .HasColumnType("smallint(6)");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.CityT)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("fk_city_t_branch_t");

                entity.HasOne(d => d.Governorate)
                    .WithMany(p => p.CityT)
                    .HasForeignKey(d => d.GovernorateId)
                    .HasConstraintName("fk_city_t_governorate_t");
            });

            modelBuilder.Entity<Cleaningsubscribers>(entity =>
            {
                entity.ToTable("cleaningsubscribers");

                entity.HasIndex(e => e.ClientId)
                    .HasName("IX_CleaningSubscribers_ClientId");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("IX_CleaningSubscribers_SystemUserId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.ClientId).HasColumnType("int(11)");

                entity.Property(e => e.Package).HasColumnType("int(11)");

                entity.Property(e => e.SystemUserId)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Cleaningsubscribers)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_CleaningSubscribers_client_t_ClientId");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.Cleaningsubscribers)
                    .HasForeignKey(d => d.SystemUserId)
                    .HasConstraintName("FK_CleaningSubscribers_system_user_t_SystemUserId");
            });

            modelBuilder.Entity<ClientPhonesT>(entity =>
            {
                entity.HasKey(e => e.ClientPhoneId);

                entity.ToTable("client_phones_t");

                entity.HasIndex(e => e.ClientId)
                    .HasName("fk_client_phones_t_client_t_idx");

                entity.HasIndex(e => e.ClientPhone)
                    .HasName("client_phone_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.ClientPhoneId)
                    .HasColumnName("client_phone_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientPhone)
                    .IsRequired()
                    .HasColumnName("client_phone")
                    .HasColumnType("varchar(11)");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasColumnType("varchar(6)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.IsDefault)
                    .HasColumnName("is_default")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.PwdUsr)
                    .HasColumnName("pwd_usr")
                    .HasColumnType("varchar(40)")
                    .HasDefaultValueSql("''");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientPhonesT)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("fk_client_phones_t_client_t");
            });

            modelBuilder.Entity<ClientPointT>(entity =>
            {
                entity.HasKey(e => e.ClientPointId);

                entity.ToTable("client_point_t");

                entity.HasIndex(e => e.ClientId)
                    .HasName("fk_client_t_idx");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("fk_system_user_t_idx");

                entity.Property(e => e.ClientPointId)
                    .HasColumnName("client_point_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creation_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.PointType)
                    .HasColumnName("point_type")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Points)
                    .HasColumnName("points")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Reason)
                    .HasColumnName("reason")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientPointT)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_client_t");
            });

            modelBuilder.Entity<ClientSubscriptionT>(entity =>
            {
                entity.HasKey(e => e.ClientSubscriptionId);

                entity.ToTable("client_subscription_t");

                entity.HasIndex(e => e.ClientId)
                    .HasName("client_subscription_t_client_t_idx");

                entity.HasIndex(e => e.SubscriptionId)
                    .HasName("client_subscription_t_subscribtion_t_idx");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("client_subscription_t_system_user_t_idx");

                entity.Property(e => e.ClientSubscriptionId)
                    .HasColumnName("client_subscription_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creation_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.SubscriptionId)
                    .HasColumnName("subscription_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientSubscriptionT)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("client_subscription_t_client_t");

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.ClientSubscriptionT)
                    .HasForeignKey(d => d.SubscriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("client_subscription_t_subscribtion_t");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.ClientSubscriptionT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("client_subscription_t_system_user_t");
            });

            modelBuilder.Entity<ClientT>(entity =>
            {
                entity.HasKey(e => e.ClientId);

                entity.ToTable("client_t");

                entity.HasIndex(e => e.BranchId)
                    .HasName("fk_client_t_branch_t1_idx");

                entity.HasIndex(e => e.ClientName)
                    .HasName("name_index");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("fk_client_systemuser_idx");

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BranchId)
                    .HasColumnName("branch_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientEmail)
                    .HasColumnName("client_email")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.ClientKnowUs)
                    .HasColumnName("client_know_us")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.ClientName)
                    .HasColumnName("client_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.ClientNotes)
                    .HasColumnName("client_notes")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.ClientPoints)
                    .HasColumnName("client_points")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientRegDate)
                    .HasColumnName("client_reg_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CurrentAddress)
                    .HasColumnName("current_address")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CurrentPhone)
                    .HasColumnName("current_phone")
                    .HasColumnType("varchar(11)");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'500'");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.ClientT)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("fk_client_t_branch_t1");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.ClientT)
                    .HasForeignKey(d => d.SystemUserId)
                    .HasConstraintName("fk_client_systemuser");
            });

            modelBuilder.Entity<ContractPaymentT>(entity =>
            {
                entity.HasKey(e => e.ContractPaymentId);

                entity.ToTable("contract_payment_t");

                entity.HasIndex(e => e.ContactId)
                    .HasName("fk_contract_payment_t_contact_t_idx");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("fk_contract_payment_t_system_user_t_idx");

                entity.Property(e => e.ContractPaymentId)
                    .HasColumnName("contract_payment_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ContactId)
                    .HasColumnName("contact_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.PaymentAmount)
                    .HasColumnName("payment_amount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.ContractPaymentT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_contract_payment_t_system_user_t");
            });

            modelBuilder.Entity<CountryT>(entity =>
            {
                entity.HasKey(e => e.CountryId);

                entity.ToTable("country_t");

                entity.Property(e => e.CountryId)
                    .HasColumnName("country_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CountryName)
                    .HasColumnName("country_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.LocationLang)
                    .HasColumnName("location_lang")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.LocationLat)
                    .HasColumnName("location_lat")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.LocationUrl)
                    .HasColumnName("location_url")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<CouponCityT>(entity =>
            {
                entity.HasKey(e => e.CouponCityId);

                entity.ToTable("coupon_city_t");

                entity.HasIndex(e => e.CityId)
                    .HasName("fk_coupon_city_t_city_t_idx");

                entity.HasIndex(e => e.CouponId)
                    .HasName("fk_coupon_city_t_coupon_t_idx");

                entity.Property(e => e.CouponCityId)
                    .HasColumnName("coupon_city_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CityId)
                    .HasColumnName("city_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CouponId)
                    .HasColumnName("coupon_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.CouponCityT)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_coupon_city_t_city_t");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.CouponCityT)
                    .HasForeignKey(d => d.CouponId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_coupon_city_t_coupon_t");
            });

            modelBuilder.Entity<CouponDepartmentT>(entity =>
            {
                entity.HasKey(e => e.CouponDepartmentId);

                entity.ToTable("coupon_department_t");

                entity.HasIndex(e => e.CouponId)
                    .HasName("coupon_department_t_coupon_t_idx");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("fk_coupon_department_t_department_t_idx");

                entity.Property(e => e.CouponDepartmentId)
                    .HasColumnName("coupon_department_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CouponId)
                    .HasColumnName("coupon_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.CouponDepartmentT)
                    .HasForeignKey(d => d.CouponId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("coupon_department_t_coupon_t");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.CouponDepartmentT)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_coupon_department_t_department_t");
            });

            modelBuilder.Entity<CouponT>(entity =>
            {
                entity.HasKey(e => e.CouponId);

                entity.ToTable("coupon_t");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("fk_coupon_t_system_user_t_idx");

                entity.Property(e => e.CouponId)
                    .HasColumnName("coupon_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CouponCode)
                    .IsRequired()
                    .HasColumnName("coupon_code")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creation_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsForAllCities)
                    .HasColumnName("is_for_all_cities")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsForAllDepartments)
                    .HasColumnName("is_for_all_departments")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.MaxUseCount)
                    .HasColumnName("max_use_count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MinimumCharge)
                    .HasColumnName("minimum_charge")
                    .HasColumnType("int(11)");

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.UsedCount)
                    .HasColumnName("used_count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.CouponT)
                    .HasForeignKey(d => d.SystemUserId)
                    .HasConstraintName("fk_coupon_t_system_user_t");
            });

            modelBuilder.Entity<DayWorkingTimeT>(entity =>
            {
                entity.HasKey(e => e.DayWorkingTimeId);

                entity.ToTable("day_working_time_t");

                entity.Property(e => e.DayWorkingTimeId)
                    .HasColumnName("day_working_time_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DayNameInEnglish)
                    .IsRequired()
                    .HasColumnName("day_name_in_english")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.DayOfTheWeekIndex)
                    .HasColumnName("day_of_the_week_index")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EndTime)
                    .HasColumnName("end_time")
                    .HasColumnType("time");

                entity.Property(e => e.StartTime)
                    .HasColumnName("start_time")
                    .HasColumnType("time");
            });

            modelBuilder.Entity<DepartmentEmployeeT>(entity =>
            {
                entity.HasKey(e => e.DepartmentEmployeeId);

                entity.ToTable("department_employee_t");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("fk_department_employee_t_department_t_idx");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("fk_department_t_has_employee_t_employee_t1_idx");

                entity.Property(e => e.DepartmentEmployeeId)
                    .HasColumnName("department_employee_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_Id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasColumnName("department_name")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasColumnName("employee_id")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.Percentage)
                    .HasColumnName("percentage")
                    .HasColumnType("tinyint(4)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.DepartmentEmployeeT)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("fk_department_employee_t_department_t");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.DepartmentEmployeeT)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("fk_department_t_has_employee_t_employee_t1");
            });

            modelBuilder.Entity<DepartmentSub0T>(entity =>
            {
                entity.HasKey(e => e.DepartmentSub0Id);

                entity.ToTable("department_sub0_t");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("fk_department_sub0_t_department_t_idx");

                entity.Property(e => e.DepartmentSub0Id)
                    .HasColumnName("department_sub0_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasColumnName("department_name")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.DepartmentSub0)
                    .IsRequired()
                    .HasColumnName("department_sub0")
                    .HasColumnType("varchar(25)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.DepartmentSub0T)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("fk_department_sub0_t_department_t");
            });

            modelBuilder.Entity<DepartmentSub1T>(entity =>
            {
                entity.HasKey(e => e.DepartmentId);

                entity.ToTable("department_sub1_t");

                entity.HasIndex(e => e.DepartmentSub0Id)
                    .HasName("fk_department_sub1_t_department_sub0_t_idx");

                entity.HasIndex(e => new { e.DepartmentName, e.DepartmentSub0, e.DepartmentSub1 })
                    .HasName("dept")
                    .IsUnique();

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DepartmentDes)
                    .HasColumnName("department_des")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasColumnName("department_name")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.DepartmentSub0)
                    .IsRequired()
                    .HasColumnName("department_sub0")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.DepartmentSub0Id)
                    .HasColumnName("department_sub0_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DepartmentSub1)
                    .IsRequired()
                    .HasColumnName("department_sub1")
                    .HasColumnType("varchar(25)");

                entity.HasOne(d => d.DepartmentSub0Navigation)
                    .WithMany(p => p.DepartmentSub1T)
                    .HasForeignKey(d => d.DepartmentSub0Id)
                    .HasConstraintName("fk_department_sub1_t_department_sub0_t");
            });

            modelBuilder.Entity<DepartmentT>(entity =>
            {
                entity.HasKey(e => e.DepartmentId);

                entity.ToTable("department_t");

                entity.HasIndex(e => e.DepartmentName)
                    .HasName("department_name_index");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DepartmentDes)
                    .HasColumnName("department_des")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.DepartmentImage)
                    .HasColumnName("department_image")
                    .HasColumnType("text");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasColumnName("department_name")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.DepartmentPercentage)
                    .HasColumnName("department_percentage")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.MaximumDiscountPercentage)
                    .HasColumnName("maximum_discount_percentage")
                    .HasColumnType("tinyint(4)");
            });

            modelBuilder.Entity<DiscountT>(entity =>
            {
                entity.ToTable("discount_t");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Discount2).HasColumnName("discount2");

                entity.Property(e => e.Discount3).HasColumnName("discount3");

                entity.Property(e => e.Discount4).HasColumnName("discount4");

                entity.Property(e => e.DiscountMore).HasColumnName("discount_more");
            });

            modelBuilder.Entity<DiscountTypeT>(entity =>
            {
                entity.HasKey(e => e.DiscountTypeId);

                entity.ToTable("discount_type_t");

                entity.Property(e => e.DiscountTypeId)
                    .HasColumnName("discount_type_id")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.DiscountTypeDes)
                    .HasColumnName("discount_type_des")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.DiscountTypeName)
                    .IsRequired()
                    .HasColumnName("discount_type_name")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''");
            });

            modelBuilder.Entity<EmployeeApproval>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("employee_approval");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.Approval)
                    .HasColumnName("approval")
                    .HasColumnType("varchar(11)");
            });

            modelBuilder.Entity<EmployeeLocation>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("employee_location");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.Latitude).HasColumnType("varchar(75)");

                entity.Property(e => e.Location).HasColumnType("varchar(75)");

                entity.Property(e => e.Longitude).HasColumnType("varchar(75)");

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.EmployeeLocation)
                    .HasForeignKey<EmployeeLocation>(d => d.EmployeeId)
                    .HasConstraintName("fk_employee_location");
            });

            modelBuilder.Entity<EmployeeSubscriptionT>(entity =>
            {
                entity.HasKey(e => e.SubscriptionId);

                entity.ToTable("employee_subscription_t");

                entity.Property(e => e.SubscriptionId)
                    .HasColumnName("subscription_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.InsuranceAmount)
                    .HasColumnName("insurance_amount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MaxRequestCount)
                    .HasColumnName("max_request_count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MaxRequestPrice)
                    .HasColumnName("max_request_price")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<EmployeeT>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("employee_t");

                entity.HasIndex(e => e.EmployeeFileNum)
                    .HasName("employee_file_num_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("employee_national_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.EmployeeName)
                    .HasName("employee_name_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.EmployeePhone)
                    .HasName("employee_phone1_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.EmployeeRelativePhone)
                    .HasName("employee_relative_num_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.SubscriptionId)
                    .HasName("fk_employee_subscription_t_idx");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.EmployeeBlockNum)
                    .HasColumnName("employee_block_num")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EmployeeCity)
                    .HasColumnName("employee_city")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.EmployeeDes)
                    .HasColumnName("employee_des")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.EmployeeFileNum)
                    .IsRequired()
                    .HasColumnName("employee_file_num")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.EmployeeFlatNum)
                    .HasColumnName("employee_flat_num")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EmployeeGov)
                    .HasColumnName("employee_gov")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.EmployeeHireDate)
                    .HasColumnName("employee_hire_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.EmployeeImageUrl)
                    .HasColumnName("employee_image_url")
                    .HasColumnType("text");

                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasColumnName("employee_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.EmployeePercentage)
                    .HasColumnName("employee_percentage")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.EmployeePhone)
                    .IsRequired()
                    .HasColumnName("employee_phone")
                    .HasColumnType("varchar(11)");

                entity.Property(e => e.EmployeePhone1)
                    .HasColumnName("employee_phone1")
                    .HasColumnType("varchar(11)");

                entity.Property(e => e.EmployeeRegion)
                    .HasColumnName("employee_region")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.EmployeeRelativeName)
                    .IsRequired()
                    .HasColumnName("employee_relative_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.EmployeeRelativePhone)
                    .IsRequired()
                    .HasColumnName("employee_relative_phone")
                    .HasColumnType("varchar(11)");

                entity.Property(e => e.EmployeeStreet)
                    .HasColumnName("employee_street")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.EmployeeType)
                    .HasColumnName("employee_type")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.SubscriptionId)
                    .HasColumnName("subscription_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.EmployeeT)
                    .HasForeignKey(d => d.SubscriptionId)
                    .HasConstraintName("fk_employee_subscription_t");
            });

            modelBuilder.Entity<EmployeeTypeT>(entity =>
            {
                entity.HasKey(e => e.EmployeeTypeId);

                entity.ToTable("employee_type_t");

                entity.Property(e => e.EmployeeTypeId)
                    .HasColumnName("employee_type_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.EmployeeTypeName)
                    .HasColumnName("employee_type_name")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<EmployeeWorkplacesT>(entity =>
            {
                entity.HasKey(e => e.EmployeeWorkplaceId);

                entity.ToTable("employee_workplaces_t");

                entity.HasIndex(e => e.BranchId)
                    .HasName("fk_branch_t_has_employee_t_branch_t1_idx");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("fk_branch_t_has_employee_t_employee_t1_idx");

                entity.Property(e => e.EmployeeWorkplaceId)
                    .HasColumnName("employee_workplace_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BranchId)
                    .HasColumnName("branch_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasColumnName("employee_id")
                    .HasColumnType("varchar(14)");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.EmployeeWorkplacesT)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_branch_t_has_employee_t_branch_t1");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeWorkplacesT)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("fk_branch_t_has_employee_t_employee_t1");
            });

            modelBuilder.Entity<EmploymentApplicationsT>(entity =>
            {
                entity.ToTable("employment_applications_t");

                entity.HasIndex(e => e.EmployeePhone)
                    .HasName("employee_phone1_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.EmployeeRelativePhone)
                    .HasName("employee_relative_num_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.NationalId)
                    .HasName("employee_national_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ApplicationStatus)
                    .HasColumnName("application_status")
                    .HasColumnType("varchar(10)")
                    .HasDefaultValueSql("'جديد'");

                entity.Property(e => e.BranchId)
                    .HasColumnName("branch_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasColumnName("department")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.EmployeeBlockNum)
                    .HasColumnName("employee_block_num")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EmployeeDes)
                    .HasColumnName("employee_des")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("'null'");

                entity.Property(e => e.EmployeeFlatNum)
                    .HasColumnName("employee_flat_num")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasColumnName("employee_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.EmployeePhone)
                    .IsRequired()
                    .HasColumnName("employee_phone")
                    .HasColumnType("varchar(11)");

                entity.Property(e => e.EmployeeRelativeName)
                    .IsRequired()
                    .HasColumnName("employee_relative_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.EmployeeRelativePhone)
                    .IsRequired()
                    .HasColumnName("employee_relative_phone")
                    .HasColumnType("varchar(11)");

                entity.Property(e => e.LocationLangitude).HasColumnName("location_langitude");

                entity.Property(e => e.LocationLatitude).HasColumnName("location_latitude");

                entity.Property(e => e.LocationText)
                    .HasColumnName("location_text")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.NationalId)
                    .IsRequired()
                    .HasColumnName("national_id")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");
            });

            modelBuilder.Entity<FavouriteServiceT>(entity =>
            {
                entity.HasKey(e => e.FavouriteServiceId);

                entity.ToTable("favourite_service_t");

                entity.HasIndex(e => e.ClientId)
                    .HasName("favourite_service_t_client_t_idx");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("favourite_service_t_service_t_idx");

                entity.Property(e => e.FavouriteServiceId)
                    .HasColumnName("favourite_service_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creation_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("service_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.FavouriteServiceT)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("favourite_service_t_client_t");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.FavouriteServiceT)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("favourite_service_t_service_t");
            });

            modelBuilder.Entity<FawryChargeRequestT>(entity =>
            {
                entity.HasKey(e => new { e.ChargeId, e.RequestId });

                entity.ToTable("fawry_charge_request_t");

                entity.HasIndex(e => e.RequestId)
                    .HasName("fk_request_idx");

                entity.Property(e => e.ChargeId)
                    .HasColumnName("charge_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RequestId)
                    .HasColumnName("request_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Charge)
                    .WithMany(p => p.FawryChargeRequestT)
                    .HasForeignKey(d => d.ChargeId)
                    .HasConstraintName("fk_fawry_charge");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.FawryChargeRequestT)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("fk_request");
            });

            modelBuilder.Entity<FawryChargeT>(entity =>
            {
                entity.HasKey(e => e.SystemId);

                entity.ToTable("fawry_charge_t");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("fk_fawry_charge_t_employee_t_idx");

                entity.HasIndex(e => e.FawryRefNumber)
                    .HasName("fk_fawry_charge_t_idx");

                entity.Property(e => e.SystemId)
                    .HasColumnName("system_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ChargeAmount).HasColumnName("charge_amount");

                entity.Property(e => e.ChargeExpireDate)
                    .HasColumnName("charge_expire_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ChargeStatus)
                    .HasColumnName("charge_status")
                    .HasColumnType("varchar(40)")
                    .HasDefaultValueSql("'NEW'");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.FawryRefNumber)
                    .HasColumnName("fawry_ref_number")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.IsConfirmed)
                    .HasColumnName("is_confirmed")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.RecordTimestamp)
                    .HasColumnName("record_timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.FawryChargeT)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("fk_fawry_charge_t_employee_t");
            });

            modelBuilder.Entity<FiredStaffT>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("fired_staff_t");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.FiredDate)
                    .HasColumnName("fired_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.FiredReasons)
                    .IsRequired()
                    .HasColumnName("fired_reasons")
                    .HasColumnType("varchar(100)");

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.FiredStaffT)
                    .HasForeignKey<FiredStaffT>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_fired_staff_t_employee_t1");
            });

            modelBuilder.Entity<FollowUpT>(entity =>
            {
                entity.HasKey(e => new { e.RequestId, e.Timestamp });

                entity.ToTable("follow_up_t");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("system_user_fk_idx");

                entity.Property(e => e.RequestId)
                    .HasColumnName("request_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Behavior)
                    .HasColumnName("behavior")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Cleaness)
                    .HasColumnName("cleaness")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Paid).HasColumnName("paid");

                entity.Property(e => e.Prices)
                    .HasColumnName("prices")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Product)
                    .HasColumnName("product")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.ProductCost).HasColumnName("product_cost");

                entity.Property(e => e.Rate)
                    .HasColumnName("rate")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Reason)
                    .HasColumnName("reason")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Review)
                    .IsRequired()
                    .HasColumnName("review")
                    .HasColumnType("text");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Tps)
                    .HasColumnName("tps")
                    .HasColumnType("tinyint(4)");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.FollowUpT)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("request_fk");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.FollowUpT)
                    .HasForeignKey(d => d.SystemUserId)
                    .HasConstraintName("system_user_fk");
            });

            modelBuilder.Entity<GovernorateT>(entity =>
            {
                entity.HasKey(e => e.GovernorateId);

                entity.ToTable("governorate_t");

                entity.HasIndex(e => e.CountryId)
                    .HasName("fk_governorate_t_country_t_idx");

                entity.Property(e => e.GovernorateId)
                    .HasColumnName("governorate_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CountryId)
                    .HasColumnName("country_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.GovernorateName)
                    .HasColumnName("governorate_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.LocationLang)
                    .HasColumnName("location_lang")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.LocationLat)
                    .HasColumnName("location_lat")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.LocationUrl)
                    .HasColumnName("location_url")
                    .HasColumnType("text");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.GovernorateT)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("fk_governorate_t_country_t");
            });

            modelBuilder.Entity<IncreaseDiscountT>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.Timestamp, e.IncreaseDiscountReason });

                entity.ToTable("increase_discount_t");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("fk_increase_discount_t_system_user_t1_idx");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.Timestamp)
                    .HasColumnName("timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.IncreaseDiscountReason)
                    .HasColumnName("increase_discount_reason")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.IncreaseDiscountType)
                    .HasColumnName("increase_discount_type")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.IncreaseDiscountValue)
                    .HasColumnName("increase_discount_value")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.IncreaseDiscountT)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("fk_increase_discount_t_employee_t1");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.IncreaseDiscountT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_increase_discount_t_system_user_t1");
            });

            modelBuilder.Entity<InsurancePaymentT>(entity =>
            {
                entity.HasKey(e => e.InsurancePaymentId);

                entity.ToTable("insurance_payment_t");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("fk_insurance_payment_t_employee_t_idx");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("fk_insurance_payment_t_system_user_t_idx");

                entity.Property(e => e.InsurancePaymentId)
                    .HasColumnName("insurance_payment_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creation_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.ReferenceId)
                    .HasColumnName("reference_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReferenceType)
                    .HasColumnName("reference_type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.InsurancePaymentT)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("fk_insurance_payment_t_employee_t");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.InsurancePaymentT)
                    .HasForeignKey(d => d.SystemUserId)
                    .HasConstraintName("fk_insurance_payment_t_system_user_t");
            });

            modelBuilder.Entity<LandingScreenItemDetailsT>(entity =>
            {
                entity.ToTable("landing_screen_item_details_t");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("fk_landing_screen_item_details_t_department_t_idx");

                entity.HasIndex(e => e.ItemId)
                    .HasName("fk_landing_screen_item_details_t_item_t_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasColumnName("department_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("image_url")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ItemId)
                    .HasColumnName("item_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.LandingScreenItemDetailsT)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_landing_screen_item_details_t_department_t");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.LandingScreenItemDetailsT)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("fk_landing_screen_item_details_t_item_t");
            });

            modelBuilder.Entity<LoginT>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.ToTable("login_t");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.LastActiveTimestamp)
                    .HasColumnName("last_active_timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.LoginAccountDeactiveMessage)
                    .HasColumnName("login_account_deactive_message")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.LoginAccountState)
                    .HasColumnName("login_account_state")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.LoginAvailability)
                    .HasColumnName("login_availability")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'فارغ'");

                entity.Property(e => e.LoginMessage)
                    .HasColumnName("login_message")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.LoginPassword)
                    .HasColumnName("login_password")
                    .HasColumnType("varchar(45)");

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.LoginT)
                    .HasForeignKey<LoginT>(d => d.EmployeeId)
                    .HasConstraintName("fk_login_t_employee_t1");
            });

            modelBuilder.Entity<MessagesT>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.MessageTimestamp });

                entity.ToTable("messages_t");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.MessageTimestamp)
                    .HasColumnName("message_timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Body)
                    .HasColumnName("body")
                    .HasColumnType("text");

                entity.Property(e => e.IsRead)
                    .HasColumnName("is_read")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasColumnType("varchar(30)");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.MessagesT)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("fk_employee_id");
            });

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.ToTable("notifications");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasColumnName("body")
                    .HasColumnType("text");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(75)");
            });

            modelBuilder.Entity<OpeningSoonDepartmentT>(entity =>
            {
                entity.ToTable("opening_soon_department_t");

                entity.HasIndex(e => e.CityId)
                    .HasName("fk_opening_soon_department_t_city_t_idx");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("fk_opening_soon_department_t_department_t_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CityId)
                    .HasColumnName("city_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.OpeningSoonDepartmentT)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("fk_opening_soon_department_t_city_t");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.OpeningSoonDepartmentT)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("fk_opening_soon_department_t_department_t");
            });

            modelBuilder.Entity<PartinerCartT>(entity =>
            {
                entity.ToTable("partiner_cart_t");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ServiceCount)
                    .HasColumnName("service_count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("service_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SystemUsername)
                    .IsRequired()
                    .HasColumnName("system_username")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<PartinerPaymentRequestT>(entity =>
            {
                entity.HasKey(e => new { e.PaymentId, e.RequestId });

                entity.ToTable("partiner_payment_request_t");

                entity.HasIndex(e => e.RequestId)
                    .HasName("partiner_request_fk_idx");

                entity.Property(e => e.PaymentId)
                    .HasColumnName("payment_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RequestId)
                    .HasColumnName("request_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.PartinerPaymentRequestT)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("partiner_payment_fk");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.PartinerPaymentRequestT)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("partiner_request_fk");
            });

            modelBuilder.Entity<PartinerPaymentT>(entity =>
            {
                entity.ToTable("partiner_payment_t");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("partiner_systemuser_fk_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.DateFrom)
                    .HasColumnName("date_from")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateTo)
                    .HasColumnName("date_to")
                    .HasColumnType("datetime");

                entity.Property(e => e.RecordTimestamp)
                    .HasColumnName("record_timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.PartinerPaymentT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("partiner_systemuser_fk");
            });

            modelBuilder.Entity<PaymentT>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.ToTable("payment_t");

                entity.HasIndex(e => e.RequestId)
                    .HasName("fk_payment_t_request_t1_idx");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("fk_payment_t_system_user_t1_idx");

                entity.Property(e => e.RequestId)
                    .HasColumnName("request_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Payment).HasColumnName("payment");

                entity.Property(e => e.PaymentTimestamp)
                    .HasColumnName("payment_timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Request)
                    .WithOne(p => p.PaymentT)
                    .HasForeignKey<PaymentT>(d => d.RequestId)
                    .HasConstraintName("fk_payment_t_request_t1");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.PaymentT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_payment_t_system_user_t1");
            });

            modelBuilder.Entity<Poll>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.ToTable("poll");

                entity.Property(e => e.RequestId)
                    .HasColumnName("request_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Employee)
                    .IsRequired()
                    .HasColumnName("employee")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.Employee2)
                    .IsRequired()
                    .HasColumnName("employee2")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.Knowme)
                    .IsRequired()
                    .HasColumnName("knowme")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasColumnName("note")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.Place)
                    .IsRequired()
                    .HasColumnName("place")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnName("price")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.Time)
                    .IsRequired()
                    .HasColumnName("time")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.Vote)
                    .IsRequired()
                    .HasColumnName("vote")
                    .HasColumnType("varchar(25)");
            });

            modelBuilder.Entity<ProductReceiptT>(entity =>
            {
                entity.HasKey(e => e.ReceiptId);

                entity.ToTable("product_receipt_t");

                entity.Property(e => e.ReceiptId)
                    .HasColumnName("receipt_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BranchId)
                    .HasColumnName("branch_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductReceiptPaid).HasColumnName("product_receipt_paid");

                entity.Property(e => e.ReceiptEmployeeBuyer)
                    .HasColumnName("receipt_employee_buyer")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.ReceiptTimestamp)
                    .HasColumnName("receipt_timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.SystemUsername)
                    .HasColumnName("system_username")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<ProductSoldT>(entity =>
            {
                entity.HasKey(e => new { e.ReceiptId, e.ProductId, e.ProductSoldNote });

                entity.ToTable("product_sold_t");

                entity.HasIndex(e => e.ProductId)
                    .HasName("fk_product_receipt_t_has_product_t_product_t1_idx");

                entity.HasIndex(e => e.ReceiptId)
                    .HasName("fk_product_receipt_t_has_product_t_product_receipt_t1_idx");

                entity.Property(e => e.ReceiptId)
                    .HasColumnName("receipt_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductSoldNote)
                    .HasColumnName("product_sold_note")
                    .HasColumnType("varchar(5)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.ProductSoldPrice).HasColumnName("product_sold_price");

                entity.Property(e => e.ProductSoldQuantity)
                    .HasColumnName("product_sold_quantity")
                    .HasColumnType("smallint(6)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductSoldT)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_product_receipt_t_has_product_t_product_t1");

                entity.HasOne(d => d.Receipt)
                    .WithMany(p => p.ProductSoldT)
                    .HasForeignKey(d => d.ReceiptId)
                    .HasConstraintName("fk_product_receipt_t_has_product_t_product_receipt_t1");
            });

            modelBuilder.Entity<ProductT>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("product_t");

                entity.HasIndex(e => e.BranchId)
                    .HasName("fk_products_t_branch_t1_idx");

                entity.HasIndex(e => e.ProductName)
                    .HasName("product_name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BranchId)
                    .HasColumnName("branch_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ProductCustomerPrice).HasColumnName("product_customer_price");

                entity.Property(e => e.ProductDepartment)
                    .HasColumnName("product_department")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.ProductDes)
                    .HasColumnName("product_des")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("product_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.ProductPriceBuy).HasColumnName("product_price_buy");

                entity.Property(e => e.ProductPriceSell).HasColumnName("product_price_sell");

                entity.Property(e => e.ProductQuantity)
                    .HasColumnName("product_quantity")
                    .HasColumnType("smallint(6)");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.ProductT)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_products_t_branch_t1");
            });

            modelBuilder.Entity<PromocodeCityT>(entity =>
            {
                entity.HasKey(e => e.PromocodeCityId);

                entity.ToTable("promocode_city_t");

                entity.HasIndex(e => e.CityId)
                    .HasName("fk_promocode_ciry_t_city_t_idx");

                entity.HasIndex(e => e.PromocodeId)
                    .HasName("fk_promocode_city_t_promocode_t_idx");

                entity.Property(e => e.PromocodeCityId)
                    .HasColumnName("promocode_city_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CityId)
                    .HasColumnName("city_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PromocodeId)
                    .HasColumnName("promocode_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.PromocodeCityT)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("fk_promocode_city_t_city_t");

                entity.HasOne(d => d.Promocode)
                    .WithMany(p => p.PromocodeCityT)
                    .HasForeignKey(d => d.PromocodeId)
                    .HasConstraintName("fk_promocode_city_t_promocode_t");
            });

            modelBuilder.Entity<PromocodeDepartmentT>(entity =>
            {
                entity.HasKey(e => e.PromocodeDepartmentJd);

                entity.ToTable("promocode_department_t");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("fk_promocode_department_t_depart,emt_t_idx");

                entity.HasIndex(e => e.PromocodeId)
                    .HasName("fk_promocode_department_t_promocode_t_idx");

                entity.Property(e => e.PromocodeDepartmentJd)
                    .HasColumnName("promocode_department_jd")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PromocodeId)
                    .HasColumnName("promocode_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.PromocodeDepartmentT)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("fk_promocode_department_t_department_t");

                entity.HasOne(d => d.Promocode)
                    .WithMany(p => p.PromocodeDepartmentT)
                    .HasForeignKey(d => d.PromocodeId)
                    .HasConstraintName("fk_promocode_department_t_promocode_t");
            });

            modelBuilder.Entity<PromocodeT>(entity =>
            {
                entity.HasKey(e => e.PromocodeId);

                entity.ToTable("promocode_t");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("fk_promocode_t_sysyem_user_t_idx");

                entity.Property(e => e.PromocodeId)
                    .HasColumnName("promocode_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CompanyDiscountPercentage)
                    .HasColumnName("company_discount_percentage")
                    .HasColumnType("decimal(10,0)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.ExpireTime)
                    .HasColumnName("expire_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.MaxUsageCount)
                    .HasColumnName("max_usage_count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MinimumCharge)
                    .HasColumnName("minimum_charge")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Promocode)
                    .IsRequired()
                    .HasColumnName("promocode")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UsageCount)
                    .HasColumnName("usage_count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.PromocodeT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_promocode_t_sysyem_user_t");
            });

            modelBuilder.Entity<QuantityHistoryT>(entity =>
            {
                entity.HasKey(e => new { e.QuantityTimestamp, e.ProductId });

                entity.ToTable("quantity_history_t");

                entity.HasIndex(e => e.ProductId)
                    .HasName("fk_quantity_history_t_product_t1_idx");

                entity.Property(e => e.QuantityTimestamp)
                    .HasColumnName("quantity_timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.ProductId)
                    .HasColumnName("product_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.QuantityHistory)
                    .HasColumnName("quantity_history")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.SystemUsername)
                    .HasColumnName("system_username")
                    .HasColumnType("varchar(45)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.QuantityHistoryT)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_quantity_history_t_product_t1");
            });

            modelBuilder.Entity<RegestrationT>(entity =>
            {
                entity.HasKey(e => new { e.RegestrationName, e.RegestrationPhone });

                entity.ToTable("regestration_t");

                entity.HasIndex(e => e.RegestrationPhone)
                    .HasName("regestration_phone_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.RegestrationName)
                    .HasColumnName("regestration_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.RegestrationPhone)
                    .HasColumnName("regestration_phone")
                    .HasColumnType("varchar(11)");

                entity.Property(e => e.RegestrationAge)
                    .HasColumnName("regestration_age")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.RegestrationCity)
                    .IsRequired()
                    .HasColumnName("regestration_city")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.RegestrationDepartment)
                    .IsRequired()
                    .HasColumnName("regestration_department")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.RegestrationExperience)
                    .HasColumnName("regestration_experience")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.RegestrationGov)
                    .IsRequired()
                    .HasColumnName("regestration_gov")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.RegestrationPassword)
                    .IsRequired()
                    .HasColumnName("regestration_password")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.RegestrationTimestamep)
                    .HasColumnName("regestration_timestamep")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.RegestrationTransport)
                    .HasColumnName("regestration_transport")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.RegestrationView)
                    .HasColumnName("regestration_view")
                    .HasColumnType("varchar(6)")
                    .HasDefaultValueSql("'لا'");
            });

            modelBuilder.Entity<RegionT>(entity =>
            {
                entity.HasKey(e => e.RegionId);

                entity.ToTable("region_t");

                entity.HasIndex(e => e.CityId)
                    .HasName("fk_region_t_city_t_idx");

                entity.Property(e => e.RegionId)
                    .HasColumnName("region_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CityId)
                    .HasColumnName("city_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DeliveryPrice)
                    .HasColumnName("delivery_price")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.IsDeliveryPriceActive)
                    .IsRequired()
                    .HasColumnName("is_delivery_price_active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.IsMinimumChargeActive)
                    .IsRequired()
                    .HasColumnName("is_minimum_charge_active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.LocationLang)
                    .HasColumnName("location_lang")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.LocationLat)
                    .HasColumnName("location_lat")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.LocationUrl)
                    .HasColumnName("location_url")
                    .HasColumnType("text");

                entity.Property(e => e.MinimumCharge)
                    .HasColumnName("minimum_charge")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.RegionName)
                    .HasColumnName("region_name")
                    .HasColumnType("varchar(45)");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.RegionT)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("fk_region_t_city_t");
            });

            modelBuilder.Entity<RejectRequestT>(entity =>
            {
                entity.HasKey(e => e.RejectRequestId);

                entity.ToTable("reject_request_t");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("fk_reject_request_t_employee_t1_idx");

                entity.HasIndex(e => e.RequestId)
                    .HasName("fk_reject_request_t_request_t1_idx");

                entity.Property(e => e.RejectRequestId)
                    .HasColumnName("reject_request_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasColumnName("employee_id")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.RejectRequestTimestamp)
                    .HasColumnName("reject_request_timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.RequestId)
                    .HasColumnName("request_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.RejectRequestT)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("fk_reject_request_t_employee_t1");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RejectRequestT)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_reject_request_t_request_t1");
            });

            modelBuilder.Entity<RequestCanceledT>(entity =>
            {
                entity.HasKey(e => new { e.RequestId, e.CancelRequestTimestamp });

                entity.ToTable("request_canceled_t");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("fk_cancel_request_t_system_user_t1_idx");

                entity.Property(e => e.RequestId)
                    .HasColumnName("request_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CancelRequestTimestamp)
                    .HasColumnName("cancel_request_timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CancelRequestReason)
                    .IsRequired()
                    .HasColumnName("cancel_request_reason")
                    .HasColumnType("text");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestCanceledT)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("fk_cancel_requests_t_requests_t1");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.RequestCanceledT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cancel_request_t_system_user_t1");
            });

            modelBuilder.Entity<RequestComplaintT>(entity =>
            {
                entity.HasKey(e => new { e.RequestId, e.ComplaintTimestamp });

                entity.ToTable("request_complaint_t");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("fk_request_complaint_t_system_users_t1_idx");

                entity.HasIndex(e => new { e.RequestId, e.NewRequestId })
                    .HasName("fk_request_complaint_t_request_t1_idx");

                entity.Property(e => e.RequestId)
                    .HasColumnName("request_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ComplaintTimestamp)
                    .HasColumnName("complaint_timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.ComplaintDes)
                    .HasColumnName("complaint_des")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.ComplaintIsSolved)
                    .IsRequired()
                    .HasColumnName("complaint_is_solved")
                    .HasColumnType("varchar(3)")
                    .HasDefaultValueSql("'لا'");

                entity.Property(e => e.NewRequestId)
                    .HasColumnName("new_request_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestComplaintT)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("fk_request_complaint_t_request_t1");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.RequestComplaintT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_request_complaint_t_system_users_t1");
            });

            modelBuilder.Entity<RequestDelayedT>(entity =>
            {
                entity.HasKey(e => new { e.RequestId, e.DelayRequestTimestamp });

                entity.ToTable("request_delayed_t");

                entity.HasIndex(e => e.RequestId)
                    .HasName("fk_delay_requests_t_requests_t1_idx");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("fk_delay_request_t_system_user_t1_idx");

                entity.Property(e => e.RequestId)
                    .HasColumnName("request_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DelayRequestTimestamp)
                    .HasColumnName("delay_request_timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.DelayRequestNewTimestamp)
                    .HasColumnName("delay_request_new_timestamp")
                    .HasColumnType("datetime");

                entity.Property(e => e.DelayRequestReason)
                    .IsRequired()
                    .HasColumnName("delay_request_reason")
                    .HasColumnType("text");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestDelayedT)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("fk_delay_requests_t_requests_t1");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.RequestDelayedT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_delay_request_t_system_user_t1");
            });

            modelBuilder.Entity<RequestDiscountT>(entity =>
            {
                entity.HasKey(e => e.RequestDiscountId);

                entity.ToTable("request_discount_t");

                entity.HasIndex(e => e.DiscountTypeId)
                    .HasName("fk_discount_type_t_idx");

                entity.HasIndex(e => e.RequestId)
                    .HasName("fk_request_t_idx");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("fk_request_discount_t_system_user_t_idx");

                entity.Property(e => e.RequestDiscountId)
                    .HasColumnName("request_discount_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creation_time")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.DiscountTypeId)
                    .HasColumnName("discount_type_id")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.DiscountValue)
                    .HasColumnName("discount_value")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.CompanyPercentage)
                   .HasColumnName("company_percentage")
                   .HasColumnType("decimal(10,2)");

                entity.Property(e => e.RequestId)
                    .HasColumnName("request_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.DiscountType)
                    .WithMany(p => p.RequestDiscountT)
                    .HasForeignKey(d => d.DiscountTypeId)
                    .HasConstraintName("fk_request_discount_t_discount_type_t");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestDiscountT)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_request_t");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.RequestDiscountT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_request_discount_t_system_user_t");
            });

            modelBuilder.Entity<RequestServicesT>(entity =>
            {
                entity.HasKey(e => e.RequestServiceId);

                entity.ToTable("request_services_t");

                entity.HasIndex(e => e.RequestId)
                    .HasName("fk_requests_t_has_service_t_requests_t1_idx");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("fk_requests_t_has_service_t_service_t1_idx");

                entity.Property(e => e.RequestServiceId)
                    .HasColumnName("request_service_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTimestamp)
                    .HasColumnName("add_timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.RequestId)
                    .HasColumnName("request_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RequestServiceCost)
                    .HasColumnName("request_service_cost")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.RequestServiceDiscount)
                    .HasColumnName("request_service_discount")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.RequestServiceMaterial)
                    .HasColumnName("request_service_material")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.RequestServicePoint)
                    .HasColumnName("request_service_point")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.RequestServicesQuantity)
                    .HasColumnName("request_services_quantity")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("service_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestServicesT)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("fk_requests_t_has_service_t_requests_t1");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.RequestServicesT)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("fk_requests_t_service_t");
            });

            modelBuilder.Entity<RequestStagesT>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.ToTable("request_stages_t");

                entity.HasIndex(e => e.RequestId)
                    .HasName("fk_request_stages_request_t1_idx");

                entity.Property(e => e.RequestId)
                    .HasColumnName("request_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AcceptTimestamp)
                    .HasColumnName("accept_timestamp")
                    .HasColumnType("datetime");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.FinishTimestamp)
                    .HasColumnName("finish_timestamp")
                    .HasColumnType("datetime");

                entity.Property(e => e.PaymentFlag)
                    .HasColumnName("payment_flag")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ReceiveTimestamp)
                    .HasColumnName("receive_timestamp")
                    .HasColumnType("datetime");

                entity.Property(e => e.SentTimestamp)
                    .HasColumnName("sent_timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.HasOne(d => d.Request)
                    .WithOne(p => p.RequestStagesT)
                    .HasForeignKey<RequestStagesT>(d => d.RequestId)
                    .HasConstraintName("fk_request_stages_request_t1");
            });

            modelBuilder.Entity<RequestStatusT>(entity =>
            {
                entity.HasKey(e => e.RequestStatusId);

                entity.ToTable("request_status_t");

                entity.Property(e => e.RequestStatusId)
                    .HasColumnName("request_status_id")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.RequestStatusDes)
                    .HasColumnName("request_status_des")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.RequestStatusName)
                    .IsRequired()
                    .HasColumnName("request_status_name")
                    .HasColumnType("varchar(15)");
            });

            modelBuilder.Entity<RequestT>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.ToTable("request_t");

                entity.HasIndex(e => e.BranchId)
                    .HasName("fk_request_t_branch_t1_idx");

                entity.HasIndex(e => e.ClientId)
                    .HasName("fk_requests_t_clients_t1_idx");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("fk_requests_t_department_t1_idx");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("fk_request_t_employee_t1_idx");

                entity.HasIndex(e => e.RequestStatus)
                    .HasName("fk_requests_t_request_status_t1_idx");

                entity.HasIndex(e => e.RequestTimestamp)
                    .HasName("timestamp_index");

                entity.HasIndex(e => e.RequestedAddressId)
                    .HasName("fk_requests_t_address_t1_idx");

                entity.HasIndex(e => e.RequestedPhoneId)
                    .HasName("fk_requests_t_clients_phones_t1_idx");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("fk_request_t_system_user_t1_idx");

                entity.Property(e => e.RequestId)
                    .HasColumnName("request_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BranchId)
                    .HasColumnName("branch_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CompanyPercentageAmount)
                    .HasColumnName("company_percentage_amount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CustomerPrice)
                    .HasColumnName("customer_price")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.EmployeePercentageAmount)
                    .HasColumnName("employee_percentage_amount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.MaterialCost)
                    .HasColumnName("material_cost")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NetPrice)
                    .HasColumnName("net_price")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RequestCurrentTimestamp)
                    .HasColumnName("request_current_timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.RequestNote)
                    .HasColumnName("request_note")
                    .HasColumnType("text");

                entity.Property(e => e.RequestPoints)
                    .HasColumnName("request_points")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RequestStatus)
                    .HasColumnName("request_status")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.RequestTimestamp)
                    .HasColumnName("request_timestamp")
                    .HasColumnType("datetime");

                entity.Property(e => e.RequestedAddressId)
                    .HasColumnName("requested_address_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RequestedPhoneId)
                    .HasColumnName("requested_phone_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TotalDiscount)
                    .HasColumnName("total_discount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TotalPrice)
                    .HasColumnName("total_price")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_request_t_branch_t1");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("fk_requests_t_clients_t1");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("fk_requests_t_department_t1");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("fk_request_t_employee_t1");

                entity.HasOne(d => d.RequestStatusNavigation)
                    .WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.RequestStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_requests_t_request_status_t1");

                entity.HasOne(d => d.RequestedAddress)
                    .WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.RequestedAddressId)
                    .HasConstraintName("fk_requests_t_address_t1");

                entity.HasOne(d => d.RequestedPhone)
                    .WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.RequestedPhoneId)
                    .HasConstraintName("fk_requests_t_clients_phones_t1");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_request_t_system_user_t1");
            });

            modelBuilder.Entity<RoleT>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("role_t");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''");

                entity.Property(e => e.RoleDes)
                    .HasColumnName("role_des")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("role_name")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<ServiceRatioDetailsT>(entity =>
            {
                entity.HasKey(e => e.ServiceRatioDetailsId);

                entity.ToTable("service_ratio_details_t");

                entity.HasIndex(e => e.CityId)
                    .HasName("fk_service_ratio_city_t_idx");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("fk_service_ratio_department_t_idx");

                entity.HasIndex(e => e.ServiceRatioId)
                    .HasName("fk_service_ratio_detatils_t_service_ratio_t_idx");

                entity.Property(e => e.ServiceRatioDetailsId)
                    .HasColumnName("service_ratio_details_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CityId)
                    .HasColumnName("city_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ServiceRatioId)
                    .HasColumnName("service_ratio_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.ServiceRatioDetailsT)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("fk_service_ratio_city_t");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.ServiceRatioDetailsT)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("fk_service_ratio_department_t");

                entity.HasOne(d => d.ServiceRatio)
                    .WithMany(p => p.ServiceRatioDetailsT)
                    .HasForeignKey(d => d.ServiceRatioId)
                    .HasConstraintName("fk_service_ratio_detatils_t_service_ratio_t");
            });

            modelBuilder.Entity<ServiceRatioT>(entity =>
            {
                entity.HasKey(e => e.ServiceRatioId);

                entity.ToTable("service_ratio_t");

                entity.Property(e => e.ServiceRatioId)
                    .HasColumnName("service_ratio_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''");

                entity.Property(e => e.Ratio)
                    .HasColumnName("ratio")
                    .HasColumnType("decimal(10,2)");
            });

            modelBuilder.Entity<ServiceT>(entity =>
            {
                entity.HasKey(e => e.ServiceId);

                entity.ToTable("service_t");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("fk_service_t_department_sub1t1_idx");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("service_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CompanyDiscountPercentage)
                    .HasColumnName("company_discount_percentage")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'100'");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiscountServiceCount)
                    .HasColumnName("discount_service_count")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.MaterialCost)
                    .HasColumnName("material_cost")
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.NoDiscount)
                    .HasColumnName("no_discount")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

                entity.Property(e => e.ServiceCost)
                    .HasColumnName("service_cost")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.ServiceDes)
                    .HasColumnName("service_des")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.ServiceDiscount)
                    .HasColumnName("service_discount")
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ServiceDuration).HasColumnName("service_duration");

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasColumnName("service_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.ServicePoints)
                    .HasColumnName("service_points")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.ServiceT)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("fk_service_t_department_sub1t1");
            });

            modelBuilder.Entity<SubscriptionSequenceT>(entity =>
            {
                entity.HasKey(e => e.ClientSubscriptionSequenceId);

                entity.ToTable("subscription_sequence_t");

                entity.HasIndex(e => e.SubscriptionId)
                    .HasName("fk_subscription_t_subscription_sequence_t_idx");

                entity.Property(e => e.ClientSubscriptionSequenceId)
                    .HasColumnName("client_subscription_sequence_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiscountPercentage)
                    .HasColumnName("discount_percentage")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.CompanyDiscountPercentage)
                   .HasColumnName("company_discount_percentage")
                   .HasColumnType("decimal(10,2)");

                entity.Property(e => e.Sequence)
                    .HasColumnName("sequence")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.SubscriptionId)
                    .HasColumnName("subscription_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Subscription)
                    .WithMany(p => p.SubscriptionSequenceT)
                    .HasForeignKey(d => d.SubscriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_subscription_t_subscription_sequence_t");
            });

            modelBuilder.Entity<SubscriptionT>(entity =>
            {
                entity.HasKey(e => e.SubscriptionId);

                entity.ToTable("subscription_t");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("fk_client_subscription_t_department_t_idx");

                entity.Property(e => e.SubscriptionId)
                    .HasColumnName("subscription_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.RequestNumberPerMonth)
                    .HasColumnName("request_number_per_month")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.SubscriptionName)
                    .HasColumnName("subscription_name")
                    .HasColumnType("varchar(45)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.SubscriptionT)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("fk_client_subscription_t_department_t");
            });

            modelBuilder.Entity<SystemUserT>(entity =>
            {
                entity.HasKey(e => e.SystemUserId);

                entity.ToTable("system_user_t");

                entity.HasIndex(e => e.BranchId)
                    .HasName("fk_system_users_t_branch_t1_idx");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("fk_system_users_t_employee_t1_idx");

                entity.HasIndex(e => e.SystemUserUsername)
                    .HasName("system_user_username_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BranchId)
                    .HasColumnName("branch_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EmployeeId)
                    .IsRequired()
                    .HasColumnName("employee_id")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.SystemUserLevel)
                    .IsRequired()
                    .HasColumnName("system_user_level")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.SystemUserPass)
                    .IsRequired()
                    .HasColumnName("system_user_pass")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.SystemUserUsername)
                    .IsRequired()
                    .HasColumnName("system_user_username")
                    .HasColumnType("varchar(45)");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.SystemUserT)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_system_users_t_branch_t1");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.SystemUserT)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_system_users_t_employee_t1");
            });

            modelBuilder.Entity<TimetableT>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.TimetableDate });

                entity.ToTable("timetable_t");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.TimetableDate)
                    .HasColumnName("timetable_date")
                    .HasColumnType("date");

                entity.Property(e => e.Timetable1)
                    .HasColumnName("timetable_1")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Timetable10)
                    .HasColumnName("timetable_10")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Timetable4)
                    .HasColumnName("timetable_4")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Timetable7)
                    .HasColumnName("timetable_7")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Timetable9)
                    .HasColumnName("timetable_9")
                    .HasColumnType("tinyint(4)");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TimetableT)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_timetable_t_employee_t1");
            });

            modelBuilder.Entity<TokenT>(entity =>
            {
                entity.HasKey(e => e.TokenId);

                entity.ToTable("token_t");

                entity.HasIndex(e => e.AccountId)
                    .HasName("fk_token_t_account_t_idx");

                entity.Property(e => e.TokenId)
                    .HasColumnName("token_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creation_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Token)
                    .HasColumnName("token")
                    .HasColumnType("longtext");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TokenT)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("fk_token_t_account_t");
            });

            modelBuilder.Entity<TransactionT>(entity =>
            {
                entity.ToTable("transaction_t");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("fk_transaction_t_system_user_t_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creation_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.IsCanceled)
                    .HasColumnName("is_canceled")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.ReferenceId)
                    .HasColumnName("reference_id")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.ReferenceType)
                    .HasColumnName("reference_type")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.TransactionT)
                    .HasForeignKey(d => d.SystemUserId)
                    .HasConstraintName("fk_transaction_t_system_user_t");
            });

            modelBuilder.Entity<TranslatorT>(entity =>
            {
                entity.HasKey(e => e.TranslatorId);

                entity.ToTable("translator_t");

                entity.Property(e => e.TranslatorId)
                    .HasColumnName("translator_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Key)
                    .HasColumnName("key")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.LangId)
                    .HasColumnName("lang_id")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ReferenceId)
                    .HasColumnName("reference_id")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.ReferenceType)
                    .HasColumnName("reference_type")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<VersionT>(entity =>
            {
                entity.HasKey(e => e.VersionNumber);

                entity.ToTable("version_t");

                entity.Property(e => e.VersionNumber)
                    .HasColumnName("version_number")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<WorkingAreaT>(entity =>
            {
                entity.HasKey(e => e.WorkingAreaId);

                entity.ToTable("working_area_t");

                entity.HasIndex(e => e.BranchId)
                    .HasName("fk_work_areas_branch_t1_idx");

                entity.HasIndex(e => new { e.WorkingAreaGov, e.WorkingAreaCity, e.WorkingAreaRegion })
                    .HasName("unique_area")
                    .IsUnique();

                entity.Property(e => e.WorkingAreaId)
                    .HasColumnName("working_area_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BranchId)
                    .HasColumnName("branch_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.WorkingAreaCity)
                    .IsRequired()
                    .HasColumnName("working_area_city")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.WorkingAreaGov)
                    .IsRequired()
                    .HasColumnName("working_area_gov")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.WorkingAreaRegion)
                    .IsRequired()
                    .HasColumnName("working_area_region")
                    .HasColumnType("varchar(25)");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.WorkingAreaT)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_work_areas_branch_t1");
            });
        }

    }
}
