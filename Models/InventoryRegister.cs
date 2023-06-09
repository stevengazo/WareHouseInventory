using System.ComponentModel.DataAnnotations;
namespace Models;

public class InventoryRegister
{
    [Key]
    [Required]
    public int InventoryRegisterId { get; set; }
    [Required]
    [MaxLength(320)]
    public string Notes { get; set; }
    public bool IsEntry { get; set; }
    public int Quantity {get;set;}
    public Inventory Inventory { get; set; }
    public int InventoryId { get; set; }
    public DateTime Creation {get;set;}
    public string Author {get;set;}
}