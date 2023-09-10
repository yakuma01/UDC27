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
    }

    private void Update()
    {
        var timerE = FindObjectOfType<Timer>();
        if (timerE != null)
        {
            _timer = timerE;
        }
    }

    public void DoHidePanel()
    {
        // Hide the last panel
        var lastPanel = _panelManager.GetLastPanel();
        if (lastPanel.PanelId == "SettingsOptionsPanel")
        {
            _panelManager.HideLastPanel();
            _panelManager.ShowPanel("SettingsPanel");
        }
        else
        {
            _panelManager.HideLastPanel();
        }
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
