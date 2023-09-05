using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePanelButton : MonoBehaviour
{
    /// <summary>
    /// Cached reference to the PanelManager
    /// </summary>
    private PanelManager _panelManager;

    private void Start()
    {
        // Cache the manager
        _panelManager = PanelManager.Instance;
    }

    public void DoHidePanel()
    {
        // Hide the last panel
        _panelManager.HideLastPanel();
        _panelManager.ShowMainPanel();
    }

    public void DoHideMainPanel()
    {
        _panelManager.HideMainPanel();
    }
}
