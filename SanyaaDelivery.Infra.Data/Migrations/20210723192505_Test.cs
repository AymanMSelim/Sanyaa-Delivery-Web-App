using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SanyaaDelivery.Infra.Data.Migrations
{
    public partial class Test : Migration
    {

        protected override void Up(MigrationBuilder migrationBuilder) { }
        protected override void Down(MigrationBuilder migrationBuilder) { }
        //protected override void Up(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.CreateTable(
        //        name: "address_t",
        //        columns: table => new
        //        {
        //            address_id = table.Column<int>(nullable: false)
        //                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        //            client_id = table.Column<int>(nullable: false),
        //            address_gov = table.Column<string>(type: "varchar(20)", nullable: true),
        //            address_city = table.Column<string>(type: "varchar(25)", nullable: true),
        //            address_region = table.Column<string>(type: "varchar(25)", nullable: true),
        //            address_street = table.Column<string>(type: "text", nullable: true),
        //            address_block_num = table.Column<short>(nullable: true, defaultValueSql: "'0'"),
        //            address_flat_num = table.Column<short>(nullable: true, defaultValueSql: "'0'"),
        //            address_des = table.Column<string>(type: "text", nullable: true),
        //            Location = table.Column<string>(type: "text", nullable: true),
        //            Latitude = table.Column<string>(type: "varchar(75)", nullable: true),
        //            Longitude = table.Column<string>(type: "varchar(75)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_address_t", x => x.address_id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "branch_t",
        //        columns: table => new
        //        {
        //            branch_id = table.Column<int>(nullable: false)
        //                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        //            branch_name = table.Column<string>(type: "varchar(15)", nullable: false),
        //            branch_phone = table.Column<string>(type: "varchar(11)", nullable: false),
        //            branch_gov = table.Column<string>(type: "varchar(20)", nullable: false),
        //            branch_city = table.Column<string>(type: "varchar(20)", nullable: false),
        //            branch_region = table.Column<string>(type: "varchar(20)", nullable: false),
        //            branch_street = table.Column<string>(type: "varchar(45)", nullable: false),
        //            branch_block_num = table.Column<int>(nullable: true),
        //            branch_flat_num = table.Column<int>(nullable: true),
        //            branch_des = table.Column<string>(type: "varchar(150)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_branch_t", x => x.branch_id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "cart",
        //        columns: table => new
        //        {
        //            id = table.Column<int>(nullable: false)
        //                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        //            Barcode = table.Column<string>(type: "text", nullable: false),
        //            UserId = table.Column<int>(nullable: false),
        //            QTE = table.Column<int>(nullable: false),
        //            Note = table.Column<string>(type: "text", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_cart", x => x.id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "client_phones_t",
        //        columns: table => new
        //        {
        //            client_id = table.Column<int>(nullable: false),
        //            client_phone = table.Column<string>(type: "varchar(11)", nullable: false),
        //            pwd_usr = table.Column<string>(type: "varchar(40)", nullable: true, defaultValueSql: "''"),
        //            code = table.Column<string>(type: "varchar(6)", nullable: true, defaultValueSql: "''"),
        //            active = table.Column<sbyte>(nullable: true, defaultValueSql: "'0'")
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_client_phones_t", x => new { x.client_id, x.client_phone });
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "department_t",
        //        columns: table => new
        //        {
        //            department_name = table.Column<string>(type: "varchar(25)", nullable: false),
        //            department_image = table.Column<string>(type: "varchar(10)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_department_t", x => x.department_name);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "discount_t",
        //        columns: table => new
        //        {
        //            id = table.Column<int>(nullable: false)
        //                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        //            discount2 = table.Column<float>(nullable: true),
        //            discount3 = table.Column<float>(nullable: true),
        //            discount4 = table.Column<float>(nullable: true),
        //            discount_more = table.Column<float>(nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_discount_t", x => x.id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "employee_approval",
        //        columns: table => new
        //        {
        //            employee_id = table.Column<string>(type: "varchar(14)", nullable: false),
        //            approval = table.Column<string>(type: "varchar(11)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_employee_approval", x => x.employee_id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "employee_t",
        //        columns: table => new
        //        {
        //            employee_id = table.Column<string>(type: "varchar(14)", nullable: false),
        //            employee_file_num = table.Column<string>(type: "varchar(10)", nullable: false),
        //            employee_name = table.Column<string>(type: "varchar(45)", nullable: false),
        //            employee_phone = table.Column<string>(type: "varchar(11)", nullable: false),
        //            employee_phone1 = table.Column<string>(type: "varchar(11)", nullable: true),
        //            employee_gov = table.Column<string>(type: "varchar(20)", nullable: true),
        //            employee_city = table.Column<string>(type: "varchar(20)", nullable: true),
        //            employee_region = table.Column<string>(type: "varchar(20)", nullable: true),
        //            employee_street = table.Column<string>(type: "varchar(45)", nullable: true),
        //            employee_block_num = table.Column<int>(nullable: true),
        //            employee_flat_num = table.Column<int>(nullable: true),
        //            employee_des = table.Column<string>(type: "varchar(100)", nullable: true),
        //            employee_relative_name = table.Column<string>(type: "varchar(45)", nullable: false),
        //            employee_relative_phone = table.Column<string>(type: "varchar(11)", nullable: false),
        //            employee_hire_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'CURRENT_TIMESTAMP'")
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_employee_t", x => x.employee_id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "employment_applications_t",
        //        columns: table => new
        //        {
        //            id = table.Column<int>(nullable: false)
        //                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        //            branch_id = table.Column<int>(nullable: false),
        //            department = table.Column<string>(type: "varchar(25)", nullable: false),
        //            national_id = table.Column<string>(type: "varchar(14)", nullable: false),
        //            employee_name = table.Column<string>(type: "varchar(45)", nullable: false),
        //            employee_phone = table.Column<string>(type: "varchar(11)", nullable: false),
        //            employee_block_num = table.Column<int>(nullable: true),
        //            employee_flat_num = table.Column<int>(nullable: true),
        //            employee_des = table.Column<string>(type: "varchar(100)", nullable: true, defaultValueSql: "'null'"),
        //            location_text = table.Column<string>(type: "varchar(150)", nullable: true),
        //            location_latitude = table.Column<double>(nullable: true),
        //            location_langitude = table.Column<double>(nullable: true),
        //            employee_relative_name = table.Column<string>(type: "varchar(45)", nullable: false),
        //            employee_relative_phone = table.Column<string>(type: "varchar(11)", nullable: false),
        //            application_status = table.Column<string>(type: "varchar(10)", nullable: true, defaultValueSql: "'جديد'"),
        //            timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'CURRENT_TIMESTAMP'")
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_employment_applications_t", x => x.id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "notifications",
        //        columns: table => new
        //        {
        //            id = table.Column<int>(nullable: false)
        //                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        //            title = table.Column<string>(type: "varchar(75)", nullable: false),
        //            body = table.Column<string>(type: "text", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_notifications", x => x.id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "partiner_cart_t",
        //        columns: table => new
        //        {
        //            id = table.Column<int>(nullable: false)
        //                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        //            service_id = table.Column<int>(nullable: false),
        //            system_username = table.Column<string>(type: "varchar(45)", nullable: false),
        //            service_count = table.Column<int>(nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_partiner_cart_t", x => x.id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "poll",
        //        columns: table => new
        //        {
        //            request_id = table.Column<int>(nullable: false)
        //                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        //            employee = table.Column<string>(type: "varchar(25)", nullable: false),
        //            time = table.Column<string>(type: "varchar(25)", nullable: false),
        //            employee2 = table.Column<string>(type: "varchar(25)", nullable: false),
        //            price = table.Column<string>(type: "varchar(25)", nullable: false),
        //            place = table.Column<string>(type: "varchar(25)", nullable: false),
        //            knowme = table.Column<string>(type: "varchar(25)", nullable: false),
        //            note = table.Column<string>(type: "varchar(25)", nullable: false),
        //            vote = table.Column<string>(type: "varchar(25)", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_poll", x => x.request_id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "product_receipt_t",
        //        columns: table => new
        //        {
        //            receipt_id = table.Column<int>(nullable: false)
        //                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        //            receipt_timestamp = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "'CURRENT_TIMESTAMP'"),
        //            receipt_employee_buyer = table.Column<string>(type: "varchar(45)", nullable: true),
        //            product_receipt_paid = table.Column<float>(nullable: true),
        //            system_username = table.Column<string>(type: "varchar(45)", nullable: true),
        //            branch_id = table.Column<int>(nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_product_receipt_t", x => x.receipt_id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "promocode",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(nullable: false)
        //                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        //            Code = table.Column<string>(type: "varchar(25)", nullable: false),
        //            Type = table.Column<string>(type: "varchar(25)", nullable: false),
        //            Date_Ex = table.Column<string>(type: "varchar(100)", nullable: false),
        //            Num_Max = table.Column<string>(type: "varchar(10)", nullable: false),
        //            Num_Now = table.Column<string>(type: "varchar(10)", nullable: false),
        //            Minimum_Charge = table.Column<string>(type: "varchar(10)", nullable: false),
        //            Dis_Amount = table.Column<string>(type: "varchar(10)", nullable: false),
        //            Dis_Percent = table.Column<string>(type: "varchar(10)", nullable: false),
        //            User_ID = table.Column<string>(type: "varchar(10)", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_promocode", x => x.Id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "regestration_t",
        //        columns: table => new
        //        {
        //            regestration_name = table.Column<string>(type: "varchar(45)", nullable: false),
        //            regestration_department = table.Column<string>(type: "varchar(25)", nullable: false),
        //            regestration_gov = table.Column<string>(type: "varchar(20)", nullable: false),
        //            regestration_city = table.Column<string>(type: "varchar(25)", nullable: false),
        //            regestration_phone = table.Column<string>(type: "varchar(11)", nullable: false),
        //            regestration_age = table.Column<sbyte>(nullable: false),
        //            regestration_experience = table.Column<sbyte>(nullable: false),
        //            regestration_password = table.Column<string>(type: "varchar(45)", nullable: false),
        //            regestration_transport = table.Column<string>(type: "varchar(45)", nullable: true),
        //            regestration_timestamep = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'CURRENT_TIMESTAMP'"),
        //            regestration_view = table.Column<string>(type: "varchar(6)", nullable: true, defaultValueSql: "'لا'")
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_regestration_t", x => new { x.regestration_name, x.regestration_phone });
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "setting_t",
        //        columns: table => new
        //        {
        //            id = table.Column<int>(nullable: false)
        //                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        //            fawry_pay_send_date = table.Column<DateTime>(type: "datetime", nullable: true),
        //            fawry_autopay_flag = table.Column<bool>(type: "bit(1)", nullable: true),
        //            fawry_auto_update_staus_flag = table.Column<bool>(type: "bit(1)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_setting_t", x => x.id);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "version_t",
        //        columns: table => new
        //        {
        //            version_number = table.Column<int>(nullable: false)
        //                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_version_t", x => x.version_number);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "product_t",
        //        columns: table => new
        //        {
        //            product_id = table.Column<int>(nullable: false)
        //                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        //            product_name = table.Column<string>(type: "varchar(45)", nullable: false),
        //            product_price_buy = table.Column<float>(nullable: false),
        //            product_price_sell = table.Column<float>(nullable: false),
        //            product_customer_price = table.Column<float>(nullable: false),
        //            product_quantity = table.Column<short>(nullable: false),
        //            product_department = table.Column<string>(type: "varchar(25)", nullable: true),
        //            product_des = table.Column<string>(type: "varchar(45)", nullable: true),
        //            branch_id = table.Column<int>(nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_product_t", x => x.product_id);
        //            table.ForeignKey(
        //                name: "fk_products_t_branch_t1",
        //                column: x => x.branch_id,
        //                principalTable: "branch_t",
        //                principalColumn: "branch_id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "working_area_t",
        //        columns: table => new
        //        {
        //            working_area_gov = table.Column<string>(type: "varchar(20)", nullable: false),
        //            working_area_city = table.Column<string>(type: "varchar(25)", nullable: false),
        //            working_area_region = table.Column<string>(type: "varchar(25)", nullable: false),
        //            branch_id = table.Column<int>(nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_working_area_t", x => new { x.working_area_gov, x.working_area_city, x.working_area_region });
        //            table.ForeignKey(
        //                name: "fk_work_areas_branch_t1",
        //                column: x => x.branch_id,
        //                principalTable: "branch_t",
        //                principalColumn: "branch_id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "department_sub0_t",
        //        columns: table => new
        //        {
        //            department_name = table.Column<string>(type: "varchar(25)", nullable: false),
        //            department_sub0 = table.Column<string>(type: "varchar(25)", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_department_sub0_t", x => new { x.department_sub0, x.department_name });
        //            table.ForeignKey(
        //                name: "fk_department_sub0_t_department_t1",
        //                column: x => x.department_name,
        //                principalTable: "department_t",
        //                principalColumn: "department_name",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "department_employee_t",
        //        columns: table => new
        //        {
        //            employee_id = table.Column<string>(type: "varchar(14)", nullable: false),
        //            department_name = table.Column<string>(type: "varchar(25)", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_department_employee_t", x => new { x.employee_id, x.department_name });
        //            table.ForeignKey(
        //                name: "fk_department_employee_t_department_t1",
        //                column: x => x.department_name,
        //                principalTable: "department_t",
        //                principalColumn: "department_name",
        //                onDelete: ReferentialAction.Restrict);
        //            table.ForeignKey(
        //                name: "fk_department_t_has_employee_t_employee_t1",
        //                column: x => x.employee_id,
        //                principalTable: "employee_t",
        //                principalColumn: "employee_id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "employee_location",
        //        columns: table => new
        //        {
        //            employee_id = table.Column<string>(type: "varchar(14)", nullable: false),
        //            Location = table.Column<string>(type: "varchar(75)", nullable: true),
        //            Latitude = table.Column<string>(type: "varchar(75)", nullable: true),
        //            Longitude = table.Column<string>(type: "varchar(75)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_employee_location", x => x.employee_id);
        //            table.ForeignKey(
        //                name: "fk_employee_location",
        //                column: x => x.employee_id,
        //                principalTable: "employee_t",
        //                principalColumn: "employee_id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "employee_workplaces_t",
        //        columns: table => new
        //        {
        //            employee_id = table.Column<string>(type: "varchar(14)", nullable: false),
        //            branch_id = table.Column<int>(nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_employee_workplaces_t", x => new { x.employee_id, x.branch_id });
        //            table.ForeignKey(
        //                name: "fk_branch_t_has_employee_t_branch_t1",
        //                column: x => x.branch_id,
        //                principalTable: "branch_t",
        //                principalColumn: "branch_id",
        //                onDelete: ReferentialAction.Restrict);
        //            table.ForeignKey(
        //                name: "fk_branch_t_has_employee_t_employee_t1",
        //                column: x => x.employee_id,
        //                principalTable: "employee_t",
        //                principalColumn: "employee_id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "fawry_charge_t",
        //        columns: table => new
        //        {
        //            system_id = table.Column<int>(nullable: false)
        //                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        //            fawry_ref_number = table.Column<int>(nullable: true),
        //            charge_status = table.Column<string>(type: "varchar(40)", nullable: true, defaultValueSql: "'NEW'"),
        //            charge_amount = table.Column<double>(nullable: true),
        //            charge_expire_date = table.Column<DateTime>(type: "datetime", nullable: true),
        //            employee_id = table.Column<string>(type: "varchar(14)", nullable: true),
        //            record_timestamp = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "'CURRENT_TIMESTAMP'"),
        //            is_confirmed = table.Column<bool>(type: "bit(1)", nullable: true, defaultValueSql: "'b\\'0\\''")
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_fawry_charge_t", x => x.system_id);
        //            table.ForeignKey(
        //                name: "fk_fawry_charge_t_employee_t",
        //                column: x => x.employee_id,
        //                principalTable: "employee_t",
        //                principalColumn: "employee_id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "fired_staff_t",
        //        columns: table => new
        //        {
        //            employee_id = table.Column<string>(type: "varchar(14)", nullable: false),
        //            fired_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'CURRENT_TIMESTAMP'"),
        //            fired_reasons = table.Column<string>(type: "varchar(100)", nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_fired_staff_t", x => x.employee_id);
        //            table.ForeignKey(
        //                name: "fk_fired_staff_t_employee_t1",
        //                column: x => x.employee_id,
        //                principalTable: "employee_t",
        //                principalColumn: "employee_id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "login_t",
        //        columns: table => new
        //        {
        //            employee_id = table.Column<string>(type: "varchar(14)", nullable: false),
        //            last_active_timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'CURRENT_TIMESTAMP'"),
        //            login_password = table.Column<string>(type: "varchar(45)", nullable: false),
        //            login_account_state = table.Column<sbyte>(nullable: false, defaultValueSql: "'1'"),
        //            login_availability = table.Column<string>(type: "varchar(50)", nullable: false, defaultValueSql: "'فارغ'"),
        //            login_message = table.Column<string>(type: "varchar(150)", nullable: true),
        //            login_account_deactive_message = table.Column<string>(type: "varchar(150)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_login_t", x => x.employee_id);
        //            table.ForeignKey(
        //                name: "fk_login_t_employee_t1",
        //                column: x => x.employee_id,
        //                principalTable: "employee_t",
        //                principalColumn: "employee_id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "messages_t",
        //        columns: table => new
        //        {
        //            employee_id = table.Column<string>(type: "varchar(14)", nullable: false),
        //            message_timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'CURRENT_TIMESTAMP'"),
        //            title = table.Column<string>(type: "varchar(30)", nullable: true),
        //            body = table.Column<string>(type: "text", nullable: true),
        //            is_read = table.Column<sbyte>(nullable: true, defaultValueSql: "'0'")
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_messages_t", x => new { x.employee_id, x.message_timestamp });
        //            table.ForeignKey(
        //                name: "fk_employee_id",
        //                column: x => x.employee_id,
        //                principalTable: "employee_t",
        //                principalColumn: "employee_id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "reject_request_t",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(nullable: false)
        //                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        //            employee_id = table.Column<string>(type: "varchar(14)", nullable: false),
        //            request_id = table.Column<int>(nullable: true),
        //            reject_request_timestamp = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "'CURRENT_TIMESTAMP'")
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_reject_request_t", x => x.Id);
        //            table.ForeignKey(
        //                name: "fk_reject_request_t_employee_t1",
        //                column: x => x.employee_id,
        //                principalTable: "employee_t",
        //                principalColumn: "employee_id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "system_user_t",
        //        columns: table => new
        //        {
        //            system_user_id = table.Column<int>(nullable: false)
        //                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        //            system_user_username = table.Column<string>(type: "varchar(45)", nullable: false),
        //            system_user_pass = table.Column<string>(type: "varchar(45)", nullable: false),
        //            system_user_level = table.Column<string>(type: "varchar(15)", nullable: false),
        //            employee_id = table.Column<string>(type: "varchar(14)", nullable: false),
        //            branch_id = table.Column<int>(nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_system_user_t", x => x.system_user_id);
        //            table.ForeignKey(
        //                name: "fk_system_users_t_branch_t1",
        //                column: x => x.branch_id,
        //                principalTable: "branch_t",
        //                principalColumn: "branch_id",
        //                onDelete: ReferentialAction.Restrict);
        //            table.ForeignKey(
        //                name: "fk_system_users_t_employee_t1",
        //                column: x => x.employee_id,
        //                principalTable: "employee_t",
        //                principalColumn: "employee_id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "timetable_t",
        //        columns: table => new
        //        {
        //            employee_id = table.Column<string>(type: "varchar(14)", nullable: false),
        //            timetable_date = table.Column<DateTime>(type: "date", nullable: false),
        //            timetable_10 = table.Column<sbyte>(nullable: false),
        //            timetable_1 = table.Column<sbyte>(nullable: false),
        //            timetable_4 = table.Column<sbyte>(nullable: false),
        //            timetable_7 = table.Column<sbyte>(nullable: false),
        //            timetable_9 = table.Column<sbyte>(nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_timetable_t", x => new { x.employee_id, x.timetable_date });
        //            table.ForeignKey(
        //                name: "fk_timetable_t_employee_t1",
        //                column: x => x.employee_id,
        //                principalTable: "employee_t",
        //                principalColumn: "employee_id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "product_sold_t",
        //        columns: table => new
        //        {
        //            receipt_id = table.Column<int>(nullable: false),
        //            product_id = table.Column<int>(nullable: false),
        //            product_sold_quantity = table.Column<short>(nullable: true),
        //            product_sold_price = table.Column<float>(nullable: true),
        //            product_sold_note = table.Column<string>(type: "varchar(5)", nullable: false, defaultValueSql: "''")
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_product_sold_t", x => new { x.receipt_id, x.product_id, x.product_sold_note });
        //            table.ForeignKey(
        //                name: "fk_product_receipt_t_has_product_t_product_t1",
        //                column: x => x.product_id,
        //                principalTable: "product_t",
        //                principalColumn: "product_id",
        //                onDelete: ReferentialAction.Restrict);
        //            table.ForeignKey(
        //                name: "fk_product_receipt_t_has_product_t_product_receipt_t1",
        //                column: x => x.receipt_id,
        //                principalTable: "product_receipt_t",
        //                principalColumn: "receipt_id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "quantity_history_t",
        //        columns: table => new
        //        {
        //            product_id = table.Column<int>(nullable: false),
        //            quantity_history = table.Column<short>(nullable: true),
        //            quantity_timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'CURRENT_TIMESTAMP'"),
        //            system_username = table.Column<string>(type: "varchar(45)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_quantity_history_t", x => new { x.quantity_timestamp, x.product_id });
        //            table.ForeignKey(
        //                name: "fk_quantity_history_t_product_t1",
        //                column: x => x.product_id,
        //                principalTable: "product_t",
        //                principalColumn: "product_id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "department_sub1_t",
        //        columns: table => new
        //        {
        //            department_id = table.Column<int>(nullable: false)
        //                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        //            department_name = table.Column<string>(type: "varchar(25)", nullable: false),
        //            department_sub0 = table.Column<string>(type: "varchar(25)", nullable: false),
        //            department_sub1 = table.Column<string>(type: "varchar(25)", nullable: false),
        //            department_des = table.Column<string>(type: "varchar(45)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_department_sub1_t", x => x.department_id);
        //            table.ForeignKey(
        //                name: "fk_department_sub1_t_department_sub0_t1",
        //                columns: x => new { x.department_sub0, x.department_name },
        //                principalTable: "department_sub0_t",
        //                principalColumns: new[] { "department_sub0", "department_name" },
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "client_t",
        //        columns: table => new
        //        {
        //            client_id = table.Column<int>(nullable: false)
        //                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        //            client_name = table.Column<string>(type: "varchar(45)", nullable: true),
        //            current_address = table.Column<int>(nullable: true),
        //            current_phone = table.Column<string>(type: "varchar(11)", nullable: true),
        //            client_email = table.Column<string>(type: "varchar(45)", nullable: true),
        //            client_reg_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "'CURRENT_TIMESTAMP'"),
        //            client_notes = table.Column<string>(type: "varchar(45)", nullable: true),
        //            client_know_us = table.Column<string>(type: "varchar(45)", nullable: true),
        //            branch_id = table.Column<int>(nullable: true),
        //            system_user_id = table.Column<int>(nullable: true, defaultValueSql: "'500'")
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_client_t", x => x.client_id);
        //            table.ForeignKey(
        //                name: "fk_client_t_branch_t1",
        //                column: x => x.branch_id,
        //                principalTable: "branch_t",
        //                principalColumn: "branch_id",
        //                onDelete: ReferentialAction.Restrict);
        //            table.ForeignKey(
        //                name: "fk_client_systemuser",
        //                column: x => x.system_user_id,
        //                principalTable: "system_user_t",
        //                principalColumn: "system_user_id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "increase_discount_t",
        //        columns: table => new
        //        {
        //            employee_id = table.Column<string>(type: "varchar(14)", nullable: false),
        //            timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'CURRENT_TIMESTAMP'"),
        //            increase_discount_type = table.Column<sbyte>(nullable: false),
        //            increase_discount_value = table.Column<short>(nullable: false),
        //            increase_discount_reason = table.Column<string>(type: "varchar(45)", nullable: false),
        //            system_user_id = table.Column<int>(nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_increase_discount_t", x => new { x.employee_id, x.timestamp, x.increase_discount_reason });
        //            table.ForeignKey(
        //                name: "fk_increase_discount_t_employee_t1",
        //                column: x => x.employee_id,
        //                principalTable: "employee_t",
        //                principalColumn: "employee_id",
        //                onDelete: ReferentialAction.Cascade);
        //            table.ForeignKey(
        //                name: "fk_increase_discount_t_system_user_t1",
        //                column: x => x.system_user_id,
        //                principalTable: "system_user_t",
        //                principalColumn: "system_user_id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "partiner_payment_t",
        //        columns: table => new
        //        {
        //            id = table.Column<int>(nullable: false)
        //                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        //            record_timestamp = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "'CURRENT_TIMESTAMP'"),
        //            system_user_id = table.Column<int>(nullable: false),
        //            amount = table.Column<double>(nullable: true),
        //            date_from = table.Column<DateTime>(type: "datetime", nullable: true),
        //            date_to = table.Column<DateTime>(type: "datetime", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_partiner_payment_t", x => x.id);
        //            table.ForeignKey(
        //                name: "partiner_systemuser_fk",
        //                column: x => x.system_user_id,
        //                principalTable: "system_user_t",
        //                principalColumn: "system_user_id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "service_t",
        //        columns: table => new
        //        {
        //            service_id = table.Column<int>(nullable: false)
        //                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        //            service_name = table.Column<string>(type: "varchar(45)", nullable: false),
        //            department_id = table.Column<int>(nullable: true),
        //            service_cost = table.Column<short>(nullable: false),
        //            service_duration = table.Column<float>(nullable: false),
        //            service_des = table.Column<string>(type: "varchar(150)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_service_t", x => x.service_id);
        //            table.ForeignKey(
        //                name: "fk_service_t_department_sub1t1",
        //                column: x => x.department_id,
        //                principalTable: "department_sub1_t",
        //                principalColumn: "department_id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "request_t",
        //        columns: table => new
        //        {
        //            request_id = table.Column<int>(nullable: false)
        //                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
        //            request_current_timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'CURRENT_TIMESTAMP'"),
        //            request_timestamp = table.Column<DateTime>(type: "datetime", nullable: true),
        //            request_note = table.Column<string>(type: "text", nullable: true),
        //            request_status = table.Column<sbyte>(nullable: false, defaultValueSql: "'1'"),
        //            system_user_id = table.Column<int>(nullable: false),
        //            client_id = table.Column<int>(nullable: false),
        //            employee_id = table.Column<string>(type: "varchar(14)", nullable: true),
        //            branch_id = table.Column<int>(nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_request_t", x => x.request_id);
        //            table.ForeignKey(
        //                name: "fk_request_t_branch_t1",
        //                column: x => x.branch_id,
        //                principalTable: "branch_t",
        //                principalColumn: "branch_id",
        //                onDelete: ReferentialAction.Restrict);
        //            table.ForeignKey(
        //                name: "fk_requests_t_clients_t1",
        //                column: x => x.client_id,
        //                principalTable: "client_t",
        //                principalColumn: "client_id",
        //                onDelete: ReferentialAction.Cascade);
        //            table.ForeignKey(
        //                name: "fk_request_t_employee_t1",
        //                column: x => x.employee_id,
        //                principalTable: "employee_t",
        //                principalColumn: "employee_id",
        //                onDelete: ReferentialAction.Restrict);
        //            table.ForeignKey(
        //                name: "fk_request_t_system_user_t1",
        //                column: x => x.system_user_id,
        //                principalTable: "system_user_t",
        //                principalColumn: "system_user_id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "bill_number_t",
        //        columns: table => new
        //        {
        //            bill_number = table.Column<string>(type: "varchar(45)", nullable: false),
        //            request_id = table.Column<int>(nullable: false),
        //            bill_timestamp = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "'CURRENT_TIMESTAMP'"),
        //            system_user_id = table.Column<int>(nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_bill_number_t", x => x.bill_number);
        //            table.ForeignKey(
        //                name: "fk_bill_number_t_request_t1",
        //                column: x => x.request_id,
        //                principalTable: "request_t",
        //                principalColumn: "request_id",
        //                onDelete: ReferentialAction.Cascade);
        //            table.ForeignKey(
        //                name: "fk_bill_number_t_system_user_t1",
        //                column: x => x.system_user_id,
        //                principalTable: "system_user_t",
        //                principalColumn: "system_user_id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "fawry_charge_request_t",
        //        columns: table => new
        //        {
        //            charge_id = table.Column<int>(nullable: false),
        //            request_id = table.Column<int>(nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_fawry_charge_request_t", x => new { x.charge_id, x.request_id });
        //            table.ForeignKey(
        //                name: "fk_fawry_charge",
        //                column: x => x.charge_id,
        //                principalTable: "fawry_charge_t",
        //                principalColumn: "system_id",
        //                onDelete: ReferentialAction.Cascade);
        //            table.ForeignKey(
        //                name: "fk_request",
        //                column: x => x.request_id,
        //                principalTable: "request_t",
        //                principalColumn: "request_id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "follow_up_t",
        //        columns: table => new
        //        {
        //            request_id = table.Column<int>(nullable: false),
        //            timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'CURRENT_TIMESTAMP'"),
        //            paid = table.Column<float>(nullable: true),
        //            prices = table.Column<string>(type: "varchar(15)", nullable: true),
        //            time = table.Column<sbyte>(nullable: true),
        //            tps = table.Column<sbyte>(nullable: true),
        //            reason = table.Column<string>(type: "varchar(15)", nullable: true),
        //            cleaness = table.Column<sbyte>(nullable: true),
        //            rate = table.Column<sbyte>(nullable: true),
        //            product = table.Column<sbyte>(nullable: true),
        //            product_cost = table.Column<float>(nullable: true),
        //            review = table.Column<string>(type: "text", nullable: false),
        //            behavior = table.Column<string>(type: "varchar(15)", nullable: true),
        //            system_user_id = table.Column<int>(nullable: true, defaultValueSql: "'1'")
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_follow_up_t", x => new { x.request_id, x.timestamp });
        //            table.ForeignKey(
        //                name: "request_fk",
        //                column: x => x.request_id,
        //                principalTable: "request_t",
        //                principalColumn: "request_id",
        //                onDelete: ReferentialAction.Cascade);
        //            table.ForeignKey(
        //                name: "system_user_fk",
        //                column: x => x.system_user_id,
        //                principalTable: "system_user_t",
        //                principalColumn: "system_user_id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "partiner_payment_request_t",
        //        columns: table => new
        //        {
        //            payment_id = table.Column<int>(nullable: false),
        //            request_id = table.Column<int>(nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_partiner_payment_request_t", x => new { x.payment_id, x.request_id });
        //            table.ForeignKey(
        //                name: "partiner_payment_fk",
        //                column: x => x.payment_id,
        //                principalTable: "partiner_payment_t",
        //                principalColumn: "id",
        //                onDelete: ReferentialAction.Cascade);
        //            table.ForeignKey(
        //                name: "partiner_request_fk",
        //                column: x => x.request_id,
        //                principalTable: "request_t",
        //                principalColumn: "request_id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "payment_t",
        //        columns: table => new
        //        {
        //            request_id = table.Column<int>(nullable: false),
        //            payment_timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'CURRENT_TIMESTAMP'"),
        //            payment = table.Column<double>(nullable: false),
        //            system_user_id = table.Column<int>(nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_payment_t", x => x.request_id);
        //            table.ForeignKey(
        //                name: "fk_payment_t_request_t1",
        //                column: x => x.request_id,
        //                principalTable: "request_t",
        //                principalColumn: "request_id",
        //                onDelete: ReferentialAction.Cascade);
        //            table.ForeignKey(
        //                name: "fk_payment_t_system_user_t1",
        //                column: x => x.system_user_id,
        //                principalTable: "system_user_t",
        //                principalColumn: "system_user_id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "request_canceled_t",
        //        columns: table => new
        //        {
        //            request_id = table.Column<int>(nullable: false),
        //            cancel_request_timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'CURRENT_TIMESTAMP'"),
        //            cancel_request_reason = table.Column<string>(type: "text", nullable: false),
        //            system_user_id = table.Column<int>(nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_request_canceled_t", x => new { x.request_id, x.cancel_request_timestamp });
        //            table.ForeignKey(
        //                name: "fk_cancel_requests_t_requests_t1",
        //                column: x => x.request_id,
        //                principalTable: "request_t",
        //                principalColumn: "request_id",
        //                onDelete: ReferentialAction.Cascade);
        //            table.ForeignKey(
        //                name: "fk_cancel_request_t_system_user_t1",
        //                column: x => x.system_user_id,
        //                principalTable: "system_user_t",
        //                principalColumn: "system_user_id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "request_complaint_t",
        //        columns: table => new
        //        {
        //            request_id = table.Column<int>(nullable: false),
        //            complaint_timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'CURRENT_TIMESTAMP'"),
        //            complaint_des = table.Column<string>(type: "varchar(150)", nullable: true),
        //            new_request_id = table.Column<int>(nullable: true),
        //            complaint_is_solved = table.Column<string>(type: "varchar(3)", nullable: false, defaultValueSql: "'لا'"),
        //            system_user_id = table.Column<int>(nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_request_complaint_t", x => new { x.request_id, x.complaint_timestamp });
        //            table.ForeignKey(
        //                name: "fk_request_complaint_t_request_t1",
        //                column: x => x.request_id,
        //                principalTable: "request_t",
        //                principalColumn: "request_id",
        //                onDelete: ReferentialAction.Cascade);
        //            table.ForeignKey(
        //                name: "fk_request_complaint_t_system_users_t1",
        //                column: x => x.system_user_id,
        //                principalTable: "system_user_t",
        //                principalColumn: "system_user_id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "request_delayed_t",
        //        columns: table => new
        //        {
        //            request_id = table.Column<int>(nullable: false),
        //            delay_request_timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "'CURRENT_TIMESTAMP'"),
        //            delay_request_reason = table.Column<string>(type: "text", nullable: false),
        //            delay_request_new_timestamp = table.Column<DateTime>(type: "datetime", nullable: false),
        //            system_user_id = table.Column<int>(nullable: false)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_request_delayed_t", x => new { x.request_id, x.delay_request_timestamp });
        //            table.ForeignKey(
        //                name: "fk_delay_requests_t_requests_t1",
        //                column: x => x.request_id,
        //                principalTable: "request_t",
        //                principalColumn: "request_id",
        //                onDelete: ReferentialAction.Cascade);
        //            table.ForeignKey(
        //                name: "fk_delay_request_t_system_user_t1",
        //                column: x => x.system_user_id,
        //                principalTable: "system_user_t",
        //                principalColumn: "system_user_id",
        //                onDelete: ReferentialAction.Restrict);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "request_services_t",
        //        columns: table => new
        //        {
        //            request_id = table.Column<int>(nullable: false),
        //            service_id = table.Column<int>(nullable: false),
        //            request_services_quantity = table.Column<sbyte>(nullable: false, defaultValueSql: "'1'"),
        //            add_timestamp = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "'CURRENT_TIMESTAMP'")
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_request_services_t", x => new { x.request_id, x.service_id });
        //            table.ForeignKey(
        //                name: "fk_requests_t_has_service_t_requests_t1",
        //                column: x => x.request_id,
        //                principalTable: "request_t",
        //                principalColumn: "request_id",
        //                onDelete: ReferentialAction.Cascade);
        //            table.ForeignKey(
        //                name: "fk_requests_t_has_service_t_service_t1",
        //                column: x => x.service_id,
        //                principalTable: "service_t",
        //                principalColumn: "service_id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "request_stages_t",
        //        columns: table => new
        //        {
        //            request_id = table.Column<int>(nullable: false),
        //            sent_timestamp = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "'CURRENT_TIMESTAMP'"),
        //            receive_timestamp = table.Column<DateTime>(type: "datetime", nullable: true),
        //            accept_timestamp = table.Column<DateTime>(type: "datetime", nullable: true),
        //            finish_timestamp = table.Column<DateTime>(type: "datetime", nullable: true),
        //            cost = table.Column<float>(nullable: true),
        //            payment_flag = table.Column<sbyte>(nullable: true, defaultValueSql: "'0'")
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_request_stages_t", x => x.request_id);
        //            table.ForeignKey(
        //                name: "fk_request_stages_request_t1",
        //                column: x => x.request_id,
        //                principalTable: "request_t",
        //                principalColumn: "request_id",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateTable(
        //        name: "bill_details_t",
        //        columns: table => new
        //        {
        //            bill_number = table.Column<string>(type: "varchar(45)", nullable: false),
        //            bill_type = table.Column<sbyte>(nullable: false),
        //            bill_cost = table.Column<float>(nullable: false),
        //            bill_io = table.Column<string>(type: "varchar(10)", nullable: true),
        //            bill_note = table.Column<string>(type: "varchar(20)", nullable: true)
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_bill_details_t", x => new { x.bill_number, x.bill_type, x.bill_cost });
        //            table.ForeignKey(
        //                name: "fk_bill_details_t_bill_number_t1",
        //                column: x => x.bill_number,
        //                principalTable: "bill_number_t",
        //                principalColumn: "bill_number",
        //                onDelete: ReferentialAction.Cascade);
        //        });

        //    migrationBuilder.CreateIndex(
        //        name: "fk_bill_number_t_request_t1_idx",
        //        table: "bill_number_t",
        //        column: "request_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_bill_number_t_system_user_t1_idx",
        //        table: "bill_number_t",
        //        column: "system_user_id");

        //    migrationBuilder.CreateIndex(
        //        name: "branch_name_UNIQUE",
        //        table: "branch_t",
        //        column: "branch_name",
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "branch_phone_UNIQUE",
        //        table: "branch_t",
        //        column: "branch_phone",
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "client_phone_UNIQUE",
        //        table: "client_phones_t",
        //        column: "client_phone",
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "fk_client_t_branch_t1_idx",
        //        table: "client_t",
        //        column: "branch_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_client_systemuser_idx",
        //        table: "client_t",
        //        column: "system_user_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_department_employee_t_department_t1_idx",
        //        table: "department_employee_t",
        //        column: "department_name");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_department_t_has_employee_t_employee_t1_idx",
        //        table: "department_employee_t",
        //        column: "employee_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_department_sub0_t_department_t1_idx",
        //        table: "department_sub0_t",
        //        column: "department_name");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_department_sub1_t_department_sub0_t1_idx",
        //        table: "department_sub1_t",
        //        columns: new[] { "department_sub0", "department_name" });

        //    migrationBuilder.CreateIndex(
        //        name: "dept",
        //        table: "department_sub1_t",
        //        columns: new[] { "department_name", "department_sub0", "department_sub1" },
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "employee_file_num_UNIQUE",
        //        table: "employee_t",
        //        column: "employee_file_num",
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "employee_national_id_UNIQUE",
        //        table: "employee_t",
        //        column: "employee_id",
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "employee_name_UNIQUE",
        //        table: "employee_t",
        //        column: "employee_name",
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "employee_phone1_UNIQUE",
        //        table: "employee_t",
        //        column: "employee_phone",
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "employee_relative_num_UNIQUE",
        //        table: "employee_t",
        //        column: "employee_relative_phone",
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "fk_branch_t_has_employee_t_branch_t1_idx",
        //        table: "employee_workplaces_t",
        //        column: "branch_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_branch_t_has_employee_t_employee_t1_idx",
        //        table: "employee_workplaces_t",
        //        column: "employee_id");

        //    migrationBuilder.CreateIndex(
        //        name: "employee_phone1_UNIQUE",
        //        table: "employment_applications_t",
        //        column: "employee_phone",
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "employee_relative_num_UNIQUE",
        //        table: "employment_applications_t",
        //        column: "employee_relative_phone",
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "employee_national_id_UNIQUE",
        //        table: "employment_applications_t",
        //        column: "national_id",
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "fk_request_idx",
        //        table: "fawry_charge_request_t",
        //        column: "request_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_fawry_charge_t_employee_t_idx",
        //        table: "fawry_charge_t",
        //        column: "employee_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_fawry_charge_t_idx",
        //        table: "fawry_charge_t",
        //        column: "fawry_ref_number");

        //    migrationBuilder.CreateIndex(
        //        name: "system_user_fk_idx",
        //        table: "follow_up_t",
        //        column: "system_user_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_increase_discount_t_system_user_t1_idx",
        //        table: "increase_discount_t",
        //        column: "system_user_id");

        //    migrationBuilder.CreateIndex(
        //        name: "partiner_request_fk_idx",
        //        table: "partiner_payment_request_t",
        //        column: "request_id");

        //    migrationBuilder.CreateIndex(
        //        name: "partiner_systemuser_fk_idx",
        //        table: "partiner_payment_t",
        //        column: "system_user_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_payment_t_request_t1_idx",
        //        table: "payment_t",
        //        column: "request_id",
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "fk_payment_t_system_user_t1_idx",
        //        table: "payment_t",
        //        column: "system_user_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_product_receipt_t_has_product_t_product_t1_idx",
        //        table: "product_sold_t",
        //        column: "product_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_product_receipt_t_has_product_t_product_receipt_t1_idx",
        //        table: "product_sold_t",
        //        column: "receipt_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_products_t_branch_t1_idx",
        //        table: "product_t",
        //        column: "branch_id");

        //    migrationBuilder.CreateIndex(
        //        name: "product_name_UNIQUE",
        //        table: "product_t",
        //        column: "product_name",
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "fk_quantity_history_t_product_t1_idx",
        //        table: "quantity_history_t",
        //        column: "product_id");

        //    migrationBuilder.CreateIndex(
        //        name: "regestration_phone_UNIQUE",
        //        table: "regestration_t",
        //        column: "regestration_phone",
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "fk_reject_request_t_employee_t1_idx",
        //        table: "reject_request_t",
        //        column: "employee_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_reject_request_t_request_t1_idx",
        //        table: "reject_request_t",
        //        column: "request_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_cancel_request_t_system_user_t1_idx",
        //        table: "request_canceled_t",
        //        column: "system_user_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_request_complaint_t_system_users_t1_idx",
        //        table: "request_complaint_t",
        //        column: "system_user_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_request_complaint_t_request_t1_idx",
        //        table: "request_complaint_t",
        //        columns: new[] { "request_id", "new_request_id" });

        //    migrationBuilder.CreateIndex(
        //        name: "fk_delay_requests_t_requests_t1_idx",
        //        table: "request_delayed_t",
        //        column: "request_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_delay_request_t_system_user_t1_idx",
        //        table: "request_delayed_t",
        //        column: "system_user_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_requests_t_has_service_t_requests_t1_idx",
        //        table: "request_services_t",
        //        column: "request_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_requests_t_has_service_t_service_t1_idx",
        //        table: "request_services_t",
        //        column: "service_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_request_stages_request_t1_idx",
        //        table: "request_stages_t",
        //        column: "request_id",
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "fk_request_t_branch_t1_idx",
        //        table: "request_t",
        //        column: "branch_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_requests_t_clients_t1_idx",
        //        table: "request_t",
        //        column: "client_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_request_t_employee_t1_idx",
        //        table: "request_t",
        //        column: "employee_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_request_t_system_user_t1_idx",
        //        table: "request_t",
        //        column: "system_user_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_service_t_department_sub1t1_idx",
        //        table: "service_t",
        //        column: "department_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_system_users_t_branch_t1_idx",
        //        table: "system_user_t",
        //        column: "branch_id");

        //    migrationBuilder.CreateIndex(
        //        name: "fk_system_users_t_employee_t1_idx",
        //        table: "system_user_t",
        //        column: "employee_id");

        //    migrationBuilder.CreateIndex(
        //        name: "system_user_username_UNIQUE",
        //        table: "system_user_t",
        //        column: "system_user_username",
        //        unique: true);

        //    migrationBuilder.CreateIndex(
        //        name: "fk_work_areas_branch_t1_idx",
        //        table: "working_area_t",
        //        column: "branch_id");
        //}

        //protected override void Down(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.DropTable(
        //        name: "address_t");

        //    migrationBuilder.DropTable(
        //        name: "bill_details_t");

        //    migrationBuilder.DropTable(
        //        name: "cart");

        //    migrationBuilder.DropTable(
        //        name: "client_phones_t");

        //    migrationBuilder.DropTable(
        //        name: "department_employee_t");

        //    migrationBuilder.DropTable(
        //        name: "discount_t");

        //    migrationBuilder.DropTable(
        //        name: "employee_approval");

        //    migrationBuilder.DropTable(
        //        name: "employee_location");

        //    migrationBuilder.DropTable(
        //        name: "employee_workplaces_t");

        //    migrationBuilder.DropTable(
        //        name: "employment_applications_t");

        //    migrationBuilder.DropTable(
        //        name: "fawry_charge_request_t");

        //    migrationBuilder.DropTable(
        //        name: "fired_staff_t");

        //    migrationBuilder.DropTable(
        //        name: "follow_up_t");

        //    migrationBuilder.DropTable(
        //        name: "increase_discount_t");

        //    migrationBuilder.DropTable(
        //        name: "login_t");

        //    migrationBuilder.DropTable(
        //        name: "messages_t");

        //    migrationBuilder.DropTable(
        //        name: "notifications");

        //    migrationBuilder.DropTable(
        //        name: "partiner_cart_t");

        //    migrationBuilder.DropTable(
        //        name: "partiner_payment_request_t");

        //    migrationBuilder.DropTable(
        //        name: "payment_t");

        //    migrationBuilder.DropTable(
        //        name: "poll");

        //    migrationBuilder.DropTable(
        //        name: "product_sold_t");

        //    migrationBuilder.DropTable(
        //        name: "promocode");

        //    migrationBuilder.DropTable(
        //        name: "quantity_history_t");

        //    migrationBuilder.DropTable(
        //        name: "regestration_t");

        //    migrationBuilder.DropTable(
        //        name: "reject_request_t");

        //    migrationBuilder.DropTable(
        //        name: "request_canceled_t");

        //    migrationBuilder.DropTable(
        //        name: "request_complaint_t");

        //    migrationBuilder.DropTable(
        //        name: "request_delayed_t");

        //    migrationBuilder.DropTable(
        //        name: "request_services_t");

        //    migrationBuilder.DropTable(
        //        name: "request_stages_t");

        //    migrationBuilder.DropTable(
        //        name: "setting_t");

        //    migrationBuilder.DropTable(
        //        name: "timetable_t");

        //    migrationBuilder.DropTable(
        //        name: "version_t");

        //    migrationBuilder.DropTable(
        //        name: "working_area_t");

        //    migrationBuilder.DropTable(
        //        name: "bill_number_t");

        //    migrationBuilder.DropTable(
        //        name: "fawry_charge_t");

        //    migrationBuilder.DropTable(
        //        name: "partiner_payment_t");

        //    migrationBuilder.DropTable(
        //        name: "product_receipt_t");

        //    migrationBuilder.DropTable(
        //        name: "product_t");

        //    migrationBuilder.DropTable(
        //        name: "service_t");

        //    migrationBuilder.DropTable(
        //        name: "request_t");

        //    migrationBuilder.DropTable(
        //        name: "department_sub1_t");

        //    migrationBuilder.DropTable(
        //        name: "client_t");

        //    migrationBuilder.DropTable(
        //        name: "department_sub0_t");

        //    migrationBuilder.DropTable(
        //        name: "system_user_t");

        //    migrationBuilder.DropTable(
        //        name: "department_t");

        //    migrationBuilder.DropTable(
        //        name: "branch_t");

        //    migrationBuilder.DropTable(
        //        name: "employee_t");
        //}
    }
}
