using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataApp.Models
{
    /*
     * Der Controller initialisiert nicht direkt einen Datencontext sondern ein Repository
     * Dem Controller kennt den DatenContext nicht sondern nur das Repositotry. Das jeweilige
     * Repository kennt dem ihn zugeordneten DatenContext. Da aber jedes Repository im Controller
     * ausgetauscht werden kann das den selben InterfaceTyp implementiert ist damit auch die
     * Datenquelle austauschbar. Der Vorteil des Interfaces ist das die Schnittstellen für alle
     * Datenquellen gleich sind. z.B.: Daten werden über die selben Methodenaufrufe geliefert.
     */
    public class EFDataRepository : IDataRepository
    {
        private EFDatabaseContext context;

        public EFDataRepository(EFDatabaseContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Product> Products => context.Products;
        
        public IEnumerable<Product> GetProductsByPrice(decimal minPrice)
        {
            return context.Products.Where(p => p.Price >= minPrice).ToArray();
        }

        public Product GetProduct(long id)
        {
            return context.Products.Find(id);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            Console.WriteLine("GetAllProducts");
            return context.Products;
        }

        public void CreateProduct(Product newProduct)
        {
            newProduct.Id = 0;
            context.Products.Add(newProduct);
            context.SaveChanges();
            Console.WriteLine($"New Key: {newProduct.Id}");
        }

        public void UpdateProduct(Product changedProduct, Product originalProduct = null)
        {
            if(originalProduct == null)
            {
                originalProduct = context.Products.Find(changedProduct.Id);
            } else
            {
                context.Products.Attach(originalProduct);
            }
            originalProduct.Name = changedProduct.Name;
            originalProduct.Category = changedProduct.Category;
            originalProduct.Price = changedProduct.Price;

            context.SaveChanges();
        }

        public void DeleteProduct(long id)
        {
            context.Products.Remove(new Product { Id = id });
            context.SaveChanges();
        }

        public IEnumerable<Product> GetFilteredProducts(string category = null, decimal? price = null)
        {
            IQueryable<Product> data = context.Products;
            if(category != null)
            {
                data = data.Where(p => p.Category == category);
            }
            if(price != null)
            {
                data = data.Where(p => p.Price >= price);
            }
            return data;
        }
    }
}
