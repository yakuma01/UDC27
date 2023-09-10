using UI;
using UnityEngine;

public class ShowPanelButton : MonoBehaviour
{
    /// <summary>
    /// The id of the panel to show
    /// </summary>
    public string PanelId;
    private Timer _timer;

    /// <summary>
    /// The panel show behaviour
    /// </summary>
    public PanelShowBehaviour Behaviour;
    
    /// <summary>
    /// Cached reference to the PanelManager
    /// </summary>
    private PanelManager _panelManager;

    void Start()
    {
        // Cache the manager
        _panelManager = PanelManager.Instance;
    }
    private void Update()
    {
        var timerE = FindObjectOfType<Timer>();
        if (timerE != null)
        {
            _timer = timerE;
        }
    }
    public void DoShowPanel()
    {
        // Show the panel
        if(PanelId == "SettingsOptionsPanel")
        {
            _panelManager.HidePanel("SettingsPanel");
        }
        _panelManager.ShowPanel(PanelId, Behaviour);
    }

    public void PauseGame()
    {
        _timer.PauseTimer();
    }
}
