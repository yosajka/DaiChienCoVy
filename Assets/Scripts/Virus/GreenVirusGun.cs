using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenVirusGun : MonoBehaviour
{
    // The Green enemy will fire the bullet in 8 Direction
    public GameObject enemyBullet;

    public float delayFire = 1;
    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {

    }

    public void Fire()
    {
        float ranTime = Random.Range(3f, 4f);
        Invoke("FireByDirection", ranTime);
    }

    void FireByDirection()
    {
        GameObject playerShip = GameObject.Find("Player");

        if (playerShip != null)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject bullet = (GameObject)Instantiate(enemyBullet);
                bullet.transform.position = transform.GetChild(0).transform.position;
                Vector2 direction = playerShip.transform.position - 
                                    bullet.transform.position;
                bullet.GetComponent<EnemyBullet>().setDirection(direction);
            }
        }

        float ranTime = Random.Range(1f, 2f);
        Invoke("FireByDirection", ranTime);
    }


}


