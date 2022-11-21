using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class BossFight : MonoBehaviour
{
    public GameObject HealthBar;
    public GameObject BigVirus;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject gameOver;
    public GameObject tryAgainButton;
    public GameObject exitToMenuButton;
    public GameObject BgMusicToggle;
    public GameObject SoundEffectToggle;
    public GameObject explosion;
    public GameObject Victory;
    public AudioSource BackgroundMusic;
    public AudioSource MenuSoundEffect;

    public Text ScoreText;

    public Text InstructionText;

    bool GameIsPaused;

    public enum BossFightPhase
    {
        WaitingToStart,
        Phase_1,
        Phase_2,
        Phase_3,
        Gameover,
        Victory
    }

    public BossFightPhase Phase;

    // Start is called before the first frame update
    void Start()
    {
        ApplyInGameSetting();
        BigVirus.GetComponent<Transform>().position = new Vector2(0f, 18.1f);
        Phase = BossFightPhase.WaitingToStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Phase != BossFightPhase.Gameover && Phase != BossFightPhase.Victory
            && Phase != BossFightPhase.WaitingToStart) {
            GameIsPaused = !GameIsPaused;
            PauseGame();
        }
    }

    void UpdateBossFightPhase()
    {
        switch(Phase) 
        {
            case BossFightPhase.WaitingToStart:

                // UI 
                gameOver.SetActive(false);
                tryAgainButton.SetActive(false);
                exitToMenuButton.SetActive(false);
                InstructionText.gameObject.SetActive(true);
                ScoreText.gameObject.SetActive(false);

                
                // Game object
                playerShip.SetActive(true);
                playerShip.GetComponent<Transform>().position = new Vector2(0,-3);
                playerShip.GetComponent<Renderer>().material.color = Color.white;

                BackgroundMusic.Play();

                break;
            case BossFightPhase.Phase_1:
                
                ScoreText.GetComponent<GameScore>().Score = 0;

                InstructionText.gameObject.SetActive(false);

                ScoreText.gameObject.SetActive(true);

                // set the boss active and init its hp
                BigVirus.GetComponent<BossControl>().Init();
                HealthBar.SetActive(true);
                // set player ship active and init player lives
                playerShip.GetComponent<PlayerControl>().Init();

                break;

            case BossFightPhase.Phase_2:
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                break;

            case BossFightPhase.Phase_3:
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                break;

            case BossFightPhase.Gameover:

                // Stop spawner
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                // Display Game Over UI
                gameOver.SetActive(true);
                tryAgainButton.SetActive(true);
                exitToMenuButton.SetActive(true);
                Time.timeScale = 0;
                BackgroundMusic.Stop();
                break;

            case BossFightPhase.Victory:
                ScoreText.GetComponent<GameScore>().Score += 10000;
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                Victory.SetActive(true);
                exitToMenuButton.gameObject.SetActive(true);
                playerShip.SetActive(false);
                BackgroundMusic.Stop();
                break;
        }
    }

    void PauseGame() {
        
        if (GameIsPaused) {
            Time.timeScale = 0;
            playerShip.GetComponent<PlayerShooting>().isPause = false;
            
            BgMusicToggle.gameObject.SetActive(GameIsPaused);
            SoundEffectToggle.gameObject.SetActive(GameIsPaused);
            exitToMenuButton.gameObject.SetActive(GameIsPaused);
        }
        else {
            Time.timeScale = 1;
            playerShip.GetComponent<PlayerShooting>().isPause = true;
            
            BgMusicToggle.gameObject.SetActive(GameIsPaused);
            SoundEffectToggle.gameObject.SetActive(GameIsPaused);
            exitToMenuButton.gameObject.SetActive(GameIsPaused);
        }
    }

    public void SetBossFightPhase(BossFightPhase phase) 
    {
        Phase = phase;
        UpdateBossFightPhase();
    } 

    void ChangeToWaitingToStart() {
        SetBossFightPhase(BossFightPhase.WaitingToStart);
    }

    public void ChangeToPhaseOne() 
    {
        SetBossFightPhase(BossFightPhase.Phase_1);
    }

    public void ChangeToPhaseTwo() 
    {
        SetBossFightPhase(BossFightPhase.Phase_2);
    }

    public void ChangeToPhaseThree() 
    {
        SetBossFightPhase(BossFightPhase.Phase_3);
    }

    public void ChangeToGameover() 
    {
        SetBossFightPhase(BossFightPhase.Gameover);
    }

    public void ChangeToVictory() 
    {
        SetBossFightPhase(BossFightPhase.Victory);
    }


    public void ClickTryAgainButton() {
        Time.timeScale = 1;
        BigVirus.GetComponent<Transform>().position = new Vector2(0f, 18.1f);
        BigVirus.GetComponent<BossControl>().spriteRenderer.sprite = BigVirus.GetComponent<BossControl>().NormalFace;
        destroyAllEnemies();
        ChangeToWaitingToStart();
    }

    void destroyAllEnemies() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyShipTag");
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("AsteroidTag");
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBulletTag");
        GameObject[] yelloBullets = GameObject.FindGameObjectsWithTag("YellowBulletTag");
        for (int i = 0; i < enemies.Length;i++) {
            Destroy(enemies[i]);
        }
        for (int i = 0; i < asteroids.Length;i++) {
            Destroy(asteroids[i]);
        }

        for (int i = 0; i < bullets.Length;i++) {
            Destroy(bullets[i]);
        }
        for (int i = 0; i < yelloBullets.Length;i++) {
            Destroy(yelloBullets[i]);
        }
    }

    void ApplyInGameSetting() {
        BgMusicToggle.GetComponent<Toggle>().isOn = InGameSetting.BackgroundMusic;
        SoundEffectToggle.GetComponent<Toggle>().isOn = InGameSetting.SoundEffect;

        //MenuSoundEffect.mute = InGameSetting.SoundEffect;
        //BackgroundMusic.mute = !InGameSetting.BackgroundMusic;;
    }

    public void ClickExitToMenuButton() {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void BgMusicToggleOn() 
    {
        InGameSetting.BackgroundMusic = BgMusicToggle.GetComponent<Toggle>().isOn;
        BackgroundMusic.mute = !InGameSetting.BackgroundMusic;;
        // if (BgMusicToggle.GetComponent<Toggle>().isOn) 
        // {
        //    BackgroundMusic.UnPause();
        // }
            
        // else 
        // {
        //     BackgroundMusic.Pause();
        // }
            
    }

    public void SoundEffectToggleOn() 
    {
        InGameSetting.SoundEffect = SoundEffectToggle.GetComponent<Toggle>().isOn;
        if (SoundEffectToggle.GetComponent<Toggle>().isOn) 
        {
            playerShip.GetComponent<AudioSource>().mute = false;
            explosion.GetComponent<AudioSource>().mute = false;
            gameOver.GetComponent<AudioSource>().mute = false;
            Victory.GetComponent<AudioSource>().mute = false;
            MenuSoundEffect.mute = false;
        }
        else 
        {
            playerShip.GetComponent<AudioSource>().mute = true;
            explosion.GetComponent<AudioSource>().mute = true;
            gameOver.GetComponent<AudioSource>().mute = true;
            Victory.GetComponent<AudioSource>().mute = true;
            MenuSoundEffect.mute = true;
        }
            
    }
}
