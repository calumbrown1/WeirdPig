using UnityEngine;
using System.Collections;

public class CameraPosScript : MonoBehaviour {

    public GameObject player;
    public float speed;
	void Update () {
        //check if the player is above the camera 
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
