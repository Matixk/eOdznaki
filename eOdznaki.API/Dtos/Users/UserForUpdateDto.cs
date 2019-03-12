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

        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage =
            "The Email field is not a valid e-mail address.")]
        public string Email { get; set; }
    }
}