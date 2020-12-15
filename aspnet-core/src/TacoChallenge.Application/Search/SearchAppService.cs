using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using TacoChallenge.Models;
using TacoChallenge.Search.Dto;

namespace TacoChallenge.Search
{
    [AbpAllowAnonymous]
    public class SearchAppService : ApplicationService, ISearchAppService
    {
        private readonly ISearchDataProvider _searchDataProvider;
        private readonly IFullTextSearchEngine _ftsEngine;
        private readonly ISearchManager _searchManager;

        public SearchAppService(ISearchDataProvider searchDataProvider, IFullTextSearchEngine ftsEngine, ISearchManager searchManager)
        {
            _searchDataProvider = searchDataProvider;
            _ftsEngine = ftsEngine;
            _searchManager = searchManager;
        }

        public async Task<IList<SearchResultDto>> Search(string searchText)
        {
            //This is here just for convenience to avoid create FTS indexes manually
            if (!_ftsEngine.DoIndexesExist())
            {
               await CreateFtsIndexesAsync();
            }

            var result = new List<SearchResultDto>();
            var restaurants = _searchManager.SearchRestaurants(searchText).OrderByDescending(x => x.Value.Count).ThenBy(x => x.Key.Rank);
            var foundCategories = _searchManager.SearchCategories(searchText);

            //TODO: Automapper could be used
            foreach (KeyValuePair<Restaurant, List<MenuItem>> keyValuePair in restaurants)
            {
                var resultDto = new SearchResultDto
                {
                    Id = keyValuePair.Key.Id,
                    LogoPath = keyValuePair.Key.LogoPath,
                    Rank = keyValuePair.Key.Rank,
                    RestaurantName = keyValuePair.Key.Name,
                    Suburb = keyValuePair.Key.Suburb
                };

                result.Add(resultDto);

                foreach (MenuItem menuItem in keyValuePair.Value)
                {
                    var category = foundCategories.FirstOrDefault(x => x.Name == menuItem.Category.Name);
                    var menuItemDto = new MenuItemDto
                    {
                        Id = menuItem.Id,
                        Name = menuItem.Name,
                        Price = menuItem.Price
                    };

                    if (category != null)
                    {
                        var categoryDto = resultDto.CategoryGroups.FirstOrDefault(x => x.Id == category.Id);

                        if (categoryDto == null)
                        {
                            categoryDto = new CategoryGroupDto
                            {
                                Name = category.Name,
                                Id = category.Id
                            };

                            resultDto.CategoryGroups.Add(categoryDto);
                        }

                        categoryDto.MenuItems.Add(menuItemDto);
                    }
                    else
                    {
                        resultDto.MenuItems.Add(menuItemDto);
                    }
                }
            }

            return result;
        }

        public async Task CreateOrder(List<MenuItemDto> orderedItems)
        {
            //logic to place an order

            await Task.Yield();
        }

        //This is just for Demo purpose. The FTS indexing usually can run by some background job. In ABP the Hangfire is integrated
        public async Task RunFtsIndexing()
        {
            await CreateFtsIndexesAsync();
        }


        private async Task CreateFtsIndexesAsync()
        {
            var data = _searchDataProvider.GetRestaurants().ToList();

            _ftsEngine.IndexMenuItems(data);
            _ftsEngine.IndexCategories(data);
        }
    }
}

