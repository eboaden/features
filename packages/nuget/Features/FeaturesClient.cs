using System;
using System.Collections.Generic;
using System.Linq;
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
        public FeaturesClient(string application = null, IKubernetesClientProvider clientProvider = null, INamespaceProvider namespaceProvider = null)
        {
            _namespace = (namespaceProvider ?? new NamespaceProvider()).GetNamespace();
            _client = (clientProvider ?? new KubernetesClientProvider()).BuildClient();
            _application = application ?? "default";
        }

        public bool CheckCustomFeature<T>(Func<T, bool> validator) where T: CustomObjectSpec
        {
            var json = _client.ListNamespacedCustomObject("edward.tech", "v1", _namespace, "features",
                labelSelector: $"app={_application}").ToString();
            var featuresList = JObject.Parse(json)["items"].ToObject<List<CustomObject>>();
            return featuresList.Select(j => j.Spec).Cast<T>().Any(validator);//you cant actually cast up like this - but you get the idea
        }

        public bool CheckFeatureIsActive(string featureName)
        {
            return this.CheckCustomFeature<CustomObjectSpec>(i => i.Name == featureName && i.Active);
        }


        public bool CheckFeatureAttributes(string active)
        {
            return this.CheckCustomFeature<CustomObjectSpec>(i => i.Attributes.Contains(active));
        }
    }

    public class JoeCustomObjectSpec:CustomObjectSpec
    {
        public JoeCustomObjectSpec(IHttpContextAccessor contextAccessor)
        {

        }

        public bool Check()
        {

        }
        public int Coolness { get; set; }
    }
}