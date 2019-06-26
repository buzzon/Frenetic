using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryManager : MonoBehaviour
{
    public static BoxCollider2D Boundary { get; private set; }

    void OnTriggerStay2D(Collider2D col)
    {
        Boundary = col.gameObject.GetComponent<BoxCollider2D>();
        Debug.Log("Stay " +  col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
    }
}
