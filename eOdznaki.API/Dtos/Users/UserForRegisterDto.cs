using System;
using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Dtos
{
    public class UserForRegisterDto
    {
        public UserForRegisterDto()
        {
            Created = DateTime.Now;
        }

        [Required] public string UserName { get; set; }

        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters.")]
        [Required]
        public string Password { get; set; }

        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^male$|^female$", ErrorMessage = "The Gender field is not a valid gender.")]
        public string Gender { get; set; }

        [Required]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage =
            "The Email field is not a valid e-mail address.")]
        public string Email { get; set; }

        public DateTime Created { get; set; }
    }
}