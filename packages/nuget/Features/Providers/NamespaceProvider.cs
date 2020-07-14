using System.IO;

namespace Features.Providers
{
    public interface INamespaceProvider
    {
        string GetNamespace();
    }
    public class NamespaceProvider : INamespaceProvider
    {
        private readonly string _ns;

        public NamespaceProvider(string ns = "default", string namespacePath = "/var/run/secrets/kubernetes.io/serviceaccount/namespace")
        {
            _ns = File.Exists(namespacePath) ? File.ReadAllText(namespacePath) : ns;
        }
        
        public string GetNamespace() => _ns;
    }
}