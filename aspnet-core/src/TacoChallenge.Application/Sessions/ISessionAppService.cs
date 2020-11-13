using System.Threading.Tasks;
using Abp.Application.Services;
using TacoChallenge.Sessions.Dto;

namespace TacoChallenge.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
