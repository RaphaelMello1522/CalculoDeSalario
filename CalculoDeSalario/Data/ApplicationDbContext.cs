using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CalculoDeSalario.Models;

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
        public DbSet<CalculoDeSalario.Models.TotalCost> TotalCost { get; set; } = default!;


    }
}