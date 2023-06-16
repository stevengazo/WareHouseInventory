using Models.DataBaseContext;
using System.Threading.Tasks;

namespace Inventario.Controllers;

public class Validations
{

    private readonly WareHouseDataContext _context;

    public Validations(WareHouseDataContext Context)
    {
        _context = Context;
    }

    public bool IsUserValid(short idUser)
    {
        try
        {        
            var query = (from U in _context.Users
                         where U.Enable && U.UserId == idUser
                         select U
                            ).FirstOrDefault();
            if (query != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception f)
        {
            Console.WriteLine(f.Message);
            return false;
        }
    }

    public string HashPassword(string Password)
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception f)
        {
            Console.WriteLine(f.Message);
            return string.Empty;
        }
    }


}