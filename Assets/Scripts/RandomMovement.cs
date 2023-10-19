using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    
    public NavMeshAgent agent;

    [SerializeField] private GameObject parentObject; //objektu organizēšanai
    public GameObject autoPrefab;
   // public GameObject[] allOilObjects;
    public GameObject nearestOilObject;
   // public GameObject[] allAutoObjects;
    public GameObject nearestAutoObject;

    public Transform centrePoint; //centre of the area the agent wants to move around in

    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area
    public bool canSplit = false; //if true, auto can split
    public bool crashingWithAuto = false;
    public bool seekingOil = false;

    //need these for navigation for specific auto, barell
    public float rangeVander = 50; //radius of sphere
    public float oilDistance;
    public float nearestOilDistance = 100f;
    public float timeLeft = 0;
    public float autoDistance;
    public float nearestAutoDistance = 100f;
    public float rangeSmallVander = 15f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        transform.rotation = UnityEngine.Quaternion.Euler(0,0,0);
        nearestAutoObject = null;
        FirstTimeItemAddToList("Oil",ScoreManager.allOilObjects);
        FirstTimeItemAddToList("Auto",ScoreManager.allAutoObjects);
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
            UnityEngine.Vector3 point;
            if (RandomPoint(centrePoint.position, rangeVander, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, UnityEngine.Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.ResetPath();
                agent.SetDestination(point);
            }
        }

    }
    
    
    bool RandomPoint(UnityEngine.Vector3 center, float range, out UnityEngine.Vector3 result)
    {
        UnityEngine.Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * rangeVander; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        { 
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = UnityEngine.Vector3.zero;
        return false;
    }
    

    private void ObjectFinder()
    {
        seekingOil = false; //needed in case if someone else already took the oil or car crash already happened
        crashingWithAuto = false;
        nearestAutoDistance = 100f;  //random value given. needed so no info from previous runs doesnt get saved (usually in case of single car or no oil)
        nearestOilDistance = 100f;
      //  allOilObjects = GameObject.FindGameObjectsWithTag("Oil");   //yes, i know this is not efficient, but should be fine. There will never be situation when there are too many barrels or autos.
        for (int i=0; i<ScoreManager.allOilObjects?.Count; i++)
        {
            oilDistance = UnityEngine.Vector3.Distance(this.transform.position, ScoreManager.allOilObjects[i].transform.position);
            if (oilDistance<nearestOilDistance)
            {
                nearestOilObject = ScoreManager.allOilObjects[i];
                nearestOilDistance = oilDistance;
            }
        }
            
       // allAutoObjects = GameObject.FindGameObjectsWithTag("Auto");
        for (int i=0; i<ScoreManager.allAutoObjects?.Count; i++)
        { 
            autoDistance = UnityEngine.Vector3.Distance(this.transform.position, ScoreManager.allAutoObjects[i].transform.position);
            if (autoDistance<nearestAutoDistance && autoDistance!=0 && ScoreManager.allAutoObjects[i].GetComponent<RandomMovement>().canSplit==true)
            {     
                nearestAutoObject = ScoreManager.allAutoObjects[i];
                nearestAutoDistance = autoDistance;
            }
        }
    }
    
    public void HandleTrigger(Collider other)
    {
        if (other?.transform?.parent?.gameObject.tag=="Auto" && canSplit && nearestAutoObject!=null)
        {
            if (nearestAutoObject.GetComponent<RandomMovement>().canSplit==true)
            {
                canSplit=false;
                nearestAutoObject.GetComponent<RandomMovement>().canSplit=false;
                nearestAutoObject.GetComponent<Transform>().localScale = new UnityEngine.Vector3 (0.5f, 0.5f, 0.5f);
                this.transform.localScale = new UnityEngine.Vector3 (0.5f, 0.5f, 0.5f);
                nearestAutoObject = null;
                UnityEngine.Vector3 carSpawn = new UnityEngine.Vector3(this.transform.position.x+0.2f, 1f, this.transform.position.z);
                GameObject spawn ;
                spawn = Instantiate (autoPrefab, carSpawn, UnityEngine.Quaternion.identity,parentObject.transform);
                ScoreManager.allAutoObjects.Add(spawn); // adds newly spawned car to a list
            }
        }

        if (other.tag=="Oil" && !canSplit)
        {
            ScoreManager.allOilObjects.Remove(other.gameObject);
            Destroy(other.gameObject);
            nearestOilObject = null;
            canSplit = true;
            seekingOil = false;
            this.transform.localScale = new UnityEngine.Vector3 (0.8f, 0.8f, 0.8f);
            ObjectFinder();
            timeLeft = 4;
        }
    }

    private void FirstTimeItemAddToList(string tag,List<GameObject> list)
    {
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject foundObject in foundObjects)
        {
            //double checks so same object is not added multiple times
            if (!list.Contains(foundObject))
            {    
                // Add each object to the list
                list.Add(foundObject);
            }
            
            
        }
    }


}

