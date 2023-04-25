using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Mobile_Recharge.Migrations
{
    /// <inheritdoc />
    public partial class OnlineRecharge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceProviderModel",
                columns: table => new
                {
                    ServiceProviderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceProviderModel", x => x.ServiceProviderId);
                });

            migrationBuilder.CreateTable(
                name: "RechargePlansModel",
                columns: table => new
                {
                    RechargePlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceProviderId = table.Column<int>(type: "int", nullable: false),
                    RechargePlanName = table.Column<int>(type: "int", nullable: false),
                    RechargePlanValidity = table.Column<int>(type: "int", nullable: false),
                    RechargePlanPrice = table.Column<int>(type: "int", nullable: false),
                    RechargePlanData = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RechargePlansModel", x => x.RechargePlanId);
                    table.ForeignKey(
                        name: "FK_RechargePlansModel_ServiceProviderModel_ServiceProviderId",
                        column: x => x.ServiceProviderId,
                        principalTable: "ServiceProviderModel",
                        principalColumn: "ServiceProviderId");
                });

            migrationBuilder.CreateTable(
                name: "UserDetailsModel",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    ServiceProviderId = table.Column<int>(type: "int", nullable: false),
                    RechargePlanId = table.Column<int>(type: "int", nullable: false),
                    MailId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetailsModel", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserDetailsModel_RechargePlansModel_RechargePlanId",
                        column: x => x.RechargePlanId,
                        principalTable: "RechargePlansModel",
                        principalColumn: "RechargePlanId");
                    table.ForeignKey(
                        name: "FK_UserDetailsModel_ServiceProviderModel_ServiceProviderId",
                        column: x => x.ServiceProviderId,
                        principalTable: "ServiceProviderModel",
                        principalColumn: "ServiceProviderId");
                });

            migrationBuilder.CreateTable(
                name: "RechargeLogsModel",
                columns: table => new
                {
                    RechargeLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RechargePlanId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RechargeDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RechargeValidity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RechargeLogsModel", x => x.RechargeLogId);
                    table.ForeignKey(
                        name: "FK_RechargeLogsModel_RechargePlansModel_RechargePlanId",
                        column: x => x.RechargePlanId,
                        principalTable: "RechargePlansModel",
                        principalColumn: "RechargePlanId");
                    table.ForeignKey(
                        name: "FK_RechargeLogsModel_UserDetailsModel_UserId",
                        column: x => x.UserId,
                        principalTable: "UserDetailsModel",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RechargeLogsModel_RechargePlanId",
                table: "RechargeLogsModel",
                column: "RechargePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_RechargeLogsModel_UserId",
                table: "RechargeLogsModel",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RechargePlansModel_ServiceProviderId",
                table: "RechargePlansModel",
                column: "ServiceProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetailsModel_RechargePlanId",
                table: "UserDetailsModel",
                column: "RechargePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetailsModel_ServiceProviderId",
                table: "UserDetailsModel",
                column: "ServiceProviderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RechargeLogsModel");

            migrationBuilder.DropTable(
                name: "UserDetailsModel");

            migrationBuilder.DropTable(
                name: "RechargePlansModel");

            migrationBuilder.DropTable(
                name: "ServiceProviderModel");
        }
    }
}
