using Enemy;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Timer _timer;
    private bool IsTimerSet;
    private int levelIndex = 0;
    private List<int> levelCompletes = new List<int>();
    private GameObject playerFrog = null;
    private List<Vector3> initialFrogPos = new List<Vector3>() { new Vector3(0f,0f,-19.9f),new Vector3(83f, 0f, -19.9f), new Vector3(183f, 0f, -19.9f), new Vector3(283f, 0f, -19.9f), new Vector3(400f, 0f, -19.9f), new Vector3(500f,0f, -19.9f)};

    public static int levelTime;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var timerE = FindObjectOfType<Timer>();
        if (timerE != null)
        {
            _timer = timerE;
            IsTimerSet = true;
        }
        else
        {
            IsTimerSet = false;
        }

        if (FindObjectOfType<QueenBee>() == null)
        {
            levelTime = (int)_timer.timeLeft;
            GameOver();
            playerFrog = GameObject.FindGameObjectWithTag("Player");
            Debug.Log(initialFrogPos[levelIndex]);
            /*levelCompletes[levelIndex] = levelTime;
            levelIndex++;*/
            Debug.Log("Time took" + levelTime);
        }
    }
    private void GameOver()
    {
        PanelManager.Instance.HidePanel("TimerPanel");
        PanelManager.Instance.ShowPanel("LevelTransitionPanel", PanelShowBehaviour.KEEP_PREVIOUS);
    }

}
