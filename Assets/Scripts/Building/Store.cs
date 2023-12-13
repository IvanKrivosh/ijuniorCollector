using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{    
    private Dictionary<string, int> _resources = new Dictionary<string, int>();

    public delegate void AddedResourceHandler();
    public event AddedResourceHandler OnAddedResource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Resource>(out Resource resource))        
            AddResource(resource);        
    }

    public bool TrySpendResources(List<ResourceQuantity> listTerm)
    {
        for (int i = 0; i < listTerm.Count; i++)
            if (!_resources.ContainsKey(listTerm[i].NameResource) || _resources[listTerm[i].NameResource] < listTerm[i].Quantity)
                return false;

        for (int i = 0; i < listTerm.Count; i++)
            _resources[listTerm[i].NameResource] -= listTerm[i].Quantity;

        return true;
    }

    private void AddResource(Resource resource)
    {
         int oneQuantity = 1;

        if (_resources.ContainsKey(resource.GetType().Name))
            _resources[resource.GetType().Name]++;
        else
            _resources.Add(resource.GetType().Name, oneQuantity);

        Destroy(resource.gameObject);
        OnAddedResource.Invoke();
    }       
}
