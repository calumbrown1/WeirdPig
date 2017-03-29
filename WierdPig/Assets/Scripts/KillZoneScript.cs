using UnityEngine;
using System.Collections;

public class KillZoneScript : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Platform")
        {
            Destroy(other.gameObject);
        }
        
    }
}
