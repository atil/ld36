using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour
{
    public Transform[] Slots;
    public bool IsGhost
    {
        set
        {
            var c = _material.color;
            c.a = value ? 0.3f : 1f;
            _material.color = c;
        }
    }

    private Material _material;

    void Start()
    {
        _material = GetComponent<Renderer>().material;
    }

    public Transform GetNeighborSlot(Vector3 hitNormal)
    {
        var localNormal = transform.InverseTransformPoint(hitNormal);

        foreach (Transform slot in Slots)
        {
            if (Vector3.Dot((slot.position - transform.position).normalized, hitNormal) > 0.9)
            {
                return slot; 
            }

        }
        return transform;
    }
}
