using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject start;
    public GameObject tutorial1;
    public GameObject tutorial2;

    // Start is called before the first frame update
    void Start()
    {
       
        tutorial1.gameObject.SetActive(true);
        Invoke("Remove_Function", 5.0f);

        Invoke("Function2", 5.0f);
        Invoke("Remove_Function2", 15.0f);

        Invoke("Go", 15.0f);
        Invoke("Remove_Go", 17.0f);
        Move.first = false;
    }

    private void Remove_Function()
    {
        tutorial1.gameObject.SetActive(false);
    }

    private void Function2()
    {
        tutorial2.gameObject.SetActive(true);
    }

    private void Remove_Function2()
    {
        tutorial2.gameObject.SetActive(false);
    }

    private void Go()
    {
        start.gameObject.SetActive(true);
    }

    private void Remove_Go()
    {
        start.gameObject.SetActive(false);
        Move.isStopped = false;
    }


}
