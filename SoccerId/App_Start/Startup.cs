using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using SoccerId.Entities;
using SoccerId.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


//аттрибут, запусткающий кофиг. Owin при запуске приложения
[assembly: OwinStartup(typeof(SoccerId.Startup))]
namespace SoccerId{

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<SoccerIdDbContext>(SoccerIdDbContext.Create);//регистрация контекста
            app.CreatePerOwinContext<UserManager>(UserManager.Create);//регстрация мен. пользователей
            app.CreatePerOwinContext<RoleManager>(RoleManager.Create);
            app.CreatePerOwinContext<SignInManager>(SignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                 Provider = new CookieAuthenticationProvider
                 {
                     // Enables the application to validate the security stamp when the user logs in.
                     // This is a security feature which is used when you change a password or add an external login to your account.  
                     OnValidateIdentity = SecurityStampValidator
                .OnValidateIdentity<UserManager, User, int>(
                    validateInterval: TimeSpan.FromMinutes(30),
                    regenerateIdentityCallback: (manager, user) =>
                        user.GenerateUserIdentityAsync(manager),
                    getUserIdCallback: (id) => (id.GetUserId<int>()))
                 }

            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

        }
    }
}