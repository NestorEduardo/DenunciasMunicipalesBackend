using System.Data.Entity;

namespace DenunciasMunicipalesBackend.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }
    }
}