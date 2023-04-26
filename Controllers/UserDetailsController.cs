using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Online_Mobile_Recharge;
using Online_Mobile_Recharge.Models;
using Microsoft.AspNetCore.Authorization;

namespace Online_Mobile_Recharge.Controllers
{
   
    public class UserDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public readonly UserDetailsModel userDetails = new UserDetailsModel();
        public readonly List<RechargeLogsModel> rechargeLogs = new List<RechargeLogsModel>();


        public UserDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public ActionResult MiddleWareAction()
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.Sid)?.Value);

            Console.WriteLine("Index Middle" + userId);

            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            return RedirectToAction("Index", "UserDetails", new { id = userId });
        }

        // GET: UserDetails/Index/5
        [Authorize]
        public async Task<IActionResult> Index(int? id)
        {
            // Get the authenticated user's ID

            if (id == null || _context.UserDetailsModel == null)
            {
                return NotFound();
            }

            var userDetailsModel = await _context.UserDetailsModel
                .Include(u => u.RechargePlans)
                .Include(u => u.ServiceProvider)
                .Include(w => w.Wallet)
                .FirstOrDefaultAsync(m => m.UserId == id);

            if (userDetailsModel == null)
            {
                return NotFound();
            }
            
            ViewBag.userDetails = userDetailsModel;


            if (id == null || _context.RechargeLogsModel == null)
            {
                return NotFound();
            }

            var rechargeLogsModel = await _context.RechargeLogsModel
             .Include(r => r.RechargePlans)
             .ThenInclude(rp => rp.ServiceProvider)
             .Include(r => r.UserDetails)
             .Where(m => m.UserId == id)
             .ToListAsync();

            if (rechargeLogsModel == null)
            {
                return NotFound();
            }

            ViewBag.rechargeLogs = rechargeLogsModel;


            return View();

        }

        private bool UserDetailsModelExists(int id)
        {
          return (_context.UserDetailsModel?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
