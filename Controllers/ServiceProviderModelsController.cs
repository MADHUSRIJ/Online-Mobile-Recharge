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
    public class ServiceProviderModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiceProviderModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ServiceProviderModels
        public async Task<IActionResult> Index()
        {
              return _context.ServiceProviderModel != null ? 
                          View(await _context.ServiceProviderModel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.ServiceProviderModel'  is null.");
        }

        // GET: ServiceProviderModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ServiceProviderModel == null)
            {
                return NotFound();
            }

            var serviceProviderModel = await _context.ServiceProviderModel
                .FirstOrDefaultAsync(m => m.ServiceProviderId == id);
            if (serviceProviderModel == null)
            {
                return NotFound();
            }

            return View(serviceProviderModel);
        }

        // GET: ServiceProviderModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServiceProviderModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceProviderId,ServiceName")] ServiceProviderModel serviceProviderModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceProviderModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceProviderModel);
        }

        // GET: ServiceProviderModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ServiceProviderModel == null)
            {
                return NotFound();
            }

            var serviceProviderModel = await _context.ServiceProviderModel.FindAsync(id);
            if (serviceProviderModel == null)
            {
                return NotFound();
            }
            return View(serviceProviderModel);
        }

        // POST: ServiceProviderModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceProviderId,ServiceName")] ServiceProviderModel serviceProviderModel)
        {
            if (id != serviceProviderModel.ServiceProviderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceProviderModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceProviderModelExists(serviceProviderModel.ServiceProviderId))
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
            return View(serviceProviderModel);
        }

        // GET: ServiceProviderModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ServiceProviderModel == null)
            {
                return NotFound();
            }

            var serviceProviderModel = await _context.ServiceProviderModel
                .FirstOrDefaultAsync(m => m.ServiceProviderId == id);
            if (serviceProviderModel == null)
            {
                return NotFound();
            }

            return View(serviceProviderModel);
        }

        // POST: ServiceProviderModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ServiceProviderModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ServiceProviderModel'  is null.");
            }
            var serviceProviderModel = await _context.ServiceProviderModel.FindAsync(id);
            if (serviceProviderModel != null)
            {
                _context.ServiceProviderModel.Remove(serviceProviderModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceProviderModelExists(int id)
        {
          return (_context.ServiceProviderModel?.Any(e => e.ServiceProviderId == id)).GetValueOrDefault();
        }
    }
}
