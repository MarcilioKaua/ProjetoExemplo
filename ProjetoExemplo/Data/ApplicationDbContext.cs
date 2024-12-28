using Microsoft.EntityFrameworkCore;
using ProjetoExemplo.Models;

namespace ProjetoExemplo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Processo> Processos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Processo>()
                .Property(p => p.Npu)
                 .HasMaxLength(25)
                 .IsRequired();
        }
    }
}
