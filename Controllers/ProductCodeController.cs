
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DataBaseContext;
namespace Inventario.Controllers;

class ProductCodeController : Controller
{
    private readonly WareHouseDataContext _context;
    public ProductCodeController(WareHouseDataContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> List()
    {
        IEnumerable<ProductCode> Codes = (from Pc in _context.ProductCodes
                                          where Pc.IsActive == true
                                          select Pc).Take(100);
        return View(Codes);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var id = await (from P in _context.ProductCodes
                        select P.Code).LastOrDefaultAsync();

        ProductCode code = new()
        {
            GenerationDate = DateTime.Today,
            CanExpire = true,
            IsActive = true,
            ExpirationDate = DateTime.Now,
            Code = (id + 1)
        };
        return View(code);
    }
    [HttpPost]
    public async Task<IActionResult> Create([Bind($"Code,ExpirationDate,CanExpire")] ProductCode productCode)
    {

        if (ModelState.IsValid)
        {
            productCode.ExpirationDate = DateTime.Now;
            _context.ProductCodes.Add(productCode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
        else
        {
            return View(productCode);
        }
    }
    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        if (!string.IsNullOrEmpty(id))
        {
            ProductCode code = await _context.ProductCodes.FirstOrDefaultAsync(P => P.ProductCodeId == Convert.ToInt32(id));
            if (code != null)
            {
                return View(code);
            }
        }
        throw new NotImplementedException();
    }
    [HttpPut]
    public async Task<IActionResult> Edit([Bind($"Code,ExpirationDate,CanExpire")] ProductCode productCode)
    {

        if (ModelState.IsValid)
        {
            productCode.GenerationDate = DateTime.Now;
            _context.ProductCodes.Update(productCode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
        else
        {
            return View(productCode);
        }
    }
    [HttpGet]
    public async Task<IActionResult> Delete()
    {
        return View();
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(string id = "")
    {
        if (!string.IsNullOrEmpty(id))
        {
            ProductCode code = await _context.ProductCodes.LastOrDefaultAsync(PC => PC.ProductCodeId == Convert.ToInt32(id));
            if (code != null)
            {
                _context.ProductCodes.Remove(code);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(List));
        }
        else
        {
            return RedirectToAction(nameof(List));
        }
    }
    [HttpGet]
    public async Task<IActionResult> Details(string id){
     if(!string.IsNullOrEmpty(id)){
        int idTemp = Convert.ToInt32(id);
        ProductCode code = await _context.ProductCodes.LastOrDefaultAsync(P=>P.ProductCodeId == idTemp);
        return View(code);
     }
     else{
        return View();
     }
    }
}