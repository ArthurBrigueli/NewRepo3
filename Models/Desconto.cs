using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace pimfo.Models
{
    public class Desconto
    {
        [Key]
        public int id { get; set; }

        [DisplayName("Alimentação")]
        public float vale_alimentacao { get; set; }

        [DisplayName("Transporte")]
        public float vale_transporte { get; set; }
        [DisplayName("Ferias")]
        public float valor_ferias { get; set; }
        [DisplayName("Imposto de Renda")]
        public float imposto_renda { get; set; }
        [DisplayName("FGTS")]
        public float valor_fgts { get; set; }
    }
}
