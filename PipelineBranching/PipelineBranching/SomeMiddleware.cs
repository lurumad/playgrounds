using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PipelineBranching
{
    public class SomeMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<DummyClassLogger> logger;

        public SomeMiddleware(
            RequestDelegate next,
            ILogger<DummyClassLogger> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            logger.LogInformation("executing some middleware");

            await next(context);

            logger.LogInformation("executed some middleware");
        }
    }
}
