using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovementSystem
{
    [Serializable]
    public class PlayerFallData
    {
        [field: SerializeField][field: Range(1f, 15f)] public float FallSpeedLimit { get; private set; }
        [field: SerializeField][field: Range(1f, 100f)] public float MinimumDistanceToBeConsideredHardFall { get; private set; } = 5f;

        
    }
}
