using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Mobile_Recharge.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Online_Mobile_Recharge.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
       
        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }
       

        //Get: UserDetails/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        //Post: UserDetails/Login
        [HttpPost]
        public async Task<IActionResult> Login(UserDetailsModel model)
        {


            if (!User.Identity.IsAuthenticated)
            {
                // If the user is already authenticated, redirect them to the Home/Index action
                ViewData["Message"] = "You must be authenticated to access this page.";
            }



            string? MobileNumber = Request.Form["Number"];
            string? Password = Request.Form["Password"];

            Console.WriteLine("User " + MobileNumber);
            //check if the employee exists in the database

            UserDetailsModel userDetailsModel = await _context.UserDetailsModel
               .Where(u => u.Number == MobileNumber && u.Password == Password)
               .FirstOrDefaultAsync();

            //Generate jwt token for authentication if passwod matches and also import the necessary package
            if (userDetailsModel != null)
            {

                try
                {

                    List<Claim> claim = new List<Claim>() {
                    new Claim(ClaimTypes.Name, userDetailsModel.Number!),
                    new Claim(ClaimTypes.Sid,userDetailsModel.UserId!.ToString()),
                    };


                    var identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                    // Redirect to \UserDetails\Index\{id} action
                    return RedirectToAction("Index", "UserDetails",new { id = userDetailsModel.UserId });

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error " + ex.Message);
                }
            }
            return Unauthorized();
        }


        //Get: UserDetails/LogOut
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Auth");
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
        public async Task<IActionResult> Create([Bind("UserId,Number,ServiceProviderId,RechargePlanId,MailId,Password")] UserDetailsModel userDetailsModel, [Bind("WalletId,UserId,Amount")] WalletModel wallet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wallet);
                _context.Add(userDetailsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RechargePlanId"] = new SelectList(_context.Set<RechargePlansModel>(), "RechargePlanId", "RechargePlanId", userDetailsModel.RechargePlanId);
            ViewData["ServiceProviderId"] = new SelectList(_context.Set<ServiceProviderModel>(), "ServiceProviderId", "ServiceName", userDetailsModel.ServiceProviderId);


            // Redirect to \UserDetails\Index\{id} action
            return RedirectToAction("Index", "UserDetails", new { id = userDetailsModel.UserId });

        }

    }
}
