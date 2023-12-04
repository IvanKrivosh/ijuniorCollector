using System.Collections;
using UnityEngine;

public class LightFlesh : MonoBehaviour
{
    [SerializeField] private Light _light;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float _flashingTime = 0.5f;

    private WaitForSeconds _waitTime;

    private void Awake()
    {
        _waitTime = new WaitForSeconds(_flashingTime);
        StartCoroutine(BlinkLight());
    }

    private IEnumerator BlinkLight()
    {
        while (enabled) {
            _light.enabled = !_light.enabled;
            yield return _waitTime;
        }
    }

}
