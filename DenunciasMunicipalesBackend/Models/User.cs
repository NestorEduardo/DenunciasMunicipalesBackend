using System.ComponentModel.DataAnnotations;

namespace DenunciasMunicipalesBackend.Models
{
    public class User
    {
        public int UserId { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres", MinimumLength = 3)]
        [Display(Name = "Nombre completo")]
        public string FullName { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres", MinimumLength = 3)]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres", MinimumLength = 3)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Puntos")]
        public int Points { get; set; }
    }
}