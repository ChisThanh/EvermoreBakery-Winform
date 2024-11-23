namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class chat
    {
        public long id { get; set; }

        [Required]
        [StringLength(255)]
        public string chat_id { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string message { get; set; }

        [Required]
        [StringLength(255)]
        public string user_name { get; set; }

        public bool is_customer { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }
    }
}
