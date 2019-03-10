using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet.Actions;
using eOdznaki.Dtos;
using eOdznaki.Interfaces;
using eOdznaki.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eOdznaki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUsersRepository usersRepository;
        private readonly UserManager<User> userManager;

        public UsersController(
            UserManager<User> userManager,
            IUsersRepository usersRepository,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.usersRepository = usersRepository;
            this.mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await usersRepository.GetUsers();

            var usersToReturn = mapper.Map<IEnumerable<UserForPreviewDto>>(users);

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
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) return Unauthorized();

            var userFromRepo = await usersRepository.GetUser(id);

            mapper.Map(userForUpdateDto, userFromRepo);

            return await usersRepository.SaveAll() ? (IActionResult) NoContent() : BadRequest("No changes were detected.");
        }

        [Authorize(Policy = "RequireMemberRole")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) return Unauthorized();

            var userFromRepo = await usersRepository.GetUser(id);

            usersRepository.Delete(userFromRepo);

            if (await usersRepository.SaveAll()) return NoContent();

            return BadRequest("Error deleting the message.");
        }

    }
}