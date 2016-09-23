using UnityEngine;
using System.Collections;
using System;

public class InGameScoreScript : MonoBehaviour {

    public float score;
    public float time;
    public GUIStyle myStyle;
    public int enemiesKilled;
    GameObject scoreText;
    GameObject speedText;
	void Update () // increases the score by the passing time
    {
        time += Time.deltaTime;
        double bonusSpeed = Time.timeScale - 1;
        bonusSpeed = Math.Round(bonusSpeed, 2);
        bonusSpeed = bonusSpeed * 100;
    }
    void OnGUI() // draws score to top left corner of screen
    {

        
    }
    public void IncScore() // increments the amount of enemies killed
    {
        score++;
    }
    public void IncEnemiesKilled()
    {
        score += 10;
        enemiesKilled++;
    }
    public float GetScore()// returns score value
    {
        return time;
    }
}
