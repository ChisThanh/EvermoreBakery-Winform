namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class bill_address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long bill_id { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [StringLength(255)]
        public string phone { get; set; }

        [StringLength(255)]
        public string city { get; set; }

        [StringLength(255)]
        public string district { get; set; }

        [StringLength(255)]
        public string ward { get; set; }

        [StringLength(255)]
        public string street { get; set; }

        public virtual bill bill { get; set; }
    }
}
