namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public bill()
        {
            bill_details = new HashSet<bill_details>();
        }

        public long id { get; set; }

        public long user_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? delivery_date { get; set; }

        public double total { get; set; }

        public byte payment_status { get; set; }

        public byte payment_method { get; set; }

        public byte status { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        public long? coupon_id { get; set; }

        public string note { get; set; }

        [StringLength(255)]
        public string id_payment { get; set; }

        public virtual bill_address bill_address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<bill_details> bill_details { get; set; }

        public virtual coupon coupon { get; set; }

        public virtual user user { get; set; }
    }
}
