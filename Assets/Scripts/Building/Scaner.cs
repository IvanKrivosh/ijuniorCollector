using GameEvent;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Scaner : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    [SerializeField] private float _speed = 2;    
    [Range(5.0f, 15.0f)]
    [SerializeField] private float _widthDetector = 5;
    [SerializeField] private TransformEvent _foundResource;

    private void Awake()
    {
        transform.localScale = new Vector3(_widthDetector, transform.localScale.y, transform.localScale.z);        
        StartCoroutine(RotateDetector());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Resource>(out Resource resource) && resource.TrySetIdentifiedState())           
            _foundResource.Invoke(resource.transform);                    
    }

    private IEnumerator RotateDetector()
    {
        while (enabled)
        {
            float degrees = 20;

            transform.Rotate(new Vector3(0, degrees, 0) * _speed * Time.deltaTime);
            yield return null;
        }
    }
}
