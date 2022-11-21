using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbControl : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> bulletPrefabs;
    public string bulletName;
    public float speed;
    // Start is called before the first frame update
    public enum OrbType{
        BlueBullet=0,
        RedBullet,
        Shield,
        Heal,
        Speed
    }
    public OrbType orbType;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);
        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if (min.y > transform.position.y)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "PlayerShipTag"))
        {
            Debug.Log(gameObject.tag);
            Destroy(gameObject);
        }
    }
}
