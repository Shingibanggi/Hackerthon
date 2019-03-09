using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flash_move : MonoBehaviour
{

    public Animation anim;

    void Start()
    {
        anim = GetComponent<Animation>();
        foreach (AnimationState state in anim)
        {
            state.speed = 1F;
        }
        anim.Play();
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
