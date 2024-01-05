using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    public float minDistance;
    public float minBossDistance;
    public float maxSightDistance;
}
