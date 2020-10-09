using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // These variables represent the clips for each sound effect in the game.
    public AudioClip gunFire;
    public AudioClip upgradedGunFire;
    public AudioClip hurt;
    public AudioClip alienDeath;
    public AudioClip marineDeath;
    public AudioClip victory;
    public AudioClip elevatorArrived;
    public AudioClip powerUpPickup;
    public AudioClip powerUpAppear;

    // This is a simple a wrapper call to PlayOneShot()
    public void PlayOneShot(AudioClip clip)
    {
        soundEffectAudio.PlayOneShot(clip);
    }

    // store a static reference to the single instance of SoundManager.
    public static SoundManager Instance = null;
    // audio source we added to the SoundManager earlier that will be used to play sound effects.
    private AudioSource soundEffectAudio;

    // Start is called before the first frame update
    void Start()
    {
        // ensures that there is always only one copy of this object in existence. 
        /* (If, for some reason, a second SoundManager is created, 
        it automatically destroys itself to make sure that one
        and only one SoundManager exists.)*/
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        // GetComponents() returns all of the components of a particular type.
        /* Since we added music to one of  the sources,
        this checks for the audio source that has no music before setting soundEffectAudio */
        AudioSource[] sources = GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            if (source.clip == null)
            {
                soundEffectAudio = source;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
