using GameEvent;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [Range(1.0f, 10.0f)]
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private TransformEvent _reachedTarget;

    private Rigidbody _rigidbody;
    private Transform _target;

    public bool IsMoving => _target != null;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (IsMoving)
        {
            float radian = 20;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, (_target.transform.position - transform.position), radian, 0f);

            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    private void FixedUpdate()
    {
        if (IsMoving)
            _rigidbody.MovePosition(_rigidbody.position + (transform.forward * _speed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == _target)
        {
            _target = null;
            _reachedTarget.Invoke(other.transform);                   
        }        
    }

    public void OnSetTarget(Transform target)
    {
        if (_target == null)
            _target = target;
    }
}
