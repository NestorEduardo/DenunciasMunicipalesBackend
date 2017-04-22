using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DenunciasMunicipalesBackend.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Complaint> Complaints { get; set; }
    }
}