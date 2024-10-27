namespace DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class cart
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cart()
        {
            cart_details = new HashSet<cart_details>();
        }

        public long id { get; set; }

        public long user_id { get; set; }

        public long total { get; set; }

        public DateTime? created_at { get; set; }

        public DateTime? updated_at { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cart_details> cart_details { get; set; }

        public virtual user user { get; set; }
    }
}
