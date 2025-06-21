using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myschedule360.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConvertToSnakeCaseNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Businesses_BusinessId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Customers_CustomerId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Services_ServiceId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Staff_StaffId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_Addresses_AddressId",
                table: "Businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_CommissionRules_Businesses_BusinessId",
                table: "CommissionRules");

            migrationBuilder.DropForeignKey(
                name: "FK_CommissionRules_Services_ServiceId",
                table: "CommissionRules");

            migrationBuilder.DropForeignKey(
                name: "FK_CommissionRules_Staff_StaffId",
                table: "CommissionRules");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Businesses_BusinessId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Persons_PersonId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialTransactions_Appointments_AppointmentId",
                table: "FinancialTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialTransactions_Businesses_BusinessId",
                table: "FinancialTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_InventoryItems_Businesses_BusinessId",
                table: "InventoryItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Businesses_BusinessId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Businesses_BusinessId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Users_UserId",
                table: "Staff");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Businesses_BusinessId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Persons_PersonId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staff",
                table: "Staff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryItems",
                table: "InventoryItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialTransactions",
                table: "FinancialTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommissionRules",
                table: "CommissionRules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Businesses",
                table: "Businesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "CostPrice",
                table: "InventoryItems");

            migrationBuilder.DropColumn(
                name: "CurrentStock",
                table: "InventoryItems");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "InventoryItems");

            migrationBuilder.DropColumn(
                name: "IsForSale",
                table: "InventoryItems");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "InventoryItems");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "InventoryItems");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "tb_users");

            migrationBuilder.RenameTable(
                name: "Staff",
                newName: "tb_staff");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "tb_services");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "tb_persons");

            migrationBuilder.RenameTable(
                name: "InventoryItems",
                newName: "tb_inventory_items");

            migrationBuilder.RenameTable(
                name: "FinancialTransactions",
                newName: "tb_financial_transactions");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "tb_customers");

            migrationBuilder.RenameTable(
                name: "CommissionRules",
                newName: "tb_commission_rules");

            migrationBuilder.RenameTable(
                name: "Businesses",
                newName: "tb_businesses");

            migrationBuilder.RenameTable(
                name: "Appointments",
                newName: "tb_appointments");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "tb_addresses");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "tb_users",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "tb_users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "tb_users",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "tb_users",
                newName: "person_id");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "tb_users",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "tb_users",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "tb_users",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "tb_users",
                newName: "business_id");

            migrationBuilder.RenameIndex(
                name: "IX_Users_PersonId",
                table: "tb_users",
                newName: "IX_tb_users_person_id");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "tb_users",
                newName: "IX_tb_users_email");

            migrationBuilder.RenameIndex(
                name: "IX_Users_BusinessId",
                table: "tb_users",
                newName: "IX_tb_users_business_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_staff",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "tb_staff",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "tb_staff",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "tb_staff",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "tb_staff",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "tb_staff",
                newName: "business_id");

            migrationBuilder.RenameIndex(
                name: "IX_Staff_UserId",
                table: "tb_staff",
                newName: "IX_tb_staff_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Staff_BusinessId",
                table: "tb_staff",
                newName: "IX_tb_staff_business_id");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "tb_services",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "tb_services",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "tb_services",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_services",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "tb_services",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "tb_services",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "DurationMinutes",
                table: "tb_services",
                newName: "duration_minutes");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "tb_services",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "tb_services",
                newName: "business_id");

            migrationBuilder.RenameIndex(
                name: "IX_Services_BusinessId",
                table: "tb_services",
                newName: "IX_tb_services_business_id");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "tb_persons",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "tb_persons",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "tb_persons",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "CPF",
                table: "tb_persons",
                newName: "cpf");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_persons",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "tb_persons",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "tb_persons",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "tb_persons",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "tb_persons",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "tb_persons",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "tb_persons",
                newName: "birth_date");

            migrationBuilder.RenameIndex(
                name: "IX_Persons_Email",
                table: "tb_persons",
                newName: "IX_tb_persons_email");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "tb_inventory_items",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "tb_inventory_items",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_inventory_items",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "tb_inventory_items",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "MinimumStock",
                table: "tb_inventory_items",
                newName: "minimum_stock");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "tb_inventory_items",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "tb_inventory_items",
                newName: "business_id");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryItems_BusinessId",
                table: "tb_inventory_items",
                newName: "IX_tb_inventory_items_business_id");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "tb_financial_transactions",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "tb_financial_transactions",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "tb_financial_transactions",
                newName: "category");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "tb_financial_transactions",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_financial_transactions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TransactionDate",
                table: "tb_financial_transactions",
                newName: "transaction_date");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "tb_financial_transactions",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "tb_financial_transactions",
                newName: "business_id");

            migrationBuilder.RenameColumn(
                name: "AppointmentId",
                table: "tb_financial_transactions",
                newName: "appointment_id");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialTransactions_BusinessId",
                table: "tb_financial_transactions",
                newName: "IX_tb_financial_transactions_business_id");

            migrationBuilder.RenameIndex(
                name: "IX_FinancialTransactions_AppointmentId",
                table: "tb_financial_transactions",
                newName: "IX_tb_financial_transactions_appointment_id");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "tb_customers",
                newName: "notes");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_customers",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "tb_customers",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "tb_customers",
                newName: "person_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "tb_customers",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "tb_customers",
                newName: "business_id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_PersonId",
                table: "tb_customers",
                newName: "IX_tb_customers_person_id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_BusinessId",
                table: "tb_customers",
                newName: "IX_tb_customers_business_id");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "tb_commission_rules",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "tb_commission_rules",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "tb_commission_rules",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_commission_rules",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "tb_commission_rules",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "tb_commission_rules",
                newName: "staff_id");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "tb_commission_rules",
                newName: "service_id");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "tb_commission_rules",
                newName: "is_active");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "tb_commission_rules",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "tb_commission_rules",
                newName: "business_id");

            migrationBuilder.RenameIndex(
                name: "IX_CommissionRules_StaffId",
                table: "tb_commission_rules",
                newName: "IX_tb_commission_rules_staff_id");

            migrationBuilder.RenameIndex(
                name: "IX_CommissionRules_ServiceId",
                table: "tb_commission_rules",
                newName: "IX_tb_commission_rules_service_id");

            migrationBuilder.RenameIndex(
                name: "IX_CommissionRules_BusinessId",
                table: "tb_commission_rules",
                newName: "IX_tb_commission_rules_business_id");

            migrationBuilder.RenameColumn(
                name: "Website",
                table: "tb_businesses",
                newName: "website");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "tb_businesses",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "tb_businesses",
                newName: "slug");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "tb_businesses",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "tb_businesses",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "CNPJ",
                table: "tb_businesses",
                newName: "cnpj");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_businesses",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "tb_businesses",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "IsOnboardingComplete",
                table: "tb_businesses",
                newName: "is_onboarding_complete");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "tb_businesses",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "tb_businesses",
                newName: "address_id");

            migrationBuilder.RenameIndex(
                name: "IX_Businesses_Slug",
                table: "tb_businesses",
                newName: "IX_tb_businesses_slug");

            migrationBuilder.RenameIndex(
                name: "IX_Businesses_AddressId",
                table: "tb_businesses",
                newName: "IX_tb_businesses_address_id");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "tb_appointments",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "tb_appointments",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "tb_appointments",
                newName: "notes");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_appointments",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "tb_appointments",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "tb_appointments",
                newName: "staff_id");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "tb_appointments",
                newName: "service_id");

            migrationBuilder.RenameColumn(
                name: "ScheduledAt",
                table: "tb_appointments",
                newName: "scheduled_at");

            migrationBuilder.RenameColumn(
                name: "DurationMinutes",
                table: "tb_appointments",
                newName: "duration_minutes");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "tb_appointments",
                newName: "customer_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "tb_appointments",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "BusinessId",
                table: "tb_appointments",
                newName: "business_id");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_StaffId",
                table: "tb_appointments",
                newName: "IX_tb_appointments_staff_id");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_ServiceId",
                table: "tb_appointments",
                newName: "IX_tb_appointments_service_id");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_CustomerId",
                table: "tb_appointments",
                newName: "IX_tb_appointments_customer_id");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_BusinessId",
                table: "tb_appointments",
                newName: "IX_tb_appointments_business_id");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "tb_addresses",
                newName: "street");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "tb_addresses",
                newName: "state");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "tb_addresses",
                newName: "number");

            migrationBuilder.RenameColumn(
                name: "Neighborhood",
                table: "tb_addresses",
                newName: "neighborhood");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "tb_addresses",
                newName: "longitude");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "tb_addresses",
                newName: "latitude");

            migrationBuilder.RenameColumn(
                name: "Complement",
                table: "tb_addresses",
                newName: "complement");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "tb_addresses",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "CEP",
                table: "tb_addresses",
                newName: "cep");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_addresses",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "tb_addresses",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "tb_addresses",
                newName: "created_at");

            migrationBuilder.AddColumn<decimal>(
                name: "commission_rate",
                table: "tb_staff",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "role",
                table: "tb_staff",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "minimum_stock",
                table: "tb_inventory_items",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,3)",
                oldPrecision: 10,
                oldScale: 3);

            migrationBuilder.AddColumn<decimal>(
                name: "price",
                table: "tb_inventory_items",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "tb_inventory_items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_users",
                table: "tb_users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_staff",
                table: "tb_staff",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_services",
                table: "tb_services",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_persons",
                table: "tb_persons",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_inventory_items",
                table: "tb_inventory_items",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_financial_transactions",
                table: "tb_financial_transactions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_customers",
                table: "tb_customers",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_commission_rules",
                table: "tb_commission_rules",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_businesses",
                table: "tb_businesses",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_appointments",
                table: "tb_appointments",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_addresses",
                table: "tb_addresses",
                column: "id");

            migrationBuilder.CreateTable(
                name: "tb_staff_availabilities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    staff_id = table.Column<Guid>(type: "uuid", nullable: false),
                    day_of_week = table.Column<int>(type: "integer", nullable: false),
                    start_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    end_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    is_available = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_staff_availabilities", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_staff_availabilities_tb_staff_staff_id",
                        column: x => x.staff_id,
                        principalTable: "tb_staff",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_staff_availabilities_staff_id",
                table: "tb_staff_availabilities",
                column: "staff_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_appointments_tb_businesses_business_id",
                table: "tb_appointments",
                column: "business_id",
                principalTable: "tb_businesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_appointments_tb_customers_customer_id",
                table: "tb_appointments",
                column: "customer_id",
                principalTable: "tb_customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_appointments_tb_services_service_id",
                table: "tb_appointments",
                column: "service_id",
                principalTable: "tb_services",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_appointments_tb_staff_staff_id",
                table: "tb_appointments",
                column: "staff_id",
                principalTable: "tb_staff",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_businesses_tb_addresses_address_id",
                table: "tb_businesses",
                column: "address_id",
                principalTable: "tb_addresses",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_commission_rules_tb_businesses_business_id",
                table: "tb_commission_rules",
                column: "business_id",
                principalTable: "tb_businesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_commission_rules_tb_services_service_id",
                table: "tb_commission_rules",
                column: "service_id",
                principalTable: "tb_services",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_commission_rules_tb_staff_staff_id",
                table: "tb_commission_rules",
                column: "staff_id",
                principalTable: "tb_staff",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_customers_tb_businesses_business_id",
                table: "tb_customers",
                column: "business_id",
                principalTable: "tb_businesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_customers_tb_persons_person_id",
                table: "tb_customers",
                column: "person_id",
                principalTable: "tb_persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_financial_transactions_tb_appointments_appointment_id",
                table: "tb_financial_transactions",
                column: "appointment_id",
                principalTable: "tb_appointments",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_financial_transactions_tb_businesses_business_id",
                table: "tb_financial_transactions",
                column: "business_id",
                principalTable: "tb_businesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_inventory_items_tb_businesses_business_id",
                table: "tb_inventory_items",
                column: "business_id",
                principalTable: "tb_businesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_services_tb_businesses_business_id",
                table: "tb_services",
                column: "business_id",
                principalTable: "tb_businesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_staff_tb_businesses_business_id",
                table: "tb_staff",
                column: "business_id",
                principalTable: "tb_businesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_staff_tb_users_user_id",
                table: "tb_staff",
                column: "user_id",
                principalTable: "tb_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_users_tb_businesses_business_id",
                table: "tb_users",
                column: "business_id",
                principalTable: "tb_businesses",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_users_tb_persons_person_id",
                table: "tb_users",
                column: "person_id",
                principalTable: "tb_persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_appointments_tb_businesses_business_id",
                table: "tb_appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_appointments_tb_customers_customer_id",
                table: "tb_appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_appointments_tb_services_service_id",
                table: "tb_appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_appointments_tb_staff_staff_id",
                table: "tb_appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_businesses_tb_addresses_address_id",
                table: "tb_businesses");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_commission_rules_tb_businesses_business_id",
                table: "tb_commission_rules");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_commission_rules_tb_services_service_id",
                table: "tb_commission_rules");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_commission_rules_tb_staff_staff_id",
                table: "tb_commission_rules");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_customers_tb_businesses_business_id",
                table: "tb_customers");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_customers_tb_persons_person_id",
                table: "tb_customers");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_financial_transactions_tb_appointments_appointment_id",
                table: "tb_financial_transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_financial_transactions_tb_businesses_business_id",
                table: "tb_financial_transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_inventory_items_tb_businesses_business_id",
                table: "tb_inventory_items");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_services_tb_businesses_business_id",
                table: "tb_services");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_staff_tb_businesses_business_id",
                table: "tb_staff");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_staff_tb_users_user_id",
                table: "tb_staff");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_users_tb_businesses_business_id",
                table: "tb_users");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_users_tb_persons_person_id",
                table: "tb_users");

            migrationBuilder.DropTable(
                name: "tb_staff_availabilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_users",
                table: "tb_users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_staff",
                table: "tb_staff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_services",
                table: "tb_services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_persons",
                table: "tb_persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_inventory_items",
                table: "tb_inventory_items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_financial_transactions",
                table: "tb_financial_transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_customers",
                table: "tb_customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_commission_rules",
                table: "tb_commission_rules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_businesses",
                table: "tb_businesses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_appointments",
                table: "tb_appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_addresses",
                table: "tb_addresses");

            migrationBuilder.DropColumn(
                name: "commission_rate",
                table: "tb_staff");

            migrationBuilder.DropColumn(
                name: "role",
                table: "tb_staff");

            migrationBuilder.DropColumn(
                name: "price",
                table: "tb_inventory_items");

            migrationBuilder.DropColumn(
                name: "quantity",
                table: "tb_inventory_items");

            migrationBuilder.RenameTable(
                name: "tb_users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "tb_staff",
                newName: "Staff");

            migrationBuilder.RenameTable(
                name: "tb_services",
                newName: "Services");

            migrationBuilder.RenameTable(
                name: "tb_persons",
                newName: "Persons");

            migrationBuilder.RenameTable(
                name: "tb_inventory_items",
                newName: "InventoryItems");

            migrationBuilder.RenameTable(
                name: "tb_financial_transactions",
                newName: "FinancialTransactions");

            migrationBuilder.RenameTable(
                name: "tb_customers",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "tb_commission_rules",
                newName: "CommissionRules");

            migrationBuilder.RenameTable(
                name: "tb_businesses",
                newName: "Businesses");

            migrationBuilder.RenameTable(
                name: "tb_appointments",
                newName: "Appointments");

            migrationBuilder.RenameTable(
                name: "tb_addresses",
                newName: "Addresses");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "person_id",
                table: "Users",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "Users",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "business_id",
                table: "Users",
                newName: "BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_users_person_id",
                table: "Users",
                newName: "IX_Users_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_users_email",
                table: "Users",
                newName: "IX_Users_Email");

            migrationBuilder.RenameIndex(
                name: "IX_tb_users_business_id",
                table: "Users",
                newName: "IX_Users_BusinessId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Staff",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Staff",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Staff",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "Staff",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Staff",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "business_id",
                table: "Staff",
                newName: "BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_staff_user_id",
                table: "Staff",
                newName: "IX_Staff_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_staff_business_id",
                table: "Staff",
                newName: "IX_Staff_BusinessId");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Services",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Services",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Services",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Services",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Services",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "Services",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "duration_minutes",
                table: "Services",
                newName: "DurationMinutes");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Services",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "business_id",
                table: "Services",
                newName: "BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_services_business_id",
                table: "Services",
                newName: "IX_Services_BusinessId");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "Persons",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Persons",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Persons",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "cpf",
                table: "Persons",
                newName: "CPF");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Persons",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Persons",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "Persons",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "Persons",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "Persons",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Persons",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                table: "Persons",
                newName: "BirthDate");

            migrationBuilder.RenameIndex(
                name: "IX_tb_persons_email",
                table: "Persons",
                newName: "IX_Persons_Email");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "InventoryItems",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "InventoryItems",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "InventoryItems",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "InventoryItems",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "minimum_stock",
                table: "InventoryItems",
                newName: "MinimumStock");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "InventoryItems",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "business_id",
                table: "InventoryItems",
                newName: "BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_inventory_items_business_id",
                table: "InventoryItems",
                newName: "IX_InventoryItems_BusinessId");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "FinancialTransactions",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "FinancialTransactions",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "category",
                table: "FinancialTransactions",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "FinancialTransactions",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "FinancialTransactions",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "transaction_date",
                table: "FinancialTransactions",
                newName: "TransactionDate");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "FinancialTransactions",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "business_id",
                table: "FinancialTransactions",
                newName: "BusinessId");

            migrationBuilder.RenameColumn(
                name: "appointment_id",
                table: "FinancialTransactions",
                newName: "AppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_financial_transactions_business_id",
                table: "FinancialTransactions",
                newName: "IX_FinancialTransactions_BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_financial_transactions_appointment_id",
                table: "FinancialTransactions",
                newName: "IX_FinancialTransactions_AppointmentId");

            migrationBuilder.RenameColumn(
                name: "notes",
                table: "Customers",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Customers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Customers",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "person_id",
                table: "Customers",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Customers",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "business_id",
                table: "Customers",
                newName: "BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_customers_person_id",
                table: "Customers",
                newName: "IX_Customers_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_customers_business_id",
                table: "Customers",
                newName: "IX_Customers_BusinessId");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "CommissionRules",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "CommissionRules",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "CommissionRules",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CommissionRules",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "CommissionRules",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "staff_id",
                table: "CommissionRules",
                newName: "StaffId");

            migrationBuilder.RenameColumn(
                name: "service_id",
                table: "CommissionRules",
                newName: "ServiceId");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "CommissionRules",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "CommissionRules",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "business_id",
                table: "CommissionRules",
                newName: "BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_commission_rules_staff_id",
                table: "CommissionRules",
                newName: "IX_CommissionRules_StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_commission_rules_service_id",
                table: "CommissionRules",
                newName: "IX_CommissionRules_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_commission_rules_business_id",
                table: "CommissionRules",
                newName: "IX_CommissionRules_BusinessId");

            migrationBuilder.RenameColumn(
                name: "website",
                table: "Businesses",
                newName: "Website");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "Businesses",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "slug",
                table: "Businesses",
                newName: "Slug");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Businesses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Businesses",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "cnpj",
                table: "Businesses",
                newName: "CNPJ");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Businesses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Businesses",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "is_onboarding_complete",
                table: "Businesses",
                newName: "IsOnboardingComplete");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Businesses",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "address_id",
                table: "Businesses",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_businesses_slug",
                table: "Businesses",
                newName: "IX_Businesses_Slug");

            migrationBuilder.RenameIndex(
                name: "IX_tb_businesses_address_id",
                table: "Businesses",
                newName: "IX_Businesses_AddressId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Appointments",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Appointments",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "notes",
                table: "Appointments",
                newName: "Notes");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Appointments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Appointments",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "staff_id",
                table: "Appointments",
                newName: "StaffId");

            migrationBuilder.RenameColumn(
                name: "service_id",
                table: "Appointments",
                newName: "ServiceId");

            migrationBuilder.RenameColumn(
                name: "scheduled_at",
                table: "Appointments",
                newName: "ScheduledAt");

            migrationBuilder.RenameColumn(
                name: "duration_minutes",
                table: "Appointments",
                newName: "DurationMinutes");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "Appointments",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Appointments",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "business_id",
                table: "Appointments",
                newName: "BusinessId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_appointments_staff_id",
                table: "Appointments",
                newName: "IX_Appointments_StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_appointments_service_id",
                table: "Appointments",
                newName: "IX_Appointments_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_appointments_customer_id",
                table: "Appointments",
                newName: "IX_Appointments_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_appointments_business_id",
                table: "Appointments",
                newName: "IX_Appointments_BusinessId");

            migrationBuilder.RenameColumn(
                name: "street",
                table: "Addresses",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "state",
                table: "Addresses",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "number",
                table: "Addresses",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "neighborhood",
                table: "Addresses",
                newName: "Neighborhood");

            migrationBuilder.RenameColumn(
                name: "longitude",
                table: "Addresses",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "latitude",
                table: "Addresses",
                newName: "Latitude");

            migrationBuilder.RenameColumn(
                name: "complement",
                table: "Addresses",
                newName: "Complement");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "Addresses",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "cep",
                table: "Addresses",
                newName: "CEP");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Addresses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Addresses",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Addresses",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "Staff",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MinimumStock",
                table: "InventoryItems",
                type: "numeric(10,3)",
                precision: 10,
                scale: 3,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<decimal>(
                name: "CostPrice",
                table: "InventoryItems",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentStock",
                table: "InventoryItems",
                type: "numeric(10,3)",
                precision: 10,
                scale: 3,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "InventoryItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsForSale",
                table: "InventoryItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                table: "InventoryItems",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "InventoryItems",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staff",
                table: "Staff",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryItems",
                table: "InventoryItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialTransactions",
                table: "FinancialTransactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommissionRules",
                table: "CommissionRules",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Businesses",
                table: "Businesses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Businesses_BusinessId",
                table: "Appointments",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Customers_CustomerId",
                table: "Appointments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Services_ServiceId",
                table: "Appointments",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Staff_StaffId",
                table: "Appointments",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_Addresses_AddressId",
                table: "Businesses",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommissionRules_Businesses_BusinessId",
                table: "CommissionRules",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommissionRules_Services_ServiceId",
                table: "CommissionRules",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommissionRules_Staff_StaffId",
                table: "CommissionRules",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Businesses_BusinessId",
                table: "Customers",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Persons_PersonId",
                table: "Customers",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialTransactions_Appointments_AppointmentId",
                table: "FinancialTransactions",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialTransactions_Businesses_BusinessId",
                table: "FinancialTransactions",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryItems_Businesses_BusinessId",
                table: "InventoryItems",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Businesses_BusinessId",
                table: "Services",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Businesses_BusinessId",
                table: "Staff",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Users_UserId",
                table: "Staff",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Businesses_BusinessId",
                table: "Users",
                column: "BusinessId",
                principalTable: "Businesses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Persons_PersonId",
                table: "Users",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
