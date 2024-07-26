using System.ComponentModel;

namespace DynamicInputTest.Models.ViewModels
{
    public class AcondicionadorViewItem
    {
        [DisplayName("País")]
        public int IdPais { get; set; }
        [DisplayName("Fabricante")]
        public int IdFabricante { get; set; }
    }
}
