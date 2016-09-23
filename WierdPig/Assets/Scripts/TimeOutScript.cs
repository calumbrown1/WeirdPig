using UnityEngine;
using System.Collections;

public class TimeOutScript : MonoBehaviour {

    public float time;
    float actualTime;

    void Update()
    {
        actualTime += Time.deltaTime;
        if (actualTime == time) Destroy(gameObject);
    }
    

}
