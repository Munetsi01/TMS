using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class TasksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
