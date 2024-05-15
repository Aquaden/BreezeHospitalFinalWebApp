using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Domain.Entities
{
    public class DoctorOperations
    {
        [Key]
        public int Id { get; set; } 
        
        public int DoctorId { get; set; }
        public virtual Doctors Doctor { get; set; }

        public int OperationId { get; set; }
        public virtual Operations Operation { get; set; }

        //bu pivot cedvelin patientle elaqesi var one-to-one
    }
}
