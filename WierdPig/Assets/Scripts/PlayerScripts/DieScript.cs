using UnityEngine;
using System.Collections;

public class DieScript : MonoBehaviour {

    Vector2 dieForce;
    Vector2 expPos;
    GameObject player;
    GameObject gameControlObj;
    GameObject camera;


    void Start()
    {
        camera = GameObject.Find("Main Camera");
        player = GameObject.Find("Player");
        gameControlObj = GameObject.Find("GameControlObj");
        dieForce = new Vector2(-300.0f,300.0f);
        expPos = new Vector2(player.transform.position.x + (player.GetComponent<Collider2D>().bounds.extents.x)/2, player.transform.position.y-(player.GetComponent<Collider2D>().bounds.extents.y)/2);
    }
    void OnTriggerEnter2D(Collider2D other) // if hits an object tagged killzone then end game
    {

        if (other.gameObject.tag == "Killzone")
        {
            Debug.Log("hit collider");
            Debug.Log(player.GetComponent<HealthScript>().health);
            if (player.GetComponent<HealthScript>().health > 0)
            {
                Invoke("LoadCheckpoint", 3.0f);
            }
            else
            {
                Die();
            }
        }
    }
    public void Die()
    {
        GameObject.Find("GameControlObj").GetComponent<GameControlScript>().enemiesKilled = GameObject.Find("Main Camera").GetComponent<InGameScoreScript>().enemiesKilled;
        float finalMultiplier = Time.timeScale;
        gameControlObj.GetComponent<GameControlScript>().SetFinalMulti(finalMultiplier);
        Time.timeScale = 1.0f;
        //GameObject.Find("Main Camera").GetComponent<RunScript>().speed = 0;
        //GameObject.Find("JumpButton").GetComponent<JumpScript>().jumpForce.y = 0;
        PlayerPrefs.SetFloat("newScore", GameObject.Find("Main Camera").GetComponent<InGameScoreScript>().score);
        gameControlObj.GetComponent<GameControlScript>().time = GameObject.Find("Main Camera").GetComponent<InGameScoreScript>().time;
        gameControlObj.GetComponent<GameControlScript>().enemiesKilled = GameObject.Find("Main Camera").GetComponent<InGameScoreScript>().enemiesKilled;
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<Rigidbody2D>().fixedAngle = false;
        player.GetComponent<Rigidbody2D>().AddForceAtPosition(dieForce, expPos);
        Invoke("LoadScoreScene", 2.0f);
    }

    void LoadScoreScene()
    {
        Application.LoadLevel("ScoreScene");
    }

    void LoadCheckpoint()
    {
        GameObject checkpoint = player.GetComponent<CheckpointScript>().checkpointPlatform;
        Debug.Log("load checkpoint");
        player.GetComponent<Rigidbody2D>().velocity.Equals(0);
        camera.transform.position = new Vector3(checkpoint.transform.position.x, checkpoint.transform.position.y,transform.position.z);
        player.transform.position = new Vector2(checkpoint.transform.position.x, checkpoint.transform.position.y);
    }
}
