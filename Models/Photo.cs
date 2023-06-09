using System.ComponentModel.DataAnnotations;

namespace Models;

public class Photo
{
    [Key]
    [Required]
    public int PhotoId { get; set; }
    public string? FilePath {get;set; }
    public Product? Product {get;set;}
    public int ProductId { get; set; }
    
}