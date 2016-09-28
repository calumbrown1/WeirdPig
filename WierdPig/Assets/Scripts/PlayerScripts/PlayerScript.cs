using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    [SerializeField]
    //float speed to be used for running
    float speed;
    //Force applied to player when they die
    Vector2 dieForce;
    //Position of "explosion" to be used for explosive force
    Vector2 expPos;
    [SerializeField]
    //Player object
    GameObject player;
    //Countdown Text object
    Text countdownText;
    //Countdown integer
    int countdown = 3;
    //Player health
    int hp = 3;
    //Rigidbody 2D of player
    Rigidbody2D playerRigidBody;
    //Time delay until score screen launched when player dies
    float scoreScreenDelay = 2.0f;
    //Game Timer
    float timer = 0;
    //Game is started boolean
    bool isStarted = false;
    void Start()
    {
        //Set explosion force vector
        dieForce = new Vector2(-300.0f, 300.0f);
        //Set explosion force position vector
        expPos = new Vector2(player.transform.position.x + (player.GetComponent<Collider2D>().bounds.extents.x) / 2, player.transform.position.y - (player.GetComponent<Collider2D>().bounds.extents.y) / 2);
        //Init player rigidbody
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        //Find countdown text object
        countdownText = GameObject.Find("LoadingText").GetComponent<Text>();
        //Call start game function
        StartGame();
    }

    void Update ()
    { 
        if(isStarted)
        //Move player to right at run speed using modified timescale
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        //Update Timer
        timer += Time.deltaTime;
        //Check health
        if (hp <=0)
            Die();
        // if player is too far behind camera then increase speed to catch up
        if (transform.position.x < gameObject.transform.position.x - 3)
        {
            transform.Translate(Vector2.right * speed / 10 * Time.deltaTime);
        }
    }

    /// <summary>
    /// Handles collision logic 
    /// Uses string passed from OnTriggerEnter method to implement specific collision logic
    /// </summary>
    /// <param name="collider">colliding objects tag as string</param>
    public void HandleCollisions(string collider)
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
        PlayerPrefs.SetFloat("newScore", GameController.CalculateScore());
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

    void ResetToCheckpoint()
    {

    }

    public void DecrementHp()
    {
        hp--;
    }

    public float GetSpeed()
    {
        return speed;
    }

    /// <summary>
    /// Method invoked to start game
    /// </summary>
    public void StartGame()
    {
        //Get all game objects
        GameObject[] allGameObjects = Object.FindObjectsOfType<GameObject>();
        //Set countdown text to 3
        countdownText.text = countdown.ToString();
        //Enable all game objects
        foreach (GameObject go in allGameObjects)
            go.SetActive(true);
        Invoke("Countdown", 1.0f);
    }
    /// <summary>
    /// Count down from 3 at start of game and at each checkpoint
    /// </summary>
    /// <param name="countdown">integer to be displayed</param>
    /// <param name="countdownText">GUI GameObject for text to be mapped</param>
    void Countdown()
    {
        //Decrement countdown 
        countdown -= 1;
        //Update countdown text with countdown int to string
        countdownText.GetComponent<Text>().text = countdown.ToString();
        //if countdown > 1 
        if (countdown >= 1)
            //Keep counting down
            Invoke("Countdown", 1.0f);
        else
        {
            //else set text to GO! and Delete text object after 2 seconds
            countdownText.GetComponent<Text>().text = "GO!";
            isStarted = true;
            Invoke("DeleteText", 2.0f);
         }
    }
    /// <summary>
    /// Erase contents of countdown text
    /// </summary>
    void DeleteText()
    {
        countdownText.GetComponent<Text>().text = "";
    }
}
