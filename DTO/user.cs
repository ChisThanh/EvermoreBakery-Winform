namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Runtime.InteropServices.WindowsRuntime;

    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            bills = new HashSet<bill>();
            carts = new HashSet<cart>();
            permission_user = new HashSet<permission_user>();
            product_reviews = new HashSet<product_reviews>();
            role_user = new HashSet<role_user>();
            products = new HashSet<product>();
        }

        public long id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [Required]
        [StringLength(255)]
        public string email { get; set; }

        public DateTime? email_verified_at { get; set; }

        [Required]
        [StringLength(255)]
        public string password { get; set; }

        public bool is_chatbot { get; set; }

        [StringLength(255)]
        public string chat_id { get; set; }

        [StringLength(100)]
        public string remember_token { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        public DateTime? deleted_at { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bill> bills { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cart> carts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<permission_user> permission_user { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product_reviews> product_reviews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<role_user> role_user { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product> products { get; set; }

        [NotMapped]
        public virtual List<string> Permissions { get; set; } = new List<string>();

        [NotMapped]
        public virtual string Roles { get; set; } = string.Empty;

        public bool HasRoles(string roleName)
        {
            try
            {
                if (Roles == null || string.IsNullOrWhiteSpace(Roles))
                    Roles = GetRoles();

                return Roles.Contains(roleName);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool HasPermissions(string permissionName)
        {
            if (Permissions == null || !Permissions.Any())
            {
                Permissions = GetPermissions();
            }

            return Permissions.Contains(permissionName);
        }

        public string GetRoles()
        {
            using (var context = new EvermoreBakeryContext())
            {
                string sql = @"
                    SELECT r.name FROM role_user ru
                    JOIN roles r ON ru.role_id = r.id
                    WHERE ru.user_id = @p0";

                return context.Database
                              .SqlQuery<string>(sql, id)
                              .FirstOrDefault();
            }
        }

        public List<string> GetPermissions()
        {
            using (var context = new EvermoreBakeryContext())
            {
                string sql = @"
                    SELECT permission_name
                    FROM (
                        SELECT ps.name AS permission_name
                        FROM users us
                        JOIN role_user ru ON ru.user_id = us.id
                        JOIN permission_role pr ON ru.role_id = pr.role_id
                        JOIN permissions ps ON pr.permission_id = ps.id
                        WHERE us.id = @p0
                        UNION ALL
                        SELECT ps.name AS permission_name
                        FROM users us
                        JOIN permission_user pu ON us.id = pu.user_id
                        JOIN permissions ps ON ps.id = pu.permission_id
                        WHERE us.id = @p0
                    ) AS tb";

                return context.Database
                              .SqlQuery<string>(sql, id)
                              .ToList();
            }
        }
    }
}
