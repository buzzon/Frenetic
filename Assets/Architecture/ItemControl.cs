using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{

    public float distance = 1.5f; // в приделах этой дистанции дверь будет доступна
    public string doorTag = "Door"; // тег двери
    public string MoveTag = "MovedItem";
    public KeyCode key = KeyCode.F; // клавиша управления


    void Update()
    {
        if (!Input.GetKeyDown(key)) return;

        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit,
            distance)) return;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green, 5.0f);

        if (hit.collider.tag == doorTag)
        {
            hit.transform.GetComponent<DoorOpener>().enabled = true;
            hit.transform.GetComponent<DoorOpener>().Invert(transform);
        }

        if (hit.collider.tag == MoveTag)
        {
            hit.transform.GetComponent<ItemMover>().enabled = true;
            hit.transform.GetComponent<ItemMover>().Invert(transform);
        }
    }
}
