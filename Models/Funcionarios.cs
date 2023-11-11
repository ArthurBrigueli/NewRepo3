using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace pimfo.Models
{
    public class Funcionarios
    {

        [Key]
        public int id { get; set; }

        [DisplayName("Nome")]
        public string nome { get; set; }
        [DisplayName("RG")]
        public string rg { get; set; }
        [DisplayName("CPF")]
        public string cpf { get; set; }
        [DisplayName("DATA")]
        public string data_nasc { get; set; }
        [DisplayName("ENDEREÇO")]
        public string endereco { get; set; }
        [DisplayName("CARGO")]
        public string cargo { get; set; }
        [DisplayName("SALARIO BRUTO")]
        public float salario_bruto { get; set; }
        [DisplayName("RH")]
        public bool rh { get; set; }
        [DisplayName("Adiantamento")]
        public float? valor_adiantamento { get; set; } 
    }
}
