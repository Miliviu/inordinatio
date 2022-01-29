using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenThing : MonoBehaviour{    
    public GameObject Sound1;
    private AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            source.Play();
        }
    }
}
