using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Health_script : MonoBehaviour
{
    public float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;


    public float iFramesDuration;
    public int numberOfFlashes;
    private SpriteRenderer spriteRend;

    public Behaviour[] components;
    private bool invulnerable;

    private void Start()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead)
            {
                foreach (Behaviour component in components)
                    component.enabled = false;
                dead = true;
                anim.SetTrigger("die");
                if (anim.name == "warrior")
                {
                    StartCoroutine(Waiter());
                }
            }
        }
    }

    private IEnumerator Waiter()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("end");
    }

    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
        invulnerable = false;
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}