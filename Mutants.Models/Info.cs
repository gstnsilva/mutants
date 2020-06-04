using Mutants.Core;
using Mutants.Core.Caching;
using Mutants.Core.Cryptography;
using Newtonsoft.Json;

namespace Mutants.Models
{
    public class Info : Resource, IEtaggable
    {
        public string Title { get; set; }

        public string Tagline { get; set; }

        public string GetEtag()
        {
            var serialized = JsonConvert.SerializeObject(this);
            return Md5Hash.ForString(serialized);
        }
    }
}
