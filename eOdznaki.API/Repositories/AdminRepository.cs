using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eOdznaki.Dtos;
using eOdznaki.Dtos.Users;
using eOdznaki.Helpers;
using eOdznaki.Helpers.Params;
using eOdznaki.Interfaces;
using eOdznaki.Models;
using eOdznaki.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Design;

namespace eOdznaki.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DataContext context;
        private readonly UserManager<User> userManager;

        public AdminRepository(DataContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<PagedList<UserWithRolesDto>> GetUsersWithRoles(UserRolesParams userRolesParams)
        {
            var usersWithRoles = (from user in context.Users
                orderby user.UserName
                select new UserWithRolesDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Roles = (from userRole in user.UserRoles
                        join role in context.Roles
                            on userRole.RoleId
                            equals role.Id
                        select role.Name).ToList()
                }).AsQueryable();

            return await PagedList<UserWithRolesDto>.CreateAsync(usersWithRoles, userRolesParams.PageNumber,
                userRolesParams.PageSize);
        }

        public async Task<IEnumerable<string>> EditUserRoles(int userId, UserRolesListDto userRolesListDto)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            var userRoles = await userManager.GetRolesAsync(user);

            var selectedRoles = userRolesListDto.RoleNames;

            selectedRoles = selectedRoles ?? new string[] { };

            var result = await userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded) throw new OperationException("Failed to add the roles.");

            result = await userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded) throw new OperationException("Failed to remove the roles.");

            return await userManager.GetRolesAsync(user);
        }
    }
}