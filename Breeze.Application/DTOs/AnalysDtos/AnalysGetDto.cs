using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.DTOs
{
    public class AnalysGetDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Result { get; set; }
        public int PatientId { get; set; }
    }
}
