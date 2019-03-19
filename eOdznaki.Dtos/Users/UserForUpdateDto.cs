using System.ComponentModel.DataAnnotations;

namespace eOdznaki.Dtos.Users
{
    public class UserForUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        [RegularExpression(
            @"^\s*(?:\+?(\d{1,3}))?([-. (]*(\d{3})[-. )]*)?((\d{3})[-. ]*(\d{2,4})(?:[-.x ]*(\d+))?)\s*$",
            ErrorMessage = "Invalid Phone Number.")]
        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage =
            "Invalid Email.")]
        public string Email { get; set; }
    }
}