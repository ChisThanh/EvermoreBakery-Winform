namespace EvermoreBakery.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class chat
    {
        public long id { get; set; }

        public long sender_id { get; set; }

        public long receiver_id { get; set; }

        [StringLength(255)]
        public string message { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        public virtual user user { get; set; }

        public virtual user user1 { get; set; }
    }
}
