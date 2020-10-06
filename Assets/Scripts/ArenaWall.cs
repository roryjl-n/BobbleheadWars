using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaWall : MonoBehaviour
{
    private Animator arenaAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // this gets the parent GameObject by accessing the parent property on the transform.
        GameObject arena = transform.parent.gameObject;
        // Once it has a reference to the arena GameObject, it calls GetComponent() for a reference to the animator.
        arenaAnimator = arena.GetComponent<Animator>();

    }

    //When the trigger is activated, the code sets IsLowered to true.
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        arenaAnimator.SetBool("IsLowered", true);
    }

    // When the hero leaves the trigger, this code tells the Animator to set IsLowered to false to raise the walls.
    void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit");
        arenaAnimator.SetBool("IsLowered", false);
    }


    // Update is called once per frame
    void Update()
    {
       
    }

}
