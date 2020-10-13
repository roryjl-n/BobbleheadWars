using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticles : MonoBehaviour
{
    //deathParticles refers to the current particle system 
    private ParticleSystem deathParticles;
    // didStart lets you know the particle system has started to play.
    private bool didStart = false;

    //This starts the particle system and informs the script that it started. 
    public void Activate()
    {
        didStart = true;
        deathParticles.Play();
    }

    //The script first checks to see if a particle system has been loaded.
    //In case Start() hasn’t been called and deathParticles isn’t populated, this line populates it. 
    public void SetDeathFloor(GameObject deathFloor)
    {
        if (deathParticles == null)
        {
            deathParticles = GetComponent<ParticleSystem>();
        }
        deathParticles.collision.SetPlane(0, deathFloor.transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        // reference to the particle system
        deathParticles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //Once the particle system stops playing, the script deletes the death particles because
        //they are meant to play only once.
        if (didStart && deathParticles.isStopped)
        {
            Destroy(gameObject);
        }
    }
}
