using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Features.Models;
using Features.Providers;
using k8s;
using Microsoft.AspNetCore.Http;
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
        private readonly ClaimsPrincipal _claimsPrincipal;
        public FeaturesClient(string application = null, IKubernetesClientProvider clientProvider = null, INamespaceProvider namespaceProvider = null, ClaimsPrincipal claimsPrincipal = null)
        {
            _namespace = (namespaceProvider ?? new NamespaceProvider()).GetNamespace();
            _client = (clientProvider ?? new KubernetesClientProvider()).BuildClient();
            _application = application ?? "default";
            _claimsPrincipal = claimsPrincipal ?? new ClaimsPrincipal();
        }

        private bool CheckFeatureActiveForAttributes(CustomObject feature)
        {
            var claims = _claimsPrincipal.Claims.ToList();
            
            
        }

        public bool CheckFeatureIsActive(string featureName)
        {
            var json = _client.ListNamespacedCustomObject("edward.tech", "v1", _namespace, "features",
                labelSelector: $"app={_application}").ToString();
            var featuresList = JObject.Parse(json)["items"].ToObject<List<CustomObject>>();
            var feature = featuresList.Find(f => f.Spec.Name == featureName);

            if (feature == null) return false;

            if (feature.Spec.Attributes.Length != 0 || _claimsPrincipal.Claims.ToList().Count != 0)
            {
                return CheckFeatureActiveForAttributes(feature);
            }
            return feature.Spec.Active;
        }
    }
}