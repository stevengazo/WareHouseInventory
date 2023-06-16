using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DataBaseContext;

namespace Inventario.Controllers
{
    public class ProductController : Controller
    {
        private readonly WareHouseDataContext _context;

        public ProductController(WareHouseDataContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var data = (from P in _context.Products
                        select P).Include(P => P.ProductImages).ToList();
            if (data == null)
            {
                return Problem("Entity set 'WareHouseDataContext.Products'  is null.");
            }
            else
            {
                return View(data);
            }
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (LoginChecker() && CheckUserType(IUserLevels.SellerLevel))
            {
                if (id == null || _context.Products == null)
                {
                    return NotFound();
                }

                var product = await _context.Products.Include(P => P.ProductImages)
                    .FirstOrDefaultAsync(m => m.ProductId == id);
                var inventories = await (from i in _context.Inventories where i.ProductId == product.ProductId select i).Include(i => i.WareHouse).ToListAsync();
                ViewBag.Inventory = inventories;
                ViewBag.WareHouses = (from i in inventories select i.WareHouse).Distinct().ToList();

                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            if (LoginChecker() && CheckUserType(IUserLevels.SellerLevel))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }


        private bool LoginChecker()
        {
            short level = Convert.ToInt16(HttpContext.Session.GetString("UserLevel"));
            if (level > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Check if the user has a level type 
        /// </summary>
        /// <returns>true if the user can see the element</returns>
        private bool CheckUserType(short userle = 0)
        {
            short level = Convert.ToInt16(HttpContext.Session.GetString("UserLevel"));
            if (level <= userle)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Description,Buy_Price,Sell_Price")] Product product)
        {

            if (LoginChecker() && CheckUserType(IUserLevels.SellerLevel))
            {
                if (ModelState.IsValid)
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(product);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (LoginChecker() && CheckUserType(IUserLevels.ManagerLevel))
            {

                if (id == null || _context.Products == null)
                {
                    return NotFound();
                }

                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Description,Buy_Price,Sell_Price")] Product product)
        {

            if (LoginChecker() && CheckUserType(IUserLevels.ManagerLevel))
            {
                if (id != product.ProductId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(product);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductExists(product.ProductId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(product);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }


        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (LoginChecker() && CheckUserType(IUserLevels.AdminLevel))
            {
                if (id == null || _context.Products == null)
                {
                    return NotFound();
                }

                var product = await _context.Products
                    .FirstOrDefaultAsync(m => m.ProductId == id);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }


        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'WareHouseDataContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
