using Newtonsoft.Json;
using System.ComponentModel;

namespace Mutants.Core.Forms
{
    public class FormField
    {
        public string Name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Label { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Required { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool Secret { get; set; }

        [JsonProperty(
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(DefaultType)]
        public string Type { get; set; } = DefaultType;
        public const string DefaultType = "string";

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? MinLength { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxLength { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Value { get; set; }
    }

}
