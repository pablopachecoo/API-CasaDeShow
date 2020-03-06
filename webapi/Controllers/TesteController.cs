using webapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class TesteController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }       
        
    }
}