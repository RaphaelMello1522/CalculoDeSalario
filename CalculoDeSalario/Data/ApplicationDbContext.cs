using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CalculoDeSalario.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CalculoDeSalario.Models.Salary> Salary { get; set; } = default!;
        public DbSet<CalculoDeSalario.Models.People> People { get; set; } = default!;
        public DbSet<CalculoDeSalario.Models.Vagas> Vagas { get; set; } = default!;
        public DbSet<CalculoDeSalario.Models.Cargo> Cargo { get; set; } = default!;


    }
}