using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.DTOs
{
    public class PatientsDto
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Number { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string MedicalHistory { get; set; }
    }
}
