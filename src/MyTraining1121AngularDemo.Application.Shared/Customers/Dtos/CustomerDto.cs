using System;
using Abp.Application.Services.Dto;
using MyTraining1121AngularDemo.Authorization.Users;

namespace MyTraining1121AngularDemo.Customers.Dtos
{
    public class CustomerDto : EntityDto
    {
        public string CustomerName { get; set; }

        public string EmailId { get; set; }

        public string Address { get; set; }

        public DateTime? RegistrationDate { get; set; }

       public string UserName { get; set; }
    }
}
