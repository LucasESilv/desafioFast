using Microsoft.EntityFrameworkCore;

namespace rastreamentoWorkshopAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<AtaWorkshop> Atas { get; set; }
        
        public DbSet<AtaColaborador> AtaColaboradores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AtaColaborador>()
                .HasKey(ac => new { ac.AtaWorkshopId, ac.ColaboradorId });

            base.OnModelCreating(modelBuilder);
        }
    }
}