using Abp.Domain.Repositories;
using MyTraining1121AngularDemo.Customers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1121AngularDemo.Customers
{
    public class CustomerUserAppService :  MyTraining1121AngularDemoAppServiceBase, ICustomerUserAppService
    {
        private readonly IRepository<CustomerUsers> _customerUserRepository;
        public CustomerUserAppService(IRepository<CustomerUsers> customerUserRepository)
        {
            _customerUserRepository = customerUserRepository;
        }

        public async Task<GetCustomerUserForViewDto> GetCustomerUserForView(int id)
        {
            var customerUser = await _customerUserRepository.GetAsync(id);
            var output = new GetCustomerUserForViewDto { CustomerUser = ObjectMapper.Map<CustomerUserDto>(customerUser) };
            return output;
        }
    }
}
