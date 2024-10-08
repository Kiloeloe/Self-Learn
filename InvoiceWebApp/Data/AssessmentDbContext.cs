using System;
using System.Collections.Generic;
using InvoiceWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceWebApp.Data;

public partial class AssessmentDbContext : DbContext
{
    public AssessmentDbContext()
    {
    }

    public AssessmentDbContext(DbContextOptions<AssessmentDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Lookup> Lookups { get; set; }

    public virtual DbSet<LtCourierFee> LtCourierFees { get; set; }

    public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }

    public virtual DbSet<MsCourier> MsCouriers { get; set; }

    public virtual DbSet<MsPayment> MsPayments { get; set; }

    public virtual DbSet<MsProduct> MsProducts { get; set; }

    public virtual DbSet<MsSale> MsSales { get; set; }

    public virtual DbSet<TrInvoice> TrInvoices { get; set; }

    public virtual DbSet<TrInvoiceDetail> TrInvoiceDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<TrInvoiceDetail>(entity =>
        {
            entity.HasKey(e => new { e.InvoiceNo, e.ProductId }).HasName("PK_dbo.trInvoiceDetail");
            entity.HasOne(d => d.Invoice)
              .WithMany(p => p.TrInvoiceDetails)
              .HasForeignKey(d => d.InvoiceNo)
              .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Product)
              .WithMany(p => p.TrInvoiceDetails)
              .HasForeignKey(d => d.ProductId)
              .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<TrInvoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceNo).HasName("PK_dbo.trInvoice");
            entity.HasOne(t => t.Sales)
                .WithMany(s => s.TrInvoices)
                .HasForeignKey(t => t.SalesId);

            entity.HasOne(t => t.Payment)
                .WithMany(p => p.TrInvoices)
                .HasForeignKey(t => t.PaymentType);

            entity.HasOne(t => t.Courier)
                .WithMany(c => c.TrInvoices)
                .HasForeignKey(t => t.CourierId);
        });

        modelBuilder.Entity<LtCourierFee>(entity =>
        {
            entity.HasKey(e => e.WeightId).HasName("PK_dbo.LtCourierFee");
            entity.HasOne(l => l.Courier).WithMany(c => c.LtCourierFees);
            entity.Property(e => e.EndKg).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.Price).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.WeightId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<MigrationHistory>(entity =>
        {
            entity.HasKey(e => new { e.MigrationId, e.ContextKey }).HasName("PK_dbo.__MigrationHistory");
        });

        modelBuilder.Entity<MsPayment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK_dbo.msPayment");
        });

        modelBuilder.Entity<MsProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK_dbo.msProduct");
        });

        modelBuilder.Entity<MsSale>(entity =>
        {
            entity.HasKey(e => e.SalesId).HasName("PK_dbo.msSales");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
