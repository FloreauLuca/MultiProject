using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class Score
{
    public string name;
    public int score;
}

[Serializable]
public class SaveData
{
    public Score[] highScores;
}

public class PingGameManager : MonoBehaviour
{
    [SerializeField] private PingBongScript player;
    [SerializeField] private GameObject ball;
    [SerializeField] private Vector3 ballPosition;

    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject namePanel;
    [SerializeField] private GameObject scorePrefab;
    [SerializeField] private GameObject scorePanel;

    private int score;
    private int myPosition;
    Score[] highscores = new Score[10];
    ScorePrefabScript[] highscoresPrefab = new ScorePrefabScript[10];

    void Start()
    {
        startPanel.SetActive(true);
        endPanel.SetActive(false);
        namePanel.SetActive(false);

        highscores = new Score[0];
        for (int i = 0; i < highscores.Length; i++)
        {
            highscores[i] = new Score();
            highscores[i].name = "---";
            highscores[i].score = 0;
        }
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        endPanel.SetActive(false);
        Instantiate(ball, ballPosition, Quaternion.identity).GetComponent<PingBallScript>().gameManager = this;
        score = 0;
        scoreText.text = "Score : " + score;
    }

    public void AddScore()
    {
        score++;
        scoreText.text = "Score : " + score;
        StartCoroutine(player.Touch());
    }

    public void GameOver()
    {
        startPanel.SetActive(false);
        endPanel.SetActive(true);
        if (PlayerPrefs.HasKey("HighScores"))
        {
            string highscoresJson = PlayerPrefs.GetString("HighScores");
            highscores = JsonUtility.FromJson<SaveData>(highscoresJson).highScores;
            if (highscores == null)
            {
                highscores = new Score[0];
            }
        } if (highscores.Length != 10)
        {
            highscores = new Score[10];
            for (int i = 0; i < highscores.Length; i++)
            {
                highscores[i] = new Score();
                highscores[i].name = "---";
                highscores[i].score = 0;
            }
        }
        Score[] newhighScores = (Score[]) highscores.Clone();

        myPosition = -1;
        for (int i = 0; i < 10; i++)
        {
            if (myPosition > -1)
            {
                newhighScores[i] = highscores[i - 1];
            } else if (highscores[i].score <= score)
            {
                myPosition = i;
                Score myScore = new Score();
                myScore.score = score;
                myScore.name = name;
                newhighScores[myPosition] = myScore;
            }
            if (highscoresPrefab[i] == null)
            {
                ScorePrefabScript scoreprefab = Instantiate(scorePrefab, scorePanel.transform).GetComponent<ScorePrefabScript>();
                highscoresPrefab[i] = scoreprefab;
            }
            highscoresPrefab[i].score.text = newhighScores[i].score.ToString();
            highscoresPrefab[i].name.text = newhighScores[i].name;
        }
        highscores = newhighScores;
        if (myPosition > -1)
        {
            namePanel.SetActive(true);
            namePanel.GetComponent<TMP_InputField>().text = PlayerPrefs.GetString("Name");
            SetName(PlayerPrefs.GetString("Name"));
        } else
        {
            namePanel.SetActive(false);
        }
        {
            SaveData saveData = new SaveData();
            saveData.highScores = highscores;
            string highscoresJson = JsonUtility.ToJson(saveData);
            PlayerPrefs.SetString("HighScores", highscoresJson);
            PlayerPrefs.Save();
        }
    }

    public void SetName(string name)
    {
        highscores[myPosition].name = name;
        highscoresPrefab[myPosition].name.text = name;
        PlayerPrefs.SetString("Name", name);
        SaveData saveData = new SaveData();
        saveData.highScores = highscores;
        string highscoresJson = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("HighScores", highscoresJson);
        PlayerPrefs.Save();
    }


}
