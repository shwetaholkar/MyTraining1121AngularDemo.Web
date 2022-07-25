using Abp.Application.Services;
using MyTraining1121AngularDemo.Customers.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1121AngularDemo.Customers
{
    public interface ICustomerUserAppService : IApplicationService
    {
        Task<GetCustomerUserForViewDto> GetCustomerUserForView(int id);
    }
}

