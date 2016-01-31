using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomSerialization.Example.DB
{
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.Orders = new HashSet<Order>();
            this.CustomerDemographics = new HashSet<CustomerDemographic>();
        }

        public Customer(Customer other) : this()
        {
            this.CustomerID = other.CustomerID;
            this.CompanyName = other.CompanyName;
            this.CompanyName = other.ContactName;
            this.ContactTitle = other.ContactTitle;
            this.Address = other.Address;
            this.City = other.City;
            this.Region = other.Region;
            this.PostalCode = other.PostalCode;
            this.Country = other.Country;
            this.Phone = other.Phone;
            this.Fax = other.Fax;
        }

        [StringLength(5)]
        public string CustomerID { get; set; }

        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }

        [StringLength(30)]
        public string ContactName { get; set; }

        [StringLength(30)]
        public string ContactTitle { get; set; }

        [StringLength(60)]
        public string Address { get; set; }

        [StringLength(15)]
        public string City { get; set; }

        [StringLength(15)]
        public string Region { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(15)]
        public string Country { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerDemographic> CustomerDemographics { get; set; }
    }
}
