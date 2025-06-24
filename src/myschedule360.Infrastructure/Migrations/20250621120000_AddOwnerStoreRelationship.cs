using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myschedule360.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerStoreRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create owners table
            migrationBuilder.CreateTable(
                name: "owners",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_owners", x => x.id);
                    table.ForeignKey(
                        name: "fk_owners_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create stores table
            migrationBuilder.CreateTable(
                name: "stores",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    slug = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    cnpj = table.Column<string>(type: "text", nullable: true),
                    website = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false),
                    is_onboarding_complete = table.Column<bool>(type: "boolean", nullable: false),
                    address_id = table.Column<Guid>(type: "uuid", nullable: true),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stores", x => x.id);
                    table.ForeignKey(
                        name: "fk_stores_addresses_address_id",
                        column: x => x.address_id,
                        principalTable: "addresses",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_stores_owners_owner_id",
                        column: x => x.owner_id,
                        principalTable: "owners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            // Add store_id column to users table
            migrationBuilder.AddColumn<Guid>(
                name: "store_id",
                table: "users",
                type: "uuid",
                nullable: true);

            // Add store_id column to services table
            migrationBuilder.AddColumn<Guid>(
                name: "store_id",
                table: "services",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            // Add store_id column to staff table
            migrationBuilder.AddColumn<Guid>(
                name: "store_id",
                table: "staff",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            // Add store_id column to inventory_items table
            migrationBuilder.AddColumn<Guid>(
                name: "store_id",
                table: "inventory_items",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            // Add store_id column to financial_transactions table
            migrationBuilder.AddColumn<Guid>(
                name: "store_id",
                table: "financial_transactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            // Create indexes
            migrationBuilder.CreateIndex(
                name: "ix_owners_user_id",
                table: "owners",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_stores_address_id",
                table: "stores",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "ix_stores_owner_id",
                table: "stores",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "ix_stores_slug",
                table: "stores",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_store_id",
                table: "users",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "ix_services_store_id",
                table: "services",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "ix_staff_store_id",
                table: "staff",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "ix_inventory_items_store_id",
                table: "inventory_items",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "ix_financial_transactions_store_id",
                table: "financial_transactions",
                column: "store_id");

            // Add foreign key constraints
            migrationBuilder.AddForeignKey(
                name: "fk_users_stores_store_id",
                table: "users",
                column: "store_id",
                principalTable: "stores",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_services_stores_store_id",
                table: "services",
                column: "store_id",
                principalTable: "stores",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_staff_stores_store_id",
                table: "staff",
                column: "store_id",
                principalTable: "stores",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_inventory_items_stores_store_id",
                table: "inventory_items",
                column: "store_id",
                principalTable: "stores",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_financial_transactions_stores_store_id",
                table: "financial_transactions",
                column: "store_id",
                principalTable: "stores",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraints
            migrationBuilder.DropForeignKey(
                name: "fk_users_stores_store_id",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "fk_services_stores_store_id",
                table: "services");

            migrationBuilder.DropForeignKey(
                name: "fk_staff_stores_store_id",
                table: "staff");

            migrationBuilder.DropForeignKey(
                name: "fk_inventory_items_stores_store_id",
                table: "inventory_items");

            migrationBuilder.DropForeignKey(
                name: "fk_financial_transactions_stores_store_id",
                table: "financial_transactions");

            // Drop indexes
            migrationBuilder.DropIndex(
                name: "ix_users_store_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_services_store_id",
                table: "services");

            migrationBuilder.DropIndex(
                name: "ix_staff_store_id",
                table: "staff");

            migrationBuilder.DropIndex(
                name: "ix_inventory_items_store_id",
                table: "inventory_items");

            migrationBuilder.DropIndex(
                name: "ix_financial_transactions_store_id",
                table: "financial_transactions");

            // Drop columns
            migrationBuilder.DropColumn(
                name: "store_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "store_id",
                table: "services");

            migrationBuilder.DropColumn(
                name: "store_id",
                table: "staff");

            migrationBuilder.DropColumn(
                name: "store_id",
                table: "inventory_items");

            migrationBuilder.DropColumn(
                name: "store_id",
                table: "financial_transactions");

            // Drop tables
            migrationBuilder.DropTable(
                name: "stores");

            migrationBuilder.DropTable(
                name: "owners");
        }
    }
}