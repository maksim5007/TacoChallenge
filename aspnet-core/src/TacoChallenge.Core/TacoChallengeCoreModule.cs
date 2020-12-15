using System;
using System.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using Castle.MicroKernel.Registration;
using TacoChallenge.Authorization.Roles;
using TacoChallenge.Authorization.Users;
using TacoChallenge.Configuration;
using TacoChallenge.Localization;
using TacoChallenge.MultiTenancy;
using TacoChallenge.Search;
using TacoChallenge.Timing;

namespace TacoChallenge
{
    [DependsOn(typeof(AbpZeroCoreModule))]
    public class TacoChallengeCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            TacoChallengeLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = TacoChallengeConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Settings.Providers.Add<AppSettingProvider>();
        }

        public override void Initialize()
        {
            var searchProviderType = Type.GetType(ConfigurationManager.AppSettings.Get("SearchProviderType"));
            IocManager.IocContainer.Register(
                Component
                    .For<ISearchDataProvider>()
                    .ImplementedBy(searchProviderType)
            );

            IocManager.RegisterAssemblyByConvention(typeof(TacoChallengeCoreModule).GetAssembly());

        }

        public override void PostInitialize()
        {
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }
    }
}
