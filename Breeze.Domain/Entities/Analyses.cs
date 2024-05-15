using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Domain.Entities
{
    public class Analyses
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Result { get; set; }
        public int PatientId { get; set; }
        public virtual Patients Patient { get; set; }
    }
}
