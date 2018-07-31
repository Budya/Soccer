using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using SoccerId.Entities;
using SoccerId.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SoccerId.Repositories;
using System.Threading.Tasks;

namespace SoccerId.Controllers
{
    public class HomeController : Controller
    {
        private TeamRepository _teamRepository;
        private UserManager _userManager;        
        private RoleManager _roleManager;
        private IAuthenticationManager _authenticationManager;
        private UserRepository _userRepository;
        private AgreedPlayersListRepository _agreedPlayersListRepository;

        public HomeController() { }
        /* Все объекты с окончанием Repository приходят в конструктор уже проинициализированными
         * это происходит в файле App_Start/Unity_Config.
         * ---------------------------------------------------------------------
         * Имена пользователей UserName - это емайлы
         * ПАРОЛЬ ДЛЯ ВСЕХ ПОЛЬЗОВАТЕЛЕЙ - P@ssword1
         * ----------------------------------------------------------------
         * то что вряд ли понадобится:
         * Вместо объектов  * IdentityUserRole * IdentityUserClaim * IdentityUserLogin * IdentityRole * UserStore
         * нужно использовать  AppUserLogin *  AppUserClaim *   AppUserLogin * AppRole * AppUserStore
         */
        public HomeController(
            TeamRepository teamRepository, 
            UserManager userManager,
            RoleManager roleManager,
            UserRepository userRepository,
            AgreedPlayersListRepository agreedPlayersListRepository
            )
        {
            this._teamRepository = teamRepository;
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._userRepository = userRepository;
            this._agreedPlayersListRepository = agreedPlayersListRepository;
        }              


        public UserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>();
            }
            private set { _userManager = value; }

        }
        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                return _authenticationManager ?? HttpContext.GetOwinContext().Authentication;
            }
            private set { _authenticationManager = value; }
        }

        public RoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<RoleManager>();
            }
            private set { _roleManager = value; }

        }

        public  ActionResult Index()
        {

            if (_agreedPlayersListRepository != null)
                Debug.WriteLine($">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>All currect");



            //for (int i = 1; i < 451; i++)
            //{
            //    UserManager.UpdateSecurityStamp(i);
            //    Debug.WriteLine($"User_{i} updated");
            //}



            //List<TeamLogo> logos = new List<TeamLogo>();
            //using (SoccerIdDbContext context = new SoccerIdDbContext())
            //{
            //    string folderPath = Server.MapPath(@"~/App_Data");
            //    var images = Directory.GetFiles(folderPath, "*.jpg");

            //    foreach (var image in images)
            //    {
            //        Debug.WriteLine(image);
            //        TeamLogo newLogo = new TeamLogo();
            //        newLogo.Image = System.IO.File.ReadAllBytes(image);
            //        newLogo.PhotoName = Path.GetFileNameWithoutExtension(image).Replace("Logo", "");
            //        //Debug.WriteLine(newLogo.PhotoName);
            //        logos.Add(newLogo);
            //    }

            //    images = Directory.GetFiles(folderPath, "*.png");

            //    foreach (var image in images)
            //    {
            //        //Debug.WriteLine(image);
            //        TeamLogo newLogo = new TeamLogo();
            //        newLogo.Image = System.IO.File.ReadAllBytes(image);
            //        newLogo.PhotoName = Path.GetFileNameWithoutExtension(image).Replace("Logo", "");
            //        //Debug.WriteLine(newLogo.PhotoName);
            //        logos.Add(newLogo);
            //    }
            //    context.Configuration.LazyLoadingEnabled = false;
            //    var query = from team in context.Teams.Include("_CurrentLeague")
            //                where team._CurrentLeague.Id == 1
            //                select team;

            //    var teams = query.ToList();

            //    for (int i = 0; i < 10; i++)
            //    {
            //        teams[i]._Logo = logos[i];
            //        context.Entry(teams[i]).State = System.Data.Entity.EntityState.Modified;
            //    }

            //    context.SaveChanges();

            //}
            //Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>Connect");
            return View();
        }

        //[Authorize(Roles ="admin")]
        public ActionResult GetUsers()        //получение инф-и о пользователях
        {

            List<User> users = new List<User>();
            using (SoccerIdDbContext db = new SoccerIdDbContext())
            {
                users = db.Users.ToList();
            }
            return View(users);
        }

        public ActionResult GetMyRoles()//статус пользователя
        {
            IList<string> roles = new List<string> { "Неизвестен" };
            UserManager userManager = HttpContext.GetOwinContext()
                                                    .GetUserManager<UserManager>();
            User user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                roles = userManager.GetRoles(user.Id);
            return View(roles);
        }


       

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult UserPage()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult TeamPage()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult LeaguePage()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult AdminPage()
        {
            ViewBag.Message = "Your Admin page.";

            return View();
        }

        // //////////////////////////////////////////////////   DELETE USER     //////////////////////////////////////////////////////////

        //[HttpGet]
        //public ActionResult DeleteModel()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ActionName("DeleteModel")]
        //public async Task<ActionResult> DeleteConfirmed()
        //{
        //    User user = await UserManager.FindByEmailAsync(User.Identity.Name);
        //    if (user != null)
        //    {
        //        IdentityResult result = await UserManager.DeleteAsync(user);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("AdminPage", "Home");
        //        }
        //    }
        //    return RedirectToAction("Error", "Shared");
        //}
    }
}