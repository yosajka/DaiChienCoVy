using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    public GameObject explosion;
    GameObject scoreText;
    public float speed;
    int health;
    // Start is called before the first frame update
    void Start()
    {
        health = 2;
        scoreText = GameObject.FindGameObjectWithTag("ScoreTag");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);
        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        if (min.y > transform.position.y) {
            Destroy(gameObject);
        }
    }

    IEnumerator  OnTriggerEnter2D(Collider2D col) {
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag")) {
            health--;
           
            if (health == 0) {
                scoreText.GetComponent<GameScore>().Score += 200;
                PlayExplosion();
                Destroy(gameObject);
            }

            gameObject.GetComponent<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(.2f);
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            
        }
    }

    void PlayExplosion() {
        GameObject anExplosion = (GameObject)Instantiate(explosion);
        anExplosion.transform.position = transform.position;
    }
}
