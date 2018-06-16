using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;
using System.IO;

namespace TimeSheetManagementSystem.Migrations
{
    public partial class setupdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    UserInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    FullName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    LoginUserName = table.Column<string>(type: "VARCHAR(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_UserInfoId", x => x.UserInfoId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseAbbreviation = table.Column<string>(type: "VARCHAR(10)", nullable: false),
                    CourseName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    CreatedById = table.Column<int>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    DeletedById = table.Column<int>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedById = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_CourseId", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Course_UserInfo_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserInfo",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Course_UserInfo_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "UserInfo",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Course_UserInfo_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "UserInfo",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAccount",
                columns: table => new
                {
                    CustomerAccountId = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Comments = table.Column<string>(type: "NVARCHAR(4000)", nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    CreatedById = table.Column<int>(nullable: false),
                    IsVisible = table.Column<bool>(type: "BIT", nullable: false, defaultValue: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedById = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_CustomerAccountId", x => x.CustomerAccountId);
                    table.ForeignKey(
                        name: "FK_CustomerAccount_UserInfo_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserInfo",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerAccount_UserInfo_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "UserInfo",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SessionSynopsis",
                columns: table => new
                {
                    SessionSynopsisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedById = table.Column<int>(nullable: false, defaultValue: 1),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    SessionSynopsisName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    UpdatedById = table.Column<int>(nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_SessionSynopsisId", x => x.SessionSynopsisId);
                    table.ForeignKey(
                        name: "FK_SessionSynopsis_UserInfo_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserInfo",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionSynopsis_UserInfo_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "UserInfo",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheet",
                columns: table => new
                {
                    TimeSheetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApprovedAt = table.Column<DateTime>(nullable: true),
                    ApprovedById = table.Column<int>(nullable: false),
                    CheckedById = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    CreatedById = table.Column<int>(nullable: false),
                    InstructorId = table.Column<int>(nullable: false),
                    MonthAndYear = table.Column<DateTime>(nullable: false),
                    RatePerHour = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GetDate()"),
                    UpdatedById = table.Column<int>(type: "int", nullable: false),
                    VerifiedAndSubmittedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_TimeSheetId", x => x.TimeSheetId);
                    table.ForeignKey(
                        name: "FK_TimeSheet_UserInfo_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "UserInfo",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeSheet_UserInfo_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "UserInfo",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeSheet_UserInfo_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "UserInfo",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeSheet_UserInfo_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "UserInfo",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountDetail",
                columns: table => new
                {
                    AccountDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerAccountId = table.Column<int>(nullable: false),
                    DayOfWeekNumber = table.Column<int>(nullable: false),
                    EffectiveEndDate = table.Column<DateTime>(nullable: true),
                    EffectiveStartDate = table.Column<DateTime>(nullable: false),
                    EndTimeInMinutes = table.Column<int>(type: "int", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    StartTimeInMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_AccountDetailId", x => x.AccountDetailId);
                    table.ForeignKey(
                        name: "FK_AccountDetail_CustomerAccount_CustomerAccountId",
                        column: x => x.CustomerAccountId,
                        principalTable: "CustomerAccount",
                        principalColumn: "CustomerAccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountRate",
                columns: table => new
                {
                    AccountRateId = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerAccountId = table.Column<int>(nullable: false),
                    EffectiveEndDate = table.Column<DateTime>(nullable: true),
                    EffectiveStartDate = table.Column<DateTime>(nullable: false),
                    RatePerHour = table.Column<decimal>(type: "DECIMAL(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_AccountRateId", x => x.AccountRateId);
                    table.ForeignKey(
                        name: "FK_AccountRate_CustomerAccount_CustomerAccountId",
                        column: x => x.CustomerAccountId,
                        principalTable: "CustomerAccount",
                        principalColumn: "CustomerAccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstructorAccount",
                columns: table => new
                {
                    InstructorAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comments = table.Column<string>(type: "NVARCHAR(4000)", nullable: true),
                    CustomerAccountId = table.Column<int>(type: "int", nullable: false),
                    EffectiveEndDate = table.Column<DateTime>(nullable: true),
                    EffectiveStartDate = table.Column<DateTime>(nullable: false),
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    WageRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_InstructorAccounttId", x => x.InstructorAccountId);
                    table.ForeignKey(
                        name: "FK_InstructorAccount_CustomerAccount_CustomerAccountId",
                        column: x => x.CustomerAccountId,
                        principalTable: "CustomerAccount",
                        principalColumn: "CustomerAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructorAccount_UserInfo_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "UserInfo",
                        principalColumn: "UserInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheetDetail",
                columns: table => new
                {
                    TimeSheetDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountDetailId = table.Column<int>(nullable: false),
                    DateOfLesson = table.Column<DateTime>(nullable: false),
                    IsReplacementInstructor = table.Column<bool>(nullable: false, defaultValue: false),
                    OfficialTimeInMinutes = table.Column<int>(nullable: false),
                    OfficialTimeOutMinutes = table.Column<int>(nullable: false),
                    SessionSynopsisNames = table.Column<string>(type: "VARCHAR(300)", nullable: false),
                    TimeInInMinutes = table.Column<int>(nullable: false),
                    TimeOutInMinutes = table.Column<int>(nullable: false),
                    TimeSheetId = table.Column<int>(nullable: false),
                    WageRatePerHour = table.Column<decimal>(type: "decimal(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_TimeSheetDetailId", x => x.TimeSheetDetailId);
                    table.ForeignKey(
                        name: "FK_TimeSheetDetail_AccountDetail_AccountDetailId",
                        column: x => x.AccountDetailId,
                        principalTable: "AccountDetail",
                        principalColumn: "AccountDetailId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeSheetDetail_TimeSheet_TimeSheetId",
                        column: x => x.TimeSheetId,
                        principalTable: "TimeSheet",
                        principalColumn: "TimeSheetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeSheetDetailSignature",
                columns: table => new
                {
                    TimeSheetDetailSignatureId = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Signature = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: true),
                    TimeSheetIDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_TimeSheetSignatureId", x => x.TimeSheetDetailSignatureId);
                    table.ForeignKey(
                        name: "FK_TimeSheetDetailSignature_TimeSheetDetail_TimeSheetIDetailId",
                        column: x => x.TimeSheetIDetailId,
                        principalTable: "TimeSheetDetail",
                        principalColumn: "TimeSheetDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountDetail_CustomerAccountId",
                table: "AccountDetail",
                column: "CustomerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRate_CustomerAccountId",
                table: "AccountRate",
                column: "CustomerAccountId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Course_CourseAbbreviation_UniqueConstraint",
                table: "Course",
                column: "CourseAbbreviation",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Course_CreatedById",
                table: "Course",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Course_DeletedById",
                table: "Course",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Course_UpdatedById",
                table: "Course",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "CustomerAccount_AccountName_UniqueConstraint",
                table: "CustomerAccount",
                column: "AccountName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccount_CreatedById",
                table: "CustomerAccount",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccount_UpdatedById",
                table: "CustomerAccount",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorAccount_CustomerAccountId",
                table: "InstructorAccount",
                column: "CustomerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructorAccount_InstructorId",
                table: "InstructorAccount",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionSynopsis_CreatedById",
                table: "SessionSynopsis",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "SessionSynopsis_SessionSynopsisName_UniqueConstraint",
                table: "SessionSynopsis",
                column: "SessionSynopsisName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionSynopsis_UpdatedById",
                table: "SessionSynopsis",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheet_ApprovedById",
                table: "TimeSheet",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheet_CreatedById",
                table: "TimeSheet",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheet_InstructorId",
                table: "TimeSheet",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheet_UpdatedById",
                table: "TimeSheet",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetDetail_AccountDetailId",
                table: "TimeSheetDetail",
                column: "AccountDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetDetail_TimeSheetId",
                table: "TimeSheetDetail",
                column: "TimeSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSheetDetailSignature_TimeSheetIDetailId",
                table: "TimeSheetDetailSignature",
                column: "TimeSheetIDetailId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserInfo_LoginUserName_UniqueConstraint",
                table: "UserInfo",
                column: "LoginUserName",
                unique: true);

            migrationBuilder.Sql(File.ReadAllText("migrations/setup_AspNetUsers_Insert_trigger.sql"));
            migrationBuilder.Sql(File.ReadAllText("migrations/setup_AspNetUsers_Update_trigger.sql"));
            migrationBuilder.Sql(File.ReadAllText("migrations/setup_convert_HHMM_to_minutes.sql"));
            migrationBuilder.Sql(File.ReadAllText("migrations/setup_convert_minutes_to_HHMM.sql"));


        }



        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AccountRate");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "InstructorAccount");

            migrationBuilder.DropTable(
                name: "SessionSynopsis");

            migrationBuilder.DropTable(
                name: "TimeSheetDetailSignature");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TimeSheetDetail");

            migrationBuilder.DropTable(
                name: "AccountDetail");

            migrationBuilder.DropTable(
                name: "TimeSheet");

            migrationBuilder.DropTable(
                name: "CustomerAccount");

            migrationBuilder.DropTable(
                name: "UserInfo");
        }
    }
}
