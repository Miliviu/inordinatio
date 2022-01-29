using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Character2DController : MonoBehaviour
{
    public GameObject hud;
    private AudioSource source;
    private Animator anim;
    private float cooldownTimer = Mathf.Infinity;
    public float attackCooldown = 1;
    public float MovementSpeed = 10;
    public float JumpForce = 1;
    public float dirX;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        anim = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal") * MovementSpeed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x + dirX, transform.position.y);

        if (dirX != 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("hit"))
            anim.SetBool("isWalking", true);
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (dirX < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);


        if (dirX > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        {
            _rigidbody.AddForce(new Vector2(0, JumpForce));
        }

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("hit") && Input.GetButtonDown("Fire1") && cooldownTimer > attackCooldown)
        {
            anim.SetBool("isWalking", false);
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "border")
            StartCoroutine(show_hard());
        if (collision.gameObject.name == "border2")
            StartCoroutine(show_hard());
    }

    IEnumerator show_hard()
    {
        hud.SetActive(true);
        yield return new WaitForSeconds(20);
        hud.SetActive(false);
    }
    
}
