using System.ComponentModel.DataAnnotations;

namespace Models;

public class ProductImage
{
    public int ProductImageId { get; set; }
    public string FilePath {get;set; }
    public Product Product {get;set;}
    public int ProductId { get; set; }
    
}