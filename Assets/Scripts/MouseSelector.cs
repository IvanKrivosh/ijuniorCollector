using GameEvent;
using UnityEngine;

public class MouseSelector : MonoBehaviour
{
    [SerializeField] private ReplicationEvent _selectedReplication;
    [SerializeField] private Vector3Event _pickedTerrainPoint;

    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.gameObject.TryGetComponent<Replication>(out Replication replication))
                _selectedReplication.Invoke(replication);
            else if (hitInfo.collider.gameObject.TryGetComponent<Terrain>(out Terrain terrain))
                _pickedTerrainPoint.Invoke(hitInfo.point);
        }
    }
}
