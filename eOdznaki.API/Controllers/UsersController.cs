using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using eOdznaki.Configuration;
using eOdznaki.Dtos;
using eOdznaki.Dtos.Users;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;
using eOdznaki.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace eOdznaki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Cloudinary cloudinary;
        private readonly IOptions<CloudinarySettings> cloudinaryConfig;
        private readonly IMapper mapper;
        private readonly IUsersRepository usersRepository;

        public UsersController(IOptions<CloudinarySettings> cloudinaryConfig, IUsersRepository usersRepository,
            IMapper mapper
        )
        {
            this.cloudinaryConfig = cloudinaryConfig;
            this.usersRepository = usersRepository;
            this.mapper = mapper;
            
            var acc = new Account(
                cloudinaryConfig.Value.CloudName,
                cloudinaryConfig.Value.ApiKey,
                cloudinaryConfig.Value.ApiSecret
            );

            cloudinary = new Cloudinary(acc);
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
        [HttpPost("{id}/setAvatar")]
        public async Task<IActionResult> SetUserAvatar(int id, [FromForm] PhotoForUploadDto photoForUploadDto)
        {
            if (IsNotAuthorized(id)) return Unauthorized();

            var userFromRepo = await usersRepository.GetUser(id);
            
            var file = photoForUploadDto.File;

            if (file == null) return BadRequest("File not found.");
            
            var uploadResult = new ImageUploadResult();
            
            if (file.Length > 0)
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResult = cloudinary.Upload(uploadParams);
                }

            // photoForUploadDto.Url = uploadResult.Uri.ToString();
            // photoForUploadDto.PublicId = uploadResult.PublicId;
            
            userFromRepo.AvatarUrl = uploadResult.Uri.ToString();
                        
            return await usersRepository.SaveAll()
                ? (IActionResult) NoContent()
                : BadRequest("Error uploading the avatar.");
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