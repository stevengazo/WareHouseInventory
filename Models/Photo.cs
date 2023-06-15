using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace Models;

public class Photo
{
    [Key]
    [Required]
    public int PhotoId { get; set; }
    public string FileName {get;set;}

    public byte[] File {get;set; }
    public Product? Product {get;set;}
    public int ProductId { get; set; }

    
}