using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PipelineBranching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSome(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SomeMiddleware>();
        }

        public static IApplicationBuilder UsePerHost(this IApplicationBuilder app, Action<string, IApplicationBuilder> configuration)
        {
            return app.Use(next =>
                new PipelineBranchingMiddleware(
                    next,
                    app,
                    configuration,
                    new LoggerFactory().CreateLogger<DummyClassLogger>()).Invoke);
        }

        public static IApplicationBuilder UseTerminal(this IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var loggerFactory = (ILoggerFactory)context.RequestServices.GetService(typeof(ILoggerFactory));
                var logger = loggerFactory.CreateLogger<DummyClassLogger>();
                logger.LogInformation("executing terminal middleware (it could be MVC)");
                await context.Response.WriteAsync("Pipeline branching");
                logger.LogInformation("executed terminal middleware (it could be MVC)");

            });

            return app;
        }

    }
}
