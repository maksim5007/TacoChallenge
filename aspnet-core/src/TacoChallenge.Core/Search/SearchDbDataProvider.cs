using System.Linq;
using Abp.Domain.Repositories;
using TacoChallenge.Models;

namespace TacoChallenge.Search
{
    public class SearchDbDataProvider : ISearchDataProvider
    {
        private readonly IRepository<Restaurant> _restaurantRepository;
        private readonly IRepository<MenuCategory> _categoryRepository;
        private readonly IRepository<MenuItem> _menuItemRepository;

        public SearchDbDataProvider(IRepository<Restaurant> restaurantRepository, IRepository<MenuCategory> categoryRepository, IRepository<MenuItem> menuItemRepository)
        {
            _restaurantRepository = restaurantRepository;
            _categoryRepository = categoryRepository;
            _menuItemRepository = menuItemRepository;
        }

        public IQueryable<Restaurant> GetRestaurants()
        {
            return _restaurantRepository.GetAll();
        }

        public IQueryable<MenuCategory> GetCategories()
        {
            return _categoryRepository.GetAll();
        }

        public IQueryable<MenuItem> GetMenuItems()
        {
            return _menuItemRepository.GetAll();
        }
    }
}