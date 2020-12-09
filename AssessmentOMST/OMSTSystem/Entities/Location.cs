namespace OMSTSystem.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    internal partial class Location
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Location()
        {
            Theatres = new HashSet<Theatre>();
        }

        public int LocationID { get; set; }

        [Required(ErrorMessage ="Description is required")]
        [StringLength(30, ErrorMessage = "Description is linited to a maximum 30 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(1000, ErrorMessage = "Address is linited to a maximum 100 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(50, ErrorMessage = "City is linited to a maximum 50 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [StringLength(12, ErrorMessage = "Phone is linited to a maximum 12 characters")]
        public string Phone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Theatre> Theatres { get; set; }
    }
}
