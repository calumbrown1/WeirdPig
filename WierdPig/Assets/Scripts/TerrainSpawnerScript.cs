using UnityEngine;
using System.Collections;

/// <summary>
/// Script responsible for generation of backround scenery and platform terrain
/// </summary>
public class TerrainSpawnerScript : MonoBehaviour {

    //Array of platform segments
    [SerializeField]
    GameObject[] platforms;
    //Player object
    GameObject player;
    //Obstacle object to generate on platform
    [SerializeField]
    GameObject obstacle;
    //Empty gameobject to generate as parent for segments (makes scene neater)
    [SerializeField]
    GameObject emptyObj;
    //speedup "powerup" prefab
    [SerializeField]
    GameObject speedUp;
    //array of enemy prefabs
    [SerializeField]
    GameObject[] enemies;
    //star object
    [SerializeField]
    GameObject star;
    //planet object
    [SerializeField]
    GameObject planet;
    //spawn position to use for creation of platform segments
    Vector2 spawnPos;
    //position of previously spawned platform
    Vector2 prevBlockPos;
    //length player can jump, used to calc distance between platformns
    float jumpLength;
    //previous block generated
    GameObject gPrevBlock;
    //array of possibly y axis positions for platform
    float[] fYPosArray;
    //array of segments that make up platform
    GameObject[] gCurPlatform;
    //number of platforms generated
    int platformsGenerated = 0;

    void Start()
    {
        player = GameObject.Find("Player");
        fYPosArray = new float[4];
        InvokeRepeating("MakeNewPlatform", 0.0f, 2.0f);
        gPrevBlock = GameObject.Find("RockRight");
        InitialiseYPosArray();
        SpawnStartingStuff();
        InvokeRepeating("SpawnStars", 0.1f, 0.1f);
        InvokeRepeating("SpawnPlanets", 10.0f, 30.0f);
    }

