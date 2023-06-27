namespace Models;


public class RegisterOfExit
{
    public int RegisterOfExitId { get; set; }
    public int Quantity { get; set; }
    public int MyProperty { get; set; }
    public int ProductCodeId { get; set; }
    public ProductCode ProductCode { get; set; }
    public int ExistsId { get; set; }
    public Exit Exit { get; set; }

}