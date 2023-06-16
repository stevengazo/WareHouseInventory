namespace Inventario.Controllers;

public interface IUserLevels
{
    public static short AdminLevel
    {
        get { return 1; }
        set { }
    }
    public static short ManagerLevel
    {
        get { return 2; }
        set { }
    }
    public static short SellerLevel
    {
        get { return 3; }
        set { }
    }
}