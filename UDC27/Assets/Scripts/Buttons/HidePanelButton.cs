using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class HidePanelButton : MonoBehaviour
{
    /// <summary>
    /// Cached reference to the PanelManager
    /// </summary>
    private PanelManager _panelManager;
    private GameManager _gameManager;
    private Timer _timer;

    private void Start()
    {
        // Cache the manager
        _panelManager = PanelManager.Instance;
        _gameManager = GameManager.Instance;
        _timer = FindObjectOfType<Timer>(); 
    }

    public void DoHidePanel()
    {
        // Hide the last panel
        _panelManager.HideLastPanel();
    }

    public void DoHidePanelFromStart()
    {
        _panelManager.HidePanel("GamePanel");
        _panelManager.ShowPanel("TimerPanel");
    }

    public void MoveToNextLevel()
    {
        _panelManager.HideLastPanel();
        _panelManager.ShowPanel("TimerPanel");
        _gameManager.GoToNextMap();
    }

    public void BackToPreviousLevel()
    {
        _panelManager.HideLastPanel();
        _panelManager.ShowPanel("TimerPanel");
        _gameManager.GotoPreviousMap();
    }

    public void ResumeGame()
    {
        _timer.ResumeTimer();
    }
}
