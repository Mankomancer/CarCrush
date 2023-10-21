using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Rock_movement : MonoBehaviour
{
    public NavMeshAgent agentRock;
    public GameObject player;

    private NavMeshAgent rock;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rock = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
       rock.destination  = player.transform.position;    
    }

}
