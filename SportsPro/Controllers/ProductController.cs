using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class ProductController : Controller
    {
        private readonly SportsProContext _context;

        public ProductController(SportsProContext context)
        {
            _context = context;
        }

        // GET: Product
        // Lists all products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products); // Views/Product/Index.cshtml
        }

        // GET: Product/Details/5
        // Shows details of a single product
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);

            if (product == null)
                return NotFound();

            return View(product); // Views/Product/Details.cshtml
        }

        // GET: /Product/create-new/
        // Displays a blank form to create a new product
        [HttpGet]
        // [Route("/Product/create-new/")]
        public IActionResult Create()
        {
            return View(); // Views/Product/Create.cshtml
        }

        // POST: Product/Create
        // Inserts a new product into the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ProductCode,Name,YearlyPrice,ReleaseDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Product {product.Name} was added.";

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Edit/5/changes
        // Displays the Edit form for the product with the given id
        [Route("/Product/Edit/{id:int}/{name?}/changes")]
        public async Task<IActionResult> Edit(int? id, string? name)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            return View(product); // Views/Product/Edit.cshtml
        }

        // POST: Product/Edit/5/changes-made
        // Updates the product in the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Product/Edit/{id:int}/{name?}/changes-made/")]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,ProductCode,Name,YearlyPrice,ReleaseDate")] Product product)
        {
            if (id != product.ProductID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Delete/5
        // Displays a confirmation page to delete a product
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);

            if (product == null)
                return NotFound();

            return View(product); // Views/Product/Delete.cshtml
        }

        // POST: Product/Delete/5
        // Removes the product from the database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
