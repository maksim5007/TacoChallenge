using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TacoChallenge.EntityFrameworkCore;
using TacoChallenge.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace TacoChallenge.Web.Tests
{
    [DependsOn(
        typeof(TacoChallengeWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class TacoChallengeWebTestModule : AbpModule
    {
        public TacoChallengeWebTestModule(TacoChallengeEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TacoChallengeWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(TacoChallengeWebMvcModule).Assembly);
        }
    }
}