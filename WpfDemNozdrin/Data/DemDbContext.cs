using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WpfDemNozdrin.Models;

namespace WpfDemNozdrin.Data
{
    public class DemDbContext : DbContext
    {
        public DemDbContext(DbContextOptions options) : base(options)
        {
        }
        public DemDbContext()
        {
        }
        public DbSet<RoleDem> RoleDems { get; set; }
        public DbSet<UserDem> UserDems { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Report> Reports { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-QHR1LI1\\MSSQLSERVERR; database=DemNozdrin; Integrated Security = true; Trusted_Connection = true; TrustServerCertificate = true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<RoleDem>().HasData
                (
                    new RoleDem { Id = 1, Name = "Клиент" },
                    new RoleDem { Id = 2, Name = "Менеджер" },
                    new RoleDem { Id = 3, Name = "Исполнитель" }
                );
            modelBuilder.Entity<Request>()
            .HasOne(r => r.Client)
            .WithMany()
            .HasForeignKey(r => r.ClientId)
            .OnDelete(DeleteBehavior.NoAction); // NoAction вместо Cascade

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Executor)
                .WithMany()
                .HasForeignKey(r => r.ExecutorId)
                .OnDelete(DeleteBehavior.NoAction); // NoAction вместо Cascade

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Manager)
                .WithMany()
                .HasForeignKey(r => r.ManagerId)
                .OnDelete(DeleteBehavior.NoAction); // NoAction вместо Cascade
        }
    }
}
