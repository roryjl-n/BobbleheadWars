using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //followTarget is what you want the camera to follow.
    public GameObject followTarget;
    // moveSpeed is the speed at which it should move.
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //This code checks to see if there is a target available. If not, the camera doesn’t follow.
        if (followTarget != null)
        {
            /*Next, Vector3.Lerp() is called to calculate the required position of the CameraMount.
              Lerp() takes three parameters: A start position in 3D space, an end position in 3D
              space, and a value between 0 and 1 that represents a point between the starting and
              ending positions. Lerp() returns a point in 3D space between the start and end
              positions that’s determined by the last value. */
            transform.position = Vector3.Lerp(transform.position,
            followTarget.transform.position, Time.deltaTime * moveSpeed);
        }
    }
}
