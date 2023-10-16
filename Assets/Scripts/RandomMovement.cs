using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
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
    public float nearestOilDistance = 100f;
    public float timeLeft = 0;
    public GameObject[] allAutoObjects;
    public GameObject nearestAutoObject;
    public float autoDistance;
    public float nearestAutoDistance = 100f;
    public float rangeSmallVander = 15f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        
        timeLeft -= Time.deltaTime;
        if (timeLeft <0){   //every 4 seconds check nearby Oil and cars, doing that so performance wouldnt suffer that much
            timeLeft = 4;
            ObjectFinder();
        }

        if (!canSplit && nearestOilDistance<=rangeSmallVander && nearestOilObject!=null){
            agent.ResetPath();
            agent.destination = nearestOilObject.transform.position;
            seekingOil = true;
        }
        else if (canSplit && nearestAutoDistance<=rangeSmallVander && nearestAutoObject!=null){
            agent.ResetPath();
            agent.SetDestination(nearestAutoObject.transform.position);
            crashingWithAuto = true;
        }
        else if(agent.remainingDistance <= agent.stoppingDistance) //done with path             stole this implementation from internet
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, rangeVander, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.ResetPath();
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
        seekingOil = false; //needed in case if someone else already took the oil or car crash already happened
        crashingWithAuto = false;
        nearestAutoDistance = 100f;  //random value given. needed so no info from previous runs doesnt get saved (usually in case of single car or no oil)
        nearestOilDistance = 100f;
        allOilObjects = GameObject.FindGameObjectsWithTag("Oil");   //yes, i know this is not efficient, but should be fine. There will never be situation when there are too many barrels or autos.
        for (int i=0; i<allOilObjects.Length; i++){
            oilDistance = Vector3.Distance(this.transform.position, allOilObjects[i].transform.position);
            if (oilDistance<nearestOilDistance){
                nearestOilObject = allOilObjects[i];
                nearestOilDistance = oilDistance;
            }
        }
            
        allAutoObjects = GameObject.FindGameObjectsWithTag("Auto");
        for (int i=0; i<allAutoObjects.Length; i++){ 
            autoDistance = Vector3.Distance(this.transform.position, allAutoObjects[i].transform.position);
//            Debug.Log(autoDistance);
            if (autoDistance<nearestAutoDistance && autoDistance!=0 && allAutoObjects[i].GetComponent<RandomMovement>().canSplit==true){     
                nearestAutoObject = allAutoObjects[i];
                nearestAutoDistance = autoDistance;
            }
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.tag=="Auto" && canSplit==true && nearestAutoObject?.GetComponent<RandomMovement>().canSplit==true){
            canSplit=false;
            nearestAutoObject.GetComponent<RandomMovement>().canSplit=false;
            nearestAutoObject.GetComponent<Transform>().localScale = new Vector3 (0.4f, 0.4f, 0.4f);
            this.transform.localScale = new Vector3 (0.4f, 0.4f, 0.4f);
            nearestAutoObject = null;
            //create only 1 new car, not sure how to implement it, so it doesnt trigger 2 times :/
        }

        if (other.tag=="Oil" && canSplit==false){
            Destroy(other.gameObject);
            nearestOilObject = null;
            canSplit = true;
            seekingOil = false;

            this.transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
        }
    }


}

