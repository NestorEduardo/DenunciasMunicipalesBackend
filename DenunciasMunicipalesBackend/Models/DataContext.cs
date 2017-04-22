using System.Data.Entity;

namespace DenunciasMunicipalesBackend.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<DenunciasMunicipalesBackend.Models.Complaint> Complaints { get; set; }
    }
}