using System.Collections.Generic;
using Features.Models;
using Features.Providers;
using k8s;
using Newtonsoft.Json.Linq;

namespace Features
{
    public interface IFeaturesClient
    {
        bool CheckFeatureIsActive(string featureName);
    }
    public class FeaturesClient : IFeaturesClient
    {
        private readonly string _namespace;
        private readonly string _application;
        private readonly IKubernetes _client;
        public FeaturesClient(string application = null, IKubernetesClientProvider clientProvider = null, INamespaceProvider namespaceProvider = null)
        {
            _namespace = (namespaceProvider ?? new NamespaceProvider()).GetNamespace();
            _client = (clientProvider ?? new KubernetesClientProvider()).BuildClient();
            _application = application ?? "default";
        }

        public bool CheckFeatureIsActive(string featureName)
        {
            var json = _client.ListNamespacedCustomObject("edward.tech", "v1", _namespace, "features",
                labelSelector: $"app={_application}").ToString();
            var featuresList = JObject.Parse(json)["items"].ToObject<List<CustomObject>>();
            var feature = featuresList.Find(f => f.Spec.Name == featureName);
            return feature != null && feature.Spec.Active;
        }
    }
}