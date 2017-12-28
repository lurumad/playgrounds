using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace PipelineBranching
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMiddleware<SomeMiddleware>();
            app.UsePerHost((host, builder) =>
            {
                builder.Use(async (context, next) =>
                {
                    var logger = context.RequestServices.GetService<ILogger<DummyClassLogger>>();
                    logger.LogInformation($"Pipeline branching for host: {host}");
                    await next();
                });
            });
            app.UseTerminal();
        }
    }
}
