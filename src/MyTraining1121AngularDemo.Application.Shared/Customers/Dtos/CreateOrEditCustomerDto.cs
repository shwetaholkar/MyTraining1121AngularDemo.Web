using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace MyTraining1121AngularDemo.Customers.Dtos
{
    public class CreateOrEditCustomerDto : EntityDto<int?>
    {

        [Required]
        [StringLength(CustomerConsts.MaxCustomerNameLength, MinimumLength = CustomerConsts.MinCustomerNameLength)]
        public string CustomerName { get; set; }

        [Required]
        public string EmailId { get; set; }

        public string Address { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public Collection<UsersInCustomerDto> Users { get; set; }

    }

    public class UsersInCustomerDto : CreationAuditedEntityDto<long>
    {
        public string Name { get; set; }
    }
}
