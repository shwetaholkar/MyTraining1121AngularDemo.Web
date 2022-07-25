using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MyTraining1121AngularDemo.Customers.Dtos
{
    public class GetCustomerUserForViewDto 
    {
        public CustomerUserDto CustomerUser { get; set; }
       
    }
}
