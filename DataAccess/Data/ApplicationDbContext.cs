using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
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
        public DbSet<Salary> Salary { get; set; } = default!;
        public DbSet<People> People { get; set; } = default!;
        public DbSet<Vagas> Vagas { get; set; } = default!;
        public DbSet<Cargo> Cargo { get; set; } = default!;
        public DbSet<Agendamento> Agendamentos { get; set; } = default!;
        public DbSet<DatasAgendamento> DatasAgendamentos { get; set; } = default!;


    }
}