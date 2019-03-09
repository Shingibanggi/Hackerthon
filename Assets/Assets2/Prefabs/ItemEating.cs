using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEating : MonoBehaviour
{
    public AudioSource eatingSound;

    // Start is called before the first frame update
    void Start()
    {
        eatingSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Target"){
            eatingSound.Play();
        }
    }
}
