using System.IO;
using k8s;

namespace Features.Providers
{
    public interface IKubernetesClientProvider
    {
        IKubernetes BuildClient();
    }
    public class KubernetesClientProvider : IKubernetesClientProvider
    {
        private readonly IKubernetesClientProvider _clientProvider;
        public IKubernetesClientProvider InnerProvider
        {
            get => _clientProvider;
        }
        
        public KubernetesClientProvider(string clusterConfigPath = "/var/run/secrets/kubernetes.io/serviceaccount/token")
        {
            _clientProvider = File.Exists(clusterConfigPath)
                ? new KubernetesClientProviderInCluster()
                : (IKubernetesClientProvider) new KubernetesClientProviderFromConfigFile();
        }
        public IKubernetes BuildClient()
        {
            return _clientProvider.BuildClient();
        }
    }

    public class KubernetesClientProviderInCluster : IKubernetesClientProvider
    {
        public IKubernetes BuildClient() => new Kubernetes(KubernetesClientConfiguration.InClusterConfig());
    }

    public class KubernetesClientProviderFromConfigFile : IKubernetesClientProvider
    {
        public IKubernetes BuildClient() => new Kubernetes(KubernetesClientConfiguration.BuildConfigFromConfigFile());
    }
}