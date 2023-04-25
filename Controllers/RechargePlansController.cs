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


        private bool RechargePlansModelExists(int id)
        {
          return (_context.RechargePlansModel?.Any(e => e.RechargePlanId == id)).GetValueOrDefault();
        }
    }
}
