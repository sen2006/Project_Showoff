using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfo
{
    public string playerName;
    public int playerScore;

    public PlayerInfo(string name, int score)
    {
        playerName = name;
        playerScore = score;
    }
}

public class LeaderboardScores : MonoBehaviour
{
    public TMP_InputField userName;
    public TMP_InputField score;
    public TMP_InputField display;

    List<PlayerInfo> highscores;

    void Start()
    {
        highscores = new List<PlayerInfo>();
        LoadLeaderBoard();
    }

    public void SubmitButton()
    {
        if (userName.text.Length == 0 || score.text.Length == 0) return;

        AddStats(userName.text, int.Parse(score.text));

        userName.text = "";
        score.text = "";
    }

    public void AddStats(string pName, int pScore)
    {
        if (pName.Length == 0 || pScore <= 0) return;

        PlayerInfo stats = new PlayerInfo(pName, pScore);

        highscores.Add(stats);

        SortStats();
    }

    public void ClearScoresButton()
    {
        PlayerPrefs.DeleteKey("LeaderBoards");
        highscores.Clear();
        display.text = string.Empty;
    }

    void SortStats()
    {
        for (int i = highscores.Count - 1; i > 0; i--)
        {
            if (highscores[i].playerScore > highscores[i - 1].playerScore)
            {
                PlayerInfo tempInfo = highscores[i - 1];

                highscores[i - 1] = highscores[i];
                highscores[i] = tempInfo;
            }
        }

        UpdatePlayerPrefString();
    }

    void UpdatePlayerPrefString()
    {
        string stats = "";

        for (int i = 0; i < highscores.Count; i++)
        {
            if (stats.Length > 0)
            {
                stats += ",";
            }
            stats += highscores[i].playerName + ",";
            stats += highscores[i].playerScore;
        }

        PlayerPrefs.SetString("LeaderBoards", stats);

        UpdateLeaderBoardDisplay();
    }

    void UpdateLeaderBoardDisplay()
    {
        display.text = "";

        for (int i = 0; i < highscores.Count; i++)
        {
            display.text += highscores[i].playerName + " : " + highscores[i].playerScore + "\n";
        }
    }

    void LoadLeaderBoard()
    {
        string stats = PlayerPrefs.GetString("LeaderBoards", "");

        string[] stats2 = stats.Split(',');

        if (stats2.Length >= 2)
        {
            for (int i = 0; i < stats2.Length; i += 2)
            {
                PlayerInfo loadedInfo = new PlayerInfo(stats2[i], int.Parse(stats2[i + 1]));

                highscores.Add(loadedInfo);

                UpdateLeaderBoardDisplay();
            }
        }
    }
}
