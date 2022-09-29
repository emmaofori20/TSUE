using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Models;
using TSUE.Utils;
using TSUE.ViewModels;

namespace TSUE.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        //public AccountController(UserManager<IdentityUser> userManager,
        //                         SignInManager<IdentityUser> signInManager)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //}

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            string url = "";

            try
            {
                
                string finalURL = "";

                var baseUrl = AppHttpContext.AppBaseUrl(HttpContext);

                var userClaim = User.Claims.Where(x => x.Type.ToLower() == "Name".ToLower()).FirstOrDefault();

                if (userClaim == null)
                {
                    return await Logout();
                }

                string username = userClaim.Value;
                url = User.Claims.Where(x => x.Type.ToLower() == "PathURL".ToLower()).FirstOrDefault().Value;
                
                finalURL = baseUrl + url;

                return Redirect(finalURL);
            }
            catch (Exception ex)
            {
                /**/
                //return RedirectToAction("home", "error",new { area = "admin" });
                return await Logout();
            }
            //try
            //{
            //    return RedirectToAction("Index", "Admin");

            //}
            //catch (Exception err)
            //{
            //    var ErrorMessage = new ErrorViewModel()
            //    {
            //        RequestId = err.Message
            //    };
            //    return View("Error", ErrorMessage);
            //}
        }

        public async Task<IActionResult> Unauthorized()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
           // _ilogger.LogInformation("User logged out.");

            return SignOut("Cookies", "oidc");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
    }
}
