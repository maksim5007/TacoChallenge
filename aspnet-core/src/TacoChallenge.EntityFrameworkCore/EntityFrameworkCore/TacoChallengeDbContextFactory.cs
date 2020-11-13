using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TacoChallenge.Configuration;
using TacoChallenge.Web;

namespace TacoChallenge.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class TacoChallengeDbContextFactory : IDesignTimeDbContextFactory<TacoChallengeDbContext>
    {
        public TacoChallengeDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TacoChallengeDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            TacoChallengeDbContextConfigurer.Configure(builder, configuration.GetConnectionString(TacoChallengeConsts.ConnectionStringName));

            return new TacoChallengeDbContext(builder.Options);
        }
    }
}
