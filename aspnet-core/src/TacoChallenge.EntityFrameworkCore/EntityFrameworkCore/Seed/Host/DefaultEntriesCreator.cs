using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TacoChallenge.Models;

namespace TacoChallenge.EntityFrameworkCore.Seed.Host
{
    public class DefaultEntriesCreator
    {
        private readonly TacoChallengeDbContext _context;

        public DefaultEntriesCreator(TacoChallengeDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            var entries = LoadEntries();

            var categories = new List<MenuCategory>();

            if(entries.IsNullOrEmpty())
                throw new Exception("No entries");

            foreach (Restaurant entry in entries)
            {
                if (_context.Restaurants.Any(x => x.Id == entry.Id))
                {
                    continue;
                }

                var categoryEntries = entry.Categories;
                foreach (MenuCategory categoryEntry in categoryEntries)
                {
                    categoryEntry.RestaurantId = entry.Id;
                }

                categories.AddRange(categoryEntries);

                entry.Categories = new List<MenuCategory>();

                _context.Restaurants.Add(entry);

            }

            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Restaurant ON;");
            _context.SaveChanges();
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Restaurant OFF");

            _context.MenuCategories.AddRange(categories);

            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.MenuItem ON;");
            _context.SaveChanges();
            _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.MenuItem OFF");

        }

        private IList<Restaurant> LoadEntries()
        {
            var result = JsonConvert.DeserializeObject<List<Restaurant>>(File.ReadAllText(TacoChallengeConsts.JsonDataFilePath));

            return result;
        }
    }
}