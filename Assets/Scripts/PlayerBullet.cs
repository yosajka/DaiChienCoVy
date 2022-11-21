using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed;
    Vector2 _direction;
    
    
    void Start()
    {
        
    }
    //  public void setDirection(Vector2 direction) {
    //     _direction = direction.normalized;
    // }

    
    void Update()
    {
        // get bullet's current position
        Vector2 position = transform.position;

        // compute bullet's next position
        //position += _direction * speed * Time.deltaTime;

        // update bullet position
        transform.position = position;

        // destroy bullet when it goes out the screen
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
        if ((transform.position.x < min.x) || (transform.position.x > max.x) ||
            (transform.position.y < min.y) || (transform.position.y > max.y)) {
                Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if ((col.tag == "EnemyShipTag") || (col.tag == "AsteroidTag") || (col.tag == "BossTag")) { 
            
            Destroy(gameObject);
        }
    }
}
