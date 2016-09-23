using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class HighScoreScreenScript : MonoBehaviour {

    public GUIText firstText;
    public GUIText secondText;
    public GUIText thirdText;
    public List<float> scores;
	void Start () {
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
        firstText.text = "1: "+scores[3].ToString();
        secondText.text = "2: "+scores[2].ToString();
        thirdText.text = "3: "+scores[1].ToString();
	}
	
}
