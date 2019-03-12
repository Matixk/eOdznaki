using System.Collections.Generic;
using eOdznaki.Models;

namespace eOdznaki.Dtos
{
    public class UserWithRolesDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}