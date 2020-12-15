using System.Linq;
using Abp.Dependency;
using TacoChallenge.Models;

namespace TacoChallenge.Search
{
    public interface ISearchDataProvider : ITransientDependency
    {
        IQueryable<Restaurant> GetRestaurants();
        IQueryable<MenuCategory> GetCategories();
        IQueryable<MenuItem> GetMenuItems();
    }
}