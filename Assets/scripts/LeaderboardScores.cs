using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

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
        PlayerInfo stats = new PlayerInfo(userName.text, int.Parse(score.text));

        highscores.Add(stats);

        userName.text = "";
        score.text = "";

        SortStats();
    }

    public void ClearScoresButton()
    {
        PlayerPrefs.DeleteKey("LeaderBoards");
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
            stats += highscores[i].playerName + ",";
            stats += highscores[i].playerScore + ",";
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

        for (int i = 0; i < stats2.Length; i += 2)
        {
            PlayerInfo loadedInfo = new PlayerInfo(stats2[i], int.Parse(stats2[i + 1]));

            highscores.Add(loadedInfo);

            UpdateLeaderBoardDisplay();
        }
    }
}
