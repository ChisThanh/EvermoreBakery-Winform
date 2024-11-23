namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class cart
    {
        public long id { get; set; }

        public long? user_id { get; set; }

        [StringLength(255)]
        public string cookie_id { get; set; }

        public double total { get; set; }

        public string cart_details { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        public virtual user user { get; set; }
    }
}
