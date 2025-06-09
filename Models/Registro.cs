using System.ComponentModel.DataAnnotations;

namespace Michael_Jose_AP1_P1.Models
{
    public class Registro
    {
        [Key]
        public int AporteId { get; set; }

        [Required(ErrorMessage = "El Nombre es requerido")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Monto de Aporte es requerido")]
        public double Mount{ get; set; }
    }
}
