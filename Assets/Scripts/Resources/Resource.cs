using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] private GameObject _unidentified;
        
    private GameObject _unidentifiedBox;
    public bool IsIdentified => _unidentifiedBox == null;

    private void Awake()
    {
        _unidentifiedBox = Instantiate(_unidentified, transform.position, Quaternion.identity);
    }

    public bool TrySetIdentifiedState()
    {
        if (!IsIdentified)
        {            
            Destroy(_unidentifiedBox);
            _unidentifiedBox = null;
            return true;
        }

        return false;
    }
}
