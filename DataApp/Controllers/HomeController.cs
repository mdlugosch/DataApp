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
        private IDataRepository repository;

        // DatenRepository wird von MVC über Dependeny Injection übergeben.
        public HomeController(IDataRepository repo)
        {
            repository = repo;
        }
        //public IActionResult Index()
        //{
            /*
             * IQueryable hat den Nachteil das Entity Framework nicht weiss das eine
             * Count Operation und das Filtern nach dem Preis in einer Abfrage erledigt
             * werden kann. Aus eigentlich einer Abfrage werden daher zwei und bei komplexeren
             * Abfragen wird  durch ähnliche Fälle Leistung eingebüst.
             */
            //var products = repository.Products.Where(p => p.Price > 25);
            //ViewBag.ProductCount = products.Count();
            //return View(products);

            /*
             * Bei solchen Problemen hilft es mit ToList oder ToArray die Abfrage in einem IEnumerable
             * umzuwandeln. Auf diese Weise kann auf die Liste die sich schon im Speicher befindet ein count ausgeführt werden.
             */
            //var products = repository.Products.Where(p => p.Price > 25).ToArray();

            //var products = repository.GetProductsByPrice(25);
            //ViewBag.ProductCount = products.Count();
            //return View(products);

            //return View(repository.GetAllProducts());

        //    var products = repository.GetAllProducts();
        //    System.Console.WriteLine("Property value has been read");
        //    return View(products);
        //}

        public IActionResult Index(string category = null, decimal? price = null)
        {
            var products = repository.GetFilteredProducts(category, price);
            ViewBag.category = category;
            ViewBag.price = price;
            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            return View("Editor", new Product());
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            repository.CreateProduct(product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(long id)
        {
            ViewBag.CreateMode = false;
            return View("Editor", repository.GetProduct(id));
        }

        [HttpPost]
        public IActionResult Edit(Product product, Product original)
        {
            repository.UpdateProduct(product, original);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            repository.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
