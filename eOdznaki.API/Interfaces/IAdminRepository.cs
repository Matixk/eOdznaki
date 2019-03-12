using System.Collections.Generic;
using System.Threading.Tasks;
using eOdznaki.Dtos;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;

namespace eOdznaki.Interfaces
{
    public interface IAdminRepository
    {
        Task<PagedList<UserWithRolesDto>> GetUsersWithRoles(UserRolesParams userRolesParams);
        Task<IEnumerable<string>> EditUserRoles(int userId, UserRolesListDto userRolesListDto);
    }
}