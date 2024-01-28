using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ClassLibrary1.Models
{
    public partial class BTContext : DbContext
    {
        public BTContext()
        {
        }

        public BTContext(DbContextOptions<BTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Division2> Division2s { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<Division2>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Division2");

                entity.Property(e => e.Deposit).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
