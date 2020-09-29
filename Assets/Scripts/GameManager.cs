using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //player represents the hero's GameObject.
    public GameObject player;
    //locations from which aliens will spawn in the arena.
    public GameObject[] spawnPoints;
    //alien represents the prefab for the alien.
    public GameObject alien;
    //Will determine how many aliens appear on the screen at once.
    public int maxAliensOnScreen;
    //Will represent the total number of aliens the player must vanquish to claim victory.
    public int totalAliens;
    //Will control the rate at which aliens appear.
    public float minSpawnTime;
    public float maxSpawnTime;
    //Will determine how many aliens appear during a spawning event.
    public int aliensPerSpawn;
    //Will track the total number of aliens currently displayed.
    private int aliensOnScreen = 0;
    //Will track the time between spawn events.
    private float generatedSpawnTime = 0;
    //Will track the milliseconds since the last spawn.
    private float currentSpawnTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Accumulates the amount of time that's passed between each frame update.
        currentSpawnTime += Time.deltaTime;

        //Spawn-time randomizer
        //Creates a time between minSpawnTime and maxSpawnTime.
        if (currentSpawnTime > generatedSpawnTime)
        {
            //resets the timer after a spawn occurs.
            currentSpawnTime = 0;
            //Spawn-time randomizer
            //Creates a time between minSpawnTime and maxSpawnTime.
            generatedSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

            //This logic determines whether to spawn.
            //This code is a preventative measure that stops spawning when the maximum number of aliens are present.
            if (aliensPerSpawn > 0 && aliensOnScreen < totalAliens)
            {
                //This creates an array we will use to keep track of where we spawn aliens each wave. 
                List<int> previousSpawnLocations = new List<int>();

                //This limits the number of aliens we can spawn by the number of spawn points.
                if (aliensPerSpawn > spawnPoints.Length)
                {
                    aliensPerSpawn = spawnPoints.Length - 1;
                }

                // If aliensPerSpawn exceeds the maximum, then the amount of spawns will reduce.
                aliensPerSpawn = (aliensPerSpawn > totalAliens) ?
                  aliensPerSpawn - totalAliens : aliensPerSpawn;

                //This loop iterates once for each spawned alien.
                for (int i = 0; i < aliensPerSpawn; i++)
                {
                    // This code checks if aliensOnScreen is less than the maximum, and then it increments the total screen amount.
                    if (aliensOnScreen < maxAliensOnScreen)
                    {
                        aliensOnScreen += 1;

                        //This code is responsible for finding a spawn point.
                        // 1 spawnPoint is the generated spawn point number.
                        int spawnPoint = -1;
                        // 2 This loop runs until it finds a spawn point or the spawn point is no longer -1,
                        while (spawnPoint == -1)
                        {
                            // 3 This line produces a random number as a possible spawn point.
                            int randomNumber = Random.Range(0, spawnPoints.Length - 1);
                            /* 4  checks the previousSpawnLocations array to see if that random number is
                            an active spawn point.  If there’s no match, then we have our spawn point.
                            If it finds a match, the loop iterates again with a new random number. */
                            if (!previousSpawnLocations.Contains(randomNumber))
                            {
                                previousSpawnLocations.Add(randomNumber);
                                spawnPoint = randomNumber;
                            }
                        }

                        // this grabs the spawn point based on the index that we generated in the last code.
                        GameObject spawnLocation = spawnPoints[spawnPoint];
                        //Instantiate() will create an instance of any prefab passed into it.
                        //It’ll create an object that is the type Object, so we must cast it into a GameObject.
                        GameObject newAlien = Instantiate(alien) as GameObject;
                        //This positions the alien at the spawn point.
                        newAlien.transform.position = spawnLocation.transform.position;
                        //This gets a reference to the Alien script.
                        Alien alienScript = newAlien.GetComponent<Alien>();
                        //This sets the target to the space marine’s current position.
                        alienScript.target = player.transform;

                        /* This code rotates the alien towards the hero using the alien’s y-axis position so that it
                        doesn’t look upwards and stare straight ahead.*/
                        Vector3 targetRotation = new Vector3(player.transform.position.x,
                          newAlien.transform.position.y, player.transform.position.z);
                        newAlien.transform.LookAt(targetRotation);

                    }
                }
            }
        }
    }
}
