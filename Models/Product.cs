using System.ComponentModel.DataAnnotations;
namespace Models
{
    class Product
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Buy_Price { get; set; }
        public double Sell_Price { get; set; }
        public ICollection<ProductImage> ProductImages{get;set;}
        public ICollection<Inventory> Inventories { get; set; }

    }
}