using System.ComponentModel.DataAnnotations;
namespace Models
{
   public class Product
    {
        [Key]
        [Required]
        public int ProductId { get; set; }
        [MaxLength(30)]
        [Required]
        public string? Name { get; set; }
        [MaxLength(320)]
        [Required]
        public string? Description { get; set; }
        public double Buy_Price { get; set; }
        public double Sell_Price { get; set; }
        public ICollection<Photo>? ProductImages{get;set;}
        public ICollection<Inventory>? Inventories { get; set; }
    }
}