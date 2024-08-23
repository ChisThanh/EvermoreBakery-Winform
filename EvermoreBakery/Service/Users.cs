using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
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
    }
}
