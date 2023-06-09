using System.ComponentModel.DataAnnotations;

namespace Models;

public class Exit
{
    [Key]
    [Required]
    public int ExistsId { get; set; }
    public DateTime CreationDate { get; set; }
    public int Quantity { get; set; }
    public string? Author {get;set;}
    [Required]
    public string? CustomerName {get;set;}
    [Required]
    public string? Notes { get; set; }
    public Inventory? Inventory {get;set;}
    public int InventoryId { get; set; }
}