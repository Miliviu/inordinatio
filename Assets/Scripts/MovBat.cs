using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MovBat : MonoBehaviour
 {
    public int damage = 1;
    private float RotateSpeed = 3f;
    private float Radius = 0.02f;
    public Health_script playerHealth;
    public float colliderDistance = 0;
    public BoxCollider2D boxCollider;
    public LayerMask playerLayer;
    public float range = 1;
 
    private Vector2 _centre;
    private float _angle;
 
    private void Start()
    {
        _centre = transform.position;
    }
 
    private void Update()
    {
        _angle += RotateSpeed * Time.deltaTime;
 
        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        transform.Translate(offset.x,offset.y,0);
        DamagePlayer();
    }

    //start attack code
    private bool PlayerInSight()
    {
        RaycastHit2D hit = 
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health_script>();
        return hit.collider;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
            playerHealth.TakeDamage(damage);
    }
//end attack code
}