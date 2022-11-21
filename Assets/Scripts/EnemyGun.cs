using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject enemyBullet;
    public Transform firePoint;
    public float bulletForce = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.transform.parent.gameObject.name == "Boss") {
            if (gameObject.name == "MouthGun")
                Invoke("FireEnemyBullet", 1f);
            Invoke("FireEnemyYelloBullet", 8f);
        }
        else {
            Invoke("FireEnemyBullet", 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FireEnemyBullet() {
        GameObject playerShip = GameObject.Find("Player");
        
        if (playerShip != null) {
            GameObject bullet = (GameObject)Instantiate(enemyBullet);
            bullet.transform.position = transform.position;
            Vector2 direction = playerShip.transform.position - bullet.transform.position;
            bullet.GetComponent<EnemyBullet>().setDirection(direction);
        }

        Invoke("FireEnemyBullet", 1f);
    }

    void FireEnemyYelloBullet() {
        GameObject playerShip = GameObject.Find("Player");

        if (playerShip != null) 
        {
            GameObject bullet = Instantiate(enemyBullet, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }

        Invoke("FireEnemyYelloBullet", 5f);
    }

    
}


