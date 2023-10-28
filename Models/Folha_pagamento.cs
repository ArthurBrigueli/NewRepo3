using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace pimfo.Models
{
    public class Folha_pagamento
    {

        [Key]
        [DisplayName("ID folha")]
        public int id_folha { get; set; }
        [DisplayName("Id Funcionario")]
        public int id_func { get; set; }

        [DisplayName("Nome funcionario")]
        public string nome_func { get; set; }
        [DisplayName("Salario base")]
        public float salario_base { get; set; }
        [DisplayName("Salario liquido")]
        public float salario_liquido { get; set; }
        [DisplayName("Cargo")]
        public string cargo { get; set; }
        [DisplayName("Data")]
        public string data { get; set; }
        [DisplayName("Alimentação")]
        public float vale_alimentacao { get; set; }
        [DisplayName("Transporte")]

        public float vale_transporte { get; set; }
        [DisplayName("Ferias")]

        public float valor_ferias { get; set; }
        [DisplayName("Imposto de renda")]

        public float imposto_renda { get; set; }
        [DisplayName("Valor fgts")]

        public float valor_fgts { get; set; }
    }
}
