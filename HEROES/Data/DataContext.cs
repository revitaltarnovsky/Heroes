using HEROES.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HEROES.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Hero> Heroes { get; set; }

        public DbSet<Training> Trainings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var trainers = builder.Entity<Trainer>()
                .ToTable("Trainers");

            trainers.HasKey(t => t.Id);

            var heroes = builder.Entity<Hero>()
                .ToTable("Heroes");

            heroes.HasKey(h => h.Id);

            heroes.Property(h => h.Ability).HasColumnType("int");
            heroes.Property(h => h.SuitColors).HasColumnType("int");
            heroes.Property(h => h.StartingPower).HasColumnType("decimal(18,2)");
            heroes.Property(h => h.CurrentPower).HasColumnType("decimal(18,2)");

            heroes.HasOne(t => t.Trainer)
                  .WithMany(h => h.Heroes)
                  .HasForeignKey(t => t.TrainerId)
                  .OnDelete(DeleteBehavior.Cascade);


            var trainings = builder.Entity<Training>()
                .ToTable("Trainings");

            trainers.HasKey(t => t.Id);

            trainings.HasOne(h => h.Hero)
                .WithMany(t => t.Trainings)
                .HasForeignKey(h => h.HeroId)
                .OnDelete(DeleteBehavior.Cascade);




        }
    }
}
