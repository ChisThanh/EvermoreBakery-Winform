namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class product_reviews
    {
        public long id { get; set; }

        public long user_id { get; set; }

        public long product_id { get; set; }

        public float rating { get; set; }

        [Required]
        [StringLength(100)]
        public string comment { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        public bool? is_show { get; set; }

        public virtual product product { get; set; }

        public virtual user user { get; set; }
    }
}
