using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using MyTraining1121AngularDemo.Customers.Exporting;
using MyTraining1121AngularDemo.Customers.Dtos;
using MyTraining1121AngularDemo.Dto;
using Abp.Application.Services.Dto;
using MyTraining1121AngularDemo.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using MyTraining1121AngularDemo.Storage;
using MyTraining1121AngularDemo.Authorization.Users;

namespace MyTraining1121AngularDemo.Customers
{
    [AbpAuthorize(AppPermissions.Pages_Customers)]
    public class CustomersAppService : MyTraining1121AngularDemoAppServiceBase, ICustomersAppService
    {
        private readonly IRepository<User,long> _userRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly ICustomersExcelExporter _customersExcelExporter;

        public CustomersAppService(IRepository<Customer> customerRepository, ICustomersExcelExporter customersExcelExporter, IRepository<User,long> userRepository)
        {
            _customerRepository = customerRepository;
            _customersExcelExporter = customersExcelExporter;
            _userRepository = userRepository;

        }

        public async Task<PagedResultDto<GetCustomerForViewDto>> GetAll(GetAllCustomersInput input)
        {

            var filteredCustomers = _customerRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.CustomerName.Contains(input.Filter) || e.EmailId.Contains(input.Filter) || e.Address.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CustomerNameFilter), e => e.CustomerName == input.CustomerNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailIdFilter), e => e.EmailId == input.EmailIdFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AddressFilter), e => e.Address == input.AddressFilter)
                        .WhereIf(input.MinRegistrationDateFilter != null, e => e.RegistrationDate >= input.MinRegistrationDateFilter)
                        .WhereIf(input.MaxRegistrationDateFilter != null, e => e.RegistrationDate <= input.MaxRegistrationDateFilter);

            var pagedAndFilteredCustomers = filteredCustomers
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var customers = from o in pagedAndFilteredCustomers
                            select new
                            {
                                o.CustomerName,
                                o.EmailId,
                                o.Address,
                                o.RegistrationDate,
                                Id = o.Id
                            };

            var totalCount = await filteredCustomers.CountAsync();

            var dbList = await customers.ToListAsync();
            var results = new List<GetCustomerForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetCustomerForViewDto()
                {
                    Customer = new CustomerDto
                    {
                        CustomerName = o.CustomerName,
                        EmailId = o.EmailId,
                        Address = o.Address,
                        RegistrationDate = o.RegistrationDate,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetCustomerForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetCustomerForViewDto> GetCustomerForView(int id)
        {
            var customer = await _customerRepository.GetAsync(id);

            var output = new GetCustomerForViewDto { Customer = ObjectMapper.Map<CustomerDto>(customer) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Customers_Edit)]
        public async Task<GetCustomerForEditOutput> GetCustomerForEdit(EntityDto input)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetCustomerForEditOutput { Customer = ObjectMapper.Map<CreateOrEditCustomerDto>(customer) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditCustomerDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Customers_Create)]
        protected virtual async Task Create(CreateOrEditCustomerDto input)
        {

            var customer = ObjectMapper.Map<Customer>(input);

            if (AbpSession.TenantId != null)
            {
                customer.TenantId = (int?)AbpSession.TenantId;
            }

            await _customerRepository.InsertAsync(customer);

        }

        //public ListResultDto<PersonListDto> GetPeople(GetPeopleInput input)
        //{
        //    var persons = _personRepository
        //        .GetAll()
        //        .Include(p => p.Phones)
        //        .WhereIf(
        //            !input.Filter.IsNullOrEmpty(),
        //            p => p.Name.Contains(input.Filter) ||
        //                    p.Surname.Contains(input.Filter) ||
        //                    p.EmailAddress.Contains(input.Filter)
        //        )
        //        .OrderBy(p => p.Name)
        //        .ThenBy(p => p.Surname)
        //        .ToList();

        //    return new ListResultDto<PersonListDto>(ObjectMapper.Map<List<PersonListDto>>(persons));
        //}


        [AbpAuthorize(AppPermissions.Pages_Customers_Edit)]
        protected virtual async Task Update(CreateOrEditCustomerDto input)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync((int)input.Id);
            ObjectMapper.Map(input, customer);

        }

        [AbpAuthorize(AppPermissions.Pages_Customers_Delete)]
        public async Task Delete(EntityDto input)
        {
            await _customerRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetCustomersToExcel(GetAllCustomersForExcelInput input)
        {

            var filteredCustomers = _customerRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.CustomerName.Contains(input.Filter) || e.EmailId.Contains(input.Filter) || e.Address.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CustomerNameFilter), e => e.CustomerName == input.CustomerNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailIdFilter), e => e.EmailId == input.EmailIdFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AddressFilter), e => e.Address == input.AddressFilter)
                        .WhereIf(input.MinRegistrationDateFilter != null, e => e.RegistrationDate >= input.MinRegistrationDateFilter)
                        .WhereIf(input.MaxRegistrationDateFilter != null, e => e.RegistrationDate <= input.MaxRegistrationDateFilter);

            var query = (from o in filteredCustomers
                         select new GetCustomerForViewDto()
                         {
                             Customer = new CustomerDto
                             {
                                 CustomerName = o.CustomerName,
                                 EmailId = o.EmailId,
                                 Address = o.Address,
                                 RegistrationDate = o.RegistrationDate,
                                 Id = o.Id
                             }
                         });

            var customerListDtos = await query.ToListAsync();

            return _customersExcelExporter.ExportToFile(customerListDtos);
        }

        public async Task<List<UsersInCustomerDto>> GetAllUserForDropdown()
        {
            var users = await _userRepository.GetAll().Select(user => new UsersInCustomerDto
            {
                Name = user.Name.ToString()
            }).ToListAsync();

            return users;
        }

    }
}