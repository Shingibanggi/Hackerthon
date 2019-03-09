using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Example : MonoBehaviour
{
    //intensity of action - radius of sphere
    public float Intensity = 3f;
    public LayerMask Zombie_Layer;
    public float Non_Hide_Radius = 1f;
    public float Hide_Radius = 0f;
    public bool hide = false;

    private GameObject Player;

    public void Start()
    {
        //set var here
        Player = this.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        //if collide with zombie(compare by tag)
        if (other.gameObject.CompareTag("Zombie"))
        {
            PlayerHealth.damaged = true;
            other.GetComponent<Animation>().Play("attack");
            other.GetComponent<AI_Example>().OnAware();
        }
    }

    public void OnTriggerExit(Collider other){
        //if collide with zombie(compare by tag)
        if (other.gameObject.CompareTag("Zombie"))
        {
            PlayerHealth.damaged = false;
            other.GetComponent<Animation>().Play("walk_in_place");
        }
    }

    //hide from zombie
    public void Hide()
    {
        if (Player.activeSelf)
        {
            AI_Example.isaware = false;
            Player.SetActive(false);
        }
        else
        {
            Player.SetActive(true);
        }

    }

}
