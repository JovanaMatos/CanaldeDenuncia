
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projetos_App1.Models;

namespace Projetos_App1.Controllers
{
    public class ComplaintsController : Controller
    {
        private readonly AppDbContext _context;

        public ComplaintsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Complaints
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Complaints;
            return View(await appDbContext.ToListAsync());
        }

        // GET: Complaints/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Complaints == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints
                .Include(c => c.CompaniesCategory)
                .Include(c => c.CompanyRelation)
                .Include(c => c.ComplaintStatus)
                .Include(c => c.CurrentResponsible)
                .Include(c => c.ShippingMethods)
                .FirstOrDefaultAsync(m => m.ComplaintId == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // GET: Complaints/Create
        public IActionResult Create()
        {
            ViewData["CompaniesCategoryId"] = new SelectList(_context.CompaniesCategories, "CompaniesCategoryId", "CompaniesCategoryId");
            ViewData["CompanyRelationId"] = new SelectList(_context.CompanyRelations, "CompanyRelationId", "CompanyRelationId");
            ViewData["ComplaintStatusId"] = new SelectList(_context.ComplaintStatuses, "ComplaintStatusId", "ComplaintStatusId");
            ViewData["CurrentResponsibleId"] = new SelectList(_context.Responsibles, "ResponsiblesId", "ResponsiblesId");
            ViewData["ShippingMethodsId"] = new SelectList(_context.ShippingMethods, "ShippingMethodsId", "ShippingMethodsId");
            return View();
        }

        // POST: Complaints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComplaintId,PassWord,ComplaintType,ComplaintSubject,ComplaintDescription,ComplaintResponse,ComplaintStartDate,ComplaintCloseDate,CompaniesCategoryId,ShippingMethodsId,ComplaintStatusId,CompanyRelationId,CurrentResponsibleId")] Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complaint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompaniesCategoryId"] = new SelectList(_context.CompaniesCategories, "CompaniesCategoryId", "CompaniesCategoryId", complaint.CompaniesCategoryId);
            ViewData["CompanyRelationId"] = new SelectList(_context.CompanyRelations, "CompanyRelationId", "CompanyRelationId", complaint.CompanyRelationId);
            ViewData["ComplaintStatusId"] = new SelectList(_context.ComplaintStatuses, "ComplaintStatusId", "ComplaintStatusId", complaint.ComplaintStatusId);
            ViewData["CurrentResponsibleId"] = new SelectList(_context.Responsibles, "ResponsiblesId", "ResponsiblesId", complaint.CurrentResponsibleId);
            ViewData["ShippingMethodsId"] = new SelectList(_context.ShippingMethods, "ShippingMethodsId", "ShippingMethodsId", complaint.ShippingMethodsId);
            return View(complaint);
        }

        // GET: Complaints/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Complaints == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint == null)
            {
                return NotFound();
            }
            ViewData["CompaniesCategoryId"] = new SelectList(_context.CompaniesCategories, "CompaniesCategoryId", "CompaniesCategoryId", complaint.CompaniesCategoryId);
            ViewData["CompanyRelationId"] = new SelectList(_context.CompanyRelations, "CompanyRelationId", "CompanyRelationId", complaint.CompanyRelationId);
            ViewData["ComplaintStatusId"] = new SelectList(_context.ComplaintStatuses, "ComplaintStatusId", "ComplaintStatusId", complaint.ComplaintStatusId);
            ViewData["CurrentResponsibleId"] = new SelectList(_context.Responsibles, "ResponsiblesId", "ResponsiblesId", complaint.CurrentResponsibleId);
            ViewData["ShippingMethodsId"] = new SelectList(_context.ShippingMethods, "ShippingMethodsId", "ShippingMethodsId", complaint.ShippingMethodsId);
            return View(complaint);
        }

        // POST: Complaints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ComplaintId,PassWord,ComplaintType,ComplaintSubject,ComplaintDescription,ComplaintResponse,ComplaintStartDate,ComplaintCloseDate,CompaniesCategoryId,ShippingMethodsId,ComplaintStatusId,CompanyRelationId,CurrentResponsibleId")] Complaint complaint)
        {
            if (id != complaint.ComplaintId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintExists(complaint.ComplaintId))
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
            ViewData["CompaniesCategoryId"] = new SelectList(_context.CompaniesCategories, "CompaniesCategoryId", "CompaniesCategoryId", complaint.CompaniesCategoryId);
            ViewData["CompanyRelationId"] = new SelectList(_context.CompanyRelations, "CompanyRelationId", "CompanyRelationId", complaint.CompanyRelationId);
            ViewData["ComplaintStatusId"] = new SelectList(_context.ComplaintStatuses, "ComplaintStatusId", "ComplaintStatusId", complaint.ComplaintStatusId);
            ViewData["CurrentResponsibleId"] = new SelectList(_context.Responsibles, "ResponsiblesId", "ResponsiblesId", complaint.CurrentResponsibleId);
            ViewData["ShippingMethodsId"] = new SelectList(_context.ShippingMethods, "ShippingMethodsId", "ShippingMethodsId", complaint.ShippingMethodsId);
            return View(complaint);
        }

        // GET: Complaints/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Complaints == null)
            {
                return NotFound();
            }

            var complaint = await _context.Complaints
                .Include(c => c.CompaniesCategory)
                .Include(c => c.CompanyRelation)
                .Include(c => c.ComplaintStatus)
                .Include(c => c.CurrentResponsible)
                .Include(c => c.ShippingMethods)
                .FirstOrDefaultAsync(m => m.ComplaintId == id);
            if (complaint == null)
            {
                return NotFound();
            }

            return View(complaint);
        }

        // POST: Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Complaints == null)
            {
                return Problem("Entity set 'AppDbContext.Complaints'  is null.");
            }
            var complaint = await _context.Complaints.FindAsync(id);
            if (complaint != null)
            {
                _context.Complaints.Remove(complaint);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintExists(string id)
        {
          return (_context.Complaints?.Any(e => e.ComplaintId == id)).GetValueOrDefault();
        }
    }
}
