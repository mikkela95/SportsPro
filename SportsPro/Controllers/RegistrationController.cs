using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro._Controllers
{
    public class RegistrationController : Controller
    {
        private readonly SportsProContext _context;

        public RegistrationController(SportsProContext context)
        {
            _context = context;
        }

        // GET: Registration
        // Displays a list of all registrations, including the associated Customer and Product
        public async Task<IActionResult> Index()
        {
            var sportsProContext = _context.Registrations
                .Include(r => r.Customer)
                .Include(r => r.Product);

            return View(await sportsProContext.ToListAsync());
        }

        // GET: Registration/Details/5
        // Displays the details of a single registration
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.Customer)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(m => m.RegistrationID == id);

            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // GET: Registration/Create
        // Shows a form to create a new registration by selecting a Customer and a Product
        public IActionResult Create()
        {
            // Provide dropdown lists for selecting a Customer and a Product
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FullName");
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name");
            return View();
        }

        // POST: Registration/Create
        // Inserts a new registration record into the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistrationID,CustomerID,ProductID")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If model validation fails, re-populate dropdowns and return to the form
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FullName", registration.CustomerID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name", registration.ProductID);
            return View(registration);
        }

        // GET: Registration/Edit/5
        // Shows a form to edit an existing registration record
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }

            // Provide dropdown lists for selecting a Customer and a Product
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FullName", registration.CustomerID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name", registration.ProductID);
            return View(registration);
        }

        // POST: Registration/Edit/5
        // Updates the existing registration in the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegistrationID,CustomerID,ProductID")] Registration registration)
        {
            if (id != registration.RegistrationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrationExists(registration.RegistrationID))
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

            // If validation fails, show form again
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FullName", registration.CustomerID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Name", registration.ProductID);
            return View(registration);
        }

        // GET: Registration/Delete/5
        // Shows a confirmation page to delete a registration
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registrations
                .Include(r => r.Customer)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(m => m.RegistrationID == id);

            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // POST: Registration/Delete/5
        // Deletes the registration from the database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration != null)
            {
                _context.Registrations.Remove(registration);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registrations.Any(e => e.RegistrationID == id);
        }
    }
}
