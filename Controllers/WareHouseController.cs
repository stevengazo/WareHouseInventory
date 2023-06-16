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
    public class WareHouseController : Controller
    {
        private readonly WareHouseDataContext _context;

        public WareHouseController(WareHouseDataContext context)
        {
            _context = context;
        }

        // GET: WareHouse
        public async Task<IActionResult> Index()
        {
            return _context.WareHouses != null ?
                        View(await _context.WareHouses.ToListAsync()) :
                        Problem("Entity set 'WareHouseDataContext.WareHouses'  is null.");
        }

        // GET: WareHouse/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WareHouses == null)
            {
                return NotFound();
            }

            var wareHouse = await _context.WareHouses.FirstOrDefaultAsync(m => m.WareHouseId == id);
            if (wareHouse == null)
            {
                return NotFound();
            }
            else
            {
                wareHouse.Inventories = await (from I in _context.Inventories
                                               where I.WareHouseId == wareHouse.WareHouseId
                                               select I
                                                    ).Include(I => I.Product).ToListAsync();
                return View(wareHouse);
            }
        }

        // GET: WareHouse/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WareHouse/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WareHouseId,Name,Address")] WareHouse wareHouse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wareHouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wareHouse);
        }

        // GET: WareHouse/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (LoginChecker() && CheckUserType(IUserLevels.ManagerLevel))
            {
                if (id == null || _context.WareHouses == null)
                {
                    return NotFound();
                }

                var wareHouse = await _context.WareHouses.FindAsync(id);
                if (wareHouse == null)
                {
                    return NotFound();
                }
                return View(wareHouse);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }

        // POST: WareHouse/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WareHouseId,Name,Address")] WareHouse wareHouse)
        {
            if (LoginChecker() && CheckUserType(IUserLevels.ManagerLevel))
            {
                if (id != wareHouse.WareHouseId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(wareHouse);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!WareHouseExists(wareHouse.WareHouseId))
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
                return View(wareHouse);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // GET: WareHouse/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (LoginChecker() && CheckUserType(IUserLevels.AdminLevel))
            {
                var wareHouse = await _context.WareHouses
                .FirstOrDefaultAsync(m => m.WareHouseId == id);
                if (wareHouse == null)
                {
                    return NotFound();
                }
                return View(wareHouse);
            }
            else
            {
                return RedirectToAction("Login", "User");
                if (id == null || _context.WareHouses == null)
                {
                    return NotFound();
                }
            }
        }

        // POST: WareHouse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (LoginChecker() && CheckUserType(IUserLevels.AdminLevel))
            {
                if (_context.WareHouses == null)
                {
                    return Problem("Entity set 'WareHouseDataContext.WareHouses'  is null.");
                }
                var wareHouse = await _context.WareHouses.FindAsync(id);
                if (wareHouse != null)
                {
                    _context.WareHouses.Remove(wareHouse);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Login", "User");

            }
        }

        private bool WareHouseExists(int id)
        {
            return (_context.WareHouses?.Any(e => e.WareHouseId == id)).GetValueOrDefault();
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
    }
}
