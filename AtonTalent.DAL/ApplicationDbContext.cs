using AtonTalent.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtonTalent.DAL
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<User>().HasData(new User()
            {
                Id = Guid.NewGuid(),
                Login = "sanokkk",
                Password = "password",
                Name = "Alexander",
                Gender = 1,
                Birthday = new DateTime(year: 2002, month: 9, day: 18),
                Admin = true,
                CreatedOn = DateTime.Now,
                CreatedBy = "sanokkk",
            });
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().Property(p => p.Id).HasColumnName(nameof(User.Id).ToLower());
            modelBuilder.Entity<User>().Property(p => p.Login).HasColumnName(nameof(User.Login).ToLower());
            modelBuilder.Entity<User>().Property(p => p.Password).HasColumnName(nameof(User.Password).ToLower());
            modelBuilder.Entity<User>().Property(p => p.Name).HasColumnName(nameof(User.Name).ToLower());
            modelBuilder.Entity<User>().Property(p => p.Gender).HasColumnName(nameof(User.Gender).ToLower());
            modelBuilder.Entity<User>().Property(p => p.Birthday).HasColumnName(nameof(User.Birthday).ToLower());
            modelBuilder.Entity<User>().Property(p => p.Admin).HasColumnName(nameof(User.Admin).ToLower());
            modelBuilder.Entity<User>().Property(p => p.CreatedOn).HasColumnName(nameof(User.CreatedOn).ToLower());
            modelBuilder.Entity<User>().Property(p => p.CreatedBy).HasColumnName(nameof(User.CreatedBy).ToLower());
            modelBuilder.Entity<User>().Property(p => p.ModifiedOn).HasColumnName(nameof(User.ModifiedOn).ToLower());
            modelBuilder.Entity<User>().Property(p => p.ModifiedBy).HasColumnName(nameof(User.ModifiedBy).ToLower());
            modelBuilder.Entity<User>().Property(p => p.RevokedOn).HasColumnName(nameof(User.RevokedOn).ToLower());
            modelBuilder.Entity<User>().Property(p => p.RevokedBy).HasColumnName(nameof(User.RevokedBy).ToLower());
        }
    }
}
