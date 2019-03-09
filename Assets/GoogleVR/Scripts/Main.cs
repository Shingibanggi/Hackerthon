using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Main : MonoBehaviour
{
    GameObject Currentgaze = null;

    void Update()
    {

        RaycastHit hitInfo;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,
out hitInfo, 9999f)) //광선발사  카메라의 정면방향으로 (이 광선은 저희 눈에는 보이지 않습니다)
        {
            if (Currentgaze == null)
            {
                hitInfo.collider.SendMessage("OnGazeEnter", SendMessageOptions.DontRequireReceiver);
                //광선에 닿으면 그 닿은 물체에다가 "OnGazeEnter" 메시지 전달. 
                //이 메시지라는 것은 결국 그 물체 안에 있는 스크립트에 OnGazeEnter() 함수를 실행하라는 말과 같습니다.
                Currentgaze = hitInfo.collider.gameObject; //새로 바라보는 물체 설정

                SendMessage("Onhitinfo", hitInfo, SendMessageOptions.DontRequireReceiver);
            }
        }
        else
        {
            if (Currentgaze != null) //물체가 있으면
            {
                Currentgaze.SendMessage("OnGazeExit", SendMessageOptions.DontRequireReceiver);
                //메시지 전달
                Currentgaze = null;
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 1000f);
        //테스트용 기즈모 그리기. 기즈모라는 것은 Raycast에서의 가상의 광선을 실제로 보여주는 역할을 합니다.
    }

    public void Test(){
        Debug.Log("test");
    }
}
