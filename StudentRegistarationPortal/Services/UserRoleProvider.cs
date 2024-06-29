using DataAccessLayer.Context;
using System;
using System.Linq;
using System.Web.Security;

namespace StudentRegistarationPortal.Services
{
    public class UserRoleProvider : RoleProvider
    {
        private readonly StudentRPDbContext _dbContext;

        public UserRoleProvider()
        {
            _dbContext = new StudentRPDbContext(); 
        }

        public override string ApplicationName { get; set; }

        public override string[] GetRolesForUser(string username)
        {
            var userRoles = (from user in _dbContext.Users
                             join role in _dbContext.Roles on user.Id equals role.UserId
                             where user.UserName == username
                             select role.RoleName).ToArray();

            return userRoles;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}