    #region platform generation
    void InitialiseYPosArray()
    {
        for (int i = 0; i < 4; i++)
        {
            fYPosArray[i] = gPrevBlock.transform.position.y;
        }
        fYPosArray[2] = gPrevBlock.transform.position.y + 0.25f;
        fYPosArray[3] = gPrevBlock.transform.position.y - 0.25f;

    }
    void SetPrevBlock(GameObject prev)
    {
        gPrevBlock = prev;
        InitialiseYPosArray();
    }
    void MakeNewPlatform()
    {
        InitalizePlatform();
        Spawn();
    }
    void InitalizePlatform()
    {
        gCurPlatform = null;
        int size = Random.Range(25, 50);
        gCurPlatform = new GameObject[size];
        for (int i = 0; i < size; i++)
        {
            if (i == 0)
            {
                gCurPlatform[i] = platforms[0];
            }
            else if (i == size - 1 )
            {
                gCurPlatform[i] = platforms[2];
            }
            else
            {
                gCurPlatform[i] = platforms[1];
            }
        }
       
    }
    void Spawn()// this method spawns platforms 
    {
        GameObject[] platform;
        platform = new GameObject[gCurPlatform.Length];
        jumpLength = Random.Range(0.5f, 2.5f); // randomly generate the jumpLength which will be added to the distance between platforms later
        spawnPos.y = fYPosArray[Random.Range(0, 4)]; // randomly decide the y value being above below or on the level of the previous block

        spawnPos.x = gPrevBlock.transform.position.x + jumpLength;
        int iPlatSize = gCurPlatform.Length;
        for (int i = 0; i < iPlatSize; i++)
        {
            GameObject gNextPlatform = (GameObject)Instantiate(gCurPlatform[i], spawnPos, transform.rotation); // create block 
            platform[i] = gNextPlatform;
            gPrevBlock = gNextPlatform;
            spawnPos.x += (gNextPlatform.GetComponent<Collider2D>().bounds.extents.x)*2;
        }
        spawnPos = gPrevBlock.transform.position; // set prevoios blockpos as the current block pos
        CombineSections(platform);
        //every ten platforms spawn optional speedup
        if (platformsGenerated % 10 == 0)
        {
            SpawnSpeedUp(platform);
        }
        else
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                SpawnObstacles(platform);
            }
            else
            {
                SpawnEnemies(platform);
            }
        }
    }
    void CombineSections (GameObject[] platform)
    {
        //cache positions of first and last platform sections
        Vector2 firstSecPos = platform[0].transform.position;
        Vector2 lastSecPos = platform[platform.Length - 1].transform.position;
        //calculate distance between
        float distBetweenSections = lastSecPos.x - firstSecPos.x;
        //create position to instantiate empty parent object
        Vector2 parentSpawnPos = new Vector2(firstSecPos.x + distBetweenSections, firstSecPos.y);
        //create parent object
        GameObject platformContainer = (GameObject)Instantiate(emptyObj, parentSpawnPos, transform.rotation);
        //insert each section of platform into container as child
        for(int i = 0; i <platform.Length; i++)
        {
            platform[i].transform.parent = platformContainer.transform;
        }
        platformsGenerated++;
    }
    #endregion

    #region enemy and obstacle generation
    void SpawnObstacles(GameObject[] platform)
    {
        //caching length of platform
        int platformLength = platform.Length - 1;
        //array containing middle half "blocks" of the platform
        GameObject[] middleSections = new GameObject[platformLength/2];
        int middleSectionsCounter = 0;
        //get middle half platform
        for (int i = 0; i < platformLength; i++)
        {
            //if iterator is inbetween a quater of the length of the platform and three quaters of the platform length
            //" if the iterator is on the "middle half" of the platform "
            if (i > platformLength / 4 && i < (platformLength - platformLength / 4 - 1))
            {  
                //add to array
                middleSections[middleSectionsCounter] = platform[i];
                middleSectionsCounter++;
            }     
        }
        //generate random number of obstacles
        int randNumObstacles = Random.Range(1, 3);
        for (int i = 0; i < randNumObstacles; i++)
        {
            int platformI = Random.Range(0, middleSections.Length - 1);
            Vector2 obsSpawnPos = new Vector2((float)(middleSections[platformI].transform.position.x), (float)(middleSections[platformI].transform.position.y + 0.25));
            GameObject obs = (GameObject)Instantiate(obstacle, obsSpawnPos, transform.rotation);
            obs.transform.Rotate(new Vector3(0, 0, 1), 180);
        }
    }
    void SpawnEnemies(GameObject[] platform)
    {
        //random int to dictate if spawned enemy is flying or normal
        int rand = Random.Range(0, 2);
        //platform enemy is to be spawned above
        //randomly choose platform section in second half of platform object
        GameObject spawnPlatform = platform[Random.Range((int)(platform.Length / 2), platform.Length - 1)];
        //get position to create enemy from chosen platform position
        Vector2 spawnPos = new Vector2();
        spawnPos.x = spawnPlatform.transform.position.x;
        if (rand == 0)
        {
            //spawn normal enemy
            //y position of enemy = the y position of the platform + twice the height of the enemy
            spawnPos.y = spawnPlatform.transform.position.y + 2;
            Instantiate(enemies[0], spawnPos, transform.rotation);
        }
        else
        {
            //spawn flying enemy
            //y position of enemy = the y position of the platform + five times the height of the enemy
            spawnPos.y = spawnPlatform.transform.position.y + 1;
            Instantiate(enemies[1], spawnPos, transform.rotation);
        }
    }
    void SpawnSpeedUp(GameObject[] platform)
    {
        Vector2 spawnPos = new Vector2();
        GameObject middleBlock = platform[(int)(platform.Length / 2)];
        spawnPos.x = middleBlock.transform.position.x;
        spawnPos.y = middleBlock.transform.position.y + 2;
        Instantiate(speedUp, spawnPos, transform.rotation);
    }
    #endregion

    #region background generation
    /// <summary>
    /// Spawn background objects at start of game
    /// </summary>
    void SpawnStartingStuff()
    {
        //generate number of stars
        int noStars = Random.Range(15, 30);
        //spawn random number of stars
        for (int i = 0; i < noStars; i++)
        {
            spawnPos.x = Random.Range(-15, 15);
            spawnPos.y = Random.Range(-8, 8);
            Instantiate(star, spawnPos, transform.rotation);
        }
        //modify spawnpos for planet 
        spawnPos.x = Random.Range(-8, 8);
        spawnPos.y = Random.Range(-2, 4);
        //generate new planet
        GameObject gNewPlanet = (GameObject)Instantiate(planet, spawnPos, transform.rotation);
        gNewPlanet.transform.parent = gameObject.transform;
    }

    /// <summary>
    /// Spawn random number of stars
    /// </summary>
    void SpawnStars()
    {
        int noStars = Random.Range(0, 3);
        for (int i = 0; i < noStars; i++)
        {
            spawnPos.x = player.transform.position.x + 8;
            spawnPos.y = player.transform.position.y + Random.Range(-5, 5);
            Instantiate(star, spawnPos, transform.rotation);
        }
    }
    /// <summary>
    /// Spawn a planet background object
    /// </summary>
    void SpawnPlanets()
    {
        spawnPos.x = player.transform.position.x + 8;
        spawnPos.y = player.transform.position.y + Random.Range(-2, 4);
        GameObject gNewPlanet = (GameObject)Instantiate(planet, spawnPos, transform.rotation);
        gNewPlanet.transform.parent = gameObject.transform;
    }

    #endregion
}
