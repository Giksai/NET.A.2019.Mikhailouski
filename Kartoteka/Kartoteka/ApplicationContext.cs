using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;

namespace Kartoteka
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Record> Records { get; set; }

        //public ApplicationContext()
        //{
        //    Database.EnsureCreated();
        //}

        public Record this[int id] => Records.Find(id);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=Kartoteka;user=root;password=dandan2011");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Record>(entity =>
            {
                entity.HasKey(e => e.RecordId);
                entity.Property(e => e.FirstName).IsRequired();
                entity.Property(e => e.LastName).IsRequired();
                entity.Property(e => e.DateOfBirth).IsRequired();
            });
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Kartoteka;Trusted_Connection=True;");

        //}
    }
}
