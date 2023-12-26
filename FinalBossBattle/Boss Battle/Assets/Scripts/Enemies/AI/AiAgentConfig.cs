using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{
    public float minDistance = 3.5f;
    public float maxSightDistance = 7.0f;
}
