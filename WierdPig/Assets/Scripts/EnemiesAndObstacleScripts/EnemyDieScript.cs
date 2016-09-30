using UnityEngine;
using System.Collections;

public class EnemyDieScript : MonoBehaviour {

    public bool flying = false;
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameObject parentObj = transform.parent.gameObject;
            parentObj.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            parentObj.GetComponent<Rigidbody2D>().fixedAngle = false;
            parentObj.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(300.0f, 300.0f), new Vector2(transform.position.x,transform.position.y + transform.gameObject.GetComponent<Collider2D>().bounds.extents.y * 2));
            if(flying == true)
            {
                /*
                Commented ous while decide what to do with it 
                TODO REFACTOR THIS
                Vector2 jumpForce = GameObject.Find("Player").GetComponent<PlayerScript>().jumpForce;
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(jumpForce);
                */
            }
        }
    }
}
