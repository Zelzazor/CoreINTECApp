using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreIntecWeb.Models;

namespace CoreIntecWeb.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PeopleContext _context;

        public PeopleController(PeopleContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            var peopleContext = _context.People.Include(p => p.ClientType).Include(p => p.CompanyNavigation).Include(p => p.ContactType).Include(p => p.Department);
            return View(await peopleContext.ToListAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            var people = await _context.People
                .Include(p => p.ClientType)
                .Include(p => p.CompanyNavigation)
                .Include(p => p.ContactType)
                .Include(p => p.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (people == null)
            {
                return NotFound();
            }

            return View(people);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            ViewData["ClientTypeId"] = new SelectList(_context.ClientType, "Id", "Name");
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name");
            ViewData["ContactTypeId"] = new SelectList(_context.ContactType, "Id", "Name");
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Name");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,MiddleName,LastName,ClientTypeId,ContactTypeId,SupportStaff,PhoneNumber,EmailAddress,CompanyId,DepartmentId,CreatedAt,Enabled")] People people)
        {
            if (ModelState.IsValid)
            {
                _context.Add(people);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientTypeId"] = new SelectList(_context.ClientType, "Id", "Name", people.ClientTypeId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "EmailAddress", people.CompanyId);
            ViewData["ContactTypeId"] = new SelectList(_context.ContactType, "Id", "Name", people.ContactTypeId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Code", people.DepartmentId);
            return View(people);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            var people = await _context.People.FindAsync(id);
            if (people == null)
            {
                return NotFound();
            }
            ViewData["ClientTypeId"] = new SelectList(_context.ClientType, "Id", "Name", people.ClientTypeId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "EmailAddress", people.CompanyId);
            ViewData["ContactTypeId"] = new SelectList(_context.ContactType, "Id", "Name", people.ContactTypeId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Code", people.DepartmentId);
            return View(people);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,MiddleName,LastName,ClientTypeId,ContactTypeId,SupportStaff,PhoneNumber,EmailAddress,CompanyId,DepartmentId,CreatedAt,Enabled")] People people)
        {
            if (id != people.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(people);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeopleExists(people.Id))
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
            ViewData["ClientTypeId"] = new SelectList(_context.ClientType, "Id", "Name", people.ClientTypeId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "EmailAddress", people.CompanyId);
            ViewData["ContactTypeId"] = new SelectList(_context.ContactType, "Id", "Name", people.ContactTypeId);
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Code", people.DepartmentId);
            return View(people);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            var people = await _context.People
                .Include(p => p.ClientType)
                .Include(p => p.CompanyNavigation)
                .Include(p => p.ContactType)
                .Include(p => p.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (people == null)
            {
                return NotFound();
            }

            return View(people);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.People == null)
            {
                return Problem("Entity set 'PeopleContext.People'  is null.");
            }
            var people = await _context.People.FindAsync(id);
            if (people != null)
            {
                _context.People.Remove(people);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeopleExists(int id)
        {
          return _context.People.Any(e => e.Id == id);
        }
    }
}
