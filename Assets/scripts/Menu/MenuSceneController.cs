using UnityEngine;

public class MenuSceneController : MonoBehaviour
{
    public GameObject titlePanel;
    public GameObject menuPanel;

    void Start()
    {
        if (!GameState.hasSeenTitle)
        {
            titlePanel.SetActive(true);
            menuPanel.SetActive(false);
            GameState.hasSeenTitle = true;
        }
        else
        {
            titlePanel.SetActive(false);
            menuPanel.SetActive(true);
        }
    }
}
