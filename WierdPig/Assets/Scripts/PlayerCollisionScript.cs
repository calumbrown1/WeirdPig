using UnityEngine;
using System.Collections;

/// <summary>
/// Collision detector script for player
/// Sends collision info to PlayerScript
/// </summary>
public class PlayerCollisionScript : MonoBehaviour {

    //Reference to playerscript attatched to player gameobject 
    PlayerScript playerScript;

    //Get playerscript from parent
    void Start()
    {
        playerScript = transform.parent.parent.gameObject.GetComponent<PlayerScript>();
    }

    /// <summary>
    /// When the player collides with an object 
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered: " + other.gameObject.name);
        //Call collision handelling method since more functionality is availiable outside this method
        playerScript.HandleCollisions(other.gameObject.tag.ToString());
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collided: " + other.gameObject.name);
        //Call collision handelling method in playerScript
        playerScript.HandleCollisions(other.gameObject.tag.ToString());
    }
}
