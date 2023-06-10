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
    public class ExitController : Controller
    {
        private readonly WareHouseDataContext _context;

        public ExitController(WareHouseDataContext context)
        {
            _context = context;
        }

        // GET: Exit
        public async Task<IActionResult> Index()
        {            
            var wareHouseDataContext = _context.Exits.Include(e => e.Inventory);
            return View(await wareHouseDataContext.ToListAsync());
        }

        // GET: Exit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Exits == null)
            {
                return NotFound();
            }

            var exit = await _context.Exits
                .Include(e => e.Inventory)
                .FirstOrDefaultAsync(m => m.ExistsId == id);
            if (exit == null)
            {
                return NotFound();
            }

            return View(exit);
        }

        // GET: Exit/Create
        public async Task<IActionResult> CreatebyInventory(string id)
        {
            var tmp = await _context.Inventories.Where(I=>I.InventoryId == Convert.ToInt32(id) ).FirstOrDefaultAsync();           
            ViewBag.Inventory = tmp;
            ViewBag.Error= string.Empty;
            return View();
        }
        // POST: Exit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatebyInventory([Bind("ExistsId,Quantity,Author,CustomerName,Notes,InventoryId")] Exit exit)
        {
            if (ModelState.IsValid)
            {
                exit.CreationDate = DateTime.Now;
                Inventory temp = await _context.Inventories.Where(I=>I.InventoryId == exit.InventoryId ).FirstOrDefaultAsync();           
                bool QuantityValid = ((temp.QuantityOfExistances - exit.Quantity ) >=0)  ? true : false;
                if(QuantityValid){
                    temp.QuantityOfExistances = temp.QuantityOfExistances - exit.Quantity;
                    _context.Inventories.Update(temp);
                    _context.Add(exit);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }else{

                    ViewBag.Inventory = temp;
                    ViewBag.Error= "La Cantidad excede las existencias";
                    return View();
                }                
            }
            ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "Name", exit.InventoryId);
            return View(exit);
        }

        // GET: Exit/Create
        public IActionResult Create()
        {
            ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "Name");
            return View();
        }

        // POST: Exit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExistsId,CreationDate,Quantity,Author,CustomerName,Notes,InventoryId")] Exit exit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "Name", exit.InventoryId);
            return View(exit);
        }

        // GET: Exit/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Exits == null)
            {
                return NotFound();
            }

            var exit = await _context.Exits.FindAsync(id);
            if (exit == null)
            {
                return NotFound();
            }
            ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "Name", exit.InventoryId);
            return View(exit);
        }

        // POST: Exit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExistsId,CreationDate,Quantity,Author,CustomerName,Notes,InventoryId")] Exit exit)
        {
            if (id != exit.ExistsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExitExists(exit.ExistsId))
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
            ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "Name", exit.InventoryId);
            return View(exit);
        }

        // GET: Exit/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Exits == null)
            {
                return NotFound();
            }

            var exit = await _context.Exits
                .Include(e => e.Inventory)
                .FirstOrDefaultAsync(m => m.ExistsId == id);
            if (exit == null)
            {
                return NotFound();
            }

            return View(exit);
        }

        // POST: Exit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Exits == null)
            {
                return Problem("Entity set 'WareHouseDataContext.Exits'  is null.");
            }
            var exit = await _context.Exits.FindAsync(id);
            if (exit != null)
            {
                _context.Exits.Remove(exit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExitExists(int id)
        {
          return (_context.Exits?.Any(e => e.ExistsId == id)).GetValueOrDefault();
        }
    }
}
