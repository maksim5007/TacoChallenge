using System.Collections.Generic;
using Abp.Domain.Services;
using TacoChallenge.Models;
namespace TacoChallenge.Search
{
    public interface ISearchManager : IDomainService
    {
        IDictionary<Restaurant, List<MenuItem>> SearchRestaurants(string searchText);
        IList<MenuCategory> SearchCategories(string searchText);
    }
}