using System.ComponentModel.DataAnnotations;

namespace Models;

public class ProductCode
{
    [Key]
    [Required]
    public int ProductCodeId { get; set; }
    public DateTime GenerationDate { get; set; }
    public int Code { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; }
    public bool CanExpire {get;set;}
    public ICollection<Entry> Entries {get;set;}
    public ICollection<RegisterOfExit> RegisterOfExits {get;set;}
 
}