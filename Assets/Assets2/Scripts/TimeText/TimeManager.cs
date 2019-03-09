using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int time;
    public int seconds;
    Text text;
    //public PlayerHealth playerhealth;

    void Start()
    {
        text = GetComponent<Text>();
        time = PlayerHealth.currentHealth;
    
    }

    // Update is called once per frame
    void Update()
    {
        time = PlayerHealth.currentHealth;
        //print("LIFE: "+time+", player currentHealth: "+PlayerHealth.currentHealth);
        text.text = "LIFE: " + time; 
    }
}
