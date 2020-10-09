using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // set the bulletPrefab to the bullet prefab
    public GameObject bulletPrefab;
    //  set the launchPosition to the position of the barrel of the Space Marine’s gun.
    public Transform launchPosition;

    private AudioSource audioSource;

    // flag that lets the script know whether to fire one bullet or three.
    public bool isUpgraded;
    //  how long the upgrade will last (in seconds).
    public float upgradeTime = 10.0f;
    //  keeps track of how long it’s been since the gun was upgraded.
    private float currentTime;

    //this method simply encapsulates the bullet creation process. 
    // It returns a Rigidbody after you create the bullet.
    private Rigidbody createBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        bullet.transform.position = launchPosition.position;
        return bullet.GetComponent<Rigidbody>();
    }

    // This method lets the gun know it’s been upgraded and sets the counter timer to zero.
    public void UpgradeGun()
    {
        isUpgraded = true;
        currentTime = 0;
    }

    // old code
    /*void fireBullet()
    {
         1 Instantiate() is a built-in method that creates a GameObject instance for a
        particular prefab. In this case, this will create a bullet based on the bullet prefab.
        Since Instantiate() returns a type of Object, the result must be cast into a GameObject. 
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
         2 The bullet’s position is set to the launcher’s position — you’ll set the launcher as
        the barrel of the gun in just a moment. 
        bullet.transform.position = launchPosition.position;
         3 Since the bullet has a Rigidbody attached to it, you can specify its velocity to make
        the bullet move at a constant rate. Direction is determined by the transform of the
        object to which this script is attached — you’ll soon attach it to the body of the
        space marine, ensuring the bullet travels in same the direction as the marine faces. 
        bullet.GetComponent<Rigidbody>().velocity =
        transform.parent.forward * 100;

        //This code plays the shooting sound.
        audioSource.PlayOneShot(SoundManager.Instance.gunFire);
    } */

    //This code creates the bullet just like the previous version of the script.
    void fireBullet()
    {
        Rigidbody bullet = createBullet();
        bullet.velocity = transform.parent.forward * 100;

        // This bit of code fires the next two bullets at angles
        /* It calculates the angle by adding the forward direction 
         to either the right- or left-hand direction and dividing in half*/
        if (isUpgraded)
        {
            Rigidbody bullet2 = createBullet();
            bullet2.velocity =
            (transform.right + transform.forward / 0.5f) * 100;
            Rigidbody bullet3 = createBullet();
            bullet3.velocity =
            ((transform.right * -1) + transform.forward / 0.5f) * 100;
        }

        // This provides the shooting sound.
        if (isUpgraded)
        {
            audioSource.PlayOneShot(SoundManager.Instance.upgradedGunFire);
        }
        else
        {
            audioSource.PlayOneShot(SoundManager.Instance.gunFire);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // gets a reference to the attached AudioSource 
        audioSource = GetComponent<AudioSource>();
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

        /* If the time here is greater than the time the player has been 
         upgraded, this code takes the upgrade away */
        currentTime += Time.deltaTime;
        if (currentTime > upgradeTime && isUpgraded == true)
        {
            isUpgraded = false;
        }
    }
}
