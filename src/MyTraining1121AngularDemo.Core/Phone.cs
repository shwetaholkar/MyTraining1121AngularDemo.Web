using Abp.Domain.Entities.Auditing;
using MyTraining1121AngularDemo.PhoneBook;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1121AngularDemo
{
    [Table("PbPhones")]
    public class Phone : CreationAuditedEntity<long>
    {
        public const int MaxNumberLength = 16;

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
        public virtual int PersonId { get; set; }

        [Required]
        public virtual PhoneType Type { get; set; }

        [Required]
        [MaxLength(MaxNumberLength)]
        public virtual string Number { get; set; }
    }

}
