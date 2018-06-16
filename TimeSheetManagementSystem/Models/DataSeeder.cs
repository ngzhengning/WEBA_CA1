using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Globalization;
using TimeSheetManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
//Reference:
//http://stackoverflow.com/questions/34536021/seed-initial-data-in-entity-framework-7-rc-1-and-asp-net-mvc-6
namespace TimeSheetManagementSystem.Models
{
public static class DataSeeder
{
    public static async void SeedData(this IApplicationBuilder app)
    {
    var db = app.ApplicationServices.GetService<ApplicationDbContext>();

    //Caution: Clear all the tables in the database first.
    db.Database.Migrate();


     //RoleStore needs using Microsoft.AspNet.Identity.EntityFramework;
     var identityRoleStore = new RoleStore<IdentityRole>(db);
     var identityRoleManager = new RoleManager<IdentityRole>(identityRoleStore, null, null, null, null, null);


     //Defining user "roles" inside the AspNetRoles table.
     var adminRole = new IdentityRole { Name = "ADMIN" };
     await identityRoleManager.CreateAsync(adminRole);

     var officerRole = new IdentityRole { Name = "INSTRUCTOR" };
     await identityRoleManager.CreateAsync(officerRole);




     var userStore = new UserStore<ApplicationUser>(db);
     //The following command is different from the previous ASP.NET Identity. Asp.Net Core Identity's UserManager requires 9 arguments
     //instead of 10. I have deleted one "null" away from the input of the constructor.
     var userManager = new UserManager<ApplicationUser>(userStore, null, null, null, null, null, null, null, null);


     var kennyUser = new ApplicationUser { UserName = "88881", Email = "KENNY@FAIRYSCHOOL.COM", FullName = "KENNY" };
     PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
     kennyUser.PasswordHash = ph.HashPassword(kennyUser, "P@ssw0rd"); //More complex password
     await userManager.CreateAsync(kennyUser);

     await userManager.AddToRoleAsync(kennyUser, "ADMIN");



     var julietUser = new ApplicationUser { UserName = "88882", Email = "JULIET@FAIRYSCHOOL.COM", FullName = "JULIET" };
     julietUser.PasswordHash = ph.HashPassword(julietUser, "P@ssw0rd"); //More complex password
     await userManager.CreateAsync(julietUser);

     await userManager.AddToRoleAsync(julietUser, "ADMIN");



     var randyUser = new ApplicationUser { UserName = "88883", Email = "RANDY@HOTINSTRUCTOR.COM", FullName = "RANDY" };
     randyUser.PasswordHash = ph.HashPassword(randyUser, "P@ssw0rd"); //More complex password
     await userManager.CreateAsync(randyUser);

     await userManager.AddToRoleAsync(randyUser, "INSTRUCTOR");


     var thomasUser = new ApplicationUser { UserName = "88884", Email = "THOMAS@HOTINSTRUCTOR.COM", FullName = "THOMAS" };
     thomasUser.PasswordHash = ph.HashPassword(thomasUser, "P@ssw0rd"); //More complex password

     await userManager.CreateAsync(thomasUser);
     await userManager.AddToRoleAsync(thomasUser, "INSTRUCTOR");

     var benUser = new ApplicationUser { UserName = "88885", Email = "BEN@HOTINSTRUCTOR.COM", FullName = "BEN" };
     benUser.PasswordHash = ph.HashPassword(benUser, "P@ssw0rd"); //More complex password

     await userManager.CreateAsync(benUser);

     await userManager.AddToRoleAsync(benUser, "INSTRUCTOR");

     var gabrielUser = new ApplicationUser { UserName = "88886", Email = "GABRIEL@HOTINSTRUCTOR.COM", FullName = "GABRIEL" };
     gabrielUser.PasswordHash = ph.HashPassword(gabrielUser, "P@ssw0rd"); //More complex password

     //Use the UserManager class instance, userManager
     //which manages the many-to-many AspNetUserRoles table
     //to create a user, Gabriel.
     await userManager.CreateAsync(gabrielUser); //Add the user
                                                 //Use the UserManager class instance, userManager
                                                 //which also manages the many-to-many AspNetUserRoles table
                                                 //to create a relationship describing that, Gabriel user is an OFFICER role user
     await userManager.AddToRoleAsync(gabrielUser, "INSTRUCTOR");

     var fredUser = new ApplicationUser { UserName = "88887", Email = "FRED@HOTINSTRUCTOR.COM", FullName = "FRED" };
     fredUser.PasswordHash = ph.HashPassword(fredUser, "P@ssw0rd"); //More complex password

     //Use the UserManager class instance, userManager
     //which manages the many-to-many AspNetUserRoles table
     //to create a user, Fred.
     await userManager.CreateAsync(fredUser); //Add the user
                                              //Use the UserManager class instance, userManager
                                              //which also manages the many-to-many AspNetUserRoles table
                                              //to create a relationship describing that, Fred user is an OFFICER role user
     await userManager.AddToRoleAsync(fredUser, "INSTRUCTOR");

	

						db.SaveChanges();


   return;
}//End of SeedData() method




  
}
}




