namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class product_interactions
    {
        public long id { get; set; }

        [Required]
        [StringLength(20)]
        public string ip_address { get; set; }

        public int? user_id { get; set; }

        public long product_id { get; set; }

        [Required]
        [StringLength(10)]
        public string type { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        public virtual product product { get; set; }
    }
}
