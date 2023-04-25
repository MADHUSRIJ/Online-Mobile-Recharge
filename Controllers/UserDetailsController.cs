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
        public readonly UserDetailsModel userDetails = new UserDetailsModel();
        public readonly ServiceProviderModel serviceProvider = new ServiceProviderModel();
        public readonly RechargePlansModel rechargePlans = new RechargePlansModel();


        public UserDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetId(UserDetailsModel user)
        {
            return View();
        }

        // POST: UserDetails/GetId
        [HttpPost]
        public async Task<IActionResult> GetId()
        {
            int id = Convert.ToInt32(Request.Form["UserId"]);
            Console.WriteLine(id);

            return RedirectToAction("Index", new { id = id });
        }


        // GET: UserDetails/Index/5
        public async Task<IActionResult> Index(int? id)
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
            
            ViewBag.userDetails = userDetailsModel;


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

            ViewBag.serviceProvider = serviceProviderModel;
            if (id == null || _context.RechargePlansModel == null)
            {
                return NotFound();
            }


            var rechargePlansModel = await _context.RechargePlansModel
                .Include(r => r.ServiceProvider)
                .FirstOrDefaultAsync(m => m.RechargePlanId == userDetailsModel.RechargePlanId);
            if (rechargePlansModel == null)
            {
                return NotFound();
            }

            ViewBag.rechargePlans = rechargePlansModel;

            return View();

        }


        // GET: UserDetails/Create
        public IActionResult Create()
        {
            ViewData["RechargePlanId"] = new SelectList(_context.Set<RechargePlansModel>(), "RechargePlanId", "RechargePlanName");
            ViewData["ServiceProviderId"] = new SelectList(_context.Set<ServiceProviderModel>(), "ServiceProviderId", "ServiceName");
            return View();
        }

        // POST: UserDetails/Create
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

        private bool UserDetailsModelExists(int id)
        {
          return (_context.UserDetailsModel?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
