using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OrderManagement.API.Models.Entity
{
    public partial class OrderManagementContext : DbContext
    {
        public OrderManagementContext()
        {
        }

        public OrderManagementContext(DbContextOptions<OrderManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ItemCode)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerOrderNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DestinationAddress)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ItemCode)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Note).HasMaxLength(250);

                entity.Property(e => e.OutputAddress)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.QuantityUnit)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.StatusUpdateDate).HasColumnType("datetime");

                entity.Property(e => e.SystemOrderNo)
                    .IsRequired()
                    .HasMaxLength(36);

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.WeightUnit)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("OrderStatus");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
