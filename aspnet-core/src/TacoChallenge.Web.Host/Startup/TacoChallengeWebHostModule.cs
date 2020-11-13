using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TacoChallenge.Configuration;

namespace TacoChallenge.Web.Host.Startup
{
    [DependsOn(
       typeof(TacoChallengeWebCoreModule))]
    public class TacoChallengeWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public TacoChallengeWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TacoChallengeWebHostModule).GetAssembly());
        }
    }
}
