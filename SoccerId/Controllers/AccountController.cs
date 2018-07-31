using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SoccerId.Entities;
using SoccerId.Models;
using SoccerId.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SoccerId.Controllers
{
    public class AccountController : Controller
    {

        //private IRepository<Team> _teamRepository;
        private UserManager _userManager;
        private RoleManager _roleManager;
        private IAuthenticationManager _authenticationManager;

        public AccountController() { }
        public AccountController(
            //IRepository<Team> teamRepository,
            UserManager userManager,
            RoleManager roleManager
            )
        {
            //this._teamRepository = teamRepository;
            this._roleManager = roleManager;
            this._userManager = userManager;
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

        /// ///////////////////////////////////////    Login   //////////////////////////////////////////////

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await UserManager.FindAsync(model.Email, model.Password);
                if (user != null)
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(
                                            user,
                                            DefaultAuthenticationTypes.ApplicationCookie
                                            );
                    AuthenticationManager.SignOut(); //удаляем ранее определенные куки
                    AuthenticationManager.SignIn(new AuthenticationProperties // устанавливаем новые куки
                    {
                        IsPersistent = true,
                    }, claim);

                    IList<string> roles = new List<string> { };
                    UserManager userManager = HttpContext.GetOwinContext().GetUserManager<UserManager>();
                    User users = userManager.FindByEmail(User.Identity.Name);
                    roles = userManager.GetRoles(user.Id);
                    foreach (var item in roles)
                    {
                        if (item == "admin")
                        {
                            return RedirectToAction("AdminPage", "Home");
                        }
                        else if (item == "teamManager")
                        {
                            return RedirectToAction("TeamPage", "Home");
                        }
                        else if (item == "leagueManager")
                        {
                            return RedirectToAction("LeaguePage", "Home");
                        }
                        else { return RedirectToAction("UserPage", "Home"); }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
            }

            return View(model);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(LoginModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user = await UserManager.FindAsync(model.Email, model.Password);
        //        if (user == null)
        //        {
        //            ModelState.AddModelError("", "Неверный логин или пароль.");
        //        }
        //        else
        //        {
        //            ClaimsIdentity claim = await UserManager.CreateIdentityAsync(
        //                                    user,
        //                                    DefaultAuthenticationTypes.ApplicationCookie
        //                                    );
        //            AuthenticationManager.SignOut(); //удаляем ранее определенные куки
        //            AuthenticationManager.SignIn(new AuthenticationProperties // устанавливаем новые куки
        //            {
        //                IsPersistent = true,
        //            }, claim);

        //            return RedirectToAction("AdminIndex", "Home");
        //        }
        //    }

        //    return View(model);
        //}

        /// //////////////////////////////////////  LogOff  //////////////////////////////////////////////////

        public ActionResult LogOff()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff(RegisterModel model)
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        /// //////////////////////////////////////  Register  //////////////////////////////////////////////////

        // GET: Account
        public ActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<Team> teams = db.Teams;

                User customer = new User
                {
                    UserName = model.Email,
                    Email = model.Email,

                };
                IdentityResult result = await UserManager.CreateAsync(customer, model.Password);

                if (result.Succeeded)
                {

                    await UserManager.AddToRoleAsync(customer.Id, "player");
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    return RedirectToAction("About", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                    }
                }

            }
            return View(model);
        }

        //////////////////////////////// SelectTeam  ////////////////////////////////////////////

        SoccerIdDbContext db = new SoccerIdDbContext();

        public ActionResult SelectTeam()
        {
            SelectList teams = new SelectList(db.Teams);
            ViewBag.Teams = teams;
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var tp in db.Teams.ToList())
            {
                SelectListItem li = new SelectListItem
                {
                    Value = tp.TeamName,
                    Text = tp.TeamName
                };
                items.Add(li);
            }
            ViewBag.Teams = items;



            return View(db.Teams.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectTeam(string TeamName)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            ViewBag.Teams = items;
            return View();
        }



        // /// ///////////////////////////////////////////////   EDIT  ////////////////////////////////////////////////////////////////////

        //public async Task<ActionResult> Edit()
        //{
        //    User user = await UserManager.FindByEmailAsync(User.Identity.Name);
        //    if (user != null)
        //    {
        //        EdmModel model = new EdmModel { Year = user.Year };
        //        return View(model);
        //    }
        //    return RedirectToAction("Login", "Account");
        //}

        //[HttpPost]
        //public async Task<ActionResult> Edit(EdmModel model)
        //{
        //    User user = await UserManager.FindByEmailAsync(User.Identity.Name);
        //    if (user != null)
        //    {
        //        user.Year = model.Year;
        //        IdentityResult result = await UserManager.UpdateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("AdminIndex", "Home");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Что-то пошло не так");
        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError("", "Пользователь не найден");
        //    }

        //    return View(model);
        //}


        // // ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public  ActionResult Register(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        User customer = new User
        //        {
        //            UserName = model.Email,
        //            Email = model.Email
        //        };
        //        UserLoginInfo uli = new UserLoginInfo("dd", "dd");
        //        var result =  UserManager.Find(uli);

        //        if (result == null)
        //        {
        //            return RedirectToAction("AdminIndex", "Home");
        //        }
        //        else
        //        {
        //            return RedirectToAction("Register", "Account");
        //        }

        //    }
        //    return View(model);
        //}


    }
}