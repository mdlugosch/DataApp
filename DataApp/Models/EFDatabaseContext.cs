using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataApp.Models
{
    /*
     * Contextklasse versorgt EF mit Details zu den Datenmodellen
     * die in der Datenbank abgebildet werden sollen.
     */
    public class EFDatabaseContext : DbContext
    {
        // Ein Konstruktor muss immer eingebunden werden selbst wenn er leer ist, andernfalls wird eine Fehlermeldung geworfen.
        public EFDatabaseContext(DbContextOptions<EFDatabaseContext> opts) : base(opts) { }

        public DbSet<Product> Products { get; set; }
    }
}
