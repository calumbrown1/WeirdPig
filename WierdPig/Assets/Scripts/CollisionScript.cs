﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Collision detector script for player
/// Sends collision info to PlayerScript
/// </summary>
public class CollisionScript : MonoBehaviour {

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
        //Call collision handelling method since more functionality is availiable outside this method
        playerScript.HandleCollisions(other.gameObject.tag.ToString());
    }
}
