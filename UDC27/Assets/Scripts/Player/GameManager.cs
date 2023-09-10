using Enemy;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Timer _timer;
    private bool IsTimerSet;
    private int currentTime = 0;
    private int levelIndex = 0;
  
    private Dictionary<int, Vector3> initialLocations = new Dictionary<int, Vector3>();
    private static Dictionary<int, int> playerTimes = new Dictionary<int, int>();
    private GameObject playerFrog = null;
    private List<Vector3> initialFrogPos = new List<Vector3>() { new Vector3(0f,0f,-19.9f),new Vector3(83f, 0f, -19.9f), new Vector3(183f, 0f, -19.9f), new Vector3(283f, 0f, -19.9f), new Vector3(400f, 0f, -19.9f), new Vector3(500f,0f, -19.9f)};

    public static int levelTime;
    // Start is called before the first frame update
    void Start()
    {
        playerFrog = GameObject.Find("Frog");
        
        initialLocations.Add(0, new Vector3(0f,0f,-19.9f));
        initialLocations.Add(1, new Vector3(83f, 0f, -19.9f));
        initialLocations.Add(2, new Vector3(83f, 0f, -19.9f));
        initialLocations.Add(3, new Vector3(183f, 0f, -19.9f));
        initialLocations.Add(4, new Vector3(283f, 0f, -19.9f));
        initialLocations.Add(5, new Vector3(300f, 0f, -19.9f));
        initialLocations.Add(6, new Vector3(400f, 0f, -19.9f));
        
    }

    private bool first = true;

    // Update is called once per frame
    void Update()
    {
        var timerE = FindObjectOfType<Timer>();
        if (timerE != null)
        {
            _timer = timerE;
            IsTimerSet = true;
            levelTime = (int)_timer.timeLeft;
        }
        else
        {
            IsTimerSet = false;
        }

        if (FindObjectOfType<QueenBee>() == null && first)
        {
            GameOver();
            
            /*levelCompletes[levelIndex] = levelTime;
            levelIndex++;*/
        }
    }
    private void GameOver()
    {
        playerTimes.Add(levelIndex, levelTime);
        
        PanelManager.Instance.HidePanel("TimerPanel");
        PanelManager.Instance.ShowPanel("LevelTransitionPanel", PanelShowBehaviour.KEEP_PREVIOUS);
        
        GoToNextMap();
        first = false;
    }

    public void GoToNextMap()
    {
        levelIndex++;
        playerFrog.transform.position = initialLocations[levelIndex];
    }

}
