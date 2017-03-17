using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    #region Variables

    //float speed to be used for running
    float speed = 5;

    //Force applied to player when they die
    Vector2 dieForce;

    //Position of "explosion" to be used for explosive force
    Vector2 expPos;

    //Player object
    GameObject player;

    //player collider gameobject
    GameObject playerCollider;

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

    //Jumping force
    [SerializeField]
    Vector2 jumpForce;

    //maximum value for jumping time
    [SerializeField]
    float jumpTimerMax;

    //Jumping bool
    bool jumping = false;

    //distance to ground
    float groundDist;

    //Timer for jumping
    float jumpTimer;

    [SerializeField]
    int slamForce;

    #endregion

    void Start()
    {
        //Get Player
        player = GameObject.Find("Player");
        //get player collider object
        playerCollider = player.transform.GetChild(0).gameObject;
        //Set explosion force vector
        dieForce = new Vector2(-300.0f, 300.0f);
        //Set explosion force position vector
        expPos = new Vector2(playerCollider.transform.position.x + (playerCollider.GetComponent<Collider2D>().bounds.extents.x) / 2, playerCollider.transform.position.y - (playerCollider.GetComponent<Collider2D>().bounds.extents.y) / 2);
        //Init player rigidbody
        playerRigidBody = playerCollider.GetComponent<Rigidbody2D>();
        //Find countdown text object
        countdownText = GameObject.Find("LoadingText").GetComponent<Text>();
        //get distance to ground
        groundDist = playerCollider.GetComponentInParent<Collider2D>().bounds.extents.y;
        //init jump timer
        jumpTimer = jumpTimerMax;
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
        if (hp <= 0)
        {
            Die();
        }

        // if player is too far behind camera then increase speed to catch up
        if (player.transform.position.x < transform.position.x - 3)
            player.transform.Translate(Vector2.right * speed / 10 * Time.deltaTime);
        #region Jumping code
        //get gravity scale
        float graveScale = playerRigidBody.gravityScale;
        //if presses spacebar then try to jump
        if(Input.GetKeyDown(KeyCode.Space) && isStarted == true)
        {
            Jump();
            jumping = true;
        }
        //if jump button is held and jumptimer > 0 and gravescale > 0 and jumping then lower gravscale so can jump higher
        if (Input.GetButton("Jump") && jumpTimer > 0 && graveScale > 0 && jumping)
            player.GetComponent<Rigidbody2D>().gravityScale -= 0.05f;
        //lower jumptimer if player not grounded
        if (!IsGrounded())
            jumpTimer -= 0.1f;
        //if jumptimer <= 0 reset gravity scale
        if (jumpTimer <= 0)
            player.GetComponent<Rigidbody2D>().gravityScale = 1;
        //if grounded reset jump timer and gravity scale
        if (IsGrounded())
        {
            jumpTimer = jumpTimerMax;
            player.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        #endregion
        #region slamming code
        //If enter button is pressed check if grounded 
        if (Input.GetKeyDown(KeyCode.Return) && isStarted == true){
            //check if grounded
            if (!IsGrounded())
                //le slam time
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, slamForce));
        }
        #endregion
    }

    #region jumping stuff
    void Jump()
    {
        if (IsGrounded())
            player.GetComponent<Rigidbody2D>().AddForce(jumpForce);
    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(player.transform.position, -Vector2.up, groundDist + 0.1f);
    }
    #endregion

    #region Player Damage, Death and Score Screen
    /// <summary>
    /// Handles collision logic 
    /// Uses string passed from OnTriggerEnter method to implement specific collision logic
    /// </summary>
    /// <param name="collider">colliding objects tag as string</param>
    public void HandleCollisions(string collider)
    {
        Debug.Log("Handle Collision with: "+collider);
        switch (collider)
        {
            //If player hits killzone (falls below platforms)
            case "KillZone":
                //Check hp value, if > zero then reset to checkpoint
                if (hp > 0){
                    DecrementHp();
                    ResetToCheckpoint();
                }
                //if <= 0 then player dies
                else{
                    Die();
                }
                break;
            //If player hits obstacle then decrement hp
            case "Obstacle":
                Debug.Log("Hit Obstacle");
                DecrementHp();
                break;
            //If player hits enemy decrement hp
            case "Enemy":
                Debug.Log("Hit Enemy");
                DecrementHp();
                break;
            //If player hits speedup object increase timescale by time speed increment from GameController
            case "SpeedUp":
                Time.timeScale += GameController.GetSpeedInc();
                Debug.Log("Speedup. New Time: " + Time.timeScale);
                break;
            //check for grounded
            case "Platform":
                IsGrounded();
                break;
            default:
                Debug.Log(string.Format("Unknown Collision, {0}", collider));
                break;
        }

    }

    /// <summary>
    /// Player Die Method
    /// </summary>
    void Die()
    {
        Debug.Log("Player Death");
        isStarted = false;
        //TODO update final multiplier in gameControlScript for use in score
        GameController.SetFinalMultiplier();
        //Update new score property
        PlayerPrefs.SetFloat("newScore", GameController.CalculateScore());
        //Update time variable in ControlScript
        GameController.SetTime(timer);
        //Disable Collider and Fixed angle of player
        playerRigidBody.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<BoxCollider2D>().enabled = false;
        playerRigidBody.constraints = RigidbodyConstraints2D.None;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        //Apply explosive force to ping player away
        playerRigidBody.AddForceAtPosition(dieForce, expPos);
        //Invoke Score Screen after wait time
        Invoke("LoadScoreScreen", scoreScreenDelay);
    }

    /// <summary>
    /// Method to handle transition to score screen
    /// </summary>
    void LoadScoreScreen()
    {
        Application.LoadLevel(2);
    }

    /// <summary>
    /// Method to reset game to last checkpoint
    /// </summary>
    void ResetToCheckpoint()
    {
        Debug.Log("Reset To Checkpoint");
    }

    /// <summary>
    /// Method to decrement hp
    /// </summary>
    public void DecrementHp()
    {
        hp--;
        Debug.Log(hp);
    }

    #endregion

    public float GetSpeed()
    {
        return speed;
    }

    #region Game Starting Code
    /// <summary>
    /// Method invoked to start game
    /// </summary>
    public void StartGame()
    {
        //Get all game objects
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();
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
    #endregion


}
