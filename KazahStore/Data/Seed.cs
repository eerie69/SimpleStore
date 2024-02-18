using KazahStore.Data.Enum;
using KazahStore.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Diagnostics;
using System.Net;

namespace KazahStore.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Items.Any())
                {
                    context.Items.AddRange(new List<Store>()
                    {
                        new Store()
                        {
                            Title = "Wow CATACLYZM CLASSIC",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "Почти любой способ оплаты/Almost any payment method",
                            StoreCategory = StoreCategory.Games,
                            
                         },
                        new Store()
                        {
                            Title = "WoW SUBSCRIPTIONS",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "Почти любой способ оплаты/Almost any payment method",
                            StoreCategory = StoreCategory.Subscription,
                            
                        },
                        new Store()
                        {
                            Title = "WoW The War Within(Turkish region)",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "Почти любой способ оплаты/Almost any payment method",
                            StoreCategory = StoreCategory.Games,
                            
                        },
                        new Store()
                        {
                            Title = "WoW The War Within(Kazakh region)",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first club",
                            StoreCategory = StoreCategory.Games,
                            
                        }
                    });
                    context.SaveChanges();
                }
                //Trades
                if (!context.Trades.Any())
                {
                    context.Trades.AddRange(new List<Trade>()
                    {
                        new Trade()
                        {
                            Title = "Продам Варлока 417 гир, 2632 рио, бис тринькетки",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "по вопросам в лс",
                            TradeCategory = TradeCategory.Accounts,

                        },
                        new Trade()
                        {
                            Title = "Продам Варлока 417 гир, 2632 рио, бис тринькетки 2",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first race",
                            TradeCategory = TradeCategory.Accounts,
                        }

                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "kazahorsuperadmin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "karolshamanov",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
