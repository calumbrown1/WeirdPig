using UnityEngine;
using System.Collections;

public class MoveUpScript : MonoBehaviour {


    public int speed;

	void Update () 
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
	}
}
