using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager3D : MonoBehaviour
{
    private Rigidbody body;
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    private float horizontal;
    private float vertical;
    float moveLimiter = 0.7f;

    public float runSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = 0;
        vertical = 0;
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Animate();
        Move();
    }

    private bool IsMove()
    {
        return vertical != 0 || horizontal != 0;
    }

    private void Move()
    {
        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector3(horizontal * runSpeed, 0, vertical * runSpeed);

        if (!audioSource.isPlaying && IsMove())
            UseStepSound();
    }

    public void UseStepSound()
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.volume = Random.Range(0.8f, 1f);
        audioSource.pitch = Random.Range(0.8f, 1.1f);
        audioSource.Play();
    }

    private void Animate()
    {
        animator.SetBool("WalkUp", vertical > 0.1);
        animator.SetBool("WalkDown", vertical < -0.1);
        animator.SetBool("WalkRight", horizontal > 0.1);
        animator.SetBool("WalkLeft", horizontal < -0.1);
    }
}