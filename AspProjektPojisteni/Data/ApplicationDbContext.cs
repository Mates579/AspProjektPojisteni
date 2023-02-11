using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AspProjektPojisteni.Models;

namespace AspProjektPojisteni.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Policyholder> Policyholder { get; set; }
        public DbSet<Insurance> Insurance { get; set; }
        public DbSet<InsuranceEvent> InsuranceEvent { get; set; }
    }
}