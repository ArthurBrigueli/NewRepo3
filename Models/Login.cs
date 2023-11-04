using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace pimfo.Models
{
    public class Login
    {
        [Key]
        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("Usuario")]
        public string usuario { get; set; }

        [DisplayName("Senha")]
        public string senha { get; set; }

        [DisplayName("rh")]
        public bool rh { get; set; }
    }
}
