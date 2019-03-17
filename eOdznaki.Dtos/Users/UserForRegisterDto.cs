using System;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Dtos.Users
{
    public class UserForRegisterDto
    {
        public UserForRegisterDto()
        {
            Created = DateTime.Now;
        }

        [Required] public string UserName { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 64 characters.")]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage =
            "The Email field is invalid.")]
        public string Email { get; set; }
        public DateTime Created { get; set; }
    }
}