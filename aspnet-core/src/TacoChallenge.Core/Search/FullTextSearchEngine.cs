using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lucene.Net.Analysis.En;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Microsoft.AspNetCore.Hosting;
using TacoChallenge.Models;
using Directory = System.IO.Directory;

namespace TacoChallenge.Search
{

    public class FullTextSearchEngine : IFullTextSearchEngine
    {
        private const string MenuItemIndexFolder = "MenuItems";
        private const string CategoryIndexFolder = "Categories";

        private readonly string _indexRootPath;

        private readonly string[] _menuItemIndexedFieldNames =
        {
            "RestaurantId",
            "CategoryId",
            "CategoryName",
            "MenuItemId",
            "MenuItemName"
        }; 
        
        private readonly string[] _categoryIndexedFieldNames =
        {
            "RestaurantId",
            "CategoryId",
            "CategoryName"
        };

        public FullTextSearchEngine(IWebHostEnvironment env)
        {
            //TODO: move to settings the index folder name
            _indexRootPath = Path.Combine(env.ContentRootPath, "Lucene_Index");
        }

        public bool DoIndexesExist()
        {
            return Directory.Exists(_indexRootPath);
        }

        public void IndexMenuItems(IList<Restaurant> restaurants)
        {
            using var analyzer = new EnglishAnalyzer(LuceneVersion.LUCENE_48);
            using var writer = new IndexWriter(GetDirectory(MenuItemIndexFolder), new IndexWriterConfig(LuceneVersion.LUCENE_48, analyzer));
            foreach (var restaurant in restaurants)
            {
                foreach (MenuCategory category in restaurant.Categories)
                {
                    foreach (MenuItem item in category.MenuItems)
                    {
                        var doc = new Document();

                        //TODO: implement custom attributes to mark which entity fields should indexed
                        doc.Add(new StringField("RestaurantId", restaurant.Id.ToString(), Field.Store.YES));
                        doc.Add(new StringField("CategoryId", category.Id.ToString(), Field.Store.YES));
                        doc.Add(new TextField("CategoryName", category.Name, Field.Store.YES) { Boost = 4.0f });
                        doc.Add(new StringField("MenuItemId", item.Id.ToString(), Field.Store.YES));
                        doc.Add(new TextField("MenuItemName", item.Name, Field.Store.YES));

                        writer.AddDocument(doc);
                    }
                }
            }

            writer.Commit();
        }

        public void IndexCategories(IList<Restaurant> restaurants)
        {
            using var analyzer = new EnglishAnalyzer(LuceneVersion.LUCENE_48);
            using var writer = new IndexWriter(GetDirectory(CategoryIndexFolder), new IndexWriterConfig(LuceneVersion.LUCENE_48, analyzer));
            foreach (var restaurant in restaurants)
            {
                foreach (MenuCategory category in restaurant.Categories)
                {
                       var doc = new Document();

                        //TODO: implement custom attributes to mark which entity fields should indexed
                        doc.Add(new StringField("RestaurantId", restaurant.Id.ToString(), Field.Store.YES));
                        doc.Add(new StringField("CategoryId", category.Id.ToString(), Field.Store.YES));
                        doc.Add(new TextField("CategoryName", category.Name, Field.Store.YES));

                        writer.AddDocument(doc);
                }
            }

            writer.Commit();
        }

        public IList<Document> SearchMenuItems(string searchText)
        {
            return Search(searchText, MenuItemIndexFolder, _menuItemIndexedFieldNames);
        }

        public IList<Document> SearchCategories(string searchText)
        {
            return Search(searchText, CategoryIndexFolder, _categoryIndexedFieldNames);
        }

        private IList<Document> Search(string searchText, string itemsFolder, string[] fieldNames)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return new List<Document>();
            }

            try
            {
                using var analyzer = new EnglishAnalyzer(LuceneVersion.LUCENE_48);
                using var reader = DirectoryReader.Open(GetDirectory(itemsFolder));

                var searcher = new IndexSearcher(reader);
                var parser = new MultiFieldQueryParser(LuceneVersion.LUCENE_48, fieldNames, analyzer);
                var query = parser.Parse(QueryParserBase.Escape(searchText.Trim()));

                //TODO: change 100 to page size when paging is implemented
                var hits = searcher.Search(query, null, 100, Sort.RELEVANCE).ScoreDocs;


                return hits.Select(x => searcher.Doc(x.Doc)).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private FSDirectory GetDirectory(string subDirectory)
        {
            var folder = System.IO.Directory.CreateDirectory(Path.Combine(_indexRootPath, subDirectory));

            return FSDirectory.Open(folder);
        }
    }
}