using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.MyModels
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }
        public int Status { get; set; }

    }
}
