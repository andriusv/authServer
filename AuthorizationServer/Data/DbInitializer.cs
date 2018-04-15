using AuthorizationServer.Models;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppSettings _appSettings;

        public DbInitializer(
            UserDBContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _appSettings = appSettings.Value;
        }

        public async Task Initialize()
        {
            _context.Database.Migrate();

            //If there is already an Administrator role, abort
            if (!_context.Roles.Any(r => r.Name == "Administrator")) {
                var t = _roleManager.CreateAsync(new IdentityRole("Administrator")).GetAwaiter().GetResult();
            }         

            //Create the default Admin account and apply the Administrator role
            string username = _appSettings.AdminName;
            string email = _appSettings.AdminEmail;
            string password = _appSettings.AdminPassword;

            var admin = await _userManager.FindByNameAsync(username);
            if (admin == null)
            {
                try
                {
                    var newAdmin = _userManager.CreateAsync(new ApplicationUser { UserName = username, Email = email, EmailConfirmed = true }, password).GetAwaiter().GetResult();
                    var adminRole = _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(username), "Administrator").GetAwaiter().GetResult(); ;
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
