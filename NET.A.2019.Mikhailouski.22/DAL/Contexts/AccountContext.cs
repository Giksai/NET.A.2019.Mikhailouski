using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using BLL.Interface.Entities;

namespace DAL.Contexts
{
    public class AccountContext : DbContext
    {
        public AccountContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Account> Accounts { get; set; }

        public Account this[int id] => Accounts.Find(id);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;database=AccountsDB;user=root;password=dandan2011");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OwnerFirstName).IsRequired();
                entity.Property(e => e.OwnerLastName).IsRequired();
                entity.Property(e => e.Amount).IsRequired();
                entity.Property(e => e.Points).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.Type).IsRequired();
            });
        }

    }
}
