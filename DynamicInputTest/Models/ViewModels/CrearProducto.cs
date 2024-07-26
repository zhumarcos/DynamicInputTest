namespace DynamicInputTest.Models.ViewModels
{
    public class CrearProducto
    {
        public string Nombre { get; set; }
        public List<AcondicionadorViewItem> Acondicionadores { get; set; } = new List<AcondicionadorViewItem>();
    }
}
