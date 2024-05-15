using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.DTOs
{
    public class OperationGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }

        public int PatientId { get; set; }

        public DateTime Date { get; set; }
    }
}
