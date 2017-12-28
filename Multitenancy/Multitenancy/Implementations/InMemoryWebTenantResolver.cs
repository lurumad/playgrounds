using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Multitenancy.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multitenancy.Implementations
{
    public class InMemoryWebTenantResolver : ITenantResolver<MyTenant>
    {
        private readonly IMemoryCache cache;
        private readonly IHttpContextAccessor httpContextAccessor;

        public InMemoryWebTenantResolver(
            IMemoryCache cache,
            IHttpContextAccessor httpContextAccessor)
        {
            this.cache = cache;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task<MyTenant> Resolve()
        {
            var key = httpContextAccessor.HttpContext.Request.Host.Value.ToString();

            if (key == null)
            {
                return null;
            }
            
        }
    }
}
