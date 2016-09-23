using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

    public int health;
    public GUIStyle mystyle;
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            Debug.Log("hit obstacle");
            health--;
        }
        if (other.gameObject.tag == "SpeedUp")
        {
            Time.timeScale += 0.05f;
            other.gameObject.GetComponent<MoveUpScript>().enabled = true;
            other.gameObject.GetComponent<MoveRightScript>().enabled = true;
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    public void DecHealth()
    {
        health--;
    }
    void Update()
    {
        if (health <= 0)
        {
            gameObject.GetComponent<DieScript>().Die();
        }
    }
}
