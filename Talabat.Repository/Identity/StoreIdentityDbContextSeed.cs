using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public class StoreIdentityDbContextSeed
    {
        public static async Task SeedAppUserAsync(UserManager<AppUser> userManager) 
        {
            if (userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    Email = "tarekHesham@gmail.com",
                    DisplayName = "Tarek Hesham",
                    UserName = "Tarek_Hesham",
                    PhoneNumber = "01023456789",
                    Address = new Address()
                    {
                        FName = "Tarek",
                        LName = "Hesham",
                        City = "Cairo",
                        Street = "Taha Hussein",
                        Country = "Egypt"
                    }
                };
                await userManager.CreateAsync(user, "P@ssw0rd");
            }
           
        }
    }
}
