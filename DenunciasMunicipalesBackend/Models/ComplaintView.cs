using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace DenunciasMunicipalesBackend.Models
{
    [NotMapped]
    public class ComplaintView : Complaint
    {
        [Display(Name = "Imágen")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}