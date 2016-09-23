using UnityEngine;
using System.Collections;

public class MoveRightScript : MonoBehaviour {

    public float speed;
    public GameObject camera;
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        speed = (camera.GetComponent<RunScript>().speed)/2;
    }
    void Update() // moves gameObject right at half players speed
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}