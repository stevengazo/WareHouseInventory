namespace Models;

public class Entry
{
    public int EntryId { get; set; }
    public DateTime CreationDate { get; set; }
    public int Quantity { get; set; }
    public int Discounts {get;set;}
    public bool Avariable {get;set;}
    public string? Author { get; set; }
    public string? Notes { get; set; }
    public Inventory? Inventory {get;set;}
    public int InventoryId { get; set; }
    public int ProductCodeId { get; set; }
    public ProductCode ProductCode {get;set;}
}