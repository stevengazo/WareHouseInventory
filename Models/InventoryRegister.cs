using System.ComponentModel.DataAnnotations;
namespace Models;

class InventoryRegister
{
    [Key]
    [Required]
    public int InventoryRegisterId { get; set; }
    [Required]
    [MaxLength(320)]
    public string Notes { get; set; }
    public bool IsEntry { get; set; }
    public Inventory Inventory { get; set; }
    public int InventoryId { get; set; }

}