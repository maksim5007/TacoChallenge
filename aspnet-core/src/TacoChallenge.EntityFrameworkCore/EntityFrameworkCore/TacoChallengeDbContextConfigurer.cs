using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace TacoChallenge.EntityFrameworkCore
{
    public static class TacoChallengeDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<TacoChallengeDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<TacoChallengeDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
