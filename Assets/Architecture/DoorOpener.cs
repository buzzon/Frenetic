using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public Transform anchorLeft; // дверная петля
    public Transform anchorRight; // дверная петля
    public float distance = 5; // если игрок уходит на большую дистанцию - скрипт отключается, для оптимизации
    public bool isOpen = false; // на старте сцены дверь открыта?
    public float openAngle = 120;
    public float closeAngle = 0;
    public float smooth = 2;

    private Transform target;
    private BoxCollider collider;

    void Awake()
    {
        collider = gameObject.GetComponent(typeof(BoxCollider)) as BoxCollider;
        openAngle = Mathf.Abs(openAngle);
        closeAngle = Mathf.Abs(closeAngle);
        if (isOpen) anchorLeft.localRotation = Quaternion.Euler(0, openAngle, 0);
        if (isOpen) anchorRight.localRotation = Quaternion.Euler(0, -openAngle, 0);
        enabled = false;
        if (collider != null) collider.isTrigger = isOpen;
    }

    void Update()
    {
        if (isOpen)
        {
            Quaternion rotationLeft = Quaternion.Euler(0, openAngle, 0);
            Quaternion rotationRight = Quaternion.Euler(0, -openAngle, 0);
            anchorLeft.localRotation = Quaternion.Lerp(anchorLeft.localRotation, rotationLeft, smooth * Time.deltaTime);
            anchorRight.localRotation = Quaternion.Lerp(anchorRight.localRotation, rotationRight, smooth * Time.deltaTime);
        }
        else
        {
            Quaternion rotation = Quaternion.Euler(0, closeAngle, 0);
            anchorLeft.localRotation = Quaternion.Lerp(anchorLeft.localRotation, rotation, smooth * Time.deltaTime);
            anchorRight.localRotation = Quaternion.Lerp(anchorRight.localRotation, rotation, smooth * Time.deltaTime);
        }

        if (collider != null) collider.isTrigger = isOpen;

        if (target)
        {
            float dis = Vector3.Distance(transform.position, target.position);
            if (dis > distance) enabled = false;
        }
    }

    public void Invert(Transform player)
    {
        target = player;
        isOpen = !isOpen;
    }
}