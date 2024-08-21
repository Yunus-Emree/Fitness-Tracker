using Fitness_Tracker.Data;
using Fitness_Tracker.Entities;

namespace AspNetCoreMvc_IdentityAuthenticationApp.Extensions
{
    public static class StartExtensions
    {
        public static void AddIdentityExtensions(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(
                opt =>
                {
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequiredLength = 3;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireDigit = false;

                    opt.User.RequireUniqueEmail = true;  //aynı email adresinin girilmesine izin vermez.

                    //opt.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvyzqw0123456789";  //Kullanıcı adı girilirken sadece bu karakterlere izin verir.

                    opt.Lockout.MaxFailedAccessAttempts = 3; //default=5
                    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1); //default=5
                }
            ).AddEntityFrameworkStores<FitnessTrackerDbContext>();

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Auth/Login");
                opt.LogoutPath = new PathString("/Auth/Logout");
                //opt.AccessDeniedPath = new PathString("/Account/AccessDenied");
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                opt.SlidingExpiration = true; //10 dk. dolmadan kullanıcı yeniden login olursa süre baştan başlar.

                opt.Cookie = new CookieBuilder()
                {
                    Name = "Identity.App.Cookie",
                    HttpOnly = true
                };
            });
        }
    }
}
