using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    public static BoxCollider Boundary { get; private set; }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.layer != LayerMask.NameToLayer("RoomBoundary")) return;

        Boundary = col.gameObject.GetComponent<BoxCollider>();
    }
}
