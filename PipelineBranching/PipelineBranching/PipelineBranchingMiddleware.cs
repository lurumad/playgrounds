using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PipelineBranching
{
    public class PipelineBranchingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IApplicationBuilder rootApp;
        private readonly Action<string, IApplicationBuilder> configuration;
        private readonly ILogger<DummyClassLogger> logger;
        private readonly ConcurrentDictionary<string, Lazy<RequestDelegate>> pipelines =
            new ConcurrentDictionary<string, Lazy<RequestDelegate>>();

        public PipelineBranchingMiddleware(
            RequestDelegate next,
            IApplicationBuilder rootApp,
            Action<string ,IApplicationBuilder> configuration,
            ILogger<DummyClassLogger> logger)
        {
            this.next = next;
            this.rootApp = rootApp;
            this.configuration = configuration;
            this.logger = logger;
        }

        public Task Invoke(HttpContext context)
        {
            var next = pipelines.GetOrAdd(
                context.Request.Host.Value,
                new Lazy<RequestDelegate>(() => BuildPipeline(context)));

            return next.Value(context);
        }

        public RequestDelegate BuildPipeline(HttpContext context)
        {
            string host = context.Request.Host.Value;
            logger.LogInformation($"creating the pipeline for {host}");

            var branch = rootApp.New();

            configuration(host, branch);

            branch.Run(next);

            return branch.Build();
        }
    }
}
