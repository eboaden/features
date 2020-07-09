using System.Collections.Generic;
using System.IO;
using Features.Models;
using k8s;
using Newtonsoft.Json.Linq;

namespace Features
{
    public class FeaturesClient
    {
        private readonly string _namespace;
        private readonly string _application;
        private readonly Kubernetes _client;
        public FeaturesClient(string fallbackNamespace, string application)
        {
            _namespace = File.Exists("/var/run/secrets/kubernetes.io/serviceaccount/namespace") ? File.ReadAllText("/var/run/secrets/kubernetes.io/serviceaccount/namespace") : fallbackNamespace ?? "default";
            _client = File.Exists("/var/run/secrets/kubernetes.io/serviceaccount/token")
                ? new Kubernetes(KubernetesClientConfiguration.InClusterConfig())
                : new Kubernetes(KubernetesClientConfiguration.BuildConfigFromConfigFile());
            _application = application ?? "default";
        }

        public bool GetFeature(string featureName)
        {
            var featuresList = JObject.Parse(_client.ListNamespacedCustomObject("edward.tech", "v1", _namespace, "features",
                labelSelector: $"app={_application}").ToString())["items"].ToObject<List<CustomObject>>();
            var feature = featuresList.Find(f => f.Spec.Name == featureName);
            return feature != null && feature.Spec.Active;
        }
    }
}