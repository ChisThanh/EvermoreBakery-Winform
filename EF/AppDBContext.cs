using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace EvermoreBakery.EF
{
    public partial class AppDBContext : DbContext
    {
        public AppDBContext()
            : base("name=AppDBContext")
        {
        }

        public virtual DbSet<bill> bills { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<chat> chats { get; set; }
        public virtual DbSet<password_reset_tokens> password_reset_tokens { get; set; }
        public virtual DbSet<permission_user> permission_user { get; set; }
        public virtual DbSet<permission> permissions { get; set; }
        public virtual DbSet<product_interactions> product_interactions { get; set; }
        public virtual DbSet<product_reviews> product_reviews { get; set; }
        public virtual DbSet<product_translations> product_translations { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<role_user> role_user { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<bill_details> bill_details { get; set; }
        public virtual DbSet<config> configs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<bill>()
                .HasMany(e => e.bill_details)
                .WithRequired(e => e.bill)
                .HasForeignKey(e => e.bill_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<category>()
                .HasMany(e => e.products)
                .WithRequired(e => e.category)
                .HasForeignKey(e => e.category_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<permission>()
                .HasMany(e => e.permission_user)
                .WithRequired(e => e.permission)
                .HasForeignKey(e => e.permission_id);

            modelBuilder.Entity<permission>()
                .HasMany(e => e.roles)
                .WithMany(e => e.permissions)
                .Map(m => m.ToTable("permission_role"));

            modelBuilder.Entity<product>()
                .HasMany(e => e.product_interactions)
                .WithRequired(e => e.product)
                .HasForeignKey(e => e.product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.product_reviews)
                .WithRequired(e => e.product)
                .HasForeignKey(e => e.product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.product_translations)
                .WithRequired(e => e.product)
                .HasForeignKey(e => e.product_id);

            modelBuilder.Entity<product>()
                .HasMany(e => e.bill_details)
                .WithRequired(e => e.product)
                .HasForeignKey(e => e.product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<role>()
                .HasMany(e => e.role_user)
                .WithRequired(e => e.role)
                .HasForeignKey(e => e.role_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.bills)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.chats)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.receiver_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.chats1)
                .WithRequired(e => e.user1)
                .HasForeignKey(e => e.sender_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.product_reviews)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);
        }
    }
}
