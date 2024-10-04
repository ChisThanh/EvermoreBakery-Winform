namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class product_translations
    {
        public long id { get; set; }

        public long product_id { get; set; }

        [Required]
        [StringLength(255)]
        public string locale { get; set; }

        public string description { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        public virtual product product { get; set; }
    }
}
