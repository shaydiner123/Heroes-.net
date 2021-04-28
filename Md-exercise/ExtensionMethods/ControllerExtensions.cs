using Md_exercise.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Mvc
{
    public static class ControllerExtensions
    {

        public static ActionResult GenareteBadRequestResponse(this ControllerBase controller, string errorMessage)
        {
            return controller.BadRequest(new Response
            {
                IsSuccess = false,
                Error = new ResponseError()
                {
                    Messages = new List<string> { errorMessage }
                }
            });
        }


        public static ActionResult Genarete500Response(this ControllerBase controller, Exception e)
        {
            return controller.StatusCode(StatusCodes.Status500InternalServerError, new Response()
            {
                IsSuccess = false,
                Error = new ResponseError()
                {
                    Messages = new List<string>() { e.Message },
                    Reason = e.StackTrace
                }
            });
        }
    }
}
