using System.ComponentModel.DataAnnotations;
namespace Models;

class Inventory
{
    [Key]
    public int InventoryId { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name {get;set;}
    public DateTime CreationDate { get; set; }
    public ICollection<Product> Products{get;set;}
    public WareHouse WareHouse { get; set; }
   public int WareHouseId { get; set; }
}