using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private Animator _animator;
    private BoxCollider2D _collider;
    private bool _isOpen;
    private bool _playerDetected;


    void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (!_playerDetected) return;

        if (!Input.GetButtonDown("Use")) return;

        _animator.SetBool("Vertical", !_isOpen);
        _isOpen = !_isOpen;
        _collider.isTrigger = !_collider.isTrigger;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag != "Player") return;
        _playerDetected = true;
        Debug.Log("player detected");
    }
    //checks to see if player has left the collider, and disables door text if so
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag != "Player") return;
        _playerDetected = false;
        Debug.Log("player left");
    }
}
