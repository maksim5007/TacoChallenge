using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TacoChallenge.Authorization;

namespace TacoChallenge
{
    [DependsOn(
        typeof(TacoChallengeCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class TacoChallengeApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<TacoChallengeAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(TacoChallengeApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
