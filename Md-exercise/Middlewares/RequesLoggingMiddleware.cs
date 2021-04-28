using log4net;
using Md_exercise.Log4net;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Md_exercise.Middlewares
{
    public class RequesLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ILog log = LogHelper.GetLogger();
        private const int REQUEST_SIZE = 4000;
        public RequesLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                log.Info(await getRequestDescription(context.Request));
            }
            catch (Exception exe)
            {
                log.Error("There was an error with the getRequestDescription method", exe);
            }
            await _next(context);

        }

        private async Task<string> getRequestDescription(HttpRequest httpRequest)
        {
            httpRequest.EnableBuffering();
            var stringBuilder = new StringBuilder(REQUEST_SIZE);
            stringBuilder.AppendLine($"Host: {httpRequest.Host} ");
            stringBuilder.AppendLine($"{httpRequest.Method} {httpRequest.Path}{httpRequest.QueryString.ToUriComponent()} {httpRequest.Protocol}");

            if (httpRequest.Headers.Any())
            {
                foreach (var header in httpRequest.Headers)
                {
                    stringBuilder.AppendLine($"{header.Key}: {header.Value}");
                }
            }

            stringBuilder.AppendLine();
            string bodyStr;
            using (StreamReader reader
                = new StreamReader(httpRequest.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyStr = await reader.ReadToEndAsync();
                httpRequest.Body.Seek(0, SeekOrigin.Begin);
            }
            if (bodyStr != null)
                stringBuilder.AppendLine($"Body:{bodyStr}");
            return stringBuilder.ToString();
            
        }
    }
}
