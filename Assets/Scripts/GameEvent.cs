using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameEvent
{
    [Serializable] public class ResourceEvent : UnityEvent<Resource> { };

    [Serializable] public class TransformEvent : UnityEvent<Transform> { };

    [Serializable] public class StoreEvent : UnityEvent<Store> { };

    [Serializable] public class Vector3Event : UnityEvent<Vector3> { };

    [Serializable] public class CollectorEvent : UnityEvent<Collector> { };

    [Serializable] public class ReplicationEvent : UnityEvent<Replication> { };   
}