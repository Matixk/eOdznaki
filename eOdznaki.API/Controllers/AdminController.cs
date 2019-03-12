using System.Threading.Tasks;
using eOdznaki.Configuration;
using eOdznaki.Dtos;
using eOdznaki.Helpers.Params;
using eOdznaki.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design;

namespace eOdznaki.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("users")]
        public async Task<IActionResult> GetUsersWithRoles(UserRolesParams userRolesParams)
        {
            var usersWithRoles = await adminRepository.GetUsersWithRoles(userRolesParams);

            Response.AddPagination(usersWithRoles.CurrentPage, usersWithRoles.PageSize, usersWithRoles.TotalCount,
                usersWithRoles.TotalPages);

            return Ok(usersWithRoles);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("editRoles/{userId}")]
        public async Task<IActionResult> EditRoles(int userId, UserRolesListDto userRolesListDto)
        {
            try
            {
                return Ok(await adminRepository.EditUserRoles(userId, userRolesListDto));
            }
            catch (OperationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}