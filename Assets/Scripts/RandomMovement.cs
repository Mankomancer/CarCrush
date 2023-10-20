using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class RandomMovement : MonoBehaviour
{
    
    public NavMeshAgent agent;

    [SerializeField] private GameObject parentObject; //objektu organizēšanai
    public GameObject autoPrefab;
    public GameObject conePrefab;
   // public GameObject[] allOilObjects;
    public GameObject nearestOilObject;
   // public GameObject[] allAutoObjects;
    public GameObject nearestAutoObject;

    public Transform centrePoint; //centre of the area the agent wants to move around in

    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area
    public bool canSplit = false; //if true, auto can split
    //need these for navigation for specific auto, barell
    public float rangeVander = 50; //radius of sphere
    public float oilDistance;
    public float nearestOilDistance = 100f;
    public float timeLeft = 0;
    public float autoDistance;
    public float nearestAutoDistance = 100f;
    public float rangeSmallVander = 15f;
    private float boostTime;    //needed for rock speed boost
    private bool boosting;
    
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
        if (timeLeft <0)
        {   
            timeLeft = 4f;//every 4 seconds check nearby Oil and cars, doing that so performance wouldnt suffer that much
            ObjectFinder();
        }

        if (boosting){
            boostTime+=Time.deltaTime;
            if (boostTime>5){
                this.GetComponent<NavMeshAgent>().speed = 10f;
                boosting = false;
                boostTime = 0;
            }
        }

         /* // somewhere i messed up but new code works 
        if (!canSplit && nearestOilDistance<=rangeSmallVander && nearestOilObject!=null)
        {
            agent.ResetPath();
            agent.destination = nearestOilObject.transform.position;
        }
        else if (canSplit && nearestAutoDistance<=rangeSmallVander && nearestAutoObject!=null)
        {
            agent.ResetPath();
            agent.SetDestination(nearestAutoObject.transform.position);
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
        }*/
        switch (canSplit)
        {
            case false:
                if (nearestOilDistance <= rangeSmallVander && nearestOilObject != null)
                {
                    agent.ResetPath();
                    agent.destination = nearestOilObject.transform.position;
                }
                break;
            case true:
                if (nearestAutoDistance <= rangeSmallVander && nearestAutoObject?.GetComponent<RandomMovement>()?.canSplit == true)
                {
                    agent.ResetPath();
                    agent.SetDestination(nearestAutoObject.transform.position);
                }
                break;
        }

        // Use the var keyword to declare local variables
        if (agent?.remainingDistance <= agent?.stoppingDistance) //done with path
        {
            var point = Vector3.zero;
            if (RandomPoint(centrePoint.position, rangeVander, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
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
        // Initialize the nearest distances to a large value
        nearestAutoDistance = 100f;
        nearestOilDistance = 100f;

        // If the object cannot split, find the nearest oil object
        if (!canSplit)
        {
            foreach (GameObject oilObject in ScoreManager.allOilObjects)
            {
                // Check if the oil object is not null
                if (oilObject != null)
                {
                    // Calculate the distance to the oil object
                    oilDistance = UnityEngine.Vector3.Distance(transform.position, oilObject.transform.position);

                    // Update the nearest oil object and distance if needed
                    if (oilDistance < nearestOilDistance)
                    {
                        nearestOilObject = oilObject;
                        nearestOilDistance = oilDistance;
                    }
                }
            }
        }
        // If the object can split, find the nearest auto object that can also split
        else
        {
            foreach (GameObject autoObject in ScoreManager.allAutoObjects)
            {
                // Check if the auto object is not null and can split
                if (autoObject != null &&  autoObject.GetComponent<RandomMovement>().canSplit)
                {
                    // Calculate the distance to the auto object
                    autoDistance = UnityEngine.Vector3.Distance(transform.position, autoObject.transform.position);

                    // Update the nearest auto object and distance if needed and not zero
                    if (autoDistance < nearestAutoDistance && autoDistance != 0)
                    {
                        nearestAutoObject = autoObject;
                        nearestAutoDistance = autoDistance;
                    }
                }
            }
        }
    }
    public void HandleTrigger(Collider other)
    {
        if (other?.transform?.parent?.gameObject.tag=="Auto" && canSplit && nearestAutoObject!=null)
        {
            if (nearestAutoObject?.GetComponent<RandomMovement>().canSplit==true)
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
            this.transform.localScale = new UnityEngine.Vector3 (0.8f, 0.8f, 0.8f);
            ObjectFinder();
            timeLeft = 4;
        }

        if (other.tag=="Rock"){
            boosting = true;
            boostTime = 0;
            this.GetComponent<NavMeshAgent>().speed = 15f;
        }

        if (other.tag=="Cone"){
            ScoreManager.DropItem();
            ScoreManager.itemSlot = null;
            Destroy(other.gameObject);
            GameObject spawn;
            UnityEngine.Vector3 coneSpawn = new UnityEngine.Vector3(-40f, 1.4f, 2.7f);
            spawn = Instantiate (conePrefab, coneSpawn, UnityEngine.Quaternion.identity);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Object_pick_up>().boughtCone=false;
            //GameObject.FindGameObjectWithTag("Cone").transform.position = new UnityEngine.Vector3 (-40f, 1.4f, 2.7f);
            ScoreManager.allAutoObjects.Remove(this.gameObject);
           // Destroy(this.gameObject);
            StartCoroutine(DelayedDestroy(gameObject));
          

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
    IEnumerator DelayedDestroy(GameObject gameobject)
    {
        yield return new WaitForSeconds(ScoreManager.CarDestroyTime);
        Destroy(gameobject);
    }
}

