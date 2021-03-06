using System;
using System.Net.Http;
using ITJakub.Shared.Contracts;

namespace ITJakub.SearchService.Core.Exist.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ExistResource : ExistAttribute
    {
        public ResourceLevelEnumContract Type { get; set; }
    }
}