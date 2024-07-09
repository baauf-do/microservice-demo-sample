using Microsoft.AspNetCore.Mvc;

namespace Hangfire.API.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }


        public IActionResult Index()
        {
            return Redirect(url: "~/swagger");
        }
    }
}
