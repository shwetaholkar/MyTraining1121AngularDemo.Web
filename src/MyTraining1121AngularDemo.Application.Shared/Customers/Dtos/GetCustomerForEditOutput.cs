﻿using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyTraining1121AngularDemo.Customers.Dtos
{
    public class GetCustomerForEditOutput
    {
        public CreateOrEditCustomerDto Customer { get; set; }

    }
}