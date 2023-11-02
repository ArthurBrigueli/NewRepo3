using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace pimfo.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            Response.Cookies.Delete("TokenCookie");
            return RedirectToAction("Index", "Home");
        }
    }
}
