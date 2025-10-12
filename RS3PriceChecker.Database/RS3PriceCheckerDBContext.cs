using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RS3PriceChecker.Database;

public partial class RS3PriceCheckerDBContext : DbContext
{
    public RS3PriceCheckerDBContext()
    {
    }

    public RS3PriceCheckerDBContext(DbContextOptions<RS3PriceCheckerDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Icon> Icons { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Name> Names { get; set; }

    public virtual DbSet<Price> Prices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Category_ID");

            entity.ToTable("Category");

            entity.HasIndex(e => e.Name, "UQ__Category__737584F60129494C").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(256)
                .HasDefaultValueSql("([dbo].[fn_GetCurrentUserID]())");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Icon).HasMaxLength(256);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(256)
                .HasDefaultValueSql("([dbo].[fn_GetCurrentUserID]())");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<Icon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Icon_ID");

            entity.ToTable("Icon");

            entity.HasIndex(e => e.Small, "UQ__Icon__BB7594E11D1FDC08").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(256)
                .HasDefaultValueSql("([dbo].[fn_GetCurrentUserID]())");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Large).HasMaxLength(256);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(256)
                .HasDefaultValueSql("([dbo].[fn_GetCurrentUserID]())");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Small).HasMaxLength(256);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Item_ID");

            entity.ToTable("Item");

            entity.HasIndex(e => e.ItemId, "UQ__Item__727E83EA1B28B72F").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(256)
                .HasDefaultValueSql("([dbo].[fn_GetCurrentUserID]())");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IconId).HasColumnName("IconID");
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(256)
                .HasDefaultValueSql("([dbo].[fn_GetCurrentUserID]())");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NameId).HasColumnName("NameID");

            entity.HasOne(d => d.Category).WithMany(p => p.Items)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Item_Category_ID");

            entity.HasOne(d => d.Icon).WithMany(p => p.Items)
                .HasForeignKey(d => d.IconId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Item_Icon_ID");

            entity.HasOne(d => d.Name).WithMany(p => p.Items)
                .HasForeignKey(d => d.NameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Item_Name_ID");
        });

        modelBuilder.Entity<Name>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Name_ID");

            entity.ToTable("Name");

            entity.HasIndex(e => e.Value, "UQ__Name__07D9BBC2F71BE948").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(256)
                .HasDefaultValueSql("([dbo].[fn_GetCurrentUserID]())");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(256);
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(256)
                .HasDefaultValueSql("([dbo].[fn_GetCurrentUserID]())");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Value).HasMaxLength(256);
        });

        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Price_ID");

            entity.ToTable("Price");

            entity.HasIndex(e => new { e.ItemId, e.Date }, "UC_Price_ItemID_DATE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(256)
                .HasDefaultValueSql("([dbo].[fn_GetCurrentUserID]())");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.ModifiedBy)
                .HasMaxLength(256)
                .HasDefaultValueSql("([dbo].[fn_GetCurrentUserID]())");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Item).WithMany(p => p.Prices)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Price_Item_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
