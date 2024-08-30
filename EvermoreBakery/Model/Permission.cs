using System.ComponentModel.DataAnnotations.Schema;

namespace EvermoreBakery.Service
{
    [Table("permissions")]
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; } 
    }
}