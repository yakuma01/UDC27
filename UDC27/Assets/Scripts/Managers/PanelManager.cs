
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEditor.Progress;

public class PanelManager : Singleton<PanelManager>
{
    /// <summary>
    /// This is going to hold all of our instances
    /// </summary>
    private List<PanelInstanceModel> _panelInstanceModels = new List<PanelInstanceModel>();

    /// <summary>
    /// Pool of panels
    /// </summary>
    private ObjectPool _objectPool;

    private void Start()
    {
        // Cache the ObjectPool
        _objectPool = ObjectPool.Instance;
        ShowPanel("GamePanel", PanelShowBehaviour.KEEP_PREVIOUS);
        ShowPanel("SettingsPanel", PanelShowBehaviour.KEEP_PREVIOUS);
    }

    public void ShowPanel(string panelId, PanelShowBehaviour behaviour = PanelShowBehaviour.KEEP_PREVIOUS)
    {
        // Get a panel instance from the ObjectPool
        GameObject panelInstance = _objectPool.GetObjectFromPool(panelId);

        // If we have one
        if (panelInstance != null)
        {
            // If we should hide the previous panel, and we have one
            if (behaviour == PanelShowBehaviour.HIDE_PREVIOUS && GetAmountPanelsInQueue() > 0)
            {
                // Get the last panel
                var lastPanel = GetLastPanel();

                // Disable it
                lastPanel?.PanelInstance.SetActive(false);
            }

            // Add this new panel to the queue
            _panelInstanceModels.Add(new PanelInstanceModel
            {
                PanelId = panelId,
                PanelInstance = panelInstance
            });
        }
        else
        {
            Debug.LogWarning($"Trying to use panelId = {panelId}, but this is not found in the ObjectPool");
        }
    }

    public void HideLastPanel()
    {
        // Make sure we do have a panel showing
        if (AnyPanelShowing())
        {
            // Get the last panel showing
            var lastPanel = GetLastPanel();

            // Remove it from the list of instances
            _panelInstanceModels.Remove(lastPanel);

            // Pool the object
            _objectPool.PoolObject(lastPanel.PanelInstance);

            // If we have more panels in the queue
            if (GetAmountPanelsInQueue() > 0)
            {
                lastPanel = GetLastPanel();
                if (lastPanel != null && !lastPanel.PanelInstance.activeInHierarchy)
                {
                    lastPanel.PanelInstance.SetActive(true);
                }
            }
        }
    }

    public void HidePanel(string panelId)
    {
        if (IsPanelShowing(panelId))
        {
            int index = _panelInstanceModels.FindIndex(i => i.PanelId == panelId);
            var panel = GetPanel(index);
            _panelInstanceModels.Remove(panel);
            _objectPool.PoolObject(panel.PanelInstance);
        }
    }

    /// <summary>
    /// Returns the last panel in the queue
    /// </summary>
    /// <returns>The last panel in the queue</returns>
    PanelInstanceModel GetLastPanel()
    {
        return _panelInstanceModels[_panelInstanceModels.Count - 1];
    }

    PanelInstanceModel GetPanel(int index)
    {
        return _panelInstanceModels[index];
    }

    /// <summary>
    /// Returns if any panel is showing
    /// </summary>
    /// <returns>Do we have a panel showing?</returns>
    public bool AnyPanelShowing()
    {
        return GetAmountPanelsInQueue() > 0;
    }

    public bool IsPanelShowing(string panelId)
    {
        return _panelInstanceModels.Any(i => i.PanelId == panelId);
    }
    /// <summary>
    /// Returns how many panels we have in queue
    /// </summary>
    /// <returns>Amount of panels in queue</returns>
    public int GetAmountPanelsInQueue()
    {
        return _panelInstanceModels.Count;
    }
}
