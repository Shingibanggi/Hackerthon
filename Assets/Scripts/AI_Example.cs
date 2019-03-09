using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.AI;

public class AI_Example : MonoBehaviour
{
    //player
    public GameObject Player;

    public float fov = 120f;
    public float viewDistance = 20f;
    public static bool isaware = false;
    public float wanderRadius = 7f;

    private NavMeshAgent agent;
    private Vector3 wanderPoint;

   
    public void Start()
    {
        //get component and set var
        agent = GetComponent<NavMeshAgent>();

        //set random point in navmesh for zombie
        wanderPoint = RandomWanderPoint();
    }

    public void Update()
    {
        //keep check for Player

        if(isaware){
            //Chase
            agent.SetDestination(Player.transform.position);
            //Change Color
        }
        else{
            //Search and randomly wander
            if(!Move.inlocker){
                SearchPlayer();
            }
            Wander();
        }
    }

    public void SearchPlayer(){
        
        if(Vector3.Angle(Vector3.forward,transform.InverseTransformPoint(Player.transform.position))< fov/2){
            if(Vector3.Distance(Player.transform.position,transform.position) < viewDistance){
                RaycastHit hit;
                //save data in hit which collide with them
                if(Physics.Linecast(transform.position,Player.transform.position,out hit, -1)){
                    //check tag
                    if(hit.transform.CompareTag("Player")){
                        OnAware();
                    }
                }
            }
        }
    }

    //change status of zombie
    public void OnAware(){
        isaware = true;
    }

    //create random wander point within Navmesh
    public Vector3 RandomWanderPoint(){
        //wanderRadius의 반지름을 가지는 구 안에서 무작위 포인트를 생성하고 현재 위치에 더해준다
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius)+transform.position;

        //from random point it create next position within navmesh
        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(randomPoint, out navMeshHit, wanderRadius, -1);

        //create new vector for nexy detination
        return new Vector3(navMeshHit.position.x,transform.position.y,navMeshHit.position.z);
    }

    public void Wander(){
        //if next position is too close create new destination
        if(Vector3.Distance(transform.position,wanderPoint) < 2f){
            wanderPoint = RandomWanderPoint();
        }
        //move to its destination
        else{
            agent.SetDestination(wanderPoint);
        }
    }
}
