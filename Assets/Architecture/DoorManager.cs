using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    private Animator _animator;
    private bool _isOpen;
    private bool _playerDetected;


    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!_playerDetected) return;

        if (!Input.GetButtonDown("Use")) return;

        _animator.SetBool("Vertical", !_isOpen);
        _isOpen = !_isOpen;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag != "Player") return;
        _playerDetected = true;
        Debug.Log("player detected");
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag != "Player") return;
        _playerDetected = false;
        Debug.Log("player left");
    }
}
