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
        _timer = FindObjectOfType<Timer>();
    }
    public void DoShowPanel()
    {
        // Show the panel
        _panelManager.ShowPanel(PanelId, Behaviour);
    }

    public void PauseGame()
    {
        _timer.PauseTimer();
    }

    
}
