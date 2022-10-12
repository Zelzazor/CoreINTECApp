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
    public class ContactTypesController : Controller
    {
        private readonly PeopleContext _context;

        public ContactTypesController(PeopleContext context)
        {
            _context = context;
        }

        // GET: ContactTypes
        public async Task<IActionResult> Index()
        {
              return View(await _context.ContactType.ToListAsync());
        }

        // GET: ContactTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ContactType == null)
            {
                return NotFound();
            }

            var contactType = await _context.ContactType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactType == null)
            {
                return NotFound();
            }

            return View(contactType);
        }

        // GET: ContactTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Enabled,CreatedAt")] ContactType contactType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactType);
        }

        // GET: ContactTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ContactType == null)
            {
                return NotFound();
            }

            var contactType = await _context.ContactType.FindAsync(id);
            if (contactType == null)
            {
                return NotFound();
            }
            return View(contactType);
        }

        // POST: ContactTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Enabled,CreatedAt")] ContactType contactType)
        {
            if (id != contactType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactTypeExists(contactType.Id))
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
            return View(contactType);
        }

        // GET: ContactTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ContactType == null)
            {
                return NotFound();
            }

            var contactType = await _context.ContactType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactType == null)
            {
                return NotFound();
            }

            return View(contactType);
        }

        // POST: ContactTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ContactType == null)
            {
                return Problem("Entity set 'PeopleContext.ContactType'  is null.");
            }
            var contactType = await _context.ContactType.FindAsync(id);
            if (contactType != null)
            {
                _context.ContactType.Remove(contactType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactTypeExists(int id)
        {
          return _context.ContactType.Any(e => e.Id == id);
        }
    }
}
