using System.Collections.Generic;

namespace eOdznaki.Dtos.Users
{
    public class UserWithRolesDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}