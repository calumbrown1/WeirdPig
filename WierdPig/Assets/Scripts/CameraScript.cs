using UnityEngine;
using System.Collections;

/// <summary>
/// Camera logic script
/// Adjusts y position of camera if player object is above or below it
/// </summary>
public class CameraScript : MonoBehaviour {

    [SerializeField]
    //Player object
    GameObject player;
    //Speed camera will adjust y value
    float speed;
    void Update()
    {
        //check if the player is above or below camera then adjust y pos accordingly
        if ((transform.position.y) < (player.transform.position.y) - 0.25)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else if (transform.position.y > player.transform.position.y + 0.25)
        {
            transform.Translate(-Vector2.up * speed * Time.deltaTime);
        }
    }
}
