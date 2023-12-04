using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameEvent
{
    [Serializable] public class ResourceEvent : UnityEvent<Resource> { };

    [Serializable] public class TransformEvent : UnityEvent<Transform> { };
}