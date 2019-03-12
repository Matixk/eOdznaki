using System.Linq;
using System.Threading.Tasks;
using eOdznaki.Dtos;
using eOdznaki.Models;
using eOdznaki.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eOdznaki.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly DataContext context;
        private readonly UserManager<User> userManager;

        public AdminController(
            DataContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("users")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var usersWithRoles = await (from user in context.Users
                orderby user.UserName
                select new
                {
                    user.Id,
                    user.UserName,
                    Roles = (from userRole in user.UserRoles
                        join role in context.Roles
                            on userRole.RoleId
                            equals role.Id
                        select role.Name).ToList()
                }).ToListAsync();

            return Ok(usersWithRoles);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("editRoles/{id}")]
        public async Task<IActionResult> EditRoles(string id, RoleEditDto roleEditDto)
        {
            var user = await userManager.FindByIdAsync(id);

            var userRoles = await userManager.GetRolesAsync(user);

            var selectedRoles = roleEditDto.RoleNames;

            selectedRoles = selectedRoles ?? new string[] { };

            var result = await userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded) return BadRequest("Failed to add to roles.");

            result = await userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded) return BadRequest("Failed to remove the roles.");

            return Ok(await userManager.GetRolesAsync(user));
        }
    }
}