using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace TacoChallenge.Controllers
{
    public abstract class TacoChallengeControllerBase: AbpController
    {
        protected TacoChallengeControllerBase()
        {
            LocalizationSourceName = TacoChallengeConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
