using Microsoft.AspNetCore.Mvc;
using Projetos_App1.Models;
using System.Diagnostics;

namespace Projetos_App1.Controllers
{
    public class HomeController : Controller
    {
       

     

        public IActionResult Index() //home
        {
            return View();
        }

     
    }
}
