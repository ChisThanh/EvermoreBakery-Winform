namespace EvermoreBakery.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class password_reset_tokens
    {
        [Key]
        [StringLength(255)]
        public string email { get; set; }

        [Required]
        [StringLength(255)]
        public string token { get; set; }

        public DateTime? created_at { get; set; }
    }
}
