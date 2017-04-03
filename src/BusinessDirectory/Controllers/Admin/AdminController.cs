using BusinessDirectory.Models;
using BusinessDirectory.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDirectory.Controllers.Admin
{
    public class AdminController : Controller
    {
        private ILogger<AdminController> _logger;
        private SignInManager<BusinessUser> _signInManager;

        public AdminController(SignInManager<BusinessUser> signInManager, ILogger<AdminController> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "App");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login (LoginViewModel vm, string returnUrl)
        {
            var result = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password,true,false);
            if (result.Succeeded)
            {
                if (string.IsNullOrWhiteSpace(returnUrl))
                {
                    return RedirectToAction("myBusinesses", "App");
                }
                else
                {
                    return Redirect(returnUrl);
                }
            }
            else
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrectos");
            }

            return View(vm);
        }

    }
}
