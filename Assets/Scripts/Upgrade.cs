using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{

    public Gun gun;

    /* When the player collides with the power-up, this sets the gun to Upgrade mode, and 
     then it will destroy itself (each pickup is good for one upgrade only). Also, a sound plays 
     when the player picks up the upgrade. */
    void OnTriggerEnter(Collider other)
    {
        gun.UpgradeGun();
        Destroy(gameObject);
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.powerUpPickup);
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
