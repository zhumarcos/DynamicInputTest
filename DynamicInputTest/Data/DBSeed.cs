using DynamicInputTest.Models;

namespace DynamicInputTest.Data
{
    public class DBSeed
    {
        public static void Seed(MyDbContext context)
        {
            context.Database.EnsureCreated();

            // Cargar países
            if (!context.Paises.Any())
            {
                var paises = new List<Pais> {
                    new Pais { Nombre = "Panamá"},
                    new Pais { Nombre = "Colombia"},
                    new Pais {Nombre = "Costa Rica"}
                };

                context.Paises.AddRange(paises);
            }


            // Cargar fabricantes
            if (!context.Fabricantes.Any())
            {
                var fabricantes = new List<Fabricante>
                {
                    new Fabricante {Nombre="Fabricante 1"},
                    new Fabricante {Nombre="Fabricante 2"},
                    new Fabricante {Nombre="Fabricante 3"}
                };

                context.Fabricantes.AddRange(fabricantes);
            }

            context.SaveChanges();
        }
    }
}
