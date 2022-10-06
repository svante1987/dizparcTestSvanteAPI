using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace dizparcTestSvante
{
    public partial class CSVImportContext : DbContext
    {
        public CSVImportContext()
        {
        }

        public CSVImportContext(DbContextOptions<CSVImportContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Born> Borns { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-7I73AQN6;Database=SvanteDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Born>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Born");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Kön)
                    .HasMaxLength(20)
                    .HasColumnName("kön")
                    .IsFixedLength();

                entity.Property(e => e.LevandeFödda2016).HasColumnName("Levande_Födda_2016");

                entity.Property(e => e.LevandeFödda2017).HasColumnName("Levande_Födda_2017");

                entity.Property(e => e.LevandeFödda2018).HasColumnName("Levande_Födda_2018");

                entity.Property(e => e.LevandeFödda2019).HasColumnName("Levande_Födda_2019");

                entity.Property(e => e.LevandeFödda2020).HasColumnName("Levande_Födda_2020");

                entity.Property(e => e.Region)
                    .HasMaxLength(50)
                    .HasColumnName("region")
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
