using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Collector : MonoBehaviour
{
    [Range(1.0f, 10.0f)]
    [SerializeField] private float _speed = 1.0f;    

    private Rigidbody _rigidbody;
    private Transform _target;
    private Store _store;
    private Resource _stash;

    public bool IsBusy => _target != null;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();        
    }

    private void Update()
    {
        if (IsBusy)
        {
            float _radian = 20;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, (_target.transform.position - transform.position), _radian, 0f);

            transform.rotation = Quaternion.LookRotation(newDirection);
        }        
    }

    private void FixedUpdate()
    {
        if (IsBusy)
            _rigidbody.MovePosition(_rigidbody.position + (transform.forward * _speed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Resource>(out Resource resource) && resource.transform == _target)
        {
            _stash = resource;
            _target = _store.transform;
            resource.transform.SetParent(transform);
            resource.GetComponent<Rigidbody>().isKinematic = true;
            resource.transform.position = new Vector3(transform.position.x, transform.position.y + transform.localScale.y, transform.position.z);
        }
        else if (other.gameObject.TryGetComponent<Store>(out Store store) && store.transform == _target)
        {            
            _target = null;
            store.TryAddResource(this, _stash);                  
        }
    }

    public bool TrySetTarget(Transform target)
    {
        if (!IsBusy)
        {
            _target = target;
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

}
