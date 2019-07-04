using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    public static BoxCollider Boundary { get; private set; }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer != LayerMask.NameToLayer("RoomBoundary")) return;
        Debug.Log("Set Room " + col.name);

        Boundary = col.gameObject.GetComponent<BoxCollider>();
    }
}
