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
    public class ClientTypesController : Controller
    {
        private readonly PeopleContext _context;

        public ClientTypesController(PeopleContext context)
        {
            _context = context;
        }

        // GET: ClientTypes
        public async Task<IActionResult> Index()
        {
              return View(await _context.ClientType.ToListAsync());
        }

        // GET: ClientTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClientType == null)
            {
                return NotFound();
            }

            var clientType = await _context.ClientType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientType == null)
            {
                return NotFound();
            }

            return View(clientType);
        }

        // GET: ClientTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClientTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Enabled,CreatedAt")] ClientType clientType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientType);
        }

        // GET: ClientTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClientType == null)
            {
                return NotFound();
            }

            var clientType = await _context.ClientType.FindAsync(id);
            if (clientType == null)
            {
                return NotFound();
            }
            return View(clientType);
        }

        // POST: ClientTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Enabled,CreatedAt")] ClientType clientType)
        {
            if (id != clientType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientTypeExists(clientType.Id))
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
            return View(clientType);
        }

        // GET: ClientTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClientType == null)
            {
                return NotFound();
            }

            var clientType = await _context.ClientType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientType == null)
            {
                return NotFound();
            }

            return View(clientType);
        }

        // POST: ClientTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClientType == null)
            {
                return Problem("Entity set 'PeopleContext.ClientType'  is null.");
            }
            var clientType = await _context.ClientType.FindAsync(id);
            if (clientType != null)
            {
                _context.ClientType.Remove(clientType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientTypeExists(int id)
        {
          return _context.ClientType.Any(e => e.Id == id);
        }
    }
}
