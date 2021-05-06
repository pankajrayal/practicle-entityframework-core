using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EF_Activity001.Models
{
    [Table("Employee", Schema = "HumanResources")]
    [Index(nameof(LoginId), Name = "AK_Employee_LoginID", IsUnique = true)]
    [Index(nameof(NationalIdnumber), Name = "AK_Employee_NationalIDNumber", IsUnique = true)]
    [Index(nameof(Rowguid), Name = "AK_Employee_rowguid", IsUnique = true)]
    public partial class Employee
    {
        public Employee()
        {
            EmployeeDepartmentHistories = new HashSet<EmployeeDepartmentHistory>();
            EmployeePayHistories = new HashSet<EmployeePayHistory>();
            JobCandidates = new HashSet<JobCandidate>();
            PurchaseOrderHeaders = new HashSet<PurchaseOrderHeader>();
        }

        [Key]
        [Column("BusinessEntityID")]
        public int BusinessEntityId { get; set; }
        [Required]
        [Column("NationalIDNumber")]
        public byte[] NationalIdnumber { get; set; }
        [Required]
        [Column("LoginID")]
        [StringLength(256)]
        public string LoginId { get; set; }
        public short? OrganizationLevel { get; set; }
        [Required]
        public byte[] JobTitle { get; set; }
        public byte[] BirthDate { get; set; }
        [Required]
        public byte[] MaritalStatus { get; set; }
        [Required]
        public byte[] Gender { get; set; }
        public byte[] HireDate { get; set; }
        [Required]
        public bool? SalariedFlag { get; set; }
        public short VacationHours { get; set; }
        public short SickLeaveHours { get; set; }
        [Required]
        public bool? CurrentFlag { get; set; }
        [Column("rowguid")]
        public Guid Rowguid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        [ForeignKey(nameof(BusinessEntityId))]
        [InverseProperty(nameof(Person.Employee))]
        public virtual Person BusinessEntity { get; set; }
        [InverseProperty("BusinessEntity")]
        public virtual SalesPerson SalesPerson { get; set; }
        [InverseProperty(nameof(EmployeeDepartmentHistory.BusinessEntity))]
        public virtual ICollection<EmployeeDepartmentHistory> EmployeeDepartmentHistories { get; set; }
        [InverseProperty(nameof(EmployeePayHistory.BusinessEntity))]
        public virtual ICollection<EmployeePayHistory> EmployeePayHistories { get; set; }
        [InverseProperty(nameof(JobCandidate.BusinessEntity))]
        public virtual ICollection<JobCandidate> JobCandidates { get; set; }
        [InverseProperty(nameof(PurchaseOrderHeader.Employee))]
        public virtual ICollection<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }

        [StringLength(15)]
        public string NationalIDNumberBackup { get; set; }
        [StringLength(50)]
        public string JobTitleBackup { get; set; }

        [Column(TypeName = "date")]
        public DateTime BirthDateBackup { get; set; }
        [StringLength(1)]
        public string MaritalStatusBackup { get; set; }
        [Required]
        [StringLength(1)]
        public string GenderBackup { get; set; }
        [Column(TypeName = "date")]
        public DateTime HireDateBackup { get; set; }

    }
}
