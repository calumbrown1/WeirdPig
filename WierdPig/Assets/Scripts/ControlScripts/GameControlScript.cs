using UnityEngine;
using System.Collections;

public class GameControlScript : MonoBehaviour {

    public float time;
    public int enemiesKilled;
    public int score;
    public float fSpeed;
    public float fSpeedInc;
    public float fFinalMulti;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void SetFinalMulti(float finalMulti)
    {
        fFinalMulti = finalMulti;
    }
}
