using System;
using System.Collections.Generic;
using System.Linq;
using TacoChallenge.Models;

namespace TacoChallenge.Search
{
    public class SearchManager : ISearchManager
    {
        private readonly ISearchDataProvider _dataProvider;
        private readonly IFullTextSearchEngine _fullTextSearchEngine;

        public SearchManager(ISearchDataProvider dataProvider, IFullTextSearchEngine fullTextSearchEngine)
        {
            _dataProvider = dataProvider;
            _fullTextSearchEngine = fullTextSearchEngine;
        }

        public IDictionary<Restaurant, List<MenuItem>> SearchRestaurants(string searchText)
        {
            var menuItemDocs = _fullTextSearchEngine.SearchMenuItems(searchText);

            var menuItemsGrouped = menuItemDocs.GroupBy(key => Int32.Parse(key.Get("RestaurantId")),
                element => new
                {
                    MenuItemId = Int32.Parse(element.Get("MenuItemId")),
                    CategoryName = element.Get("CategoryName")
                });

            var restaurants = _dataProvider.GetRestaurants()
                .Where(x => menuItemsGrouped.Any(group => group.Key == x.Id));

            var menuItems = _dataProvider.GetMenuItems();
            var categories = _dataProvider.GetCategories(); 

            var result = menuItemsGrouped.ToDictionary(key => restaurants.First(x => x.Id == key.Key),
                value =>
                {
                    var items =  menuItems.Where(mi => value.Any(x => x.MenuItemId == mi.Id)).ToList();
                    foreach (MenuItem menuItem in items)
                    {
                        var item = value.First(v => v.MenuItemId == menuItem.Id);
                        menuItem.Category = categories.FirstOrDefault(c => c.Name == item.CategoryName);
                    }

                    return items;
                });

            return result;
        }

        public IList<MenuCategory> SearchCategories(string searchText)
        {
            var categoryDocs = _fullTextSearchEngine.SearchCategories(searchText);
            var categoryNames = categoryDocs.Select(x => x.Get("CategoryName")).ToList();

            var categories = _dataProvider.GetCategories().Where(x => categoryNames.Contains(x.Name)).ToList();

            return categories;
        }
    }
}