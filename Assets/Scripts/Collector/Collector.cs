using GameEvent;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private TransformEvent _setTarget;
    [SerializeField] private ResourceEvent _reachedResource;

    private Store _store;    
    private Transform _target;    

    public bool IsBusy => _target != null;
    
    public bool TrySetTarget(Transform target)
    {
        if (!IsBusy)
        {
            _target = target;
            _setTarget.Invoke(_target);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TrySetOwner(Store store)
    {
        if (!IsBusy)
        {
            _store = store;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnReachedTarget(Transform target)
    {
        if (target.gameObject.TryGetComponent<Resource>(out Resource resource))
        {            
            _reachedResource.Invoke(resource);
            _setTarget.Invoke(_store.transform);
        }
        else if (target.gameObject.TryGetComponent<Store>(out Store store) && _target.gameObject.TryGetComponent<Resource>(out Resource stashedResource))
        {
            _target = null;
            store.TryAddResource(this, stashedResource);
        }   
    }

}
