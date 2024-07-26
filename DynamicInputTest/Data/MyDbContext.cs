using DynamicInputTest.Models;
using Microsoft.EntityFrameworkCore;

namespace DynamicInputTest.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Acondicionador> Acondicionadores { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Fabricante> Fabricantes { get; set; }
    }
}
