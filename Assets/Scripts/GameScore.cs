using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    Text ScoreTextUi;
    int score;

    public int Score
    {
        get
        {
            return this.score;
        }
        set 
        {
            this.score = value;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        ScoreTextUi = GetComponent<Text>();
        UpdateScoreTextUI();
    }

    void UpdateScoreTextUI() 
    {
        string scoreStr = string.Format("{0:0000000}", score);

        ScoreTextUi.text = scoreStr; 
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScoreTextUI();
    }
}
