using UnityEngine;
using System.Collections;

public class RunScript : MonoBehaviour {

    public float speed;
    public GameObject player;
    float fSpeed;
    Vector3 spawnPos;
    void Start()
    {
        fSpeed = GameObject.Find("GameControlObj").GetComponent<GameControlScript>().fSpeed;
        Time.timeScale = fSpeed;
    }
	void Update () 
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
	}
}
