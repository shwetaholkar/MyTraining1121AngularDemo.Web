using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System.Collections.Generic;
using MyTraining1121AngularDemo.Authorization.Users;

namespace MyTraining1121AngularDemo.Customers
{
    [Table("Customers")]
    public class Customer : FullAuditedEntity, IMayHaveTenant
    {
        public int? TenantId { get; set; }

        [Required]
        [StringLength(CustomerConsts.MaxCustomerNameLength, MinimumLength = CustomerConsts.MinCustomerNameLength)]
        public virtual string CustomerName { get; set; }

        [Required]
        public virtual string EmailId { get; set; }

        public virtual string Address { get; set; }

        public virtual DateTime? RegistrationDate { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}