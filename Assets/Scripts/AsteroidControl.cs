using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidControl : MonoBehaviour
{
    public GameObject explosion;
    public float speed;
    GameObject scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("ScoreTag");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y - speed*Time.deltaTime);
        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        if (min.y > transform.position.y) {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col) {
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag")) {
            scoreText.GetComponent<GameScore>().Score += 100;
            PlayExplosion();
            Destroy(gameObject);
        }
    }

    void PlayExplosion() {
        GameObject anExplosion = (GameObject)Instantiate(explosion);
        anExplosion.transform.position = transform.position;
    }
}
