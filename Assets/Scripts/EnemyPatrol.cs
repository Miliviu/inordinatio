using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform leftEdge;
    public Transform rightEdge;

    public Transform enemy;

    public float speed;
    private Vector3 initScale;
    private bool movingLeft;


    public Animator anim;

    private void Start()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange(1);
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange(-1);
        }
    }

    private void DirectionChange(int _direction)
    {
        anim.SetBool("moving", false);
        enemy.position = new Vector3(enemy.position.x + _direction,
            enemy.position.y, enemy.position.z);
        enemy.localScale = new Vector3((Mathf.Abs(initScale.x) * _direction),
          initScale.y, initScale.z);
        movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        anim.SetBool("moving", true);

        //Move in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }
}