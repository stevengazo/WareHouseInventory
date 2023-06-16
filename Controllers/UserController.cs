using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DataBaseContext;

namespace Inventario.Controllers
{
    public class UserController : Controller
    {
        private readonly WareHouseDataContext _context;

        public UserController(WareHouseDataContext context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            if (LoginChecker() && CheckUserType(IUserLevels.AdminLevel))
            {
                var wareHouseDataContext = _context.Users.Include(u => u.GroupsUser);
                return View(await wareHouseDataContext.ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }


        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login([Bind("UserName,Password")] User user)
        {
            string Password = (string.IsNullOrEmpty(user.Password)) ? string.Empty : user.Password;
            string UserName = (string.IsNullOrEmpty(user.UserName)) ? string.Empty : user.UserName;
            if (!string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(UserName))
            {
                var User = (from U in _context.Users
                            where U.UserName == UserName && U.Password == Password
                            select U).Include(U => U.GroupsUser).FirstOrDefault();
                if (User != null)
                {
                    User.Password = string.Empty;
                    HttpContext.Session.SetString("UserName", User.UserName.ToString());
                    HttpContext.Session.SetString("UserLevel", User.GroupsUser.Level.ToString());
                    // do some logic
                    return View();
                }
                else
                {
                    HttpContext.Session.Clear();
                    return View();
                }

            }
            else
            {
                HttpContext.Session.Clear();
                return View();
            }
        }
        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (LoginChecker() && CheckUserType(IUserLevels.AdminLevel))
            {
                if (id == null || _context.Users == null)
                {
                    return NotFound();
                }
                var user = await _context.Users
                    .Include(u => u.GroupsUser)
                    .FirstOrDefaultAsync(m => m.UserId == id);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }


        }

        // GET: User/Create
        public IActionResult Create()
        {
            if (LoginChecker() && CheckUserType(IUserLevels.AdminLevel))
            {
                ViewData["GroupsUserId"] = new SelectList(_context.Groups, "GroupsUserId", "GroupsUserId");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserName,Name,LastName,Password,UserImagePath,Enable,LastLogin,GroupsUserId")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupsUserId"] = new SelectList(_context.Groups, "GroupsUserId", "GroupsUserId", user.GroupsUserId);
            return View(user);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (LoginChecker() && CheckUserType(IUserLevels.AdminLevel))
            {
                if (id == null || _context.Users == null)
                {
                    return NotFound();
                }
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                ViewData["GroupsUserId"] = new SelectList(_context.Groups, "GroupsUserId", "GroupsUserId", user.GroupsUserId);
                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserName,Name,LastName,Password,UserImagePath,Enable,LastLogin,GroupsUserId")] User user)
        {
            if (LoginChecker() && CheckUserType(IUserLevels.AdminLevel))
            {
                if (id != user.UserId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(user);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserExists(user.UserId))
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
                ViewData["GroupsUserId"] = new SelectList(_context.Groups, "GroupsUserId", "GroupsUserId", user.GroupsUserId);
                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }



        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.GroupsUser)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'WareHouseDataContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
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
