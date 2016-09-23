using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

    public int speed;
    public float lifeTime;

	void Update () {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) Destroy(gameObject);
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
            Destroy(gameObject);
        }
    }
}
