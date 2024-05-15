using Breeze.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.DTOs
{
    public class OperationDto
    {
        public string Name { get; set; }
        public float Price { get; set; }

        public int PatientId { get; set; }

        public DateTime Date { get; set; }
        
    }
}
