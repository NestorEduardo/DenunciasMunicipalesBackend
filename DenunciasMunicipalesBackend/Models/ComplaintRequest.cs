using System.ComponentModel.DataAnnotations.Schema;

namespace DenunciasMunicipalesBackend.Models
{
    [NotMapped]
    public class ComplaintRequest : Complaint
    {
        public byte[] ImageArray { get; set; }
    }
}