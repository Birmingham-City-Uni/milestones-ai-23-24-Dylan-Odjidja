using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Rendering;

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

    [Header("BoxExtent Settings")]
    public Vector2 boxExtents = new Vector2(1.0f, 1.0f);

    [Header("Sphere Settings")]
    public float sphereRadius = 1.0f;

    [Header("RayBundle Settings")]
    [Range(1, 20)]
    public int rayRes = 5;
    [Range(0, 360)]
    public int searchArc = 90;

    Transform cachedTransform;

    void Start()
    {
        cachedTransform = GetComponent<Transform>();
    }

    public bool Hit { get; private set; }
    public RaycastHit info = new RaycastHit();
    public RaycastHit[] hits;

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
            case Type.BoxCast:
                if (Physics.CheckBox(this.transform.position, new Vector3(boxExtents.x, boxExtents.y, raycastLength) / 2.0f, this.transform.rotation, hitMask, QueryTriggerInteraction.Ignore))
                {
                    Hit = true;
                    Debug.Log("Hit");
                    return true;
                }

                break;
            case Type.SphereCast:
                if (Physics.SphereCast(new Ray(cachedTransform.position, dir), sphereRadius, out info, raycastLength, hitMask, QueryTriggerInteraction.Ignore))
                {
                    Hit = true;
                    Debug.Log("Hit");
                    return true;
                }

                break;
            case Type.RayBundle:
                hits = new RaycastHit[rayRes + 1];
                int hit_count = 0;
                float startSweep = -searchArc * 0.5f;
                float finishSweep = searchArc * 0.5f;
                float sweepGap = searchArc / rayRes;
                for (int i = 0; i < rayRes + 1; i++)
                {
                    dir = (Quaternion.Euler(0, startSweep + i * sweepGap, 0) * this.transform.forward).normalized * raycastLength;
                    // Debug.DrawRay(this.transform.position + dir * Mathf.Epsilon, dir, Color.blue, 2.0f);
                    if (Physics.Linecast(cachedTransform.position + dir * Mathf.Epsilon, cachedTransform.position + dir, out hits[i], hitMask, QueryTriggerInteraction.Ignore))
                    {
                        hit_count++;
                    }
                }
                if (hit_count > 0)
                {
                    System.Array.Sort(hits, (s1, s2) =>
                    {
                        if (s1.distance > s2.distance)
                            return 1;
                        if (s2.distance > s1.distance)
                            return -1;
                        return 0;
                    });
                    Hit = true;
                    info = hits[hits.Length - 1];
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
            case Type.BoxCast:
                Gizmos.DrawWireCube(Vector3.zero, new Vector3(boxExtents.x, boxExtents.y, length));

                break;
            case Type.SphereCast:
                Gizmos.DrawWireSphere(Vector3.zero, sphereRadius);
                if (Hit)
                {
                    Vector3 ballCenter = info.point + info.normal * sphereRadius;
                    length = Vector3.Distance(cachedTransform.position, ballCenter);
                }
                float halfExtents = sphereRadius;
                Gizmos.DrawLine(Vector3.up * halfExtents, Vector3.up * halfExtents + Vector3.forward * length);
                Gizmos.DrawLine(-Vector3.up * halfExtents, -Vector3.up * halfExtents + Vector3.forward * length);
                Gizmos.DrawLine(Vector3.right * halfExtents, Vector3.right * halfExtents + Vector3.forward * length);
                Gizmos.DrawLine(-Vector3.right * halfExtents, -Vector3.right * halfExtents + Vector3.forward * length);
                Gizmos.DrawWireSphere(Vector3.zero + Vector3.forward * length, sphereRadius);

                break;
            case Type.RayBundle:
                if (Hit)
                    length = Vector3.Distance(this.transform.position, info.point);
                float startSweep = -searchArc * 0.5f;
                float finishSweep = searchArc * 0.5f;
                float sweepGap = searchArc / rayRes;
                for (int i = 0; i < rayRes + 1; i++)
                {
                    Vector3 dir = (Quaternion.Euler(0, startSweep + i * sweepGap, 0) * Vector3.forward).normalized;
                    Gizmos.DrawLine(Vector3.zero, dir * length);
                }
                Gizmos.color = Color.black;
                Gizmos.DrawWireCube(Vector3.zero, new Vector3(0.02f, 0.02f, 0.02f));
                Gizmos.color = Color.green;
                if (Hit)
                {
                    Gizmos.matrix = Matrix4x4.identity;
                    Gizmos.DrawWireCube(info.point, new Vector3(0.02f, 0.02f, 0.02f));
                }
                break;
        }
    }
}
