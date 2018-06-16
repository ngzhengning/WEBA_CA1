using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
//***************************************************************
//********* Additional namespaces which I have added to support the 
//                     application configuration to support security and authorization
using TimeSheetManagementSystem.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using TimeSheetManagementSystem.Models;
using TimeSheetManagementSystem.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TimeSheetManagementSystem.Models.Claims;
//***************************************************************
namespace TimeSheetManagementSystem
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            /* The following command can work too, to tell the web project how to connect to db
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            */
            services.AddDbContext<ApplicationDbContext>();
            services.AddSession();
            services.AddMemoryCache();
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddEntityFrameworkSqlServer();
            /* ------- The following code defines policy and authorization - Start  ------- */
            //Only allow authenticated users
            var defaultPolicy = new AuthorizationPolicyBuilder()
              .RequireAuthenticatedUser()
              .Build();
            services.AddScoped<Microsoft.AspNetCore.Identity.IUserClaimsPrincipalFactory<ApplicationUser>, AppClaimsPrincipalFactory>();
            services.AddAuthorization(options =>
            {
                // inline policies
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("ADMIN"));
                options.AddPolicy("RequireOfficerRole", policy => policy.RequireRole("INSTRUCTOR"));
            });
            /* ------- The following code defines policy and authorization - End  ------- */
            /* ------- The following code were modified (originally it was only 
                                                                 one command: services.AddMvc() ----- */
            services.AddMvc(setup =>
            {
                setup.Filters.Add(new AuthorizeFilter(defaultPolicy));
            });


            //Don't delete the following two commands.
            //Although my code does not support SMS or Email features. I have no time to debug why the
            //application has runtime error when the following two commands are removed.
            // Add application services. 
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseIdentity();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            
           
          // app.SeedData();//Remember to comment out this command if the database is not empty.
        }
    }
}
