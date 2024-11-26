using DotNetEnv;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DTO
{
    public partial class EvermoreBakeryContext : DbContext
    {
        private static readonly Lazy<EvermoreBakeryContext> _instance =
                new Lazy<EvermoreBakeryContext>(() => new EvermoreBakeryContext());

        private static readonly Lazy<string> _connectionString = new Lazy<string>(CreateConnectionString);

        public EvermoreBakeryContext() : base()
        {
            Database.Connection.ConnectionString = _connectionString.Value;
            RefreshContext();
        }

        public static EvermoreBakeryContext Instance => _instance.Value;


        private static string CreateConnectionString()
        {
            try
            {
                Env.TraversePath().Load();

                string dbHost = Env.GetString("DB_HOST");
                string dbName = Env.GetString("DB_DATABASE");
                string dbUser = Env.GetString("DB_USERNAME");
                string dbPass = Env.GetString("DB_PASSWORD");

                if (string.IsNullOrEmpty(dbHost) || string.IsNullOrEmpty(dbName) ||
                    string.IsNullOrEmpty(dbUser) || string.IsNullOrEmpty(dbPass))
                {
                    throw new InvalidOperationException("Database connection information is incomplete.");
                }

                return $@"data source={dbHost};initial catalog={dbName};persist security info=True;user id={dbUser};password={dbPass};encrypt=False;MultipleActiveResultSets=True;App=EntityFramework;Pooling=False";
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to create the connection string.", ex);
            }
        }

        private void RefreshContext()
        {
            foreach (var entry in this.ChangeTracker.Entries().ToList())
            {
                entry.State = EntityState.Detached;
            }
            this.Database.Connection.Close();
            this.Database.Connection.Open();
        }


        public virtual DbSet<bill_address> bill_address { get; set; }
        public virtual DbSet<bill_details> bill_details { get; set; }
        public virtual DbSet<bill> bills { get; set; }
        public virtual DbSet<cart> carts { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<chat> chats { get; set; }
        public virtual DbSet<coupon> coupons { get; set; }
        public virtual DbSet<event_products> event_products { get; set; }
        public virtual DbSet<_event> events { get; set; }
        public virtual DbSet<image> images { get; set; }
        public virtual DbSet<ingredient_product> ingredient_product { get; set; }
        public virtual DbSet<ingredient> ingredients { get; set; }
        public virtual DbSet<nutrition> nutritions { get; set; }
        public virtual DbSet<nutrition_product> nutrition_product { get; set; }
        public virtual DbSet<permission_user> permission_user { get; set; }
        public virtual DbSet<permission_role> permission_role { get; set; }
        public virtual DbSet<permission> permissions { get; set; }
        public virtual DbSet<product_interactions> product_interactions { get; set; }
        public virtual DbSet<product_reviews> product_reviews { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<purchase_order_details> purchase_order_details { get; set; }
        public virtual DbSet<purchase_orders> purchase_orders { get; set; }
        public virtual DbSet<role_user> role_user { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<supplier> suppliers { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<config> configs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<bill>()
                .HasOptional(e => e.bill_address)
                .WithRequired(e => e.bill);

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

            modelBuilder.Entity<coupon>()
                .HasMany(e => e.bills)
                .WithOptional(e => e.coupon)
                .HasForeignKey(e => e.coupon_id);

            modelBuilder.Entity<_event>()
                .HasMany(e => e.event_products)
                .WithRequired(e => e._event)
                .HasForeignKey(e => e.event_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ingredient>()
                .HasMany(e => e.ingredient_product)
                .WithRequired(e => e.ingredient)
                .HasForeignKey(e => e.ingredient_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ingredient>()
                .HasMany(e => e.purchase_order_details)
                .WithRequired(e => e.ingredient)
                .HasForeignKey(e => e.ingredient_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<nutrition>()
                .HasMany(e => e.nutrition_product)
                .WithRequired(e => e.nutrition)
                .HasForeignKey(e => e.nutrition_id)
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
                .HasMany(e => e.bill_details)
                .WithRequired(e => e.product)
                .HasForeignKey(e => e.product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.event_products)
                .WithRequired(e => e.product)
                .HasForeignKey(e => e.product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.ingredient_product)
                .WithRequired(e => e.product)
                .HasForeignKey(e => e.product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.nutrition_product)
                .WithRequired(e => e.product)
                .HasForeignKey(e => e.product_id)
                .WillCascadeOnDelete(false);

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
                .HasMany(e => e.users)
                .WithMany(e => e.products)
                .Map(m => m.ToTable("likes"));

            modelBuilder.Entity<purchase_orders>()
                .HasMany(e => e.purchase_order_details)
                .WithRequired(e => e.purchase_orders)
                .HasForeignKey(e => e.purchase_order_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<role>()
                .HasMany(e => e.role_user)
                .WithRequired(e => e.role)
                .HasForeignKey(e => e.role_id);

            modelBuilder.Entity<supplier>()
                .HasMany(e => e.purchase_orders)
                .WithRequired(e => e.supplier)
                .HasForeignKey(e => e.supplier_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.bills)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.carts)
                .WithOptional(e => e.user)
                .HasForeignKey(e => e.user_id);

            modelBuilder.Entity<user>()
                .HasMany(e => e.permission_user)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.product_reviews)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.role_user)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);
        }
    }
}
