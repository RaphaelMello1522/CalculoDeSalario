using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Agendamento> Agendamentos { get; set; }

        public virtual DbSet<Cargo> Cargos { get; set; }

        public virtual DbSet<DatasAgendamento> DatasAgendamentos { get; set; }
        public virtual DbSet<People> People { get; set; }

        public virtual DbSet<Salary> Salaries { get; set; }

    }
}