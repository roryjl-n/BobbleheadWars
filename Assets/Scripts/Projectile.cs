using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    /*OnBecameInvisible() is a method that is called when the object is no longer
    visible by any camera.*/
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    //OnCollisionEnter() is called during a collision event. 
    /*The Collision object contains information about the actual 
     collision as well as the target object*/
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
