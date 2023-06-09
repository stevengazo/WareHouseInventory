namespace Models;

public class Entry
{
    public int EntryId { get; set; }
    public DateTime CreationDate { get; set; }
    public int Quantity { get; set; }
    public string? LoteCode { get; set; }
    public string? Author { get; set; }
    public string? Notes { get; set; }
    public Inventory? Inventory {get;set;}
    public int InventoryId { get; set; }
}