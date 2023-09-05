using UnityEngine;

public class ShowPanelButton : MonoBehaviour
{
    /// <summary>
    /// The id of the panel to show
    /// </summary>
    public string PanelId;

    /// <summary>
    /// The panel show behaviour
    /// </summary>
    public PanelShowBehaviour Behaviour;
    
    /// <summary>
    /// Cached reference to the PanelManager
    /// </summary>
    private PanelManager _panelManager;

    private void Start()
    {
        // Cache the manager
        _panelManager = PanelManager.Instance;
    }
    public void DoShowPanel()
    {
        // Show the panel
        _panelManager.ShowPanel(PanelId, Behaviour);
    }
}
