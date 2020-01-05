using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestCoreApp.Models.ViewModels.Admin;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using TestCoreApp.Models.ViewModels.Account;
using System.IO;
using TestCoreApp.Models;
using TestCoreApp.Models.Entities;
using Stripe;
using Newtonsoft.Json;

namespace TestCoreApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        private readonly AppDbContext _db;

        public AccountController(UserManager<ApplicationUser> userManager, 
                                 RoleManager<IdentityRole> roleManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 AppDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            _db = context;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Welcome(WelcomeViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    PirateGold = 10
                };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);

                    result = await userManager.AddToRoleAsync(user, "User");
                    if (!result.Succeeded)
                    {
                        NotFoundViewModel notfound = new NotFoundViewModel { DesiredPage = "InitialUserRoleFailed" };
                        return RedirectToAction("NotFound", "Admin", notfound);
                    }

                    WelcomeViewModel welcome = new WelcomeViewModel { Message = "Welcome to Pirate Gov!!!" };
                    return RedirectToAction("Welcome", "Account", welcome);
                    
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public IActionResult Join()
        {


            return View();
        }

        //[HttpPost]
        //public IActionResult Join(JoinViewModel model)
        //{
        //    return View();
        //}


        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }


        [HttpGet]
        public IActionResult Poke()
        {
            _db.Locations.Add(new Location { Name = "Poke!" });
            _db.SaveChanges();
            System.Diagnostics.Debug.WriteLine("CALLING POKE AND ADDING LOCATION!!!!!");
            return RedirectToAction("index", "home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password,
                                                                        model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return RedirectToAction("index", "home");
        }
        
        [HttpPost]
        public ActionResult CustomerUpdated()
        {
            //var json = new StreamReader(Request.Body).ReadToEnd();

            //var stripeEvent = EventUtility.ParseEvent(json);

            //LogStripeEvent(stripeEvent, false, true);

            //_db.Locations.Add(new Location { Name = stripeEvent.Type });
            _db.Locations.Add(new Location { Name = "Customer Updated!!" });
            _db.SaveChanges();

            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult> AccountUpdated()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();
            var details = JsonConvert.DeserializeObject<dynamic>(json);
            var event_type = details.type;
            var org_version = details.api_version;
            var org_id = details.data.@object.id;

            //var stripeEvent = EventUtility.ParseEvent(json);


            //LogStripeEvent(stripeEvent, false, true);


            //_db.Locations.Add(new Location { Name = stripeEvent.Type });//Whoops! nope.
            string myName = "Account Updated!! - " + org_id;
            _db.Locations.Add(new Location { Name = org_id });// + event_type
            _db.SaveChanges();

            return Ok();
        }
    }
}
