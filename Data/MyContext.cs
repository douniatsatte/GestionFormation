// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using GestionFormation.Models;

namespace GestionFormation.Data
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Formateur> Formateurs { get; set; }
        public DbSet<Formation> Formations { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Modalite> Modalites { get; set; }
        public DbSet<Apprenant> Apprenants { get; set; }
        public DbSet<Inscription> Inscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Formation>()
                .HasOne(f => f.Categorie)
                .WithMany(c => c.Formations)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Formation>()
                .HasOne(f => f.Formateur)
                .WithMany(fo => fo.Formations)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}