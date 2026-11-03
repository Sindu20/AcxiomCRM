
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcxiomCRM.Data;
using AcxiomCRM.Models;

namespace AcxiomCRM.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display and search customers
        public async Task<IActionResult> Index(string searchString)
        {
            var customers = _context.Customers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                customers = customers.Where(c =>
                    c.CustomerName.Contains(searchString) ||
                    c.Email.Contains(searchString) ||
                    c.Phone.Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;

            return View(await customers.ToListAsync());
        }

        // Add customer page
        public IActionResult Create()
        {
            return View();
        }

        // Save customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                bool duplicate = await _context.Customers.AnyAsync(c =>
                    c.Email == customer.Email || c.Phone == customer.Phone);

                if (duplicate)
                {
                    ModelState.AddModelError("",
                        "Email or phone number already exists.");
                    return View(customer);
                }

                customer.CreatedDate = DateTime.Now;

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        // Edit customer page
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        // Update customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.CustomerId)
                return NotFound();

            if (!ModelState.IsValid)
                return View(customer);

            bool duplicate = await _context.Customers.AnyAsync(c =>
                c.CustomerId != id &&
                (c.Email == customer.Email || c.Phone == customer.Phone));

            if (duplicate)
            {
                ModelState.AddModelError("",
                    "Email or phone number already exists.");
                return View(customer);
            }

            var existing = await _context.Customers.FindAsync(id);

            if (existing == null)
                return NotFound();

            existing.CustomerName = customer.CustomerName;
            existing.Email = customer.Email;
            existing.Phone = customer.Phone;
            existing.CompanyName = customer.CompanyName;
            existing.Address = customer.Address;
            existing.City = customer.City;
            existing.State = customer.State;
            existing.Status = customer.Status;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Delete customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
