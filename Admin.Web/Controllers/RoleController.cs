using Common.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace WebApp.Controllers
{
    [Authorize(Roles =IdentityConstant.SuperAdmin )]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null && !_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
            {
                var role =  _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<ActionResult> DeleteRole(string RoleId)
        {
            if (ModelState.IsValid)
            {
                var roleToDelete = await _roleManager.FindByIdAsync(RoleId);

                if (roleToDelete == null)
                {
                    return NotFound();
                }

                var roleresult = await _roleManager.DeleteAsync(roleToDelete);

                if (roleresult.Succeeded)
                {
                    return RedirectToAction("Index", "Role");
                }
            }

            return RedirectToAction("Index");
        }

    }
}
