using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreScreenScripts : MonoBehaviour {

    public GUIText timeTex;
    public GUIText highScoreTex;
    public GUIText scoreText;
    public GUIText enemiesKilledTex;
    public GUIText coinsText;
    public GUIText multiText;
    public GUIText totalScoreText;
    public GUIText diffMulti;
    float time;
    float enemiesKilled;
    float score;
    float highScore;
    int iCoins;
    float multiplier;
    public List<float> scores;

    /*
	void Start () 
    {
        SortHighScores();
        for (int i = 0; i > 2; i++ )
        {
            if (scores[i] == (PlayerPrefs.GetFloat("newScore")))
            {
                highScoreTex.text = "NEW HIGHSCORE!";
            }
        }
        time = GameObject.Find("GameControlObj").GetComponent<GameControlScript>().time;
        highScore = PlayerPrefs.GetFloat("highScore");
        enemiesKilled = GameObject.Find("GameControlObj").GetComponent<GameControlScript>().enemiesKilled;
        multiplier = GameObject.Find("GameControlObj").GetComponent<GameControlScript>().fFinalMulti;
        float score = iCoins + (enemiesKilled * 10);
        timeTex.text = "Time : " + Mathf.Round(time) + " seconds";
        scoreText.text = "Score : " + Mathf.Round(score);
        enemiesKilledTex.text = "Enemies Killed : "+enemiesKilled.ToString();
        multiText.text = "Max Speed Multiplier : " + multiplier;
        float fDifficultyMultiplier = GameObject.Find("GameControlObj").GetComponent<GameControlScript>().fSpeed;
        diffMulti.text = "Difficulty Multiplier : " + fDifficultyMultiplier+"x";
        float totalScore = score * multiplier*fDifficultyMultiplier;
        totalScoreText.text = "Total Score : " + Mathf.Round(totalScore);
        if (score > highScore) PlayerPrefs.SetFloat("highscore", score);
	}
    */


    void SortHighScores()
    {
        if (PlayerPrefs.GetFloat("first") == null) PlayerPrefs.SetFloat("first", 0.0f);
        if (PlayerPrefs.GetFloat("second") == null) PlayerPrefs.SetFloat("second", 0.0f);
        if (PlayerPrefs.GetFloat("third") == null) PlayerPrefs.SetFloat("third", 0.0f);
        scores.Add(PlayerPrefs.GetFloat("first"));
        scores.Add(PlayerPrefs.GetFloat("second"));
        scores.Add(PlayerPrefs.GetFloat("third"));
        scores.Add(PlayerPrefs.GetFloat("newScore"));
        scores.Sort();
        PlayerPrefs.SetFloat("first", scores[3]);
        PlayerPrefs.SetFloat("second", scores[2]);
        PlayerPrefs.SetFloat("third", scores[1]);
    }
}
