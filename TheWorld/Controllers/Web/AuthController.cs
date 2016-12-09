using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AuthController : Controller
    {
        private readonly SignInManager<WorldUser> _signInManager;

        public AuthController(SignInManager<WorldUser> signInManager )
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                RedirectToAction("Trips", "App");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(viewModel.UserName, viewModel.Password, true, false);
                if (result.Succeeded)
                {
                    if (String.IsNullOrEmpty(returnUrl))
                    {
                        RedirectToAction("Trips", "App");
                    }
                    else
                    {
                        Redirect(returnUrl);
                    }
                    
                }
                else
                {
                   ModelState.AddModelError("", "Login or password incorrect");
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            if(User.Identity.IsAuthenticated)
                await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "App");
        }
    }
}
