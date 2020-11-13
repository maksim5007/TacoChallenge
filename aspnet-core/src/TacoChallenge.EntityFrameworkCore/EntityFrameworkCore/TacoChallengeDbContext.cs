using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using TacoChallenge.Authorization.Roles;
using TacoChallenge.Authorization.Users;
using TacoChallenge.MultiTenancy;

namespace TacoChallenge.EntityFrameworkCore
{
    public class TacoChallengeDbContext : AbpZeroDbContext<Tenant, Role, User, TacoChallengeDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public TacoChallengeDbContext(DbContextOptions<TacoChallengeDbContext> options)
            : base(options)
        {
        }
    }
}
