using Microsoft.AspNetCore.Mvc;
using Online_Mobile_Recharge.Models;

namespace Online_Mobile_Recharge.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult AuthenticateUser()
        {
            return View();
        }
    }
}
