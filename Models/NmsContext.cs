using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NMS.Models;

public partial class NmsContext : DbContext
{
    public NmsContext()
    {
    }

    public NmsContext(DbContextOptions<NmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<NewsArticle> NewsArticles { get; set; }

    public virtual DbSet<SystemAccount> SystemAccounts { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(config.GetConnectionString("DBContext"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryDescription).HasMaxLength(50);
            entity.Property(e => e.CategoryName).HasMaxLength(50);
            entity.Property(e => e.ParentCategoryId).HasColumnName("ParentCategoryID");

            entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory)
                .HasForeignKey(d => d.ParentCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Category_Category");
        });

        modelBuilder.Entity<NewsArticle>(entity =>
        {
            entity.ToTable("NewsArticle");

            entity.Property(e => e.NewsArticleId).HasColumnName("NewsArticleID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedById).HasColumnName("CreatedByID");
            entity.Property(e => e.Headline).HasMaxLength(200);
            entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            entity.Property(e => e.NewsSource).HasMaxLength(200);
            entity.Property(e => e.NewsTitle).HasMaxLength(200);
            entity.Property(e => e.UpdateById).HasColumnName("UpdateByID");

            entity.HasOne(d => d.Category).WithMany(p => p.NewsArticles)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_NewsArticle_Category");

            entity.HasOne(d => d.CreatedBy).WithMany(p => p.NewsArticleCreatedBies)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK_NewsArticle_SystemAccount");

            entity.HasOne(d => d.UpdateBy).WithMany(p => p.NewsArticleUpdateBies)
                .HasForeignKey(d => d.UpdateById)
                .HasConstraintName("FK_NewsArticle_SystemAccount1");

            entity.HasMany(d => d.Tags).WithMany(p => p.NewArticles)
                .UsingEntity<Dictionary<string, object>>(
                    "NewsTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_NewsTag_Tag"),
                    l => l.HasOne<NewsArticle>().WithMany()
                        .HasForeignKey("NewArticleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_NewsTag_NewsArticle"),
                    j =>
                    {
                        j.HasKey("NewArticleId", "TagId");
                        j.ToTable("NewsTag");
                        j.IndexerProperty<int>("NewArticleId").HasColumnName("NewArticleID");
                        j.IndexerProperty<int>("TagId")
                            .ValueGeneratedOnAdd()
                            .HasColumnName("TagID");
                    });
        });

        modelBuilder.Entity<SystemAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId);

            entity.ToTable("SystemAccount");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.AccountEmail).HasMaxLength(50);
            entity.Property(e => e.AccountName).HasMaxLength(50);
            entity.Property(e => e.AccountPassword).HasMaxLength(50);
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("Tag");

            entity.Property(e => e.TagId).HasColumnName("TagID");
            entity.Property(e => e.TagName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
