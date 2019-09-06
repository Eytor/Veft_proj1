using System.Dynamic;
using Newtonsoft.Json;

namespace WebApplication5.TechnicalRadiation.Models
{
    public class HyperMediaModel
    {
        public HyperMediaModel() { Links = new ExpandoObject(); }
        [JsonProperty(PropertyName = "_links")]
        public ExpandoObject Links { get; set; }
    }
}