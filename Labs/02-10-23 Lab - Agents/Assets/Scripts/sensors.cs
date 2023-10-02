using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class sensors : MonoBehaviour
{
    public LayerMask hitMask;
    public enum Type
    {
        Line,
        RayBundle,
        SphereCast,
        BoxCast
    }
    public Type sensorType = Type.Line;
    public float raycastLength = 1.0f;
    Transform cachedTransform;
    // Start is called before the first frame update
    void Start()
    {
        cachedTransform = GetComponent<Transform>();
    }

    public bool Hit { get; private set; }
    public RaycastHit info = new RaycastHit();

    public bool Scan()
    {
        Hit = false;
        Vector3 dir = cachedTransform.forward;
        switch (sensorType)
        {
            case Type.Line:
                if (Physics.Linecast(cachedTransform.position, cachedTransform.position + dir * raycastLength, out info, hitMask, QueryTriggerInteraction.Ignore))
                {
                    Hit = true;
                    Debug.Log("Hit");
                    return true;
                }
                break;
        }
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if (cachedTransform == null)
        {
            cachedTransform = GetComponent<Transform>();
        }
        Scan();
        if (Hit)
        {
            Gizmos.color = Color.red;
        }
        Gizmos.matrix *= Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        float length = raycastLength;
        switch (sensorType)
        {
            case Type.Line:
                if (Hit)
                {
                    length = Vector3.Distance(this.transform.position, info.point);
                }
                Gizmos.DrawLine(Vector3.zero, Vector3.forward * length);
                Gizmos.color = Color.green;
                Gizmos.DrawCube(Vector3.forward * length, new Vector3(0.02f, 0.02f, 0.02f));
                break;
        }
    }
}
