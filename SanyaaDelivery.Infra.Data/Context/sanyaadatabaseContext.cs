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
        public virtual DbSet<AddressAreaT> AddressAreaT { get; set; }
        public virtual DbSet<AddressCityT> AddressCityT { get; set; }
        public virtual DbSet<AddressGovT> AddressGovT { get; set; }
        public virtual DbSet<AddressT> AddressT { get; set; }
        public virtual DbSet<AppSettingT> AppSettingT { get; set; }
        public virtual DbSet<BillDetailsT> BillDetailsT { get; set; }
        public virtual DbSet<BillNumberT> BillNumberT { get; set; }
        public virtual DbSet<BranchT> BranchT { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<CleaningSubscribersT> CleaningSubscribersT { get; set; }
        public virtual DbSet<ClientPhonesT> ClientPhonesT { get; set; }
        public virtual DbSet<ClientPointT> ClientPointT { get; set; }
        public virtual DbSet<ClientT> ClientT { get; set; }
        public virtual DbSet<DeliveryPriceT> DeliveryPriceT { get; set; }
        public virtual DbSet<DepartmentEmployeeT> DepartmentEmployeeT { get; set; }
        public virtual DbSet<DepartmentSub0T> DepartmentSub0T { get; set; }
        public virtual DbSet<DepartmentSub1T> DepartmentSub1T { get; set; }
        public virtual DbSet<DepartmentT> DepartmentT { get; set; }
        public virtual DbSet<DiscountT> DiscountT { get; set; }
        public virtual DbSet<DiscountTypeT> DiscountTypeT { get; set; }
        public virtual DbSet<EmployeeApproval> EmployeeApproval { get; set; }
        public virtual DbSet<EmployeeLocation> EmployeeLocation { get; set; }
        public virtual DbSet<EmployeeT> EmployeeT { get; set; }
        public virtual DbSet<EmployeeWorkplacesT> EmployeeWorkplacesT { get; set; }
        public virtual DbSet<EmploymentApplicationsT> EmploymentApplicationsT { get; set; }
        public virtual DbSet<FawryChargeRequestT> FawryChargeRequestT { get; set; }
        public virtual DbSet<FawryChargeT> FawryChargeT { get; set; }
        public virtual DbSet<FiredStaffT> FiredStaffT { get; set; }
        public virtual DbSet<FollowUpT> FollowUpT { get; set; }
        public virtual DbSet<GeneralDiscountT> GeneralDiscountT { get; set; }
        public virtual DbSet<IncreaseDiscountT> IncreaseDiscountT { get; set; }
        public virtual DbSet<LandingScreenItemT> LandingScreenItemT { get; set; }
        public virtual DbSet<LoginT> LoginT { get; set; }
        public virtual DbSet<MessagesT> MessagesT { get; set; }
        public virtual DbSet<Notifications> Notifications { get; set; }
        public virtual DbSet<PartinerCartT> PartinerCartT { get; set; }
        public virtual DbSet<PartinerPaymentRequestT> PartinerPaymentRequestT { get; set; }
        public virtual DbSet<PartinerPaymentT> PartinerPaymentT { get; set; }
        public virtual DbSet<PaymentT> PaymentT { get; set; }
        public virtual DbSet<Poll> Poll { get; set; }
        public virtual DbSet<ProductReceiptT> ProductReceiptT { get; set; }
        public virtual DbSet<ProductSoldT> ProductSoldT { get; set; }
        public virtual DbSet<ProductT> ProductT { get; set; }
        public virtual DbSet<Promocode> Promocode { get; set; }
        public virtual DbSet<QuantityHistoryT> QuantityHistoryT { get; set; }
        public virtual DbSet<RegestrationT> RegestrationT { get; set; }
        public virtual DbSet<RejectRequestT> RejectRequestT { get; set; }
        public virtual DbSet<RequestAccountT> RequestAccountT { get; set; }
        public virtual DbSet<RequestCanceledT> RequestCanceledT { get; set; }
        public virtual DbSet<RequestComplaintT> RequestComplaintT { get; set; }
        public virtual DbSet<RequestDelayedT> RequestDelayedT { get; set; }
        public virtual DbSet<RequestDiscountT> RequestDiscountT { get; set; }
        public virtual DbSet<RequestServicesT> RequestServicesT { get; set; }
        public virtual DbSet<RequestStagesT> RequestStagesT { get; set; }
        public virtual DbSet<RequestStatusT> RequestStatusT { get; set; }
        public virtual DbSet<RequestT> RequestT { get; set; }
        public virtual DbSet<RoleT> RoleT { get; set; }
        public virtual DbSet<ServiceCityRatioT> ServiceCityRatioT { get; set; }
        public virtual DbSet<ServiceT> ServiceT { get; set; }
        public virtual DbSet<SettingT> SettingT { get; set; }
        public virtual DbSet<SystemUserT> SystemUserT { get; set; }
        public virtual DbSet<TimetableT> TimetableT { get; set; }
        public virtual DbSet<VersionT> VersionT { get; set; }
        public virtual DbSet<WorkingAreaT> WorkingAreaT { get; set; }
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
                    .IsRequired()
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
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''");

                entity.Property(e => e.IsEmailVerfied)
                    .HasColumnName("is_email_verfied")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.IsMobileVerfied)
                    .HasColumnName("is_mobile_verfied")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.LastOtpCreationTime)
                    .HasColumnName("last_otp_creation_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.MobileOtpCode)
                    .HasColumnName("mobile_otp_code")
                    .HasColumnType("varchar(6)");

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
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''");
            });

            modelBuilder.Entity<AddressAreaT>(entity =>
            {
                entity.HasKey(e => e.AreaId);

                entity.ToTable("address_area_t");

                entity.Property(e => e.AreaId)
                    .HasColumnName("area_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AreaLang).HasColumnName("area_lang");

                entity.Property(e => e.AreaLat).HasColumnName("area_lat");

                entity.Property(e => e.AreaMapLocationText)
                    .HasColumnName("area_map_location_text")
                    .HasColumnType("text");

                entity.Property(e => e.AreaName)
                    .IsRequired()
                    .HasColumnName("area_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.CityId)
                    .HasColumnName("city_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<AddressCityT>(entity =>
            {
                entity.HasKey(e => e.CityId);

                entity.ToTable("address_city_t");

                entity.HasIndex(e => e.GovId)
                    .HasName("fk_address_gov_t_idx");

                entity.Property(e => e.CityId)
                    .HasColumnName("city_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasColumnName("city_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.GovId)
                    .HasColumnName("gov_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Gov)
                    .WithMany(p => p.AddressCityT)
                    .HasForeignKey(d => d.GovId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_address_gov_t");
            });

            modelBuilder.Entity<AddressGovT>(entity =>
            {
                entity.HasKey(e => e.GovId);

                entity.ToTable("address_gov_t");

                entity.Property(e => e.GovId)
                    .HasColumnName("gov_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.GovName)
                    .IsRequired()
                    .HasColumnName("gov_name")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<AddressT>(entity =>
            {
                entity.HasKey(e => e.AddressId);

                entity.ToTable("address_t");

                entity.HasIndex(e => e.ClientId)
                    .HasName("fk_address_t_client_t_idx");

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

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Latitude).HasColumnType("varchar(75)");

                entity.Property(e => e.Location).HasColumnType("text");

                entity.Property(e => e.Longitude).HasColumnType("varchar(75)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.AddressT)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("fk_address_t_client_t");
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

                entity.Property(e => e.SettingDatatype)
                    .HasColumnName("setting_datatype")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.SettingKey)
                    .IsRequired()
                    .HasColumnName("setting_key")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.SettingValue)
                    .IsRequired()
                    .HasColumnName("setting_value")
                    .HasColumnType("varchar(45)");

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

            modelBuilder.Entity<CleaningSubscribersT>(entity =>
            {
                entity.ToTable("cleaning_subscribers_t");

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
                    .WithMany(p => p.CleaningSubscribersT)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_CleaningSubscribers_client_t_ClientId");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.CleaningSubscribersT)
                    .HasForeignKey(d => d.SystemUserId)
                    .HasConstraintName("FK_CleaningSubscribers_system_user_t_SystemUserId");
            });

            modelBuilder.Entity<ClientPhonesT>(entity =>
            {
                entity.HasKey(e => e.ClientPhoneId);

                entity.ToTable("client_phones_t");

                entity.HasIndex(e => e.ClientId)
                    .HasName("fk_client_phones_t_client_t_idx");

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
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'0\\''");

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
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

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

            modelBuilder.Entity<DeliveryPriceT>(entity =>
            {
                entity.HasKey(e => e.DeliveryPriceId);

                entity.ToTable("delivery_price_t");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("fk_system_user_t_delivery_price_t_idx");

                entity.HasIndex(e => e.WorkingAreaId)
                    .HasName("fk_working_area_t_delivery_price_t_idx");

                entity.Property(e => e.DeliveryPriceId)
                    .HasColumnName("delivery_price_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creation_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.DateFrom)
                    .HasColumnName("date_from")
                    .HasColumnType("date");

                entity.Property(e => e.DateTo)
                    .HasColumnName("date_to")
                    .HasColumnType("date");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''");

                entity.Property(e => e.PriceValue)
                    .HasColumnName("price_value")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.WorkingAreaId)
                    .HasColumnName("working_area_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.DeliveryPriceT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_system_user_t_delivery_price_t");

                entity.HasOne(d => d.WorkingArea)
                    .WithMany(p => p.DeliveryPriceT)
                    .HasForeignKey(d => d.WorkingAreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_working_area_t_delivery_price_t");
            });

            modelBuilder.Entity<DepartmentEmployeeT>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.DepartmentName });

                entity.ToTable("department_employee_t");

                entity.HasIndex(e => e.DepartmentName)
                    .HasName("fk_department_employee_t_department_t1_idx");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("fk_department_t_has_employee_t_employee_t1_idx");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.DepartmentName)
                    .HasColumnName("department_name")
                    .HasColumnType("varchar(25)");

                entity.HasOne(d => d.DepartmentNameNavigation)
                    .WithMany(p => p.DepartmentEmployeeT)
                    .HasForeignKey(d => d.DepartmentName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_department_employee_t_department_t1");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.DepartmentEmployeeT)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("fk_department_t_has_employee_t_employee_t1");
            });

            modelBuilder.Entity<DepartmentSub0T>(entity =>
            {
                entity.HasKey(e => new { e.DepartmentSub0, e.DepartmentName });

                entity.ToTable("department_sub0_t");

                entity.HasIndex(e => e.DepartmentName)
                    .HasName("fk_department_sub0_t_department_t1_idx");

                entity.Property(e => e.DepartmentSub0)
                    .HasColumnName("department_sub0")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.DepartmentName)
                    .HasColumnName("department_name")
                    .HasColumnType("varchar(25)");

                entity.HasOne(d => d.DepartmentNameNavigation)
                    .WithMany(p => p.DepartmentSub0T)
                    .HasForeignKey(d => d.DepartmentName)
                    .HasConstraintName("fk_department_sub0_t_department_t1");
            });

            modelBuilder.Entity<DepartmentSub1T>(entity =>
            {
                entity.HasKey(e => e.DepartmentId);

                entity.ToTable("department_sub1_t");

                entity.HasIndex(e => new { e.DepartmentSub0, e.DepartmentName })
                    .HasName("fk_department_sub1_t_department_sub0_t1_idx");

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

                entity.Property(e => e.DepartmentSub1)
                    .IsRequired()
                    .HasColumnName("department_sub1")
                    .HasColumnType("varchar(25)");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.DepartmentSub1T)
                    .HasForeignKey(d => new { d.DepartmentSub0, d.DepartmentName })
                    .HasConstraintName("fk_department_sub1_t_department_sub0_t1");
            });

            modelBuilder.Entity<DepartmentT>(entity =>
            {
                entity.HasKey(e => e.DepartmentName);

                entity.ToTable("department_t");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("id_index");

                entity.Property(e => e.DepartmentName)
                    .HasColumnName("department_name")
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.DepartmentDes)
                    .HasColumnName("department_des")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DepartmentImage)
                    .HasColumnName("department_image")
                    .HasColumnType("varchar(10)");
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
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiscountTypeDes)
                    .HasColumnName("discount_type_des")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.DiscountTypeName)
                    .IsRequired()
                    .HasColumnName("discount_type_name")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
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

                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasColumnName("employee_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.EmployeePercentage)
                    .HasColumnName("employee_percentage")
                    .HasDefaultValueSql("'70'");

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
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<EmployeeWorkplacesT>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.BranchId });

                entity.ToTable("employee_workplaces_t");

                entity.HasIndex(e => e.BranchId)
                    .HasName("fk_branch_t_has_employee_t_branch_t1_idx");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("fk_branch_t_has_employee_t_employee_t1_idx");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.BranchId)
                    .HasColumnName("branch_id")
                    .HasColumnType("int(11)");

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
                    .OnDelete(DeleteBehavior.ClientSetNull)
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

            modelBuilder.Entity<GeneralDiscountT>(entity =>
            {
                entity.HasKey(e => e.DiscountId);

                entity.ToTable("general_discount_t");

                entity.HasIndex(e => e.DiscountTypeId)
                    .HasName("fk_discount_discount_type_idx");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("fk_discount_system_user_idx");

                entity.Property(e => e.DiscountId)
                    .HasColumnName("discount_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creation_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.DiscountCompanyPercantage).HasColumnName("discount_company_percantage");

                entity.Property(e => e.DiscountEmployeePercantage).HasColumnName("discount_employee_percantage");

                entity.Property(e => e.DiscountRefernceId)
                    .HasColumnName("discount_refernce_id")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.DiscountTypeId)
                    .HasColumnName("discount_type_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiscountValidDateFrom)
                    .HasColumnName("discount_valid_date_from")
                    .HasColumnType("datetime");

                entity.Property(e => e.DiscountValidDateTo)
                    .HasColumnName("discount_valid_date_to")
                    .HasColumnType("datetime");

                entity.Property(e => e.DiscountValue).HasColumnName("discount_value");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.DiscountType)
                    .WithMany(p => p.GeneralDiscountT)
                    .HasForeignKey(d => d.DiscountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_discount_discount_type");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.GeneralDiscountT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_discount_system_user");
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

            modelBuilder.Entity<LandingScreenItemT>(entity =>
            {
                entity.HasKey(e => e.LandingScreenItemId);

                entity.ToTable("landing_screen_item_t");

                entity.HasIndex(e => e.ParentSectionId)
                    .HasName("fk_landing_screen_landing_screen_idx");

                entity.Property(e => e.LandingScreenItemId)
                    .HasColumnName("landing_screen_item_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ApiUrl)
                    .HasColumnName("api_url")
                    .HasColumnType("longtext");

                entity.Property(e => e.ItemDescription)
                    .HasColumnName("item_description")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.ItemImagePath)
                    .HasColumnName("item_image_path")
                    .HasColumnType("longtext");

                entity.Property(e => e.ItemName)
                    .HasColumnName("item_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.ItemType)
                    .HasColumnName("item_type")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.ItemUrl)
                    .HasColumnName("item_url")
                    .HasColumnType("longtext");

                entity.Property(e => e.ParentSectionId)
                    .HasColumnName("parent_section_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ReferenceId)
                    .HasColumnName("reference_id")
                    .HasColumnType("varchar(45)");

                entity.HasOne(d => d.ParentSection)
                    .WithMany(p => p.InverseParentSection)
                    .HasForeignKey(d => d.ParentSectionId)
                    .HasConstraintName("fk_landing_screen_landing_screen");
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
                    .IsRequired()
                    .HasColumnName("login_availability")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'فارغ'");

                entity.Property(e => e.LoginMessage)
                    .HasColumnName("login_message")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.LoginPassword)
                    .IsRequired()
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

            modelBuilder.Entity<Promocode>(entity =>
            {
                entity.ToTable("promocode");

                entity.Property(e => e.PromocodeId)
                    .HasColumnName("promocode_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.DateEx)
                    .IsRequired()
                    .HasColumnName("Date_Ex")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.DisAmount)
                    .IsRequired()
                    .HasColumnName("Dis_Amount")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.DisPercent)
                    .IsRequired()
                    .HasColumnName("Dis_Percent")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.MinimumCharge)
                    .IsRequired()
                    .HasColumnName("Minimum_Charge")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.NumMax)
                    .IsRequired()
                    .HasColumnName("Num_Max")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.NumNow)
                    .IsRequired()
                    .HasColumnName("Num_Now")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnType("varchar(25)");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("User_ID")
                    .HasColumnType("varchar(10)");
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
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_reject_request_t_request_t1");
            });

            modelBuilder.Entity<RequestAccountT>(entity =>
            {
                entity.HasKey(e => e.RequestAccountId);

                entity.ToTable("request_account_t");

                entity.HasIndex(e => e.RequestId)
                    .HasName("fk_request_account_request_t_idx");

                entity.Property(e => e.RequestAccountId)
                    .HasColumnName("request_account_id")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.LastModificationDate)
                    .HasColumnName("last_modification_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ModificationSystemUserId)
                    .HasColumnName("modification_system_user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RequestCompanyDiscount).HasColumnName("request_company_discount");

                entity.Property(e => e.RequestCompanyPercentage).HasColumnName("request_company_percentage");

                entity.Property(e => e.RequestDeliveryCost).HasColumnName("request_delivery_cost");

                entity.Property(e => e.RequestEmployeePercentage).HasColumnName("request_employee_percentage");

                entity.Property(e => e.RequestEmpoyeeDiscount).HasColumnName("request_empoyee_discount");

                entity.Property(e => e.RequestId)
                    .HasColumnName("request_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RequestMaterialsCost).HasColumnName("request_materials_cost");

                entity.Property(e => e.RequestNetCost).HasColumnName("request_net_cost");

                entity.Property(e => e.RequestTotalCost).HasColumnName("request_total_cost");

                entity.Property(e => e.RequestTotalDiscount).HasColumnName("request_total_discount");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestAccountT)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("fk_request_account_request_t");
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

                entity.HasIndex(e => e.DiscountId)
                    .HasName("fk_discount_t_idx");

                entity.HasIndex(e => e.RequestId)
                    .HasName("fk_request_t_idx");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("fk_system_user_idx");

                entity.Property(e => e.RequestDiscountId)
                    .HasColumnName("request_discount_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creation_time")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.DiscountId)
                    .HasColumnName("discount_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiscountValue)
                    .HasColumnName("discount_value")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RequestId)
                    .HasColumnName("request_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.RequestDiscountT)
                    .HasForeignKey(d => d.DiscountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_discount_t");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestDiscountT)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_request_t");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.RequestDiscountT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_system_user");
            });

            modelBuilder.Entity<RequestServicesT>(entity =>
            {
                entity.HasKey(e => new { e.RequestId, e.ServiceId });

                entity.ToTable("request_services_t");

                entity.HasIndex(e => e.RequestId)
                    .HasName("fk_requests_t_has_service_t_requests_t1_idx");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("fk_requests_t_has_service_t_service_t1_idx");

                entity.Property(e => e.RequestId)
                    .HasColumnName("request_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ServiceId)
                    .HasColumnName("service_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AddTimestamp)
                    .HasColumnName("add_timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.RequestServicesQuantity)
                    .HasColumnName("request_services_quantity")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.ServiceDiscount).HasColumnName("service_discount");

                entity.Property(e => e.ServicePrice)
                    .HasColumnName("service_price")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestServicesT)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("fk_requests_t_has_service_t_requests_t1");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.RequestServicesT)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("fk_requests_t_has_service_t_service_t1");
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
                    .HasColumnType("int(11)");

                entity.Property(e => e.RequestStatusArabicName)
                    .HasColumnName("request_status_arabic_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.RequestStatusDes)
                    .HasColumnName("request_status_des")
                    .HasColumnType("varchar(75)");

                entity.Property(e => e.RequestStatusName)
                    .IsRequired()
                    .HasColumnName("request_status_name")
                    .HasColumnType("varchar(45)");
            });

            modelBuilder.Entity<RequestT>(entity =>
            {
                entity.HasKey(e => e.RequestId);

                entity.ToTable("request_t");

                entity.HasIndex(e => e.BranchId)
                    .HasName("fk_request_t_branch_t1_idx");

                entity.HasIndex(e => e.ClientId)
                    .HasName("fk_requests_t_clients_t1_idx");

                entity.HasIndex(e => e.EmployeeId)
                    .HasName("fk_request_t_employee_t1_idx");

                entity.HasIndex(e => e.RequestStatus)
                    .HasName("status_index");

                entity.HasIndex(e => e.RequestTimestamp)
                    .HasName("timestamp_index");

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

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasColumnType("varchar(14)");

                entity.Property(e => e.RequestCurrentTimestamp)
                    .HasColumnName("request_current_timestamp")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.RequestNote)
                    .HasColumnName("request_note")
                    .HasColumnType("text");

                entity.Property(e => e.RequestStatus)
                    .HasColumnName("request_status")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.RequestTimestamp)
                    .HasColumnName("request_timestamp")
                    .HasColumnType("datetime");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
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

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("fk_request_t_employee_t1");

                entity.HasOne(d => d.RequestStatusNavigation)
                    .WithMany(p => p.RequestT)
                    .HasForeignKey(d => d.RequestStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_request_request_status");

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
                    .IsRequired()
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

            modelBuilder.Entity<ServiceCityRatioT>(entity =>
            {
                entity.ToTable("service_city_ratio_t");

                entity.HasIndex(e => e.CityId)
                    .HasName("fk_city_t_idx");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("fk_department_t_idx");

                entity.HasIndex(e => e.SystemUserId)
                    .HasName("fk_system_user_t_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CityId)
                    .HasColumnName("city_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creation_time")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.DataTo)
                    .HasColumnName("data_to")
                    .HasColumnType("date");

                entity.Property(e => e.DateForm)
                    .HasColumnName("date_form")
                    .HasColumnType("date");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("is_active")
                    .HasColumnType("bit(1)")
                    .HasDefaultValueSql("'b\\'1\\''");

                entity.Property(e => e.Ratio).HasColumnName("ratio");

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("system_user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.ServiceCityRatioT)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_city_t");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.ServiceCityRatioT)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_system_user_t");
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

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ServiceCost)
                    .HasColumnName("service_cost")
                    .HasColumnType("smallint(6)");

                entity.Property(e => e.ServiceDes)
                    .HasColumnName("service_des")
                    .HasColumnType("varchar(150)");

                entity.Property(e => e.ServiceDuration).HasColumnName("service_duration");

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasColumnName("service_name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.ServicePoint)
                    .HasColumnName("service_point")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.ServiceT)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_service_t_department_sub1t1");
            });

            modelBuilder.Entity<SettingT>(entity =>
            {
                entity.ToTable("setting_t");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FawryAutoUpdateStausFlag)
                    .HasColumnName("fawry_auto_update_staus_flag")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.FawryAutopayFlag)
                    .HasColumnName("fawry_autopay_flag")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.FawryPaySendDate)
                    .HasColumnName("fawry_pay_send_date")
                    .HasColumnType("datetime");
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
