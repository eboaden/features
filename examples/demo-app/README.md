# Demo App for Kubernetes Feature Flagging Solution

Required Software:
-   [Kubernetes CLI (kubectl)]("https://kubernetes.io/docs/tasks/tools/install-kubectl/")
-   [Kustomize CLI]("https://kubernetes-sigs.github.io/kustomize/installation/")

Prerequisites:
-   A Kubernetes Cluster

1.  Check you can connect the the K8s cluster with the following command
``` bash
kubectl get ns
```

2. Clone this repo
``` bash
git clone https://github.com/eboaden/features.git
```

3. Open the repo you have just cloned
``` bash
cd features
```

4. Navigate to the demo app folder
``` bash
cd examples/demo-app
```

5. Navigate to the k8s folder
``` bash
cd k8s
```

6. Run the following command to build and apply the demo app config to your cluster (this will create the custom resource definition, a new demo namespace and a deployment of the RandomDataGenerator demo application along with assigning the default service account for the new namespace all the permissions it requires to access the feature flags)
``` bash
kustomize build | k apply -f -
```