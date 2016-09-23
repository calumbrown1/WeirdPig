using UnityEngine;
using System.Collections;

public class MoveLeftScript : MonoBehaviour {


    public float speed;
	
	void Update () // moves gameObject left
    {
        transform.Translate(-Vector2.right * speed * Time.deltaTime);
	}
}
