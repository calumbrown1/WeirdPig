using UnityEngine;
using System.Collections;

public class ScenerySpawnerScript : MonoBehaviour {

    public GameObject star;
    public GameObject planet;
    GameObject player;
    Vector2 spawnPos;
	void Start () {
        player = GameObject.Find("Player");
        Invoke("SpawnStartingStuff", 0.0f);
        InvokeRepeating("Spawn", 0.1f, 0.1f);
        Invoke("SpawnPlanets", 0.0f);
        InvokeRepeating("SpawnPlanets", 10.0f, 30.0f);
	}

    void Spawn() // this method spawns a random number of stars at random points on the y axis in front of the player to simulate stars
    {
        int noStars = Random.Range(0, 3);
        for (int i = 0; i < noStars; i++ )
        { 
            spawnPos.x = player.transform.position.x + 8;
            spawnPos.y = player.transform.position.y + Random.Range(-5, 5);
            Instantiate(star, spawnPos, transform.rotation);
        }
    }
	void SpawnStartingStuff() // this method spawns stuff at the start of the game so there is a background
    {
        int noStars = Random.Range(15, 30);
        for (int i = 0; i <noStars; i++)
        {
            spawnPos.x = Random.Range(-15, 15);
            spawnPos.y = Random.Range(-8, 8);
            Instantiate(star, spawnPos, transform.rotation);
        }
        spawnPos.x = Random.Range(-8, 8);
        spawnPos.y = Random.Range(-2, 4);
        GameObject gNewPlanet = (GameObject)Instantiate(planet, spawnPos, transform.rotation);
        gNewPlanet.transform.parent = gameObject.transform;
    }
    void SpawnPlanets()
    {
        spawnPos.x = player.transform.position.x + 8;
        spawnPos.y = player.transform.position.y + Random.Range(-2, 4);
        GameObject gNewPlanet = (GameObject) Instantiate(planet, spawnPos, transform.rotation);
        gNewPlanet.transform.parent = gameObject.transform;
    }
 
}
