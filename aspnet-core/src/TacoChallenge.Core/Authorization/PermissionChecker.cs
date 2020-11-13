using Abp.Authorization;
using TacoChallenge.Authorization.Roles;
using TacoChallenge.Authorization.Users;

namespace TacoChallenge.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
