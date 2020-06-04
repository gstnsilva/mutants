using Newtonsoft.Json;
using Mutants.Core;
using Mutants.Core.Caching;
using Mutants.Core.Cryptography;
using Mutants.Core.Forms;

namespace Mutants.Models
{
    public class RootResponse : Resource, IEtaggable
    {
        public Link Info { get; set; }

        public Link GetStats { get; set; }

        public Form RegisterDna { get; set; }

        public string GetEtag()
        {
            var serialized = JsonConvert.SerializeObject(this);
            return Md5Hash.ForString(serialized);
        }
    }
}
