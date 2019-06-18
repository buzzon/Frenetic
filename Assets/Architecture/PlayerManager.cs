using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;

    private float horizontal;
    private float vertical;
    float moveLimiter = 0.7f;

    public float runSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        animator.SetBool("WalkUp", vertical > 0.1);
        animator.SetBool("WalkDown", vertical < -0.1);
        animator.SetBool("WalkRight", horizontal > 0.1);
        animator.SetBool("WalkLeft", horizontal < -0.1);

        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
}
