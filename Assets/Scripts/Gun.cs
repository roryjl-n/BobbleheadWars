using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // set the bulletPrefab to the bullet prefab
    public GameObject bulletPrefab;
    //  set the launchPosition to the position of the barrel of the Space Marine’s gun.
    public Transform launchPosition;

    void fireBullet()
    {
        /* 1 Instantiate() is a built-in method that creates a GameObject instance for a
        particular prefab. In this case, this will create a bullet based on the bullet prefab.
        Since Instantiate() returns a type of Object, the result must be cast into a GameObject. */
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        /* 2 The bullet’s position is set to the launcher’s position — you’ll set the launcher as
        the barrel of the gun in just a moment. */
        bullet.transform.position = launchPosition.position;
        /* 3 Since the bullet has a Rigidbody attached to it, you can specify its velocity to make
        the bullet move at a constant rate. Direction is determined by the transform of the
        object to which this script is attached — you’ll soon attach it to the body of the
        space marine, ensuring the bullet travels in same the direction as the marine faces. */
        bullet.GetComponent<Rigidbody>().velocity =
        transform.parent.forward * 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // check with the Input Manager to see if the left mouse button is held down
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsInvoking("fireBullet"))
            {
                InvokeRepeating("fireBullet", 0f, 0.1f);
            }
        }
        // gun stops firing once the user releases the mouse button
        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("fireBullet");
        }
    }
}
