using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    [SerializeField]
    //+10 graphic object created when enemy dies
    GameObject scoreObj;
    //force the enemy pings away at
    Vector2 dieForce = new Vector2(300.0f, 300.0f);
    //position force is applied at
    Vector2 expPos;
    [SerializeField]
    //player object
    GameObject player;
    //Player Script
    PlayerScript playerScript;
    void Start()
    {
        //Get player
        player = GameObject.Find("Player");
        //Get player script
        playerScript = player.transform.parent.GetComponent<PlayerScript>();
        //Calculate expPos
        expPos = new Vector2(player.transform.position.x + (player.GetComponent<Collider2D>().bounds.extents.x) / 2, player.transform.position.y - (player.GetComponent<Collider2D>().bounds.extents.y) / 2);
    } 


    void Update()
    {
        //If enemy falls behind player too far then destroy it
        if (transform.position.x < player.transform.position.x - 20)
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Handle collisions 
    /// </summary>
    /// <param name="other">Collider enemy is colliding with</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            Debug.Log("PING");
            //Handheld.Vibrate();
            //Get position of player
            Vector2 playerPos = player.transform.position;
            //If player is above enemy then kill it
            if(playerPos.y >= (transform.position.y + 0.25)) 
            {
                Ping();
            }
            //if player is below or on the same level as enemy damage player and kill enemy
            else
            {
                //Decrement player hp
                playerScript.DecrementHp();
                //Kill enemy
                Ping();
            }
            
        }
    }
    /// <summary>
    /// Ping enemy away from player
    /// </summary>
    void Ping()
    {
        Debug.Log("PING");
        //Disable enemy collider
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //Disable Fixed camera
        gameObject.GetComponent<Rigidbody2D>().fixedAngle = false;
        //Add force to ping enemy away
        gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(dieForce, expPos);
    }
}
