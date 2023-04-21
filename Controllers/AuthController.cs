using Microsoft.AspNetCore.Mvc;

namespace Online_Mobile_Recharge.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult LoginUsingMobileNumber()
        {
            return View();
        }
    }
}
