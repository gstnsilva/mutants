using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Mutants.Core.Filters;

namespace Mutants.Core.Caching
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class EtagAttribute : Attribute, IFilterFactory
    {
        public bool IsReusable => true;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return new EtagHeaderFilter();
        }
    }
}
