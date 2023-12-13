using UnityEngine;

internal class Trunk : MonoBehaviour
{
    public void OnReachedResource(Resource resource)
    {        
        resource.transform.SetParent(transform);
        resource.GetComponent<Rigidbody>().isKinematic = true;
        resource.transform.position = new Vector3(transform.position.x, transform.position.y + transform.localScale.y, transform.position.z);
        resource.transform.rotation = transform.rotation;
    }
}