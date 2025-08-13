/*
Author: Ayush Bhardwaj
Date Created: August 12, 2025
Description: This script manages a group of tabs in a UI, allowing for selection, hover effects, and visual state management.
email: ayushb.developer@gmail.com
*/
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a group of tabs, handling their selection, hover, and visual states.
/// </summary>
public class TabGroup : MonoBehaviour
{
    /// <summary>
    /// The tab that should be selected by default when the UI starts
    /// </summary>
    [Tooltip("The tab that should be selected by default when the UI starts")]
    [SerializeField] private Tab defaultTab;

    [Header("Tab Sprites")]
    /// <summary>
    /// Sprite for tabs in idle (deselected) state
    /// </summary>
    [Tooltip("Sprite for tabs in idle (deselected) state")]
    [SerializeField] private Sprite idleTabSprite;
    /// <summary>
    /// Sprite for the currently selected tab
    /// </summary>
    [Tooltip("Sprite for the currently selected tab")]
    [SerializeField] private Sprite selectedTabSprite;
    /// <summary>
    /// Sprite for tabs when hovered by the pointer
    /// </summary>
    [Tooltip("Sprite for tabs when hovered by the pointer")]
    [SerializeField] private Sprite hoverTabSprite;

    // Public properties to access tab sprites
    public Sprite IdleTabSprite => idleTabSprite;
    public Sprite SelectedTabSprite => selectedTabSprite;
    public Sprite HoverTabSprite => hoverTabSprite;

    /// <summary>
    /// List of all tabs managed by this group
    /// </summary>
    private List<Tab> tabButtons;
    /// <summary>
    /// The currently selected tab
    /// </summary>
    private Tab selectedTab;

    /// <summary>
    /// Initializes the tab group and selects the default tab.
    /// </summary>
    void Start()
    {
        InitializeTabs();
    }

    /// <summary>
    /// Sets up the tabs, resets their states, and selects the default tab if available.
    /// </summary>
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

    /// <summary>
    /// Registers a tab with this TabGroup.
    /// </summary>
    /// <param name="tab">The tab to subscribe.</param>
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

    /// <summary>
    /// Called when a tab is hovered by the pointer.
    /// </summary>
    /// <param name="tab">The tab being hovered.</param>
    public void OnTabEntered(Tab tab)
    {
        if (selectedTab != null && selectedTab != tab)
        {
            tab.Hover();
        }
    }

    /// <summary>
    /// Called when the pointer exits a tab.
    /// </summary>
    /// <param name="tab">The tab being exited.</param>
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

    /// <summary>
    /// Called when a tab is clicked/selected.
    /// </summary>
    /// <param name="tab">The tab being selected.</param>
    public void OnTabSelected(Tab tab)
    {
        if (selectedTab != null)
        {
            selectedTab.Deselect();
        }

        selectedTab = tab;
        selectedTab.Select();
    }

    /// <summary>
    /// Deselects all tabs and clears the selected tab reference.
    /// </summary>
    public void ResetTabs()
    {
        foreach (var tab in tabButtons)
        {
            tab.Deselect();
        }
        selectedTab = null;
    }
}
