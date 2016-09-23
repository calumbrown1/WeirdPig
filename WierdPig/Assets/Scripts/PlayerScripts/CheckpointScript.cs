using UnityEngine;
using System.Collections;

public class CheckpointScript : MonoBehaviour {

    GameObject player;
    public int checkpointFrequency;
    RaycastHit2D downRay;
    public GameObject checkpointPlatform;
	void Start ()
    {
        player = GameObject.Find("Player");
        InvokeRepeating("Checkpoint", checkpointFrequency, checkpointFrequency);
	}
    void Checkpoint()
    {
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(new Vector2(player.transform.position.x, player.transform.position.y), -Vector2.up);
        for (int i = 0; i<hits.Length;i++)
        {
            if (hits[i].transform.gameObject.tag == "Platform")
            {
                Debug.Log(hits[i].transform.position.x);
                checkpointPlatform = hits[i].transform.gameObject;
            }
        }
    }
}
