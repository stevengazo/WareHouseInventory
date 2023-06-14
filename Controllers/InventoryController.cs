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
    public class InventoryController : Controller
    {
        private readonly WareHouseDataContext _context;

        public InventoryController(WareHouseDataContext context)
        {
            _context = context;
        }

        // GET: Inventory
        public async Task<IActionResult> Index()
        {
            var wareHouseDataContext = _context.Inventories.Include(i => i.Product).Include(i => i.WareHouse);
            return View(await wareHouseDataContext.ToListAsync());
        }


        private async Task<bool> ExistsRegister(int WareHouseId, int ProductId)
        {
            try
            {
                var query = await (from i in _context.Inventories
                                   where i.ProductId == ProductId && i.WareHouseId == WareHouseId
                                   select i
                ).FirstOrDefaultAsync();
                if (query != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception f)
            {
                Console.WriteLine(f.Message);
                return true;
            }
        }

        // GET: Inventory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Inventories == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                .Include(i => i.Product)
                .Include(i => i.WareHouse)
                .FirstOrDefaultAsync(m => m.InventoryId == id);
            ViewBag.Exits = await (from E in _context.Exits
                                   where E.InventoryId == inventory.InventoryId
                                   orderby E.CreationDate descending
                                   select E
                                    ).Take(20).ToListAsync();
            ViewBag.Entries = await (from E in _context.Entries
                                     where E.InventoryId == inventory.InventoryId
                                     orderby E.CreationDate descending
                                     select E).Take(20).ToListAsync();
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: Inventory/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Description");
            ViewData["WareHouseId"] = new SelectList(_context.WareHouses, "WareHouseId", "Address");
            ViewBag.ErrorMessage="";
            return View();
        }

        // POST: Inventory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InventoryId,Name,CreationDate,WareHouseId,ProductId")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                bool Exist = await ExistsRegister(inventory.WareHouseId, inventory.ProductId);
                if (!Exist)
                {
                    inventory.QuantityOfExistances = 0;
                    _context.Add(inventory);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Description", inventory.ProductId);
                    ViewData["WareHouseId"] = new SelectList(_context.WareHouses, "WareHouseId", "Address", inventory.WareHouseId);
                    ViewBag.ErrorMessage = "El producto ya se encuentra registrado en ese almacen";
                    return View(inventory);
                }
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Description", inventory.ProductId);
            ViewData["WareHouseId"] = new SelectList(_context.WareHouses, "WareHouseId", "Address", inventory.WareHouseId);
            ViewBag.ErrorMessage= "";
            return View(inventory);
        }

        // GET: Inventory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Inventories == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Description", inventory.ProductId);
            ViewData["WareHouseId"] = new SelectList(_context.WareHouses, "WareHouseId", "Address", inventory.WareHouseId);
            return View(inventory);
        }

        // POST: Inventory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InventoryId,Name,CreationDate,QuantityOfExistances,WareHouseId,ProductId")] Inventory inventory)
        {
            if (id != inventory.InventoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.InventoryId))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Description", inventory.ProductId);
            ViewData["WareHouseId"] = new SelectList(_context.WareHouses, "WareHouseId", "Address", inventory.WareHouseId);
            return View(inventory);
        }

        // GET: Inventory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Inventories == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                .Include(i => i.Product)
                .Include(i => i.WareHouse)
                .FirstOrDefaultAsync(m => m.InventoryId == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Inventories == null)
            {
                return Problem("Entity set 'WareHouseDataContext.Inventories'  is null.");
            }
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory != null)
            {
                _context.Inventories.Remove(inventory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryExists(int id)
        {
            return (_context.Inventories?.Any(e => e.InventoryId == id)).GetValueOrDefault();
        }
    }
}
