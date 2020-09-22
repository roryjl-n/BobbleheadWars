using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get current position of the GameObject and stores it in variable pos.
        //Vector3 = x, y and z of a GameObject.
        Vector3 pos = transform.position;

        //a or left key pressed = -1(left) and d or right key pressed = 1(right)
        pos.x += moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        //s or down key pressed = -1(back) and w or up key pressed = 1(forward)
        pos.z += moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;

        //updates SpaceMarine GameObject's position with the new position. 
        transform.position = pos;

    }
}
