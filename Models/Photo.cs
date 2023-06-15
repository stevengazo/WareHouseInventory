using System.ComponentModel.DataAnnotations;

namespace Models;

public class Photo
{
    [Key]
    [Required]
    public int PhotoId { get; set; }
    [Required]
    public byte[] File {get;set; }
    public Product? Product {get;set;}
    public int ProductId { get; set; }
    
}