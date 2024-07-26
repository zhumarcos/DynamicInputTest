using System.ComponentModel.DataAnnotations;

namespace DynamicInputTest.Models
{
    public class Producto
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;
        public List<Acondicionador> Acondicionadores { get; set; }
    }

    public class Acondicionador
    {
        public int Id { get; set; }
        public Pais Pais { get; set; }
        public Fabricante Fabricante { get; set; }
    }

    public class Pais
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;
    }
    public class Fabricante
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Nombre { get; set; }
    }
}
