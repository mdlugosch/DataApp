using DataApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataApp.Controllers
{
    public class HomeController : Controller
    {
        private EFDatabaseContext context;

        // Datencontext wird von MVC über Dependeny Injection übergeben.
        public HomeController(EFDatabaseContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            return View(context.Products);
        }
    }
}
