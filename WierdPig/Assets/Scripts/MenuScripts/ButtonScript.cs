using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

    public string nextScene;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GetComponent<GUITexture>().HitTest(Input.mousePosition))
            {
                Application.LoadLevel(nextScene);
            }
        }
    }
}
