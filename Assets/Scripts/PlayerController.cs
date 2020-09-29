using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 50.0f;
    //istance variable to store the CharacterController
    private CharacterController characterController;

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
}
