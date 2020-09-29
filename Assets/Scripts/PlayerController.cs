using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 50.0f;
    //istance variable to store the CharacterController
    private CharacterController characterController;
    //reference to the connected head Rigidbody
    public Rigidbody head;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent() gets a reference to current component passed into the script.
        characterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {

        //creates a new Vector3 to store the movement direction 
        /*SimpleMove() is a built-in method that automatically moves the character in the given direction, but
          not allowing the character to move through obstacles.*/
         Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"),
          0, Input.GetAxis("Vertical"));
        characterController.SimpleMove(moveDirection * moveSpeed);

        //Get current position of the GameObject and stores it in variable pos.
        //Vector3 = x, y and z of a GameObject.
        //Vector3 pos = transform.position;

        //a or left key pressed = -1(left) and d or right key pressed = 1(right)
        //pos.x += moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        //s or down key pressed = -1(back) and w or up key pressed = 1(forward)
        // pos.z += moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;

        //updates SpaceMarine GameObject's position with the new position. 
        //transform.position = pos;

    }

    //FixedUpdate() handles physics, it’s called at consistent intervals and not subject to frame rate.
    //Anything that affects a Rigidbody should be updated in FixedUpdate().
    void FixedUpdate()
    {
        //This code moves the head when the marine moves
        // First, it calculates the movement direction.If the value equals Vector3.zero, then the marine is standing still.
        //AddForce() provides a direction then multiply it by the force amount.
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"),
        0, Input.GetAxis("Vertical"));
        if (moveDirection == Vector3.zero)
        {
            // TODO
        }
        else
        {
            head.AddForce(transform.right * 150, ForceMode.Acceleration);
        }
    }
}
