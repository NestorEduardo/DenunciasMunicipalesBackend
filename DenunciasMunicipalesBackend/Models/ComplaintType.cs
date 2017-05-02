using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DenunciasMunicipalesBackend.Models
{
    public class ComplaintType
    {
        [Key]
        public int ComplaintTypeId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(30, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres", MinimumLength = 3)]
        [Display(Name = "Tipo de denuncia")]
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<Complaint> Complaints { get; set; }
    }
}