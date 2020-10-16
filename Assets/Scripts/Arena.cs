using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    //You need to access both the player and the elevator to raise the marine to the top of the arena.
    public GameObject player;
    public Transform elevator;
    //The arenaAnimator will kick off the animation.
    private Animator arenaAnimator;
    //The sphereCollider will initiate the entire sequence.
    private SphereCollider sphereCollider;


    // Start is called before the first frame update
    void Start()
    {
        arenaAnimator = GetComponent<Animator>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //The first line gets the camera then disables the movement.
    //Then the player is made into a child of the platform.
    void OnTriggerEnter(Collider other)
    {
        Camera.main.transform.parent.gameObject.
        GetComponent<CameraMovement>().enabled = false;
        player.transform.parent = elevator.transform;

        //We disable the player’s ability to control the marine.
        //This stops the player from turning, shooting or doing anything with the marine.
        player.GetComponent<PlayerController>().enabled = false;

        // audio cue to alert the player to the elevator’s arrival.
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.elevatorArrived);

        //This will start the animation state and work when the player enters the sphere collider.
        arenaAnimator.SetBool("OnElevator", true);
    }

    public void ActivatePlatform()
    {
        sphereCollider.enabled = true;
    }
}
