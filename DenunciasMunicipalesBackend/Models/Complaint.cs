using System;
using System.ComponentModel.DataAnnotations;

namespace DenunciasMunicipalesBackend.Models
{
    public class Complaint
    {
        [Key]
        public int ComplaintId { get; set; }

        [Display(Name = "Descripción")]
        [StringLength(200, ErrorMessage = "El campo {0} debe estar entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Description { get; set; }

        [Display(Name = "Dirección del caso")]
        public string CaseAddress { get; set; }

        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }

        [Display(Name = "Creado por")]
        public string CreatedBy { get; set; }
    }
}