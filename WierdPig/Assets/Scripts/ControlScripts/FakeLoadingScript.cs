using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FakeLoadingScript : MonoBehaviour {
    public Image image;
    public float increaseAmount;
    public GameObject cam;
    public Sprite backgroundTexture;
    bool gameStart = false;
    void Update()
    {
        image.fillAmount += increaseAmount * Time.deltaTime;
        if (image.fillAmount >= 1f && gameStart == false)
        {
            GameObject.Find("BackGround").GetComponent<SpriteRenderer>().sprite = backgroundTexture;
            cam.GetComponent<StartGameScript>().StartGame();
            gameStart = true;
        }
    }
}
