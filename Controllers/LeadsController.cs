
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcxiomCRM.Data;
using AcxiomCRM.Models;

namespace AcxiomCRM.Controllers
{
    public class LeadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display all leads and search
        public async Task<IActionResult> Index(string searchString)
        {
            var leads = _context.Leads.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                leads = leads.Where(l =>
                    l.LeadName.Contains(searchString) ||
                    l.Email.Contains(searchString) ||
                    l.CompanyName!.Contains(searchString));
            }

            ViewData["CurrentFilter"] = searchString;

            return View(await leads.ToListAsync());
        }

        // Add lead page
        public IActionResult Create()
        {
            return View();
        }

        // Save new lead
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lead lead)
        {
            if (ModelState.IsValid)
            {
                lead.CreatedDate = DateTime.Now;

                _context.Leads.Add(lead);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(lead);
        }

        // Edit lead page
        public async Task<IActionResult> Edit(int id)
        {
            var lead = await _context.Leads.FindAsync(id);

            if (lead == null)
                return NotFound();

            return View(lead);
        }

        // Save edited lead
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Lead lead)
        {
            if (id != lead.LeadId)
                return NotFound();

            if (!ModelState.IsValid)
                return View(lead);

            var existing = await _context.Leads.FindAsync(id);

            if (existing == null)
                return NotFound();

            existing.LeadName = lead.LeadName;
            existing.Email = lead.Email;
            existing.Phone = lead.Phone;
            existing.CompanyName = lead.CompanyName;
            existing.Source = lead.Source;
            existing.Status = lead.Status;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Delete lead
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var lead = await _context.Leads.FindAsync(id);

            if (lead == null)
                return NotFound();

            _context.Leads.Remove(lead);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
