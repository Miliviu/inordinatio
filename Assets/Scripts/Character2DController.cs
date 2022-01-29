using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Character2DController : MonoBehaviour
{
    private AudioSource source;
    private Animator anim;
    public float MovementSpeed = 10;
    public float JumpForce = 1;
    public float dirX;
    private Rigidbody2D _rigidbody;
    private void Start()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal") * MovementSpeed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x + dirX, transform.position.y);

        if (dirX != 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("hit"))
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (dirX < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (dirX > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        }
        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        {
            _rigidbody.AddForce(new Vector2(0, JumpForce));
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("hit") && Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("isWalking", false);
            anim.SetTrigger("attack");
        }
    }
}