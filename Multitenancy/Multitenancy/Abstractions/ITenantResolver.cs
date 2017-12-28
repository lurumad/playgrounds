using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multitenancy.Abstractions
{
    public interface ITenantResolver<TTenant>
    {
        Task<TTenant> Resolve();
    }
}
