using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rock_movement : MonoBehaviour
{
    public NavMeshAgent agentRock;
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().destination = player.transform.position;
        
        
        
        //test to check if works at all
        /**
        if (Input.GetMouseButtonDown(1)){
            Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(movePosition, out var hitInfo)){
                agentRock.SetDestination(hitInfo.point);
            }
        }
        **/




    }
}
