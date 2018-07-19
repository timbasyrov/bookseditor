using Microsoft.AspNetCore.Mvc;

namespace BooksEditor.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}