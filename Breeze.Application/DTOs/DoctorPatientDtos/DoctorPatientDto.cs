using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.DTOs.DoctorPatientDtos
{
    public class DoctorPatientDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }


        public int PatientId { get; set; }

    }
}
