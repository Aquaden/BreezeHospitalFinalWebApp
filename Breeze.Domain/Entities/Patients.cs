using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Domain.Entities
{
    public class Patients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Number { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string MedicalHistory { get; set; }
        public virtual ICollection<Operations>? Operation { get; set; }
        public virtual List<Analyses> PatientAnalyses { get; set; }
        public virtual ICollection<DoctorPatients> DoctorsPatients { get; set; }
    }
}
