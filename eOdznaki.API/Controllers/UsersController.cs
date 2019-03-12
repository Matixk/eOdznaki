using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using eOdznaki.Dtos;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;
using eOdznaki.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eOdznaki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUsersRepository usersRepository;

        public UsersController(IUsersRepository usersRepository, IMapper mapper)
        {
            this.usersRepository = usersRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UserParams userParams)
        {
            var users = await usersRepository.GetUsers(userParams);

            var usersToReturn = mapper.Map<IEnumerable<UserForPreviewDto>>(users);

            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await usersRepository.GetUser(id);

            var userToReturn = mapper.Map<UserForViewDto>(user);

            return Ok(userToReturn);
        }

        [Authorize(Policy = "RequireMemberRole")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if (IsNotAuthorized(id)) return Unauthorized();
            
            var userFromRepo = await usersRepository.GetUser(id);

            mapper.Map(userForUpdateDto, userFromRepo);

            return await usersRepository.SaveAll()
                ? (IActionResult) NoContent()
                : BadRequest("No changes were detected.");
        }

        [Authorize(Policy = "RequireMemberRole")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (IsNotAuthorized(id)) return Unauthorized();

            var userFromRepo = await usersRepository.GetUser(id);

            usersRepository.Delete(userFromRepo);

            if (await usersRepository.SaveAll()) return NoContent();

            return BadRequest("Error deleting the user.");
        }

        private bool IsNotAuthorized(int id)
        {
            return id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) && !User.IsInRole("Admin");
        }
    }
}