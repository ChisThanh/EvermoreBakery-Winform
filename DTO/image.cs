namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class image
    {
        public long id { get; set; }

        [Required]
        [StringLength(255)]
        public string url { get; set; }

        [StringLength(255)]
        public string imageable_type { get; set; }

        public long? imageable_id { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }
    }
}
