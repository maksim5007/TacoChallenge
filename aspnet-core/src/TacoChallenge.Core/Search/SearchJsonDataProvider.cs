using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TacoChallenge.Models;

namespace TacoChallenge.Search
{
    public class SearchJsonDataProvider : ISearchDataProvider
    {
        private readonly IQueryable<Restaurant> _restaurantSource;

        public SearchJsonDataProvider()
        {
            var restaurantsList =  JsonConvert.DeserializeObject<List<Restaurant>>(File.ReadAllText(TacoChallengeConsts.JsonDataFilePath));

            foreach (Restaurant restaurant in restaurantsList)
            {
                foreach (MenuCategory category in restaurant.Categories)
                {
                    category.RestaurantId = restaurant.Id;
                    foreach (MenuItem menuItem in category.MenuItems)
                    {
                        menuItem.MenuCategoryId = category.Id;
                    }
                }
            }

            _restaurantSource = restaurantsList.AsQueryable();
        }

        public IQueryable<Restaurant> GetRestaurants()
        {
            return _restaurantSource;
        }

        public IQueryable<MenuCategory> GetCategories()
        {
            return _restaurantSource.SelectMany(x => x.Categories);
        }

        public IQueryable<MenuItem> GetMenuItems()
        {
            return _restaurantSource.SelectMany(x => x.Categories).SelectMany(x => x.MenuItems);
        }
    }
}