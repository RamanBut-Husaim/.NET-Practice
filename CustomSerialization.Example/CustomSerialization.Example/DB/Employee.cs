using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomSerialization.Example.DB
{
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.Employees1 = new HashSet<Employee>();
            this.Orders = new HashSet<Order>();
            this.Territories = new HashSet<Territory>();
        }

        public Employee(Employee other)
        {
            this.EmployeeID = other.EmployeeID;
            this.LastName = other.LastName;
            this.FirstName = other.FirstName;
            this.Title = other.Title;
            this.TitleOfCourtesy = other.TitleOfCourtesy;
            this.BirthDate = other.BirthDate;
            this.HireDate = other.HireDate;
            this.Address = other.Address;
            this.City = other.City;
            this.Region = other.Region;
            this.PostalCode = other.PostalCode;
            this.Country = other.Country;
            this.HomePhone = other.HomePhone;
            this.Extension = other.Extension;
            this.Photo = other.Photo;
            this.Notes = other.Notes;
            this.ReportsTo = other.ReportsTo;
            this.PhotoPath = other.PhotoPath;
        }

        public int EmployeeID { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [StringLength(10)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string Title { get; set; }

        [StringLength(25)]
        public string TitleOfCourtesy { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }

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
        public string HomePhone { get; set; }

        [StringLength(4)]
        public string Extension { get; set; }

        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        public int? ReportsTo { get; set; }

        [StringLength(255)]
        public string PhotoPath { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees1 { get; set; }

        public virtual Employee Employee1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Territory> Territories { get; set; }
    }
}
