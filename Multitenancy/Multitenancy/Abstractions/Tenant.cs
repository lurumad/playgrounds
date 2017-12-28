using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multitenancy.Abstractions
{
    public class Tenant<TKey> where TKey : IEquatable<TKey>
    {
        public Tenant()
        {

        }

        public Tenant(string name)
        {
            Name = name;
        }

        public TKey Id { get; set; }
        public string Name { get; }
        public string Host { get; set; }
    }
}
