using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.Application.MyModels
{
    public class ErrorsDto
    {
        public List<String>? Errors { get; private set; } = new List<String>();

        public ErrorsDto(string error)
        {
            Errors.Add(error);
        }

        public ErrorsDto(List<string> errors)
        {
            Errors = errors;
        }

        public ErrorsDto()
        {

        }
    }
}
