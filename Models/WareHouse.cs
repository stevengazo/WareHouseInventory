using System.ComponentModel.DataAnnotations;
namespace Models;

public class WareHouse
{
    [Key]
    [Required]
    public int WareHouseId { get; set; }
    [Required]
    [MaxLength(60)]
    public string? Name {get;set;}
    [MaxLength(120)]
    [Required]
    public string? Address {get;set;}
    public ICollection<Inventory>? Inventories { get; set; }

}