using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GeniusAcademy.Types
{
    public class BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string RecordCreatedBy { get; set; }
        public string RecordModifiedBy { get; set; }
        public DateTime RecordCreatedOn { get; set; }
        public DateTime RecordModifiedOn { get; set; }


    }
}
