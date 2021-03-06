﻿using BusinessDirectory.Models;
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
        private UserManager<BusinessUser> _userManager;

        public AdminController(SignInManager<BusinessUser> signInManager, ILogger<AdminController> logger, UserManager<BusinessUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
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
            var result = await _signInManager.PasswordSignInAsync(vm.Email, vm.Password,true,false);
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
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "App");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if(await _userManager.FindByEmailAsync(vm.Email) == null)
            {
                if (ModelState.IsValid)
                {
                    var user = new BusinessUser()
                    {
                        Name = vm.Name,
                        LastName = vm.LastName,
                        UserName = vm.Email,
                        Email = vm.Email,
                        PhoneNumber = vm.PhoneNumber

                    };
                    var result = await _userManager.CreateAsync(user, vm.Password);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("Index", "App");
                    }
                    else
                    {
                        _logger.LogInformation("Hubo un error al crear al usuario");
                        ModelState.AddModelError("", "Ocurrio un error al crear el usuario");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "El usuario ya existe intente denuevo");
            }
            return View(vm);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "App");
        }

    }
}
