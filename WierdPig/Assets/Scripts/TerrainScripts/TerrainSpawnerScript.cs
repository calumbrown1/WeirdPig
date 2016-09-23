using UnityEngine;
using System.Collections;

public class TerrainSpawnerScript : MonoBehaviour {

    public GameObject[] platforms;
    public GameObject player;
    public float[] lengths;
    public int iMaxPlatLength;
    public GameObject obstacle;
    public GameObject emptyObj;
    public GameObject speedUp;
    public GameObject enemy;
    public GameObject flyingEnemy;
    Vector2 spawnPos;
    Vector2 prevBlockPos;
    float jumpLength;
    GameObject gPrevBlock;
    float[] fYPosArray;
    public GameObject[] gCurPlatform;
    int platformsGenerated = 0;
    void Start()
    {
        fYPosArray = new float[4];
        InvokeRepeating("MakeNewPlatform", 0.0f, 2.0f);
        gPrevBlock = GameObject.Find("RockRight");
        InitialiseYPosArray();
    }

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
    void SpawnSpeedUp(GameObject[] platform)
    {
        Vector2 spawnPos = new Vector2();
        GameObject middleBlock = platform[(int)(platform.Length / 2)];
        spawnPos.x = middleBlock.transform.position.x;
        spawnPos.y = middleBlock.transform.position.y + 2;
        Instantiate(speedUp, spawnPos, transform.rotation);
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
            Instantiate(enemy, spawnPos, transform.rotation);
        }
        else
        {
            //spawn flying enemy
            //y position of enemy = the y position of the platform + five times the height of the enemy
            spawnPos.y = spawnPlatform.transform.position.y + 1;
            Instantiate(flyingEnemy, spawnPos, transform.rotation);
        }
    }
}
