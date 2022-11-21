using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public GameObject gameManager; // reference to gamemanger object

    // BULLET PLAYER CONTROL 
    public GameObject playerBullet;
    public GameObject bulletPosition;
    public GameObject explosion;
    public string playerBulletName;
    private int playerBulletLevel = 1;
    private int bulletMaxlevel = 6;
    public GameObject lvlUpWeapon;
    private PlayerShield shield;
    // SPEED CONTROL
    public float baseSpeed = 4;
    private float speed = 4;
    private float speedBoostTime; // speedBoostTime of player
    private readonly float speedBoostTimeUnit = 4f; // each speedBoostItem will create a shield in 4 second.
    private bool isSpeedBoosting; // true if player is in speedboostingTime
    private float currentSpeedBoostTime;

    public Transform canvas;
    public Image heart;
    

    List<Image> heartList = new List<Image>();

    const int maxLives = 5;
    int startLives = 3;
    int lives;
    
    int gameMode;

    // Rotation
    public Vector2 mousePosition;
    public Rigidbody2D rb;

    private void SetRotation()
    {
        Vector2 lookDir = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    public void Init()
    {
        // set player ship to the center of the screen
        
        lives = startLives;
        // update player's ship's health (heart)
        RenderHeart();
        InitSpeedBoost();
        InitGun();
        shield = gameObject.GetComponent<PlayerShield>();
        shield.ActivateShield = true;
    }

    private void InitGun()
    {
        gameObject
            .GetComponent<PlayerShooting>()
            .SetFireEnable(true);
    }

    private void InitSpeedBoost()
    {
        isSpeedBoosting = false;
        speedBoostTime = 0f;
        currentSpeedBoostTime = 0f;
    }

    void Start()
    {
        gameMode = InGameSetting.GameMode;
        // Init();
    }

    // Update is called once per frame
    void Update()
    {


        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SetRotation();

        float x = Input.GetAxisRaw("Horizontal"); //the value will be -1,0,1 for left, no input, right
        float y = Input.GetAxisRaw("Vertical");  //the value will be -1,0,1 for down, no input, up

        //based on the input, we compute a direction vector, and normalize it to get a unit vector
        Vector2 direction = new Vector2(x, y).normalized;
        if (isSpeedBoosting)
        {
            UpdateSpeedBoosting();
        }

        //Call the function that computes and set player's position
        Move(direction);
    }

    private void UpdateSpeedBoosting()
    {
        currentSpeedBoostTime += Time.deltaTime;

        if (currentSpeedBoostTime > speedBoostTime)
        {
            isSpeedBoosting = false;
            currentSpeedBoostTime = 0f;
            speedBoostTime = 0f;
            speed = baseSpeed;
        }
    }


    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.285f;
        min.x = min.x + 0.285f;

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        // Get the player's current position
        Vector2 pos = transform.position;

        // Calculate the new position
        pos += direction * speed * Time.deltaTime;

        // Make sure the new position is not outside the screen
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        // Update player position
        transform.position = pos;

    }

    IEnumerator OnTriggerEnter2D(Collider2D col) {

        if (col.tag != "PlayerBulletTag" && !shield.ActivateShield && 
            ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag") || 
            (col.tag == "AsteroidTag")  || (col.tag == "BossTag")  || (col.tag == "YellowBulletTag"))) 
        {
            if (lives > 0)
            {
                lives--; 
                Destroy(heartList[lives].gameObject);

                if (lives == 0) {
                    PlayExplosion();
                    // update game manager state to game over
                    if (gameMode == 0) {
                        gameManager.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameover);
                    }
                    else if (gameMode == 2) {
                        gameManager.GetComponent<BossFight>().ChangeToGameover();
                    }
                    
                    
                    // hide player's ship when dead
                    gameObject.SetActive(false);
                }

                gameObject.GetComponent<Renderer>().material.color = Color.red;
                yield return new WaitForSeconds(.2f);
                gameObject.GetComponent<Renderer>().material.color = Color.white;
            }
            
        }
        else if (col.tag == "LevelUpWeaponTag")
        {
            string orbBulletName = col.gameObject.GetComponent<OrbControl>().bulletName;
            if (orbBulletName != playerBulletName)
            {
                ResetBullet(orbBulletName, col.gameObject);
            }
            else
            {
                LevelUpBullet(col.gameObject);
            }
            PlayLevelUpWeapon();
        }
        else if (col.tag == "HealthTag")
        {
            lives++;
            // not implement
            
        }
        else if (col.tag == "SpeedTag")
        {
            if (!isSpeedBoosting)
            {
                isSpeedBoosting = true;
            }
            speed += speedBoostTimeUnit;
            speedBoostTime += speedBoostTimeUnit;

        }
        else if (col.tag == "ShieldTag")
        {
            shield.ActivateShield = true;
        }
        else if (col.tag == "HealTag"){
            
        }
    }



    private void LevelUpBullet(GameObject go)
    {
        if (playerBulletLevel == bulletMaxlevel)
            return;
        playerBulletLevel += 1;
        List<GameObject> bulletPrefabs = go.GetComponent<OrbControl>().bulletPrefabs;
        if (bulletPrefabs[playerBulletLevel - 1] != playerBullet)
        {
            Debug.Log("differr");
        }
        playerBullet = bulletPrefabs[playerBulletLevel - 1];
    }

    private void ResetBullet(string orbBulletName, GameObject go)
    {
        List<GameObject> bulletPrefabs = go.GetComponent<OrbControl>().bulletPrefabs;
        bulletMaxlevel = bulletPrefabs.Count;
        playerBulletLevel = 1;
        playerBulletName = orbBulletName;
        playerBullet = bulletPrefabs[playerBulletLevel - 1];
    }

    void PlayExplosion()
    {
        GameObject anExplosion = (GameObject)Instantiate(explosion);
        anExplosion.transform.position = transform.position;
    }

    void PlayLevelUpWeapon()
    {
        GameObject anEffect = (GameObject)Instantiate(lvlUpWeapon);
        anEffect.transform.position = transform.position;
        Destroy(anEffect, 0.5f);
    }


    void RenderHeart() {
        heartList = new List<Image>();
        for (int i = 0; i < lives; i++) {
            Image newHeart = (Image)Instantiate(heart);
            newHeart.transform.SetParent(canvas, false);
            Vector2 postion = newHeart.transform.position;
            postion.x += (40*i); // 3 hearts cannot be displayed in the same position, right?
            newHeart.transform.position = postion; // update heart's position
            heartList.Add(newHeart);
        }
    }

  
}
