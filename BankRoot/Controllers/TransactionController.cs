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
    public class TransactionController : Controller
    {
        private readonly DataContext _context;

        public TransactionController(DataContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Transaction.Include(t => t.AccountC).Include(t => t.AccountD);
            return View(await dataContext.ToListAsync());
        }

        // GET: Transaction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transaction == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .Include(t => t.AccountC)
                .Include(t => t.AccountD)
                .FirstOrDefaultAsync(m => m.Id_transaction == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transaction/Create
        public IActionResult Create()
        {
            ViewData["Ctransaction"] = new SelectList(_context.Account, "Id_account", "Id_account");
            ViewData["Dtransaction"] = new SelectList(_context.Account, "Id_account", "Id_account");
            return View();
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_transaction,Dtransaction,Ctransaction,date_transaction,amount,status")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ctransaction"] = new SelectList(_context.Account, "Id_account", "Id_account", transaction.Ctransaction);
            ViewData["Dtransaction"] = new SelectList(_context.Account, "Id_account", "Id_account", transaction.Dtransaction);
            return View(transaction);
        }

        // GET: Transaction/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transaction == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["Ctransaction"] = new SelectList(_context.Account, "Id_account", "Id_account", transaction.Ctransaction);
            ViewData["Dtransaction"] = new SelectList(_context.Account, "Id_account", "Id_account", transaction.Dtransaction);
            return View(transaction);
        }

        // POST: Transaction/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_transaction,Dtransaction,Ctransaction,date_transaction,amount,status")] Transaction transaction)
        {
            if (id != transaction.Id_transaction)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id_transaction))
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
            ViewData["Ctransaction"] = new SelectList(_context.Account, "Id_account", "Id_account", transaction.Ctransaction);
            ViewData["Dtransaction"] = new SelectList(_context.Account, "Id_account", "Id_account", transaction.Dtransaction);
            return View(transaction);
        }

        // GET: Transaction/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transaction == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .Include(t => t.AccountC)
                .Include(t => t.AccountD)
                .FirstOrDefaultAsync(m => m.Id_transaction == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transaction == null)
            {
                return Problem("Entity set 'DataContext.Transaction'  is null.");
            }
            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction != null)
            {
                _context.Transaction.Remove(transaction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
          return _context.Transaction.Any(e => e.Id_transaction == id);
        }
    }
}
