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
    public class UserDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserDetailsModel.Include(u => u.RechargePlans).Include(u => u.ServiceProvider);
            return View(await applicationDbContext.ToListAsync());

        }

        // GET: UserDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserDetailsModel == null)
            {
                return NotFound();
            }

            var userDetailsModel = await _context.UserDetailsModel
                .Include(u => u.RechargePlans)
                .Include(u => u.ServiceProvider)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userDetailsModel == null)
            {
                return NotFound();
            }

            if (id == null || _context.ServiceProviderModel == null)
            {
                return NotFound();
            }

            var serviceProviderModel = await _context.ServiceProviderModel
                .FirstOrDefaultAsync(m => m.ServiceProviderId == userDetailsModel.ServiceProviderId);
            if (serviceProviderModel == null)
            {
                return NotFound();
            }


            return View(userDetailsModel);
        }

        // GET: UserDetails/Create
        public IActionResult Create()
        {
            ViewData["RechargePlanId"] = new SelectList(_context.Set<RechargePlansModel>(), "RechargePlanId", "RechargePlanId");
            ViewData["ServiceProviderId"] = new SelectList(_context.Set<ServiceProviderModel>(), "ServiceProviderId", "ServiceName");
            return View();
        }

        // POST: UserDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Number,ServiceProviderId,RechargePlanId,MailId,Password")] UserDetailsModel userDetailsModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userDetailsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RechargePlanId"] = new SelectList(_context.Set<RechargePlansModel>(), "RechargePlanId", "RechargePlanId", userDetailsModel.RechargePlanId);
            ViewData["ServiceProviderId"] = new SelectList(_context.Set<ServiceProviderModel>(), "ServiceProviderId", "ServiceName", userDetailsModel.ServiceProviderId);
            return View(userDetailsModel);
        }

        // GET: UserDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserDetailsModel == null)
            {
                return NotFound();
            }

            var userDetailsModel = await _context.UserDetailsModel.FindAsync(id);
            if (userDetailsModel == null)
            {
                return NotFound();
            }
            ViewData["RechargePlanId"] = new SelectList(_context.Set<RechargePlansModel>(), "RechargePlanId", "RechargePlanId", userDetailsModel.RechargePlanId);
            ViewData["ServiceProviderId"] = new SelectList(_context.Set<ServiceProviderModel>(), "ServiceProviderId", "ServiceName", userDetailsModel.ServiceProviderId);
            return View(userDetailsModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,Number,ServiceProviderId,RechargePlanId,MailId,Password")] UserDetailsModel userDetailsModel)
        {
            if (id != userDetailsModel.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userDetailsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDetailsModelExists(userDetailsModel.UserId))
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
            ViewData["RechargePlanId"] = new SelectList(_context.Set<RechargePlansModel>(), "RechargePlanId", "RechargePlanId", userDetailsModel.RechargePlanId);
            ViewData["ServiceProviderId"] = new SelectList(_context.Set<ServiceProviderModel>(), "ServiceProviderId", "ServiceName", userDetailsModel.ServiceProviderId);
            return View(userDetailsModel);
        }

        private bool UserDetailsModelExists(int id)
        {
          return (_context.UserDetailsModel?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
