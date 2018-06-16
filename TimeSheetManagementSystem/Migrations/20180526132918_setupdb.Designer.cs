using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TimeSheetManagementSystem.Data;

namespace TimeSheetManagementSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180526132918_setupdb")]
    partial class setupdb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TimeSheetManagementSystem.Models.AccountDetail", b =>
                {
                    b.Property<int>("AccountDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AccountDetailId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerAccountId");

                    b.Property<int>("DayOfWeekNumber");

                    b.Property<DateTime?>("EffectiveEndDate")
                        .HasColumnName("EffectiveEndDate");

                    b.Property<DateTime>("EffectiveStartDate")
                        .HasColumnName("EffectiveStartDate");

                    b.Property<int>("EndTimeInMinutes")
                        .HasColumnName("EndTimeInMinutes")
                        .HasColumnType("int");

                    b.Property<bool>("IsVisible")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IsVisible")
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<int>("StartTimeInMinutes")
                        .HasColumnName("StartTimeInMinutes")
                        .HasColumnType("int");

                    b.HasKey("AccountDetailId")
                        .HasName("PrimaryKey_AccountDetailId");

                    b.HasIndex("CustomerAccountId");

                    b.ToTable("AccountDetail");
                });

            modelBuilder.Entity("TimeSheetManagementSystem.Models.AccountRate", b =>
                {
                    b.Property<int>("AccountRateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AccountRateId")
                        .HasColumnType("INT")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerAccountId");

                    b.Property<DateTime?>("EffectiveEndDate")
                        .HasColumnName("EffectiveEndDate");

                    b.Property<DateTime>("EffectiveStartDate")
                        .HasColumnName("EffectiveStartDate");

                    b.Property<decimal>("RatePerHour")
                        .HasColumnName("RatePerHour")
                        .HasColumnType("DECIMAL(6,2)");

                    b.HasKey("AccountRateId")
                        .HasName("PrimaryKey_AccountRateId");

                    b.HasIndex("CustomerAccountId");

                    b.ToTable("AccountRate");
                });

            modelBuilder.Entity("TimeSheetManagementSystem.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("TimeSheetManagementSystem.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CourseId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CourseAbbreviation")
                        .IsRequired()
                        .HasColumnName("CourseAbbreviation")
                        .HasColumnType("VARCHAR(10)");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnName("CourseName")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GetDate()");

                    b.Property<int>("CreatedById");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<int?>("DeletedById");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GetDate()");

                    b.Property<int>("UpdatedById");

                    b.HasKey("CourseId")
                        .HasName("PrimaryKey_CourseId");

                    b.HasIndex("CourseAbbreviation")
                        .IsUnique()
                        .HasName("Course_CourseAbbreviation_UniqueConstraint");

                    b.HasIndex("CreatedById");

                    b.HasIndex("DeletedById");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("TimeSheetManagementSystem.Models.CustomerAccount", b =>
                {
                    b.Property<int>("CustomerAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CustomerAccountId")
                        .HasColumnType("INT")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnName("AccountName")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("Comments")
                        .HasColumnName("Comments")
                        .HasColumnType("NVARCHAR(4000)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GetDate()");

                    b.Property<int>("CreatedById");

                    b.Property<bool>("IsVisible")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IsVisible")
                        .HasColumnType("BIT")
                        .HasDefaultValue(true);

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GetDate()");

                    b.Property<int>("UpdatedById");

                    b.HasKey("CustomerAccountId")
                        .HasName("PrimaryKey_CustomerAccountId");

                    b.HasIndex("AccountName")
                        .IsUnique()
                        .HasName("CustomerAccount_AccountName_UniqueConstraint");

                    b.HasIndex("CreatedById");

                    b.HasIndex("UpdatedById");

                    b.ToTable("CustomerAccount");
                });

            modelBuilder.Entity("TimeSheetManagementSystem.Models.InstructorAccount", b =>
                {
                    b.Property<int>("InstructorAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("InstructorAccountId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments")
                        .HasColumnName("Comments")
                        .HasColumnType("NVARCHAR(4000)");

                    b.Property<int>("CustomerAccountId")
                        .HasColumnName("CustomerAccountId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EffectiveEndDate")
                        .HasColumnName("EffectiveEndDate");

                    b.Property<DateTime>("EffectiveStartDate")
                        .HasColumnName("EffectiveStartDate");

                    b.Property<int>("InstructorId")
                        .HasColumnName("InstructorId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCurrent")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IsCurrent")
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<decimal>("WageRate")
                        .HasColumnName("WageRate")
                        .HasColumnType("decimal(5,2)");

                    b.HasKey("InstructorAccountId")
                        .HasName("PrimaryKey_InstructorAccounttId");

                    b.HasIndex("CustomerAccountId");

                    b.HasIndex("InstructorId");

                    b.ToTable("InstructorAccount");
                });

            modelBuilder.Entity("TimeSheetManagementSystem.Models.SessionSynopsis", b =>
                {
                    b.Property<int>("SessionSynopsisId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SessionSynopsisId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedById")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.Property<bool>("IsVisible")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IsVisible")
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("SessionSynopsisName")
                        .IsRequired()
                        .HasColumnName("SessionSynopsisName")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<int>("UpdatedById")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.HasKey("SessionSynopsisId")
                        .HasName("PrimaryKey_SessionSynopsisId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("SessionSynopsisName")
                        .IsUnique()
                        .HasName("SessionSynopsis_SessionSynopsisName_UniqueConstraint");

                    b.HasIndex("UpdatedById");

                    b.ToTable("SessionSynopsis");
                });

            modelBuilder.Entity("TimeSheetManagementSystem.Models.TimeSheet", b =>
                {
                    b.Property<int>("TimeSheetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("TimeSheetId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ApprovedAt");

                    b.Property<int?>("ApprovedById")
                        .IsRequired();

                    b.Property<int>("CheckedById");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CreatedAt")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<int>("CreatedById");

                    b.Property<int>("InstructorId");

                    b.Property<DateTime>("MonthAndYear")
                        .HasColumnName("MonthAndYear");

                    b.Property<decimal>("RatePerHour")
                        .HasColumnName("RatePerHour")
                        .HasColumnType("decimal(6,2)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UpdatedAt")
                        .HasDefaultValueSql("GetDate()");

                    b.Property<int>("UpdatedById")
                        .HasColumnName("UpdatedById")
                        .HasColumnType("int");

                    b.Property<DateTime?>("VerifiedAndSubmittedAt")
                        .HasColumnName("VerifiedAndSubmittedAt");

                    b.HasKey("TimeSheetId")
                        .HasName("PrimaryKey_TimeSheetId");

                    b.HasIndex("ApprovedById");

                    b.HasIndex("CreatedById");

                    b.HasIndex("InstructorId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("TimeSheet");
                });

            modelBuilder.Entity("TimeSheetManagementSystem.Models.TimeSheetDetail", b =>
                {
                    b.Property<int>("TimeSheetDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("TimeSheetDetailId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountDetailId");

                    b.Property<DateTime>("DateOfLesson");

                    b.Property<bool>("IsReplacementInstructor")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IsReplacementInstructor")
                        .HasDefaultValue(false);

                    b.Property<int>("OfficialTimeInMinutes")
                        .HasColumnName("OfficialTimeInMinutes");

                    b.Property<int>("OfficialTimeOutMinutes")
                        .HasColumnName("OfficialTimeOutMinutes");

                    b.Property<string>("SessionSynopsisNames")
                        .IsRequired()
                        .HasColumnName("SessionSynopsisNames")
                        .HasColumnType("VARCHAR(300)");

                    b.Property<int>("TimeInInMinutes");

                    b.Property<int>("TimeOutInMinutes");

                    b.Property<int>("TimeSheetId");

                    b.Property<decimal>("WageRatePerHour")
                        .HasColumnName("WageRatePerHour")
                        .HasColumnType("decimal(6,2)");

                    b.HasKey("TimeSheetDetailId")
                        .HasName("PrimaryKey_TimeSheetDetailId");

                    b.HasIndex("AccountDetailId");

                    b.HasIndex("TimeSheetId");

                    b.ToTable("TimeSheetDetail");
                });

            modelBuilder.Entity("TimeSheetManagementSystem.Models.TimeSheetDetailSignature", b =>
                {
                    b.Property<int>("TimeSheetDetailSignatureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("TimeSheetDetailSignatureId")
                        .HasColumnType("INT")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Signature")
                        .HasColumnType("VARBINARY(MAX)");

                    b.Property<int>("TimeSheetIDetailId");

                    b.HasKey("TimeSheetDetailSignatureId")
                        .HasName("PrimaryKey_TimeSheetSignatureId");

                    b.HasIndex("TimeSheetIDetailId")
                        .IsUnique();

                    b.ToTable("TimeSheetDetailSignature");
                });

            modelBuilder.Entity("TimeSheetManagementSystem.Models.UserInfo", b =>
                {
                    b.Property<int>("UserInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UserInfoId")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("Email")
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnName("FullName")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("IsActive")
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("LoginUserName")
                        .IsRequired()
                        .HasColumnName("LoginUserName")
                        .HasColumnType("VARCHAR(10)");

                    b.HasKey("UserInfoId")
                        .HasName("PrimaryKey_UserInfoId");

                    b.HasIndex("LoginUserName")
                        .IsUnique()
                        .HasName("UserInfo_LoginUserName_UniqueConstraint");

                    b.ToTable("UserInfo");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TimeSheetManagementSystem.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TimeSheetManagementSystem.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TimeSheetManagementSystem.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TimeSheetManagementSystem.Models.AccountDetail", b =>
                {
                    b.HasOne("TimeSheetManagementSystem.Models.CustomerAccount", "CustomerAccount")
                        .WithMany("AccountDetails")
                        .HasForeignKey("CustomerAccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TimeSheetManagementSystem.Models.AccountRate", b =>
                {
                    b.HasOne("TimeSheetManagementSystem.Models.CustomerAccount", "CustomerAccount")
                        .WithMany("AccountRates")
                        .HasForeignKey("CustomerAccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TimeSheetManagementSystem.Models.Course", b =>
                {
                    b.HasOne("TimeSheetManagementSystem.Models.UserInfo", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("TimeSheetManagementSystem.Models.UserInfo", "DeletedBy")
                        .WithMany()
                        .HasForeignKey("DeletedById");

                    b.HasOne("TimeSheetManagementSystem.Models.UserInfo", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");
                });

            modelBuilder.Entity("TimeSheetManagementSystem.Models.CustomerAccount", b =>
                {
                    b.HasOne("TimeSheetManagementSystem.Models.UserInfo", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("TimeSheetManagementSystem.Models.UserInfo", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");
                });

            modelBuilder.Entity("TimeSheetManagementSystem.Models.InstructorAccount", b =>
                {
                    b.HasOne("TimeSheetManagementSystem.Models.CustomerAccount", "CustomerAccount")
                        .WithMany("InstructorAccounts")
                        .HasForeignKey("CustomerAccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TimeSheetManagementSystem.Models.UserInfo", "Instructor")
                        .WithMany("InstructorAccounts")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TimeSheetManagementSystem.Models.SessionSynopsis", b =>
                {
                    b.HasOne("TimeSheetManagementSystem.Models.UserInfo", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("TimeSheetManagementSystem.Models.UserInfo", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");
                });

            modelBuilder.Entity("TimeSheetManagementSystem.Models.TimeSheet", b =>
                {
                    b.HasOne("TimeSheetManagementSystem.Models.UserInfo", "ApprovedBy")
                        .WithMany()
                        .HasForeignKey("ApprovedById");

                    b.HasOne("TimeSheetManagementSystem.Models.UserInfo", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("TimeSheetManagementSystem.Models.UserInfo", "Instructor")
                        .WithMany("TimeSheets")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TimeSheetManagementSystem.Models.UserInfo", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");
                });

            modelBuilder.Entity("TimeSheetManagementSystem.Models.TimeSheetDetail", b =>
                {
                    b.HasOne("TimeSheetManagementSystem.Models.AccountDetail", "AccountDetail")
                        .WithMany("TimeSheetDetails")
                        .HasForeignKey("AccountDetailId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TimeSheetManagementSystem.Models.TimeSheet", "TimeSheet")
                        .WithMany("TimeSheetDetails")
                        .HasForeignKey("TimeSheetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TimeSheetManagementSystem.Models.TimeSheetDetailSignature", b =>
                {
                    b.HasOne("TimeSheetManagementSystem.Models.TimeSheetDetail", "TimeSheetDetail")
                        .WithOne("TimeSheetDetailSignature")
                        .HasForeignKey("TimeSheetManagementSystem.Models.TimeSheetDetailSignature", "TimeSheetIDetailId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
