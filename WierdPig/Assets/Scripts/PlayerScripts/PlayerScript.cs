using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    [SerializeField]
    //float speed to be used for running
    float speed;
    //Force applied to player when they die
    Vector2 dieForce;
    //Position of "explosion" to be used for explosive force
    Vector2 expPos;
    //Scene camera TODO - refactor this 
    GameObject camera;
    //Player object
    GameObject player;
    //Player health
    int hp = 3;
    //Rigidbody 2D of player
    Rigidbody2D playerRigidBody;
    //Time delay until score screen launched when player dies
    float scoreScreenDelay = 2.0f;
    //Game Timer
    float timer = 0;
    void Start()
    {
        //Get camera - TODO refactor this
        camera = GameObject.Find("Main Camera");
        //Set explosion force vector
        dieForce = new Vector2(-300.0f, 300.0f);
        //Set explosion force position vector
        expPos = new Vector2(gameObject.transform.position.x + (gameObject.GetComponent<Collider2D>().bounds.extents.x) / 2, gameObject.transform.position.y - (gameObject.GetComponent<Collider2D>().bounds.extents.y) / 2);
        //Init player rigidbody
        playerRigidBody = playerRigidBody.GetComponent<Rigidbody2D>();
    }

    void Update () 
    {
        //Move player to right at run speed using modified timescale
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        //Update Timer
        timer += Time.deltaTime;
        //Check health
        if (hp <=0)
            Die();
        // if player is too far behind camera then increase speed to catch up
        if (transform.position.x < camera.transform.position.x - 3)
        {
            transform.Translate(Vector2.right * speed / 10 * Time.deltaTime);
        }
    }

    /// <summary>
    /// Handles collision logic 
    /// Uses string passed from OnTriggerEnter method to implement specific collision logic
    /// </summary>
    /// <param name="collider">colliding objects tag as string</param>
    void HandleCollisions(string collider)
    {
        switch (collider)
        {
            //If player hits killzone (falls below platforms)
            case "Killzone":
                //Check hp value, if > zero then reset to checkpoint
                if (hp > 0)
                {
                    hp--;
                    ResetToCheckpoint();
                }
                //if <= 0 then player dies
                else
                {
                    Die();
                }
                break;
            //If player hits obstacle then decrement hp
            case "Obstacle":
                hp--;
                break;
            //If player hits enemy decrement hp
            case "Enemy":
                hp--;
                break;
            //If player hits speedup object increase timescale by time speed increment from GameController
            case "SpeedUp":
                Time.timeScale += GameController.GetSpeedInc();
                break;
            default:

                break;
        }

    }

    void Die()
    {
        //TODO update final multiplier in gameControlScript for use in score
        GameController.SetFinalMultiplier();
        //Update new score property
        PlayerPrefs.SetFloat("newScore", camera.GetComponent<InGameScoreScript>().time);
        //Update time variable in ControlScript
        GameController.SetTime(timer);
        //Disable Collider and Fixed angle of player
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        playerRigidBody.fixedAngle = false;
        //Apply explosive force to ping player away
        playerRigidBody.AddForceAtPosition(dieForce, expPos);
        //Invoke Score Screen after wait time
        Invoke("LoadScoreScreen", scoreScreenDelay);
    }

    void LoadScoreScreen()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Call collision handelling method since more functionality is availiable outside this method
        HandleCollisions(other.gameObject.tag.ToString());
    }

    void ResetToCheckpoint()
    {

    }
}
