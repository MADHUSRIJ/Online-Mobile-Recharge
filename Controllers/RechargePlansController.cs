using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Online_Mobile_Recharge;
using Online_Mobile_Recharge.Models;

namespace Online_Mobile_Recharge.Controllers
{
    public class RechargePlansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RechargePlansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RechargePlans
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RechargePlansModel.Include(r => r.ServiceProvider);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RechargePlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RechargePlansModel == null)
            {
                return NotFound();
            }

            var rechargePlansModel = await _context.RechargePlansModel
                .Include(r => r.ServiceProvider)
                .FirstOrDefaultAsync(m => m.RechargePlanId == id);
            if (rechargePlansModel == null)
            {
                return NotFound();
            }

            return View(rechargePlansModel);
        }

        // GET: RechargePlans/Create
        public IActionResult Create()
        {
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProviderModel, "ServiceProviderId", "ServiceName");
            return View();
        }

        // POST: RechargePlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RechargePlanId,ServiceProviderId,RechargePlanName,RechargePlanValidity,RechargePlanPrice,RechargePlanData")] RechargePlansModel rechargePlansModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rechargePlansModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProviderModel, "ServiceProviderId", "ServiceName", rechargePlansModel.ServiceProviderId);
            return View(rechargePlansModel);
        }

        // GET: RechargePlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RechargePlansModel == null)
            {
                return NotFound();
            }

            var rechargePlansModel = await _context.RechargePlansModel.FindAsync(id);
            if (rechargePlansModel == null)
            {
                return NotFound();
            }
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProviderModel, "ServiceProviderId", "ServiceName", rechargePlansModel.ServiceProviderId);
            return View(rechargePlansModel);
        }

        // POST: RechargePlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RechargePlanId,ServiceProviderId,RechargePlanName,RechargePlanValidity,RechargePlanPrice,RechargePlanData")] RechargePlansModel rechargePlansModel)
        {
            if (id != rechargePlansModel.RechargePlanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rechargePlansModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RechargePlansModelExists(rechargePlansModel.RechargePlanId))
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
            ViewData["ServiceProviderId"] = new SelectList(_context.ServiceProviderModel, "ServiceProviderId", "ServiceName", rechargePlansModel.ServiceProviderId);
            return View(rechargePlansModel);
        }

        // GET: RechargePlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RechargePlansModel == null)
            {
                return NotFound();
            }

            var rechargePlansModel = await _context.RechargePlansModel
                .Include(r => r.ServiceProvider)
                .FirstOrDefaultAsync(m => m.RechargePlanId == id);
            if (rechargePlansModel == null)
            {
                return NotFound();
            }

            return View(rechargePlansModel);
        }

        // POST: RechargePlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RechargePlansModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RechargePlansModel'  is null.");
            }
            var rechargePlansModel = await _context.RechargePlansModel.FindAsync(id);
            if (rechargePlansModel != null)
            {
                _context.RechargePlansModel.Remove(rechargePlansModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RechargePlansModelExists(int id)
        {
          return (_context.RechargePlansModel?.Any(e => e.RechargePlanId == id)).GetValueOrDefault();
        }
    }
}
