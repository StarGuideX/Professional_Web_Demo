using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreSamples.Services
{
    public class DefaultSampleService : ISampleService
    {
        private List<string> _strings = new List<string> { "one", "two", "three" };
        public IEnumerable<string> GetSampleService() => _strings;
    }
}
