﻿using Abp.Application.Services.Dto;
using System;

namespace MyTraining1121AngularDemo.Customers.Dtos
{
    public class GetAllCustomersInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string CustomerNameFilter { get; set; }

        public string EmailIdFilter { get; set; }

        public string AddressFilter { get; set; }

        public DateTime? MaxRegistrationDateFilter { get; set; }
        public DateTime? MinRegistrationDateFilter { get; set; }

    }
}