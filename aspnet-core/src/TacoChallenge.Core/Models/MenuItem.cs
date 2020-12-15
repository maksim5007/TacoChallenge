using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace TacoChallenge.Models
{
    [Table("MenuItem")]
    public class MenuItem : Entity
    {
        public int MenuCategoryId { get; set; }
        public MenuCategory Category { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        [MaxLength(128)]
        public decimal Price { get; set; }
    }
}