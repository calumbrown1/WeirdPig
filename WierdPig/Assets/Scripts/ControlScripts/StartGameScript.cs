using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StartGameScript : MonoBehaviour {
    public GameObject[] gameObjects;
    public Text text;
    GameObject camera;
    public Canvas uiCanvas;
    public GameObject image;
    public int countdown;
    public GameObject player;
    public GameObject stats;
    public GameObject jumpButt;
    public GameObject shootButt;
    GameObject[] allObjects;
    public void StartGame()
    {
        allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        image.SetActive(false);
        text.text = "3";
        player.SetActive(true);
        stats.SetActive(true);
        jumpButt.SetActive(true);
        shootButt.SetActive(true);
        Invoke("CountDown",1.0f);
    }
    void CountDown()
    {
        countdown-=1;
        text.text = countdown.ToString();
        if (countdown >= 1)
        {
            Invoke("CountDown", 1.0f);
        }
        else
        {
            text.text = "GO!";
            Invoke("DeleteText", 2.0f);
        }
    }
    void DeleteText()
    {
        text.text = "";
    }
    void Update()
    {
        if (countdown <= 0) GameStart();
    }

    void GameStart()
    {
        /*
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(true);
        }
        */
        

        foreach(GameObject gobj in allObjects)
        {
            gobj.SetActive(true);
        }
        camera = gameObject;
        //camera.GetComponent<RunScript>().enabled = true;
        camera.GetComponent<TerrainSpawnerScript>().enabled = true;
        camera.GetComponent<CameraScript>().enabled = true;
        camera.GetComponent<ScenerySpawnerScript>().enabled = true;
        GameObject.Find("LoadingBar").SetActive(false);
        uiCanvas.GetComponent<Canvas>().enabled = true;
    }
}
