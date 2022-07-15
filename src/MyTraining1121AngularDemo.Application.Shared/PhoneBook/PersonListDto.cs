using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MyTraining1121AngularDemo;

namespace MyTraining1121AngularDemo.PhoneBook
{
    public class PersonListDto : FullAuditedEntityDto
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string EmailAddress { get; set; }
        public Collection<PhoneInPersonListDto> Phones { get; set; }
    }
    public class GetPeopleInput
    {
        public string Filter { get; set; }
    }

    public enum PhoneType : byte
    {
        Mobile,
        Home,
        Business
    }


    public class PhoneInPersonListDto : CreationAuditedEntityDto<long>
    {
        public PhoneType Type { get; set; }

        public string Number { get; set; }
    }

    public class GetPersonForEditOutput
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
    }
    public class GetPersonForEditInput
    {
        public int Id { get; set; }
    }
}



