using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour //stole this from internet
{
    public NavMeshAgent agent;
    public float rangeVander = 50; //radius of sphere

    public Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area
    public bool canSplit = false; //if true, auto can split

    public bool crashingWithAuto = false;
    public bool seekingOil = false;

    //need these for navigation for specific auto, barell
    public GameObject[] allOilObjects;
    public GameObject nearestOilObject;
    public float oilDistance;
    public float nearestOilDistance;
    public float timeLeft = 5;
    public GameObject[] allAutoObjects;
    public GameObject nearestAutoObject;
    public float autoDistance;
    public float nearestAutoDistance;
    public float rangeSmallVander = 15f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        
        ObjectFinder();
/*
        if (!canSplit && nearestOilDistance<=rangeSmallVander){
            //agent.SetDestination(nearestOilObject.transform.position);
            //agent.destination = nearestOilObject.transform.position;
            seekingOil = true;
        }
        else if (canSplit && nearestAutoDistance<=rangeSmallVander){ ///need to fix car "findig oneself"
           // agent.SetDestination(nearestAutoObject.transform.position);
            crashingWithAuto = true;
        }
 */   
        
        if(agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, rangeVander, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
            }
        }

    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * rangeVander; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        { 
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private void ObjectFinder(){
        timeLeft -= Time.deltaTime;
        if (timeLeft <0){   //every 4 seconds check nearby Oil and cars
            timeLeft = 4;
            seekingOil = false; //needed in case if someone else already took the oil or car crash already happened
            crashingWithAuto = false;
            nearestAutoDistance = 500f;  //random value given. needed so no info from previous runs doesnt get saved (usually in case of single car or no oil)
            nearestOilDistance = 500f;
            allOilObjects = GameObject.FindGameObjectsWithTag("Oil");   //yes, i know this is not efficient, but should be fine
            for (int i=0; i<allOilObjects.Length; i++){
                oilDistance = Vector3.Distance(this.transform.position, allOilObjects[i].transform.position);
                if (oilDistance<nearestOilDistance){
                    nearestOilObject = allOilObjects[i];
                    nearestOilDistance = oilDistance;
                }
            }
            

            allAutoObjects = GameObject.FindGameObjectsWithTag("Auto");   //yes, i know this is not efficient, but should be fine
            for (int i=0; i<allAutoObjects.Length; i++){    //THIS CAN BE ANOTHER PART WHERE CAR FINDS ONESELF
                autoDistance = Vector3.Distance(this.transform.position, allAutoObjects[i].transform.position);
                if (autoDistance<nearestAutoDistance){     
                    nearestAutoObject = allAutoObjects[i];
                    nearestAutoDistance = autoDistance;
                }
            }
        }
    }


}

