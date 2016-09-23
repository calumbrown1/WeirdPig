using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public int speed;
    public int bounty;
    public GameObject scoreObj;
    Vector2 dieForce;
    GameObject player;
    Vector2 expPos;
    void Start()
    {
        dieForce = new Vector2(300.0f, 300.0f);
        player = GameObject.Find("Player");
        expPos = new Vector2(player.transform.position.x + (player.GetComponent<Collider2D>().bounds.extents.x) / 2, player.transform.position.y - (player.GetComponent<Collider2D>().bounds.extents.y) / 2);
        if (transform.position.x < player.transform.position.x - 20)
        {
            Debug.Log("dead enemy");
            Destroy(gameObject);
        }
    } 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") // if hits player do end game stuff
        {
            //Handheld.Vibrate();
            Vector2 playerPos = player.transform.position;
            if(playerPos.y >= (transform.position.y + 0.25)) //IF PLAYER LANDS ON ENEMY FROM ABOVE
            {
                Ping();
            }
            else //IF PLAYER COLLIDES WITH ENEMY ON LEVEL OR BELOW THEN HURT IT 
            {
                Debug.Log("hurt by enemy, player pos y : " + playerPos.y.ToString() + " | enemy pos y :" + transform.position.y.ToString());
                GameObject.Find("Player").GetComponent<HealthScript>().DecHealth();
                Ping();
            }
            
        }
        if(other.gameObject.tag == "Proj") // if hits projectile spawn +10 thing, inc score and die
        {
            Instantiate(scoreObj, transform.position, transform.rotation);
            GameObject.Find("Main Camera").GetComponent<InGameScoreScript>().IncEnemiesKilled();
            Destroy(other.gameObject);
            Ping();
        }
    }
    void Ping()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Rigidbody2D>().fixedAngle = false;
        gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(dieForce, expPos);
    }
}
