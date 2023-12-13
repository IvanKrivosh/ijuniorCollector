using UnityEngine;

[RequireComponent(typeof(Collector))]
public class Builder : MonoBehaviour
{
    [SerializeField] private Garrison _garrisonTemplate;

    public void BuildNewStore(Vector3 position)
    {
        Garrison garrison = Instantiate(_garrisonTemplate, position, Quaternion.identity);

        garrison.OnRegisteredCollector(GetComponent<Collector>());
    }
}
