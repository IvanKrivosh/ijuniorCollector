using System.Collections.Generic;
using UnityEngine;

public class Colonizer : MonoBehaviour
{
    private Replication _focusedReplication;    

    public void OnSelectedReplication(Replication replication)
    {
        _focusedReplication = replication;
    }

    public void OnPickedTerrainPoint(Vector3 position)
    {
        if (_focusedReplication != null)
        {
            _focusedReplication.TrySetReplication(position);
            _focusedReplication = null;
        }     
    }
}
