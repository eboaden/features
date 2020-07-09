using Newtonsoft.Json;

namespace Features.Models
{
    public class CustomObject
    {
        [JsonProperty("apiVersion")] public string ApiVersion { get; set; }
        [JsonProperty("kind")] public string Kind { get; set; }
        [JsonProperty("metadata")] public CustomObjectMetadata Metadata { get; set; }
        [JsonProperty("spec")] public CustomObjectSpec Spec { get; set; }
    }

    public class CustomObjectMetadata
    {
        [JsonProperty("CreationTimestamp")] public string CreationTimestamp { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("namespace")] public string Namespace { get; set; }
    }

    public class CustomObjectSpec
    {
        [JsonProperty("active")] public bool Active { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
    }

    public class CustomObjectLabels
    {
        [JsonProperty("app")] public string App { get; set; }
    }
}