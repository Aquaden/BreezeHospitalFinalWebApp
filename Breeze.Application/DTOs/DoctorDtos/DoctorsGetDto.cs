using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.DTOs
{
    public class DoctorsGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Specialization { get; set; }
        public int ExperienceYears { get; set; } // Years of experience as a doctor
        public string Email { get; set; }
    }
}
