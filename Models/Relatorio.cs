using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace pimfo.Models
{
    public class Relatorio
    {
        [Key]
        [DisplayName("ID")]
        public int id_relatorio { get; set; }

        [DisplayName("Data")]
        public string data_relatorio { get; set; }
        [DisplayName("Valor Total")]
        public double valor_total { get; set; }
    }
}
