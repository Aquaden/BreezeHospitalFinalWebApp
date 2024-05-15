using Breeze.Application.MyModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Breeze.Application.Attributes
{
    public  class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
           if(!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(x => x.Errors.Count > 0)
                .SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                ErrorsDto errorsDto = new ErrorsDto(errors.ToList());
                ResponseModel<ErrorsDto> responseModel = new ResponseModel<ErrorsDto>();
                responseModel.Data = errorsDto;
                responseModel.Status = 400;
                context.Result = new BadRequestObjectResult(responseModel);
            }
        }

    }
}