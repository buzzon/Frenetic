using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemMover : MonoBehaviour
{
    public float distance = 2;
    public Vector3 offset;
    public bool isMoved = false;

    private Transform target;

    void Awake()
    {
        enabled = false;
    }

    void Update()
    {

        if (isMoved)
            transform.position = target.position + offset;


        if (!target) return;
        var dis = Vector3.Distance(transform.position, target.position);
        if (dis > distance) enabled = false;
    }

    public void Invert(Transform player)
    {
        target = player;
        offset = transform.position - target.position;
        isMoved = !isMoved;
    }
}
