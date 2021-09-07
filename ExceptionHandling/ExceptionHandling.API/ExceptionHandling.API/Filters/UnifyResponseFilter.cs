using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionHandling.API.Filters
{
    public class UnifyResponseFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {            
            if(context.Result is OkObjectResult)
            {
                var result = context.Result as OkObjectResult;                
                context.Result = new OkObjectResult(new { Data = result.Value });
            }

            base.OnResultExecuting(context);
        }
    }
}
