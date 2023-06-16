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
    public class EntryController : Controller
    {
        private readonly WareHouseDataContext _context;

        public EntryController(WareHouseDataContext context)
        {
            _context = context;
        }

        // GET: Entry
        public async Task<IActionResult> Index()
        {
            var wareHouseDataContext = _context.Entries
                .OrderByDescending(E => E.CreationDate)
                .Include(e => e.Inventory)
                .Include(e => e.Inventory.Product)
                .Include(e => e.Inventory.WareHouse);
            return View(await wareHouseDataContext.ToListAsync());
        }

        // GET: Entry/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Entries == null)
            {
                return NotFound();
            }

            var entry = await _context.Entries
                .Include(e => e.Inventory)
                .Include(e => e.Inventory.Product)
                .Include(e => e.Inventory.WareHouse)
                .FirstOrDefaultAsync(m => m.EntryId == id);

            if (entry == null)
            {
                return NotFound();
            }

            return View(entry);
        }

        // GET: Entry/Create
        public async Task<IActionResult> Createbyinventory(string id)
        {

            if (LoginChecker() && CheckUserType(IUserLevels.ManagerLevel))
            {
                ViewBag.Inventory = await _context.Inventories.Where(I => I.InventoryId == Convert.ToInt32(id)).FirstOrDefaultAsync();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }


        // POST: Entry/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Createbyinventory([Bind("EntryId,Quantity,LoteCode,Author,Notes,InventoryId")] Entry entry)
        {

            if (LoginChecker() && CheckUserType(IUserLevels.ManagerLevel))
            {
                if (ModelState.IsValid)
                {
                    entry.CreationDate = DateTime.Now;
                    Inventory tmp = _context.Inventories.Where(I => I.InventoryId == entry.InventoryId).FirstOrDefault();
                    tmp.QuantityOfExistances = tmp.QuantityOfExistances + entry.Quantity;
                    _context.Inventories.Update(tmp);
                    _context.Add(entry);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "Name", entry.InventoryId);
                return View(entry);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }


        }



        // GET: Entry/Create
        public IActionResult Create()
        {
            ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "Name");
            return View();
        }

        // POST: Entry/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EntryId,CreationDate,Quantity,LoteCode,Author,Notes,InventoryId")] Entry entry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(entry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "Name", entry.InventoryId);
            return View(entry);
        }

        // GET: Entry/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Entries == null)
            {
                return NotFound();
            }

            var entry = await _context.Entries.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }
            ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "Name", entry.InventoryId);
            return View(entry);
        }

        // POST: Entry/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EntryId,CreationDate,Quantity,LoteCode,Author,Notes,InventoryId")] Entry entry)
        {


            if (LoginChecker() && CheckUserType(IUserLevels.AdminLevel))
            {
                if (id != entry.EntryId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(entry);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EntryExists(entry.EntryId))
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
                ViewData["InventoryId"] = new SelectList(_context.Inventories, "InventoryId", "Name", entry.InventoryId);
                return View(entry);
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
        // GET: Entry/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Entries == null)
            {
                return NotFound();
            }

            var entry = await _context.Entries
                .Include(e => e.Inventory)
                .FirstOrDefaultAsync(m => m.EntryId == id);
            if (entry == null)
            {
                return NotFound();
            }

            return View(entry);
        }

        // POST: Entry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Entries == null)
            {
                return Problem("Entity set 'WareHouseDataContext.Entries'  is null.");
            }
            var entry = await _context.Entries.FindAsync(id);
            if (entry != null)
            {
                _context.Entries.Remove(entry);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntryExists(int id)
        {
            return (_context.Entries?.Any(e => e.EntryId == id)).GetValueOrDefault();
        }
    }
}
