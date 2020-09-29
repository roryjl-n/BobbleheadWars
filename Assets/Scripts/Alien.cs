using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//access the NavmeshAgent classes
using UnityEngine.AI;

public class Alien : MonoBehaviour
{
    //target is where the alien should go.
    public Transform target;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        //This gets a reference to the NavMeshAgent so we can access it in code.
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            agent.destination = target.position;
        }
    }
}
