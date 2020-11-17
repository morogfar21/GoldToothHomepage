﻿using System;
using GuldtandMVC_Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GuldtandMVC_Identity
{
    public partial class Prj4databaseContext : IdentityDbContext<ApplicationUser>
    {
        public Prj4databaseContext(DbContextOptions<Prj4databaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Blacklist> Blacklist { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Directions> Directions { get; set; }
        public virtual DbSet<Ingredient> Ingredient { get; set; }
        public virtual DbSet<IngredientList> IngredientList { get; set; }
        public virtual DbSet<OpenHours> OpenHours { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<Recipe> Recipe { get; set; }
        public virtual DbSet<RecipeCategory> RecipeCategory { get; set; }
        public virtual DbSet<RetailChain> RetailChain { get; set; }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Blacklist>(entity =>
            {
                entity.HasKey(e => e.Category)
                    .HasName("PK__blacklis__F7F53CC351A434DF");

                entity.ToTable("blacklist");

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryName)
                    .HasName("PK__category__5189E254535378E8");

                entity.ToTable("category");

                entity.Property(e => e.CategoryName)
                    .HasColumnName("category_name")
                    .HasMaxLength(50)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Directions>(entity =>
            {
                entity.ToTable("directions");

                entity.Property(e => e.DirectionsId).HasColumnName("directions_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(255);

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.Directions)
                    .HasForeignKey(d => d.RecipeId)
                    .HasConstraintName("FK__direction__recip__67152DD3");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("ingredient");

                entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.AmountUnit)
                    .HasColumnName("amount_unit")
                    .HasMaxLength(15);

                entity.Property(e => e.IngredientListId).HasColumnName("ingredientList_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.HasOne(d => d.IngredientList)
                    .WithMany(p => p.Ingredient)
                    .HasForeignKey(d => d.IngredientListId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ingredien__ingre__60683044");

                entity.HasOne(d => d.NameNavigation)
                    .WithMany(p => p.Ingredient)
                    .HasForeignKey(d => d.Name)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryName");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Ingredient)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ingredien__produ__5F740C0B");
            });

            modelBuilder.Entity<IngredientList>(entity =>
            {
                entity.ToTable("ingredientList");

                entity.HasIndex(e => e.RecipeId)
                    .HasName("UQ__ingredie__3571ED9A29CD80B9")
                    .IsUnique();

                entity.Property(e => e.IngredientListId).HasColumnName("ingredientList_id");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.HasOne(d => d.Recipe)
                    .WithOne(p => p.IngredientList)
                    .HasForeignKey<IngredientList>(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__ingredien__recip__57D2EA43");
            });

            modelBuilder.Entity<OpenHours>(entity =>
            {
                entity.HasKey(e => e.DayOfWeek)
                    .HasName("PK__open_hou__869EECA867AD9B20");

                entity.ToTable("open_hours");

                entity.Property(e => e.DayOfWeek)
                    .HasColumnName("day_of_week")
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.OpenFrom).HasColumnName("open_from");

                entity.Property(e => e.OpenTo).HasColumnName("open_to");

                entity.Property(e => e.RetailChainId).HasColumnName("retail_chain_id");

                entity.HasOne(d => d.RetailChain)
                    .WithMany(p => p.OpenHours)
                    .HasForeignKey(d => d.RetailChainId)
                    .HasConstraintName("FK__open_hour__retai__41E3A924");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.ImgSrc)
                    .HasColumnName("img_src")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.RetailChainId).HasColumnName("retail_chain_id");

                entity.Property(e => e.ValidFrom)
                    .HasColumnName("valid_from")
                    .HasColumnType("datetime");

                entity.Property(e => e.ValidTo)
                    .HasColumnName("valid_to")
                    .HasColumnType("datetime");

                entity.Property(e => e.Volume).HasColumnName("volume");

                entity.Property(e => e.VolumePrice).HasColumnName("volume_price");

                entity.HasOne(d => d.RetailChain)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.RetailChainId)
                    .HasConstraintName("FK__product__retail___46A85E41");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.CategoryName })
                    .HasName("PK__product___121AE3D0B94E8F57");

                entity.ToTable("product_category");

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.CategoryName)
                    .HasColumnName("category_name")
                    .HasMaxLength(50);

                entity.HasOne(d => d.CategoryNameNavigation)
                    .WithMany(p => p.ProductCategory)
                    .HasForeignKey(d => d.CategoryName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__product_c__categ__5031C87B");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductCategory)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__product_c__produ__4F3DA442");
            });

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.ToTable("recipe");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.Property(e => e.CookTime).HasColumnName("cook_time");

                entity.Property(e => e.ImgSrc)
                    .HasColumnName("img_src")
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.SavingsAbsolute).HasColumnName("savings_absolute");

                entity.Property(e => e.Servings).HasColumnName("servings");
            });

            modelBuilder.Entity<RecipeCategory>(entity =>
            {
                entity.HasKey(e => new { e.RecipeId, e.CategoryName })
                    .HasName("PK__recipe_c__606973BEB82D8C04");

                entity.ToTable("recipe_category");

                entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

                entity.Property(e => e.CategoryName)
                    .HasColumnName("category_name")
                    .HasMaxLength(50);

                entity.HasOne(d => d.CategoryNameNavigation)
                    .WithMany(p => p.RecipeCategory)
                    .HasForeignKey(d => d.CategoryName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__recipe_ca__categ__5402595F");

                entity.HasOne(d => d.Recipe)
                    .WithMany(p => p.RecipeCategory)
                    .HasForeignKey(d => d.RecipeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__recipe_ca__recip__530E3526");
            });

            modelBuilder.Entity<RetailChain>(entity =>
            {
                entity.ToTable("retail_chain");

                entity.Property(e => e.RetailChainId).HasColumnName("retail_chain_id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100);
            });
        }
    }
}
