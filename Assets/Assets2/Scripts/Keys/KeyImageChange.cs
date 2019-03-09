using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyImageChange : MonoBehaviour
{
    public GameObject KeyDark1;
    public GameObject KeyDark2;
    public GameObject KeyDark3;
    public GameObject KeyDark4;

    public GameObject KeyBright1;
    public GameObject KeyBright2;
    public GameObject KeyBright3;
    public GameObject KeyBright4;

    public GameObject Image;

    public static int keyCount;
    int startCount = 0;
    int check = 0;

    // Start is called before the first frame update
    void Start()
    {
        keyCount = startCount;
    }

    // Update is called once per frame
    void Update()
    {

        if(keyCount == 1){
            KeyDark1.gameObject.SetActive(false);
            KeyBright1.gameObject.SetActive(true);

            if(check == 0)
            {
                Image.gameObject.SetActive(true);
                Invoke("Remove_Function", 2.5f);
                check++;
            }
        }
        else if(keyCount == 2){
            KeyDark2.gameObject.SetActive(false);
            KeyBright2.gameObject.SetActive(true);

            if (check == 1)
            {
                Image.gameObject.SetActive(true);
                Invoke("Remove_Function", 2.5f);
                check++;
            }
            Invoke("Scene_Function", 3.5f);
        }
        else if(keyCount == 3){
            KeyDark3.gameObject.SetActive(false);
            KeyBright3.gameObject.SetActive(true);

            if (check == 2)
            {
                Image.gameObject.SetActive(true);
                Invoke("Remove_Function", 2.5f);
                check++;
            }
        }
        else if(keyCount == 4){
            KeyDark4.gameObject.SetActive(false);
            KeyBright4.gameObject.SetActive(true);

            if (check == 3)
            {
                Image.gameObject.SetActive(true);
                Invoke("Remove_Function", 2.5f);
                check++;
            }
        }

    }

    private void Remove_Function()
    {
        Image.gameObject.SetActive(false);
    }

    private void Scene_Function()
    {
        SceneManager.LoadScene("BASML_Demo_Scene");
    }

}
