using Microsoft.AspNetCore.Mvc;

namespace Product.API.Controllers
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
