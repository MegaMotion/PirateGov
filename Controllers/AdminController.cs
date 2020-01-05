using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestCoreApp.Models;
using TestCoreApp.Models.ViewModels.Admin;
using TestCoreApp.Models.Entities;

namespace TestCoreApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly AppDbContext _db;

        public AdminController(RoleManager<IdentityRole> roleManager, 
                                UserManager<ApplicationUser> userManager, 
                                AppDbContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            _db = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied(AccessDeniedViewModel model)
        {
            return View(model);
        }

        [HttpGet]
        public IActionResult NotFound(NotFoundViewModel model)
        {
            return View(model);
        }
        

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Admin");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles.OrderByDescending(r => r.Name).ToList();
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                NotFoundViewModel notfound = new NotFoundViewModel { DesiredPage = "RoleEditCancel" };
                return View("NotFound", notfound);
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                NotFoundViewModel notfound = new NotFoundViewModel { DesiredPage = "RoleEditCancel" };
                return View("NotFound", notfound);
            }
            else
            {
                role.Name = model.RoleName;
            }

            var result = await roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("ListRoles");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                NotFoundViewModel notfound = new NotFoundViewModel { DesiredPage = "RoleEditCancel" };
                return View("NotFound", notfound);
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;

                }
                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role==null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found.";
                NotFoundViewModel notfound = new NotFoundViewModel { DesiredPage = "RoleEditCancel" };
                return View("NotFound", notfound);
            }

            for (int i=0; i<model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {                    
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if(result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            return View(_db.Users.ToList());
        }


        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                NotFoundViewModel notfound = new NotFoundViewModel { DesiredPage = "UserEditCancel" };
                return View("NotFound", notfound);
            }

            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName
            };


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                NotFoundViewModel notfound = new NotFoundViewModel { DesiredPage = "UserEditCancel" };
                return View("NotFound", notfound);
            }
            else
            {
                user.UserName = model.UserName;
            }

            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("ListUsers");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        //Temp: add warnings later, but this should do the job quick and dirty.
        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var surveyAnswers = _db.UserSurveyAnswers.Where(a => a.User == user).ToList();
            foreach (var ans in surveyAnswers)
            {
                _db.UserSurveyAnswers.Remove(ans);
            }
            await _db.SaveChangesAsync();
            await userManager.DeleteAsync(user);

            return RedirectToAction("ListUsers");
            //var user = await userManager.FindByIdAsync(id);
            //return View(model);
        }

        //[HttpPost]
        //public IActionResult DeleteUser(EditUserViewModel model)
        //{
        //    return RedirectToAction("ListUsers");
        //}



    }
}
