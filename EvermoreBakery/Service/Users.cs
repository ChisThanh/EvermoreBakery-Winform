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
        [Key]
        public int Id { set; get; }

        [Required]
        [StringLength(50)]
        public string Name { set; get; }
    }
}
