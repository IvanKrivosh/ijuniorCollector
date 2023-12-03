using GameEvent;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Scaner : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    [SerializeField] private float _speed = 2;
    [Range(0.0f, 10.0f)]
    [SerializeField] private float _distance = 5;
    [Range(5.0f, 15.0f)]
    [SerializeField] private float _widthDetector = 5;
    [SerializeField] private ResourceEvent _foundResource;

    private float[] _range = new float[2];

    private void Awake()
    {
        int startRangeIndex = 0;
        int endRangeIndex = 1;
        transform.localScale = new Vector3(_widthDetector, transform.localScale.y, transform.localScale.z);

        _range[startRangeIndex] = transform.position.z + _distance;
        _range[endRangeIndex] = transform.position.z - _distance;

        StartCoroutine(MoveDetector());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Resource>(out Resource resource) && resource.TrySetIdentifiedState())           
            _foundResource.Invoke(resource);                    
    }

    private IEnumerator MoveDetector()
    {
        while (enabled)
        {
            if (transform.position.z >= _range[0] || transform.position.z <= _range[1])
                _speed *= -1;

            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            yield return null;
        }
    }
}
