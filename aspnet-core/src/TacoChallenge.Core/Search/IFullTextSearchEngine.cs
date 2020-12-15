using System.Collections.Generic;
using Abp.Dependency;
using Lucene.Net.Documents;
using TacoChallenge.Models;

namespace TacoChallenge.Search
{
    /// <summary>
    /// This would be responsible for maintaining FTS indexes. For now it only has adding new indexing.
    /// But in real world we'd have also something that deletes and updated each record. It is possible to catch CRUD actions on EF level.
    /// </summary>
    public interface IFullTextSearchEngine : ITransientDependency
    {
        void IndexMenuItems(IList<Restaurant> restaurants);
        void IndexCategories(IList<Restaurant> restaurants);
        bool DoIndexesExist();

        //TODO: add some abstract entity over Document so FTS would be independent from FTS library
        IList<Document> SearchMenuItems(string searchText);
        IList<Document> SearchCategories(string searchText);
    }
}