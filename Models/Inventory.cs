using System.ComponentModel.DataAnnotations;
namespace Models;

public class Inventory
{
    [Key]
    public int InventoryId { get; set; }
    public DateTime CreationDate { get; set; }
    public int QuantityOfExistances {get;set;}
    public WareHouse? WareHouse {get;set;}
    public int WareHouseId { get; set; }
    public Product? Product {get;set;}
    public int ProductId { get; set; }
    public ICollection<Entry>? Entries { get; set; }
    public ICollection<Exit>? Exits {get;set; }

}