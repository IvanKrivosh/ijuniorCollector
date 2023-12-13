using UnityEngine;

[System.Serializable]
public class ResourceQuantity
{
    [SerializeField] private Resource _resource;
    [SerializeField] private int _quantity;

    public string NameResource => _resource.GetType().Name;
    public int Quantity => _quantity;
}