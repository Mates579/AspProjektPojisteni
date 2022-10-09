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
        public DbSet<AspProjektPojisteni.Models.Policyholder> Policyholder { get; set; }
        public DbSet<AspProjektPojisteni.Models.Insurance> Insurance { get; set; }
        public DbSet<AspProjektPojisteni.Models.InsuranceEvent> InsuranceEvent { get; set; }
    }
}