using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private Tab defaultTab;
    [Header("Tab Sprites")]
    [SerializeField] private Sprite idleTabSprite;
    [SerializeField] private Sprite selectedTabSprite;
    [SerializeField] private Sprite hoverTabSprite;

    public Sprite IdleTabSprite => idleTabSprite;
    public Sprite SelectedTabSprite => selectedTabSprite;
    public Sprite HoverTabSprite => hoverTabSprite;

    private List<Tab> tabButtons;
    private Tab selectedTab;

    void Start()
    {
        InitializeTabs();
    }

    private void InitializeTabs()
    {
        if (tabButtons == null || tabButtons.Count == 0)
        {
            Debug.LogWarning("No tabs assigned to TabGroup " + name, this);
            return;
        }

        ResetTabs();

        if (defaultTab != null)
        {
            selectedTab = defaultTab;
            selectedTab.Select();
        }
        else if (tabButtons.Count > 0)
        {
            selectedTab = tabButtons[0];
            selectedTab.Select();
        }
    }

    public void Subscribe(Tab tab)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<Tab>();
        }
        if (!tabButtons.Contains(tab))
        {
            tabButtons.Add(tab);
            Debug.Log($"Tab {tab.name} subscribed to TabGroup {name}");
        }
    }

    public void OnTabEntered(Tab tab)
    {
        if (selectedTab != null && selectedTab != tab)
        {
            tab.Hover();
        }
    }

    public void OnTabExited(Tab tab)
    {
        if (selectedTab != tab)
        {
            tab.Deselect();
        }
        else
        {
            tab.Select();
        }
    }

    public void OnTabSelected(Tab tab)
    {
        if (selectedTab != null)
        {
            selectedTab.Deselect();
        }

        selectedTab = tab;
        selectedTab.Select();
    }

    public void ResetTabs()
    {
        foreach (var tab in tabButtons)
        {
            tab.Deselect();
        }
        selectedTab = null;
    } 
}
