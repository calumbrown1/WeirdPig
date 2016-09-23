using UnityEngine;
using System.Collections;

public class CatchupScript : MonoBehaviour {

    GameObject camera;


	void Start () 
    {
        camera = GameObject.Find("Main Camera");
	}
	

	void Update () // if player x co ord < the camera - 3 then increase speed to catch up
    {
	    if (transform.position.x < camera.transform.position.x-3)
        {
            transform.Translate(Vector2.right * camera.GetComponent<RunScript>().speed/10 * Time.deltaTime);
        }
	}
}
