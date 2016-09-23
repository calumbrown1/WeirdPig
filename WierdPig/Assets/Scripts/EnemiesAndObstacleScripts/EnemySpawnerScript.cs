using UnityEngine;
using System.Collections;

public class EnemySpawnerScript : MonoBehaviour {

    public GameObject enemy;

    void Start() // this method spawns enemies on top of the platform the script is attatched too
    {
        int enemies = Random.Range(0, 2); // generates random number of enemies
        for (int i = 0; i < enemies; i++) // loops for random int of enemies
        {
            Vector2 spawnPos;// initialises the spawn vector
            spawnPos.x = transform.position.x + (Random.Range(0, GetComponent<Collider2D>().bounds.extents.x))/2 ; // sets x co ord of spawnpos to random value between the extremes of the platform
            spawnPos.y = transform.position.y + 0.5f; // sets y value to just above platform
            Instantiate(enemy, spawnPos, transform.rotation); // creates enemy at spawnPos
        }
    }
}
