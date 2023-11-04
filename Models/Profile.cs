using System.ComponentModel;

namespace pimfo.Models
{
    public class Profile
    {
        public string nome_func { get; set; }
        public float salario_base { get; set; }
        public float salario_liquido { get; set; }
        public string cargo { get; set; }
        public string data { get; set; }
        public float vale_alimentacao { get; set; }

        public float vale_transporte { get; set; }

        public float valor_ferias { get; set; }

        public float imposto_renda { get; set; }

        public float valor_fgts { get; set; }

    }
}
