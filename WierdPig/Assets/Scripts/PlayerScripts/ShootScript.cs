using UnityEngine;
using System.Collections;

public class ShootScript : MonoBehaviour {

    public GameObject proj;
    public GameObject player;
    public float shootingCooldown;
    float actualShootingCooldown;
    public int slamForce;
    JumpScript jumpscript;

    void Start()
    {
        jumpscript = GameObject.Find("JumpButton").GetComponent<JumpScript>();
    }
	void Update () 
    {
        if (actualShootingCooldown == shootingCooldown)
        {
            if (Input.GetButtonDown("Enter"))
            {
                // if right mouse button clock and shooting is off cooldown
                /*
                Instantiate(proj, player.transform.position, player.transform.rotation);
                actualShootingCooldown--;
                */
                bool isGrounded = jumpscript.IsGrounded();
                if(isGrounded == false)
                {
                    player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, slamForce));
                }
            }
        }
        // if ASC != SC dec ASC
        // if ASC <=0 ASC = SC
        if (actualShootingCooldown < shootingCooldown) actualShootingCooldown -= Time.deltaTime;
        if (actualShootingCooldown <= 0) actualShootingCooldown = shootingCooldown;

	}
    void OnMouseDown()
    {
        if (actualShootingCooldown == shootingCooldown)
        {
            Instantiate(proj, player.transform.position, player.transform.rotation);
            actualShootingCooldown--;
        }
    }
}

