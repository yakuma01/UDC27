using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    private float timeLeft = 10.0f;
    private bool isGameOver = false;
    public TextMeshProUGUI startText; // used for showing countdown from 3, 2, 1 
    

    private void Update()
    {
        startText.text = (timeLeft).ToString("0");
        if (timeLeft <= 0)
        {
            if (!isGameOver)
            {
                Debug.Log("Time is over");
                isGameOver = true;
                GameOver();
            }
        }
        else
        {
            timeLeft -= Time.deltaTime;
        }
    }

    private void GameOver()
    {
        PanelManager.Instance.HidePanel("TimerPanel");
        PanelManager.Instance.ShowPanel("LevelTransitionPanel", PanelShowBehaviour.KEEP_PREVIOUS);
    }
}