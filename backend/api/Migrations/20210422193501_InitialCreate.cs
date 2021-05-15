using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Sendo.Api.Data.Models;

namespace Sendo.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "user_data");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:user_data.gender", "male,female,other");

            migrationBuilder.CreateTable(
                name: "user",
                schema: "user_data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    mail_address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contact",
                schema: "user_data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    mail_address = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    middle_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    gender = table.Column<Gender>(type: "user_data.gender", nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact", x => x.id);
                    table.ForeignKey(
                        name: "FK_contact_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "user_data",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contact_group",
                schema: "user_data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contact_group", x => x.id);
                    table.ForeignKey(
                        name: "FK_contact_group_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "user_data",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mail_template",
                schema: "user_data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    body = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mail_template", x => x.id);
                    table.ForeignKey(
                        name: "FK_mail_template_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "user_data",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "session_token",
                schema: "user_data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    token = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_session_token", x => x.id);
                    table.ForeignKey(
                        name: "FK_session_token_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "user_data",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "group_membership",
                schema: "user_data",
                columns: table => new
                {
                    contact_group_id = table.Column<Guid>(type: "uuid", nullable: false),
                    contact_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_membership", x => new { x.contact_group_id, x.contact_id });
                    table.ForeignKey(
                        name: "FK_group_membership_contact_contact_id",
                        column: x => x.contact_id,
                        principalSchema: "user_data",
                        principalTable: "contact",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_group_membership_contact_group_contact_group_id",
                        column: x => x.contact_group_id,
                        principalSchema: "user_data",
                        principalTable: "contact_group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "campaign",
                schema: "user_data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    contact_group_id = table.Column<Guid>(type: "uuid", nullable: false),
                    mail_template_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_campaign", x => x.id);
                    table.ForeignKey(
                        name: "FK_campaign_contact_group_contact_group_id",
                        column: x => x.contact_group_id,
                        principalSchema: "user_data",
                        principalTable: "contact_group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_campaign_mail_template_mail_template_id",
                        column: x => x.mail_template_id,
                        principalSchema: "user_data",
                        principalTable: "mail_template",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_campaign_user_user_id",
                        column: x => x.user_id,
                        principalSchema: "user_data",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_campaign_contact_group_id",
                schema: "user_data",
                table: "campaign",
                column: "contact_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_campaign_mail_template_id",
                schema: "user_data",
                table: "campaign",
                column: "mail_template_id");

            migrationBuilder.CreateIndex(
                name: "IX_campaign_user_id",
                schema: "user_data",
                table: "campaign",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_contact_user_id",
                schema: "user_data",
                table: "contact",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_contact_group_user_id",
                schema: "user_data",
                table: "contact_group",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_membership_contact_id",
                schema: "user_data",
                table: "group_membership",
                column: "contact_id");

            migrationBuilder.CreateIndex(
                name: "IX_mail_template_user_id",
                schema: "user_data",
                table: "mail_template",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_session_token_user_id",
                schema: "user_data",
                table: "session_token",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "campaign",
                schema: "user_data");

            migrationBuilder.DropTable(
                name: "group_membership",
                schema: "user_data");

            migrationBuilder.DropTable(
                name: "session_token",
                schema: "user_data");

            migrationBuilder.DropTable(
                name: "mail_template",
                schema: "user_data");

            migrationBuilder.DropTable(
                name: "contact",
                schema: "user_data");

            migrationBuilder.DropTable(
                name: "contact_group",
                schema: "user_data");

            migrationBuilder.DropTable(
                name: "user",
                schema: "user_data");
        }
    }
}
