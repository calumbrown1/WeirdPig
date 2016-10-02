using UnityEngine;
using System.Collections;

public class MoveDirectionScript : MonoBehaviour {

    //Direction to move gameobject
    enum direction { left, right, up, down};
    [SerializeField]
    //init direction to mvoe
    direction moveDirection;
    [SerializeField]
    //Speed for object to move
    float speed;

    void Update()
    {
        // Use move direction enum and speed to move object in desired direction
        switch (moveDirection)
        {
            case direction.left:
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                break;
            case direction.right:
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                break;
            case direction.up:
                transform.Translate(Vector3.up * speed * Time.deltaTime);
                break;
            case direction.down:
                transform.Translate(Vector3.up * speed * Time.deltaTime);
                break;
        }
    }



}
