using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowVirusGun : MonoBehaviour
{
    // The Green enemy will fire the bullet in 8 Direction
    public GameObject enemyBullet;

    public float delayFire = 1;
    private Vector2[] directions;
    private void InitDirectVector()
    {
        directions = new Vector2[8];
        directions[0] = new Vector2(1f, 0f).normalized;     // East
        directions[1] = new Vector2(1f, 1f).normalized;     // NorthEast
        directions[2] = new Vector2(0f, 1f).normalized;     // North
        directions[3] = new Vector2(-1f, 1f).normalized;    // NorthWest
        directions[4] = new Vector2(-1f, 0f).normalized;    // West
        directions[5] = new Vector2(-1f, -1f).normalized;   // SouthWest
        directions[6] = new Vector2(0f, -1f).normalized;    // South
        directions[7] = new Vector2(1f, -1f).normalized;    // SouthEast
    }
    // Start is called before the first frame update
    void Start()
    {
        InitDirectVector();
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
        for (int i = 0; i < 8; i++) //8 direction
        {
            GameObject bullet = (GameObject)Instantiate(enemyBullet);
            Transform firepoint = gameObject.transform.GetChild(i).transform; 
            bullet.transform.position = firepoint.position;
            bullet.GetComponent<EnemyBullet>().setDirection(directions[i]);
        }

        float ranTime = Random.Range(3f, 4f);
        Invoke("FireByDirection", ranTime);
    }


}


