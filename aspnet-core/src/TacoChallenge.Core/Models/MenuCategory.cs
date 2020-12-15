using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace TacoChallenge.Models
{
    [Table("MenuCategory")]
    public class MenuCategory : Entity
    {
        public MenuCategory()
        {
            MenuItems = new Collection<MenuItem>();
        }

        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }
    }
}