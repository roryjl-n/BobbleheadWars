using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//access the NavmeshAgent classes
using UnityEngine.AI;
//This allows us to access UnityEvent in code.
using UnityEngine.Events;

public class Alien : MonoBehaviour
{
    //target is where the alien should go.
    public Transform target;
    private NavMeshAgent agent;
    // navigationUpdate is the amount of time, in milliseconds, for when the alien should update its path.
    public float navigationUpdate;
    // navigationTime is a private variable that tracks how much time has  passed since the previous update.
    private float navigationTime = 0;
    //The UnityEvent custom event type that we can configure in the Inspector.
    public UnityEvent OnDestroy;
    // This destroys the alien.
    public void Die()
    {
        //This notifies all listeners, including the GameManager, of the alien’s death.
        OnDestroy.Invoke();

        //This removes any listeners that are listening to the event
        OnDestroy.RemoveAllListeners();

        Destroy(gameObject);
    }

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
            //This code checks to see if a certain amount of time has passed then updates the path.
            navigationTime += Time.deltaTime;
            if (navigationTime > navigationUpdate)
            {
                agent.destination = target.position;
                navigationTime = 0;
            }
        }
    }

    //
    void OnTriggerEnter(Collider other)
    {
        // We’ve refactored the destroy code into another method.
        Die();

        //
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
    }
}
