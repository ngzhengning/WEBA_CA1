using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/* Additional using statements besides the defaults (Start) */

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimeSheetManagementSystem.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
/* Additional using statements besides the defaults (End) */

namespace TimeSheetManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<SessionSynopsis> SessionSynopses { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public DbSet<InstructorAccount> InstructorAccounts { get; set; }
        public DbSet<AccountDetail> AccountDetails { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<TimeSheetDetail> TimeSheetDetails { get; set; }
        public DbSet<AccountRate> AccountRates { get; set; }
        public DbSet<Course> Courses { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

            //The code to connect to SqlServer is very different after using Asp.Net Identity Core instead of Asp.Net Identity package.
            //Even other developers also noticed it.
            //http://stackoverflow.com/questions/42960684/migration-from-asp-net-to-asp-net-core


        }

        public ApplicationDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
       //optionsBuilder.UseSqlServer(@"Server=ILOVECODE\SQLEXPRESS;database=TimeSheetManagementSystemDB_v2;Trusted_Connection=True;MultipleActiveResultSets=True");
        optionsBuilder.UseSqlServer(@"Server=DESKTOP-K7P5KG9\SQLEXPRESS;database=TimeSheetManagementSystemDB;Trusted_Connection=True;MultipleActiveResultSets=True");
             }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.DisplayName();
            }

            //----- Defining Course Entity - Start --------------

            //Make the CourseId a  Primary Key
            modelBuilder.Entity<Course>()
            .HasKey(input => input.CourseId)
            .HasName("PrimaryKey_CourseId");

            //Defining general properties of CourseId
            modelBuilder.Entity<Course>()
            .Property(input => input.CourseId)
            .HasColumnName("CourseId")
            .HasColumnType("int")
            .UseSqlServerIdentityColumn()
            .ValueGeneratedOnAdd()
            .IsRequired();

            //Defining general properties of CourseName
            modelBuilder.Entity<Course>()
                .Property(input => input.CourseName)
                .HasColumnName("CourseName")
                .HasColumnType("VARCHAR(100)")
                .IsRequired();

            //Defining general properties of CourseAbbreviation
            modelBuilder.Entity<Course>()
            .Property(input => input.CourseAbbreviation)
            .HasColumnName("CourseAbbreviation")
            .HasColumnType("VARCHAR(10)")
            .IsRequired();

            modelBuilder.Entity<Course>()
            .Property(input => input.CreatedAt)
            .HasDefaultValueSql("GetDate()");

            modelBuilder.Entity<Course>()
            .Property(input => input.UpdatedAt)
            .HasDefaultValueSql("GetDate()");

            modelBuilder.Entity<Course>()
            .Property(input => input.DeletedAt)
            .IsRequired(false);

            //Enforce unique constraint on Course Abbreviation
            modelBuilder.Entity<Course>()
            .HasIndex(input => input.CourseAbbreviation).IsUnique()
            .HasName("Course_CourseAbbreviation_UniqueConstraint");
            //Setting up relationship with the UserInfo entity
            modelBuilder.Entity<Course>()
              .HasOne(input => input.CreatedBy)
                .WithMany()
                .HasForeignKey(input => input.CreatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            modelBuilder.Entity<Course>()
              .HasOne(input => input.DeletedBy)
                .WithMany()
                .HasForeignKey(input => input.DeletedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            modelBuilder.Entity<Course>()
              .HasOne(input => input.UpdatedBy)
                .WithMany()
                .HasForeignKey(input => input.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            //----------- Defining Course Entity - End --------------




            //----------- Defining SessionSynopsis Entity - Start --------------
            //Make the SessionSynopsisId as  Primary Key 
            modelBuilder.Entity<SessionSynopsis>()
                .HasKey(input => input.SessionSynopsisId)
                .HasName("PrimaryKey_SessionSynopsisId");

            //Make the SessionSynopsisId an Auto-Number field.
            modelBuilder.Entity<SessionSynopsis>()
                .Property(input => input.SessionSynopsisId)
                .HasColumnName("SessionSynopsisId")
                .HasColumnType("int")
                .UseSqlServerIdentityColumn()
                .ValueGeneratedOnAdd()
                .IsRequired(true);

            modelBuilder.Entity<SessionSynopsis>()
                .Property(input => input.SessionSynopsisName)
                .HasColumnName("SessionSynopsisName")
                .HasColumnType("VARCHAR(100)")
                .IsRequired(true);

            modelBuilder.Entity<SessionSynopsis>()
                .Property(input => input.IsVisible)
                .HasColumnName("IsVisible")
                .HasColumnType("bit")
                .HasDefaultValue(false)
                .IsRequired(true);


            modelBuilder.Entity<SessionSynopsis>()
         .HasOne(input => input.CreatedBy)
           .WithMany()
           .HasForeignKey(input => input.CreatedById)
           .OnDelete(DeleteBehavior.Restrict)
           .IsRequired();

           modelBuilder.Entity<SessionSynopsis>()
         .HasOne(input => input.UpdatedBy)
           .WithMany()
           .HasForeignKey(input => input.UpdatedById)
           .OnDelete(DeleteBehavior.Restrict)
           .IsRequired();

            //modelBuilder.Entity<SessionSynopsis>()
            //    .Property(input => input.CreatedById)
            //    .HasDefaultValue(true);


            //modelBuilder.Entity<SessionSynopsis>()
            //    .Property(input => input.UpdatedById)
            //    .HasDefaultValue(true);


            //Enforce unique constraint on SessionSynopsisName
            modelBuilder.Entity<SessionSynopsis>()
             .HasIndex(input => input.SessionSynopsisName).IsUnique()
             .HasName("SessionSynopsis_SessionSynopsisName_UniqueConstraint");

            //----------- Defining SessionSynopsis Entity - End --------------

            //----------- Defining CustomerAccount Entity - Start --------------
            //Make the CustomerAccountId a Primary Key 
            modelBuilder.Entity<CustomerAccount>()
                .HasKey(input => input.CustomerAccountId)
                .HasName("PrimaryKey_CustomerAccountId");
            //Make the CustomerAccountId an Auto-Number
            modelBuilder.Entity<CustomerAccount>()
                .Property(input => input.CustomerAccountId)
                .HasColumnName("CustomerAccountId")
                .HasColumnType("INT")
                .UseSqlServerIdentityColumn()
                .ValueGeneratedOnAdd()
                .IsRequired(true);

            modelBuilder.Entity<CustomerAccount>()
                .Property(input => input.AccountName)
                .HasColumnName("AccountName")
                .HasColumnType("VARCHAR(100)")
                .IsRequired();

            modelBuilder.Entity<CustomerAccount>()
                .Property(input => input.IsVisible)
                .HasColumnName("IsVisible")
                .HasColumnType("BIT")
                .HasDefaultValue(true)
                .IsRequired(true);

            modelBuilder.Entity<CustomerAccount>()
                .Property(input => input.Comments)
                .HasColumnName("Comments")
                .HasColumnType("NVARCHAR(4000)")
                .IsRequired(false);

            modelBuilder.Entity<CustomerAccount>()
                .Property(input => input.CreatedAt)
                .HasDefaultValueSql("GetDate()");

            modelBuilder.Entity<CustomerAccount>()
                 .Property(input => input.UpdatedAt)
                 .HasDefaultValueSql("GetDate()");
            //Enforce unique constraint on AccountName
            modelBuilder.Entity<CustomerAccount>()
             .HasIndex(input => input.AccountName).IsUnique()
             .HasName("CustomerAccount_AccountName_UniqueConstraint");
            //----------- Defining CustomerAccount Entity - End --------------

            //----------- Defining AccountRate Entity - Start --------------------
            //Make the AccountRateId a Primary Key 
            modelBuilder.Entity<AccountRate>()
                .HasKey(input => input.AccountRateId)
                .HasName("PrimaryKey_AccountRateId");

            //Make the AccountRateId an Auto-Number
            modelBuilder.Entity<AccountRate>()
                .Property(input => input.AccountRateId)
                .HasColumnName("AccountRateId")
                .HasColumnType("INT")
                .UseSqlServerIdentityColumn()
                .ValueGeneratedOnAdd()
                .IsRequired(true);

            modelBuilder.Entity<AccountRate>()
                .Property(input => input.EffectiveStartDate)
                .HasColumnName("EffectiveStartDate")
                .IsRequired(true);

            modelBuilder.Entity<AccountRate>()
                .Property(input => input.EffectiveEndDate)
                .HasColumnName("EffectiveEndDate")
                .IsRequired(false);

            modelBuilder.Entity<AccountRate>()
                .Property(input => input.RatePerHour)
                .HasColumnName("RatePerHour")
                .HasColumnType("DECIMAL(6,2)")
                .IsRequired(true);

            //----------- Defining AccountRate Entity - End ---------------------

            //----------- Defining UserInfo Entity - Start --------------
            //Make the UserInfotId as  Primary Key 
            modelBuilder.Entity<UserInfo>()
                .HasKey(input => input.UserInfoId)
                .HasName("PrimaryKey_UserInfoId");

            modelBuilder.Entity<UserInfo>()
                .Property(input => input.UserInfoId)
                .HasColumnName("UserInfoId")
                .HasColumnType("int")
                .UseSqlServerIdentityColumn()
                .ValueGeneratedOnAdd()
                .IsRequired(true);

            modelBuilder.Entity<UserInfo>()
                      .Property(input => input.LoginUserName)
                      .HasColumnName("LoginUserName")
                      .HasColumnType("VARCHAR(10)")
                      .IsRequired(true);

            modelBuilder.Entity<UserInfo>()
                .Property(input => input.FullName)
                .HasColumnName("FullName")
                .HasColumnType("VARCHAR(100)")
                .IsRequired(true);

            modelBuilder.Entity<UserInfo>()
                .Property(input => input.Email)
                .HasColumnName("Email")
                .HasColumnType("VARCHAR(50)")
                .IsRequired(true);

            modelBuilder.Entity<UserInfo>()
                .Property(input => input.IsActive)
                .HasColumnName("IsActive")
                .HasColumnType("bit")
                .HasDefaultValue(true)
                .IsRequired(true);

            //Enforce unique constraint on StaffId
            modelBuilder.Entity<UserInfo>()
             .HasIndex(input => input.LoginUserName).IsUnique()
             .HasName("UserInfo_LoginUserName_UniqueConstraint");

            //----------- Defining UserInfo Entity - End --------------

            //----------- Defining InstructorAccount Entity - Start --------------
            //Make the InstructorAccountId as  Primary Key 
            modelBuilder.Entity<InstructorAccount>()
                .HasKey(input => input.InstructorAccountId)
                .HasName("PrimaryKey_InstructorAccounttId");

            modelBuilder.Entity<InstructorAccount>()
                .Property(input => input.InstructorAccountId)
                .HasColumnName("InstructorAccountId")
                .HasColumnType("int")
                .UseSqlServerIdentityColumn()
                .ValueGeneratedOnAdd()
                .IsRequired(true);

            modelBuilder.Entity<InstructorAccount>()
                .Property(input => input.EffectiveStartDate)
                .HasColumnName("EffectiveStartDate")
                .IsRequired(true);

            modelBuilder.Entity<InstructorAccount>()
                .Property(input => input.EffectiveEndDate)
                .HasColumnName("EffectiveEndDate")
                .IsRequired(false);

            modelBuilder.Entity<InstructorAccount>()
                .Property(input => input.InstructorId)
                .HasColumnName("InstructorId")
                .HasColumnType("int")
                .IsRequired(true);

            modelBuilder.Entity<InstructorAccount>()
                .Property(input => input.CustomerAccountId)
                .HasColumnName("CustomerAccountId")
                .HasColumnType("int")
                .IsRequired(true);

            modelBuilder.Entity<InstructorAccount>()
                .Property(input => input.WageRate)
                .HasColumnName("WageRate")
                .HasColumnType("decimal(5,2)")
                .IsRequired(true);

            modelBuilder.Entity<InstructorAccount>()
                .Property(input => input.IsCurrent)
                .HasColumnName("IsCurrent")
                .HasColumnType("bit")
                .HasDefaultValue(true)
                .IsRequired(true);

            modelBuilder.Entity<InstructorAccount>()
                .Property(input => input.Comments)
                .HasColumnName("Comments")
                .HasColumnType("NVARCHAR(4000)")
                .IsRequired(false);
            //----------- Defining InstructorAccount Entity - End --------------

            //----------- Defining AccountDetail Entity - Start --------------
            //Make the AccountDetailId a  Primary Key 
            modelBuilder.Entity<AccountDetail>()
                .HasKey(input => input.AccountDetailId)
                .HasName("PrimaryKey_AccountDetailId");

            //Make the AccountDetailId an Auto-Number
            modelBuilder.Entity<AccountDetail>()
                .Property(input => input.AccountDetailId)
                .HasColumnName("AccountDetailId")
                .HasColumnType("int")
                .UseSqlServerIdentityColumn()
                .ValueGeneratedOnAdd()
                .IsRequired(true);

            modelBuilder.Entity<AccountDetail>()
                .Property(input => input.EffectiveStartDate)
                .HasColumnName("EffectiveStartDate")
                .IsRequired(true);

            modelBuilder.Entity<AccountDetail>()
                .Property(input => input.EffectiveEndDate)
                .HasColumnName("EffectiveEndDate")
                .IsRequired(false);

            modelBuilder.Entity<AccountDetail>()
                .Property(input => input.StartTimeInMinutes)
                .HasColumnName("StartTimeInMinutes")
                .HasColumnType("int")
                .IsRequired(true);

            modelBuilder.Entity<AccountDetail>()
                .Property(input => input.EndTimeInMinutes)
                .HasColumnName("EndTimeInMinutes")
                .HasColumnType("int")
                .IsRequired(true);

            modelBuilder.Entity<AccountDetail>()
                .Property(input => input.IsVisible)
                .HasColumnName("IsVisible")
                .HasColumnType("bit")
                .HasDefaultValue(true)
                .IsRequired(true);

            //----------- Defining AccountDetail Entity - End --------------

            //----------- Defining TimeSheet Entity - Start --------------
            //Make the TimeSheetId as  Primary Key 
            modelBuilder.Entity<TimeSheet>()
                .HasKey(input => input.TimeSheetId)
                .HasName("PrimaryKey_TimeSheetId");

            //Make the TimeSheetId an Auto-Number field.
            modelBuilder.Entity<TimeSheet>()
                .Property(input => input.TimeSheetId)
                .HasColumnName("TimeSheetId")
                .HasColumnType("int")
                .UseSqlServerIdentityColumn()
                .ValueGeneratedOnAdd()
                .IsRequired(true);

            modelBuilder.Entity<TimeSheet>()
                .Property(input => input.MonthAndYear)
                .HasColumnName("MonthAndYear")
                .IsRequired(true);

            modelBuilder.Entity<TimeSheet>()
                .Property(input => input.VerifiedAndSubmittedAt)
                .HasColumnName("VerifiedAndSubmittedAt")
                .IsRequired(false);

            modelBuilder.Entity<TimeSheet>()
                .Property(input => input.UpdatedById)
                .HasColumnName("UpdatedById")
                .HasColumnType("int")
                .IsRequired(true);

            modelBuilder.Entity<TimeSheet>()
                .Property(input => input.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasDefaultValueSql("GetDate()");

            modelBuilder.Entity<TimeSheet>()
                .Property(input => input.UpdatedAt)
                .HasColumnName("UpdatedAt")
                .HasDefaultValueSql("GetDate()");

            modelBuilder.Entity<TimeSheet>()
               .Property(input => input.RatePerHour)
               .HasColumnName("RatePerHour")
               .HasColumnType("decimal(6,2)")
               .IsRequired(true);
            //----------- Defining TimeSheet Entity - End --------------

            //----------- Defining TimeSheetDetail Entity - Start --------------
            //Make the TimeSheetDetailId as  Primary Key 
            modelBuilder.Entity<TimeSheetDetail>()
                .HasKey(input => input.TimeSheetDetailId)
                .HasName("PrimaryKey_TimeSheetDetailId");

            //Make the TimeSheetDetailId an Auto-Number field.
            modelBuilder.Entity<TimeSheetDetail>()
                .Property(input => input.TimeSheetDetailId)
                .HasColumnName("TimeSheetDetailId")
                .HasColumnType("int")
                .UseSqlServerIdentityColumn()
                .ValueGeneratedOnAdd()
                .IsRequired(true);

            modelBuilder.Entity<TimeSheetDetail>()
                .Property(input => input.SessionSynopsisNames)
                .HasColumnName("SessionSynopsisNames")
                .HasColumnType("VARCHAR(300)")
                .IsRequired(true);

            modelBuilder.Entity<TimeSheetDetail>()
                .Property(input => input.OfficialTimeInMinutes)
                .HasColumnName("OfficialTimeInMinutes")
                .IsRequired(true);

            modelBuilder.Entity<TimeSheetDetail>()
                .Property(input => input.OfficialTimeOutMinutes)
                .HasColumnName("OfficialTimeOutMinutes")
                .IsRequired(true);

            modelBuilder.Entity<TimeSheetDetail>()
                .Property(input => input.IsReplacementInstructor)
                .HasColumnName("IsReplacementInstructor")
                .HasDefaultValue(false)
                .IsRequired(true);

             modelBuilder.Entity<TimeSheetDetail>()
                .Property(input => input.WageRatePerHour)
                .HasColumnName("WageRatePerHour")
                .HasColumnType("decimal(6,2)")
                .IsRequired(true);

            //----------- Defining TimeSheetDetail Entity - End --------------

            //----------- Definining TimeSheetDetailSignature - Start --------------
            //Make the TimeSheetSignatureId as  Primary Key 
            modelBuilder.Entity<TimeSheetDetailSignature>()
                .HasKey(input => input.TimeSheetDetailSignatureId)
                .HasName("PrimaryKey_TimeSheetSignatureId");

            //Make the TimeSheetSignatureId an Auto-Number field.
            modelBuilder.Entity<TimeSheetDetailSignature>()
                .Property(input => input.TimeSheetDetailSignatureId)
                .HasColumnName("TimeSheetDetailSignatureId")
                .HasColumnType("INT")
                .UseSqlServerIdentityColumn()
                .ValueGeneratedOnAdd()
                .IsRequired(true);

            modelBuilder.Entity<TimeSheetDetailSignature>()
               .Property(input => input.Signature)
               .HasColumnType("VARBINARY(MAX)")
               .IsRequired(false);
            //----------- Definining TimeSheetDetailSignature - End --------------
            //----------------------------------------------------------------------------
            //Relationship database modeling
            //----------------------------------------------------------------------------
            //many-to-one relationship between AccountDetail and CustomerAccount
            //
            //Missing relationship code

            //-------------------------------------------------------------------------------------------
            //Note: InstructorAccount is a join table
            //--------------------------------------------------------------------------------------------
            //many-to-one relationship between InstructorAccount and CustomerAccount
            modelBuilder.Entity<InstructorAccount>()
              .HasOne(input => input.CustomerAccount)
              .WithMany(input => input.InstructorAccounts)
              .HasForeignKey(input => input.CustomerAccountId);

            //many-to-one relationship between InstructorAccount and UserInfo
            modelBuilder.Entity<InstructorAccount>()
              .HasOne(input => input.Instructor)
              .WithMany(input => input.InstructorAccounts)
              .HasForeignKey(input => input.InstructorId);

            //many-to-one relationship between TimeSheetDetail and AccountDetail
            modelBuilder.Entity<TimeSheetDetail>()
              .HasOne(input => input.AccountDetail)
              .WithMany(input => input.TimeSheetDetails)
              .HasForeignKey(input => input.AccountDetailId);

            //many-to-one relationship between TimeSheetDetail and TimeSheet
            //for the child-parent relationship
            modelBuilder.Entity<TimeSheetDetail>()
              .HasOne(input => input.TimeSheet)
              .WithMany(input => input.TimeSheetDetails)
              .HasForeignKey(input => input.TimeSheetId);
            
            //many-to-one relationship between TimeSheet and UserInfo
            //for the createdby relationship
            //Usually this relationship links to the administrator user inside
            //that UserInfo table.
            modelBuilder.Entity<TimeSheet>()
              .HasOne(input => input.CreatedBy)
                .WithMany()
                .HasForeignKey(input => input.CreatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            //many-to-one relationship between TimeSheet and UserInfo
            //for the approval relationship
            modelBuilder.Entity<TimeSheet>()
              .HasOne(input => input.ApprovedBy)
                .WithMany()
                .HasForeignKey(input => input.ApprovedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            //many-to-one relationship between TimeSheet and UserInfo
            modelBuilder.Entity<TimeSheet>()
              .HasOne(input => input.UpdatedBy)
                .WithMany()
                .HasForeignKey(input => input.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            //foreign key relationship for CustomerAccount with the UserInfo table.
            modelBuilder.Entity<CustomerAccount>()
                 .HasOne(input => input.CreatedBy)
                 .WithMany()
                 .HasForeignKey(input => input.CreatedById)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();

            modelBuilder.Entity<CustomerAccount>()
                .HasOne(input => input.UpdatedBy)
                .WithMany()
                .HasForeignKey(input => input.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            //foreign key relationship for SessionSynopsis with the UserInfo table.
            modelBuilder.Entity<SessionSynopsis>()
                 .HasOne(input => input.CreatedBy)
                 .WithMany()
                 .HasForeignKey(input => input.CreatedById)
                 .OnDelete(DeleteBehavior.Restrict)
                 .IsRequired();

            modelBuilder.Entity<SessionSynopsis>()
                .HasOne(input => input.UpdatedBy)
                .WithMany()
                .HasForeignKey(input => input.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            //----------------------------------------------------------------------------
            //Define one-to-one relationship between TimeSheetDetail and TimeSheetDetailSignaure
            //----------------------------------------------------------------------------
            //Reference: http://stackoverflow.com/questions/35506158/one-to-one-relationships-in-entity-framework-7-code-first
            modelBuilder.Entity<TimeSheetDetail>()
            .HasOne(input => input.TimeSheetDetailSignature)
            .WithOne(input => input.TimeSheetDetail)
            .HasForeignKey<TimeSheetDetailSignature>(input => input.TimeSheetIDetailId);

            //Define many-to-one relationship between TimeSheet and UserInfo
            modelBuilder.Entity<TimeSheet>()
            .HasOne(input => input.Instructor)
            .WithMany(input => input.TimeSheets)
            .HasForeignKey(input => input.InstructorId);

            //Define many-to-one relationship between AccountRate and CustomerAccount
            //-- Missing relationship

            
            //The following two lines need to be copied into the Setupdb file.
            /*
            migrationBuilder.Sql(File.ReadAllText("migrations/setup_AspNetUsers_Insert_trigger.sql"));
            migrationBuilder.Sql(File.ReadAllText("migrations/setup_AspNetUsers_Update_trigger.sql"));
            migrationBuilder.Sql(File.ReadAllText("migrations/setup_convert_HHMM_to_minutes.sql"));
            migrationBuilder.Sql(File.ReadAllText("migrations/setup_convert_minutes_to_HHMM.sql"));
            */
            base.OnModelCreating(modelBuilder);
        }
    }
}
