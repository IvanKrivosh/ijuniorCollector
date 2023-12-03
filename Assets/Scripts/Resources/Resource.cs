using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private GameObject _unidentified;

    private bool _identified = false;
    private GameObject _unidentifiedBox;
    public bool IsIdentified => _identified;

    private void Awake()
    {
        _unidentifiedBox = Instantiate(_unidentified, transform.position, Quaternion.identity);
    }

    public bool TrySetIdentifiedState()
    {
        if (!_identified)
        {
            _identified = !_identified;
            Destroy(_unidentifiedBox);
            return _identified;
        }

        return false;
    }
}
