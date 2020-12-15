using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace TacoChallenge.Models
{
    [Table("Restaurant")]
    public class Restaurant : Entity
    {
        public Restaurant()
        {
            Categories = new Collection<MenuCategory>();
        }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        [MaxLength(128)]
        public string City { get; set; }

        [Required]
        [MaxLength(128)]
        public string Suburb { get; set; }

        [Required]
        [MaxLength(512)]
        public string LogoPath { get; set; }

        public int Rank { get; set; }

        public ICollection<MenuCategory> Categories { get; set; }
    }
}
