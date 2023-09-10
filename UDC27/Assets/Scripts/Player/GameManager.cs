using Enemy;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class GameManager :Singleton<GameManager>
{
    private Timer _timer;
    private bool IsTimerSet;
    private int currentTime = 0;
    private int levelIndex = 0;
  
    private Dictionary<int, Vector3> initialLocations = new Dictionary<int, Vector3>();
    private static Dictionary<int, int> playerTimes = new Dictionary<int, int>();
    private GameObject playerFrog = null;

    [SerializeField] private List<GameObject> level0Bees;
    [SerializeField] private List<GameObject> level1Bees;
    [SerializeField] private List<GameObject> level2Bees;
    [SerializeField] private List<GameObject> level3Bees;
    [SerializeField] private List<GameObject> level4Bees;
    [SerializeField] private List<GameObject> level5Bees;

    private static Dictionary<int, List<GameObject>> levelBees = new Dictionary<int, List<GameObject>>();

    
    public static int levelTime;
    // Start is called before the first frame update
    void Start()
    {
        playerFrog = GameObject.Find("Frog");
        
        initialLocations.Add(0, new Vector3(0f,0f,-19.9f));
        initialLocations.Add(1, new Vector3(83f, 0f, -19.9f));
        initialLocations.Add(2, new Vector3(183f, 0f, -19.9f));
        initialLocations.Add(3, new Vector3(283f, 0f, -19.9f));
        initialLocations.Add(4, new Vector3(300f, 0f, -19.9f));
        initialLocations.Add(5, new Vector3(400f, 0f, -19.9f));
        
        levelBees.Add(0,level0Bees);
        levelBees.Add(1,level1Bees);
        levelBees.Add(2,level2Bees);
        levelBees.Add(3,level3Bees);
        levelBees.Add(4,level4Bees);
        levelBees.Add(5,level5Bees);
        
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
            first = false;
        }
    }
    private void GameOver()
    {
        playerTimes.Add(levelIndex, levelTime);
        
        PanelManager.Instance.HidePanel("TimerPanel");
        PanelManager.Instance.ShowPanel("LevelTransitionPanel", PanelShowBehaviour.KEEP_PREVIOUS);
        _timer.timeLeft = 0;
        

    }

    public void GoToNextMap()
    {
        foreach (var bee in levelBees[levelIndex])
        {
            bee.SetActive(false);
        }
        levelIndex++;
        foreach (var bee in levelBees[levelIndex])
        {
            bee.SetActive(true);
        }
        playerFrog.transform.position = initialLocations[levelIndex];
        first = true;
    }

    public void GotoPreviousMap()
    {
        if (levelIndex > 0)
        {
            foreach (var bee in levelBees[levelIndex])
            {
                bee.SetActive(false);
            }
            levelIndex--;
            foreach (var bee in levelBees[levelIndex])
            {
                bee.SetActive(true);
            }
            playerFrog.transform.position = initialLocations[levelIndex];
            first = true;
        }
        
    }

}
