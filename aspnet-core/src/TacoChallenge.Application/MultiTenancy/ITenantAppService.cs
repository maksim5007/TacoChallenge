using Abp.Application.Services;
using TacoChallenge.MultiTenancy.Dto;

namespace TacoChallenge.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

