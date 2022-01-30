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

//data for the attack
    public float range = 3;
    public int damage;
    public float colliderDistance = 0.25f;
    public BoxCollider2D boxCollider;
    public LayerMask enemyLayer;
    public Health_script enemyHealth;
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
            if (cooldownTimer >= attackCooldown)
            {
                anim.SetBool("isWalking", false);
                Attack();
            }
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;
    }

    //start attack code
    private bool EnemyInSight()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, enemyLayer);

        if (hit.collider != null)
            enemyHealth = hit.transform.GetComponent<Health_script>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamageEnemy()
    {
        if (EnemyInSight())
            enemyHealth.TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && anim.GetBool("attack") == true)
            collision.GetComponent<Health_script>().TakeDamage(1);
    }

//end attack code

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "border")
        {
            StartCoroutine(bad_text());
        }
    }

    IEnumerator bad_text()
    {
        hud.SetActive(true);
        yield return new WaitForSeconds(8);
        hud.SetActive(false);
    }
}