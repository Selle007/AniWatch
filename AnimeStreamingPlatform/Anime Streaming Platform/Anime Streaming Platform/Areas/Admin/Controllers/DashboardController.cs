﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AreaTest.Areas.Admin.Controllers {
    //Tregon qe ky controller gjendet ne Area admin
    [Area("Admin")]
    //Tregon qe useri duhet te jete i kyqyr dhe te kete rolin admin ne menyre qe te kete access
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller {
        private readonly UserManager<IdentityRole> usermanager;
        private readonly RoleManager<IdentityRole> rolemanager;
        public DashboardController(RoleManager<IdentityRole> _roleManager) {
            rolemanager = _roleManager;
        }
        public IActionResult Index() {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }

        //Metoda CreateRole na kthen View ku kemi formen e cila merr emrin e rolit qe deshirojme ta krijojme
        public IActionResult CreateRole() {
            return View();
        }

        //Metoda CreateRole ne method post na merr si parameter emrin e rolit dhe kerkon qe nese ky rol me kete
        //emer nuk egziston e krijon ne tabele
        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName) {

            if (!await rolemanager.RoleExistsAsync(roleName)) {
              
                await rolemanager.CreateAsync(new IdentityRole(roleName));
            }

            return View();
        }
    }
}
