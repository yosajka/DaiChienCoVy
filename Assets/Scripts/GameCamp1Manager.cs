using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameCamp1Manager : MonoBehaviour
{
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject orbSpawner;
    public GameObject gameOver;
    public GameObject tryAgainButton;
    public GameObject exitToMenuButton;
    public GameObject BgMusicToggle;
    public GameObject SoundEffectToggle;
    public AudioSource BackgroundMusic;
    public AudioSource MenuSoundEffect;
    public GameObject explosion;
    public GameObject Victory;
    public Text InstructionText;
    public Text ScoreText;

    CampaignControl campaignController;

    bool GameIsPaused;

    private OrbSpawner[] orbSpawners;
    public enum GameManagerState
    {
        Opening,
        Gameplay,
        Gameover,
        Victory
    }

    GameManagerState GMState;
    // Start is called before the first frame update
    void Start()
    {
        ApplyInGameSetting();
        GMState = GameManagerState.Opening;
        campaignController = gameObject.GetComponent<CampaignControl>();
        Invoke("ChangeToGameplay", 4f);
    }
    private void ApplyInGameSetting()
    {
        BgMusicToggle.GetComponent<Toggle>().isOn = InGameSetting.BackgroundMusic;
        SoundEffectToggle.GetComponent<Toggle>().isOn = InGameSetting.SoundEffect;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GMState != GameManagerState.Gameover
        && GMState != GameManagerState.Victory && GMState == GameManagerState.Gameplay)
        {
            GameIsPaused = !GameIsPaused;
            PauseGame();

        }
    }

    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:

                InstructionText.gameObject.SetActive(true);

                gameOver.SetActive(false);
                tryAgainButton.SetActive(false);
                exitToMenuButton.SetActive(false);
                ScoreText.gameObject.SetActive(false);

                Invoke("ChangeToGameplay", 4f);

                playerShip.SetActive(true);
                playerShip.GetComponent<PlayerShooting>().SetFireEnable(false);

                playerShip.GetComponent<Transform>().position = new Vector2(0, -3);
                playerShip.GetComponent<Renderer>().material.color = Color.white;

                BackgroundMusic.Play();

                break;
            case GameManagerState.Gameplay:

                ScoreText.GetComponent<GameScore>().Score = 0;

                InstructionText.gameObject.SetActive(false);

                ScoreText.gameObject.SetActive(true);

                // set player ship active and init player lives
                playerShip.GetComponent<PlayerControl>().Init();

                campaignController.StartCampaign1();

                // Start orbSpawner
                break;
            case GameManagerState.Gameover:


                // Stop spawner
                // Stop orbsSpawner
                // Display Game Over UI
                gameOver.SetActive(true);
                tryAgainButton.SetActive(true);
                exitToMenuButton.SetActive(true);

                BackgroundMusic.Stop();
                // Set game manager state to Opening after 8 seconds
                //Invoke("ChangeToOpening", 8f);
                break;
            case GameManagerState.Victory:
                ScoreText.GetComponent<GameScore>().Score += 10000;
                Victory.SetActive(true);
                exitToMenuButton.gameObject.SetActive(true);
                playerShip.SetActive(false);
                BackgroundMusic.Stop();
                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    // Function to change Game manager state to opening
    void ChangeToOpening()
    {
        SetGameManagerState(GameManagerState.Opening);
    }

    void ChangeToGameplay()
    {
        SetGameManagerState(GameManagerState.Gameplay);
    }

    public void ChangeToVictory(){
        SetGameManagerState(GameManagerState.Victory);
    }

    void PauseGame()
    {

        if (GameIsPaused)
        {
            Time.timeScale = 0;
            playerShip.GetComponent<PlayerShooting>().isPause = false;

            BgMusicToggle.gameObject.SetActive(GameIsPaused);
            SoundEffectToggle.gameObject.SetActive(GameIsPaused);
            exitToMenuButton.gameObject.SetActive(GameIsPaused);
        }
        else
        {
            Time.timeScale = 1;
            playerShip.GetComponent<PlayerShooting>().isPause = true;

            BgMusicToggle.gameObject.SetActive(GameIsPaused);
            SoundEffectToggle.gameObject.SetActive(GameIsPaused);
            exitToMenuButton.gameObject.SetActive(GameIsPaused);
        }
    }

    public void ClickTryAgainButton()
    {
        destroyAllEnemies();
        ChangeToOpening();
    }

    public void ClickExitToMenuButton()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void BgMusicToggleOn()
    {
        InGameSetting.BackgroundMusic = BgMusicToggle.GetComponent<Toggle>().isOn;

        BackgroundMusic.mute = !InGameSetting.BackgroundMusic;

    }

    public void SoundEffectToggleOn()
    {
        InGameSetting.SoundEffect = SoundEffectToggle.GetComponent<Toggle>().isOn;
        if (SoundEffectToggle.GetComponent<Toggle>().isOn)
        {
            playerShip.GetComponent<AudioSource>().mute = false;
            explosion.GetComponent<AudioSource>().mute = false;
            gameOver.GetComponent<AudioSource>().mute = false;
            MenuSoundEffect.mute = false;
        }
        else
        {
            playerShip.GetComponent<AudioSource>().mute = true;
            explosion.GetComponent<AudioSource>().mute = true;
            gameOver.GetComponent<AudioSource>().mute = true;
            MenuSoundEffect.mute = true;
        }

    }

    // Destroy green virus, red virus and enemy bullets on the scene
    void destroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyShipTag");
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("AsteroidTag");
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBulletTag");
        GameObject[] yelloBullets = GameObject.FindGameObjectsWithTag("EnemyBulletTag");
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
        for (int i = 0; i < asteroids.Length; i++)
        {
            Destroy(asteroids[i]);
        }

        for (int i = 0; i < bullets.Length; i++)
        {
            Destroy(bullets[i]);
        }
        for (int i = 0; i < yelloBullets.Length; i++)
        {
            Destroy(yelloBullets[i]);
        }
    }


}
