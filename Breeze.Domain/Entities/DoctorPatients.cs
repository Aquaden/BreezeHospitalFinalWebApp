using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Domain.Entities
{
    public class DoctorPatients
    {
        [Key]
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public virtual Doctors Doctor { get; set; }

        public  int PatientId { get; set; }
        public virtual Patients Patient { get; set; }
    }
}
