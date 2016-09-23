using UnityEngine;
using System.Collections;

public class ShootingScript : MonoBehaviour {

    public GameObject proj;
    public float shootingCooldown;
    float actualShootingCooldown;

	void Update () {
	    if (Input.GetButtonDown("Enter") && (actualShootingCooldown == shootingCooldown))
        {
            // if right mouse button clock and shooting is off cooldown
            Instantiate(proj, transform.position, transform.rotation);
            actualShootingCooldown--;
        }
        if (Input.touches.Length > 0)
        {
            for (int i = 0; i < Input.touches.Length; i++)
            {
                if (Input.touches[i].position.x > 1260 / 2)
                {
                    if (actualShootingCooldown == shootingCooldown)
                    {
                        Instantiate(proj, transform.position, transform.rotation);
                        actualShootingCooldown--;
                    }
                }
            }
        }

        // if ASC != SC dec ASC
        // if ASC <=0 ASC = SC
        if (actualShootingCooldown < shootingCooldown) actualShootingCooldown -= Time.deltaTime;
        if (actualShootingCooldown <= 0) actualShootingCooldown = shootingCooldown;

	}
}
