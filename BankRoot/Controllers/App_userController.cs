using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankRoot.Data;
using BankRoot.Models;

namespace BankRoot.Controllers
{
    public class App_userController : Controller
    {
        private readonly DataContext _context;

        public App_userController(DataContext context)
        {
            _context = context;
        }

        // GET: App_user
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.App_user.Include(a => a.Role);
            return View(await dataContext.ToListAsync());
        }

        // GET: App_user/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.App_user == null)
            {
                return NotFound();
            }

            var app_user = await _context.App_user
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.Id_app_user == id);
            if (app_user == null)
            {
                return NotFound();
            }

            return View(app_user);
        }

        // GET: App_user/Create
        public IActionResult Create()
        {
            ViewData["Id_role"] = new SelectList(_context.Role, "Id_role", "Id_role");
            return View();
        }

        // POST: App_user/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_app_user,app_user_number,first_name,last_name,email,password,Id_role")] App_user app_user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(app_user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_role"] = new SelectList(_context.Role, "Id_role", "Id_role", app_user.Id_role);
            return View(app_user);
        }

        // GET: App_user/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.App_user == null)
            {
                return NotFound();
            }

            var app_user = await _context.App_user.FindAsync(id);
            if (app_user == null)
            {
                return NotFound();
            }
            ViewData["Id_role"] = new SelectList(_context.Role, "Id_role", "Id_role", app_user.Id_role);
            return View(app_user);
        }

        // POST: App_user/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_app_user,app_user_number,first_name,last_name,email,password,Id_role")] App_user app_user)
        {
            if (id != app_user.Id_app_user)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(app_user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!App_userExists(app_user.Id_app_user))
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
            ViewData["Id_role"] = new SelectList(_context.Role, "Id_role", "Id_role", app_user.Id_role);
            return View(app_user);
        }

        // GET: App_user/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.App_user == null)
            {
                return NotFound();
            }

            var app_user = await _context.App_user
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.Id_app_user == id);
            if (app_user == null)
            {
                return NotFound();
            }

            return View(app_user);
        }

        // POST: App_user/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.App_user == null)
            {
                return Problem("Entity set 'DataContext.App_user'  is null.");
            }
            var app_user = await _context.App_user.FindAsync(id);
            if (app_user != null)
            {
                _context.App_user.Remove(app_user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool App_userExists(int id)
        {
          return _context.App_user.Any(e => e.Id_app_user == id);
        }
    }
}
