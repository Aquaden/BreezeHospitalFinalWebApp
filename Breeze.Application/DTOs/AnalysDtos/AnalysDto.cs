using Breeze.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.DTOs
{
    public class AnalysDto
    {
        
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Result { get; set; }
        public int PatientId { get; set; }
        
    }
}
