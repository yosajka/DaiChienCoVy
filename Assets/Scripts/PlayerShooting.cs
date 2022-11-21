using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class PlayerShooting : MonoBehaviour
{
    private GameObject bulletPrefab;

    [SerializeField]
    public Transform firePoint;

    public float bulletForce = 15f;
    private bool canFire;
    private float delayFireTimer = 0.35f;
    public float fireTimer;
    public bool isPause;
    public bool isEnable;
    [SerializeField] GameObject[] skins;
    // Start is called before the first frame update
    void Start()
    {
        fireTimer = delayFireTimer;
        isPause = true;
        //ChangePlayerSkin();
    }
    void ChangePlayerSkin() {
        //return;
        int index = GameStateManager.Instance.GetSelectedSkinIndex();
        Debug.Log(index);
        skins[index].SetActive(true);
        for (int i =0; i<skins.Length; i++)
            if (i!= index)
                skins[i].SetActive(false);
    }
    
    void Update()
    {
        if (isEnable && isPause)
            Fire();
    }
    public void SetFireEnable(bool state){
        isEnable = state;
    }


    private void Fire()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer > delayFireTimer)
        {
            canFire = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (canFire)
            {

                canFire = false;
                fireTimer = 0f;
                gameObject.GetComponent<AudioSource>().Play();

                bulletPrefab = gameObject.GetComponent<PlayerControl>().playerBullet;
                GameObject bullets = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                int n = bullets.transform.childCount;

                // int countUp = 0;
                // int countDiagonal = 0;
                int i;

                int countDiagonal = n / 3;
                int countUp = n - countDiagonal * 2;
                for (i = 0; i < countUp; i++)
                {
                    GameObject bullet = bullets
                        .transform.GetChild(i).gameObject;
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
                }
                // Vector2 up = new Vector2(Mathf.Sin(30), 1);
                // Vector2 right = new Vector2(1, Mathf.Cos(30));
                // Vector2 left = new Vector2(1, Mathf.Cos(30));

                for (; i < n; i++)
                {
                    if (i < countUp + countDiagonal)
                    {
                        GameObject bullet = bullets
                            .transform.GetChild(i).gameObject;
                        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                        rb.AddForce(firePoint.up * bulletForce - firePoint.right * bulletForce / 2, ForceMode2D.Impulse);
                    }
                    else
                    {
                        GameObject bullet = bullets
                            .transform.GetChild(i).gameObject;
                        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                        rb.AddForce(firePoint.up * bulletForce + firePoint.right * bulletForce / 2, ForceMode2D.Impulse);
                    }
                }
            }



            // bulletPrefab = gameObject.GetComponent<PlayerControl>().playerBullet;
            // GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            // Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            // rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }
    }
}