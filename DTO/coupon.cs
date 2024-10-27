namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class coupon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public coupon()
        {
            users = new HashSet<user>();
        }

        public long id { get; set; }

        [Required]
        [StringLength(255)]
        public string code { get; set; }

        public double? discount_amount { get; set; }

        public double? discount_percentage { get; set; }

        public int quantity { get; set; }

        [Column(TypeName = "date")]
        public DateTime? expires_at { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<user> users { get; set; }
    }
}
