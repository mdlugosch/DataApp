using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataApp.Models
{
    /*
     * Das InterfaceRepository wird für die DependencyInjection genutzt da
     * so nicht direkt eine Instanz eines Contextes genutzt wird. So ist es möglich
     * jedes DatenRepository zu verwenden der das RepositoryInterface implementiert. Dadurch
     * lässt sich der Datencontext austauschen.
     */
    public interface IDataRepository
    {
        /*
         * Anstatt IEnumerable nimmt man das davon abgeleitete IQueryable.
         * IQueryable ladet Daten nicht sofort ein so wie es bei der Basisklasse der Fall ist. 
         */
        IQueryable<Product> Products { get; }

        IEnumerable<Product> GetProductsByPrice(decimal minPrice);

        Product GetProduct(long id);
        IEnumerable<Product> GetAllProducts();
        void CreateProduct(Product newProduct);
        void UpdateProduct(Product changedProduct);
        void DeleteProduct(long id);
    }
}
