using System.ComponentModel.DataAnnotations;

namespace pimfo.Models
{
    public class Relatorio
    {
        [Key]
        public int id { get; set; }

        public string data { get; set; }

        public float valor { get; set; }
    }
}
