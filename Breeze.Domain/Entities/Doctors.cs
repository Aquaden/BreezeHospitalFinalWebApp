using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Domain.Entities
{
    public class Doctors
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Specialization { get; set; }
        public int ExperienceYears { get; set; } // Years of experience as a doctor
        public string Email { get; set; }
        public virtual ICollection<DoctorPatients> DoctorsPatients { get; set; }
        public virtual ICollection<DoctorOperations> DoctorsOperations { get; set; }

    }
}
