using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvermoreBakery.Service
{
    [Table("users")]
    public class Users
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }

        [Column("remember_token")]
        public string? RememberToken { set; get; }

        [Column("created_at")]
        public DateTime? CreatedAt { set; get; }

        [Column("updated_at")]
        public DateTime? UpdateAt { set; get; }

        [Column("deleted_at")]
        public DateTime? DeletedAt { set; get; }

        [NotMapped]
        public virtual List<string> Permissions { get; set; } = new List<string>();

        public bool HasPermissions(string permissionName)
        {
            if (Permissions == null || !Permissions.Any())
            {
                Permissions = GetPermissions();
            }

            return Permissions.Contains(permissionName);
        }

        public List<string> GetPermissions()
        {
            using (var context = new ApplicationDbContext())
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
                              .SqlQueryRaw<string>(sql, Id)
                              .ToList();
            }
        }
    }
}
