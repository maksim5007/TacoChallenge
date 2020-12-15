using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using TacoChallenge.Search.Dto;

namespace TacoChallenge.Search
{
    public interface ISearchAppService : IApplicationService
    {
        Task<IList<SearchResultDto>> Search(string searchText);
        Task RunFtsIndexing();
        Task CreateOrder(List<MenuItemDto> orderedItems);
    }
}
