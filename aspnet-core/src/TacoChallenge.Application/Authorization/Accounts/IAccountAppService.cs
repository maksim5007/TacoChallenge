using System.Threading.Tasks;
using Abp.Application.Services;
using TacoChallenge.Authorization.Accounts.Dto;

namespace TacoChallenge.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
