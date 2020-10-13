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
    // This code works similarly to the marine’s death, but we disable the Animator for the alien.
    // In addition, this code launches the head off the body.
    public void Die()
    {
        isAlive = false;
        head.GetComponent<Animator>().enabled = false;
        head.isKinematic = false;
        head.useGravity = true;
        head.GetComponent<SphereCollider>().enabled = true;
        head.gameObject.transform.parent = null;
        head.velocity = new Vector3(0, 26.0f, 3.0f);

        //This block notifies the listeners, removes them and then it deletes the GameObject.
        OnDestroy.Invoke();
        OnDestroy.RemoveAllListeners();
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);

        //
        head.GetComponent<SelfDestruct>().Initiate();
        Destroy(gameObject);
    }
    //head will help us launch the head
    public Rigidbody head;
    // isAlive will track the alien’s state.
    public bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        //This gets a reference to the NavMeshAgent so we can access it in code.
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
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

    // Here we check isAlive first.
    void OnTriggerEnter(Collider other)
    {
        if (isAlive)
        {
            Die();
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDeath);
        }
    }
}
