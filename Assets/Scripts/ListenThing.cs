using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenThing : MonoBehaviour{    
    public GameObject Sound1;
    private AudioSource source;
    private float cooldownTimer = Mathf.Infinity;
    public float attackCooldown = 1;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && cooldownTimer > attackCooldown)
        {
            source.Play();
            cooldownTimer = 0;
        }
        cooldownTimer += Time.deltaTime;
    }
}
