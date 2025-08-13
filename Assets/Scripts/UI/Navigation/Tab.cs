//Author: Ayush Bhardwaj
//Date Created: August 12, 2025
//Description: This script represents a single tab in a tab group UI. Handles selection, hover, and click events.
//email: ayushb.developer@gmail.com

using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Represents a single tab in a tab group UI. Handles selection, hover, and click events.
/// </summary>
public class Tab : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    /// <summary>
    /// Reference to the parent TabGroup managing this tab
    /// </summary>
    [Tooltip("Reference to the parent TabGroup managing this tab")]
    [SerializeField] private TabGroup tabGroup;
    /// <summary>
    /// The background image of the tab, used to visually indicate state
    /// </summary>
    [Tooltip("The background image of the tab, used to visually indicate state")]
    [SerializeField] private Image backgroundImage;

    [SerializeField] private TabPage associatedPage;

    /// <summary>
    /// Subscribes this tab to its TabGroup on initialization.
    /// </summary>
    void Awake()
    {
        if (tabGroup != null)
        {
            tabGroup.Subscribe(this);
        }
        else
        {
            Debug.LogError("TabGroup is not assigned for " + name);
        }
    }

    /// <summary>
    /// Called when the tab is selected. Updates visual state.
    /// </summary>
    public void Select()
    {
        // Logic to visually indicate the tab is selected
        Debug.Log($"{name} selected");
        SetBackgroundImage(tabGroup.SelectedTabSprite);
        associatedPage.ActivatePage();
    }

    /// <summary>
    /// Called when the tab is deselected. Updates visual state.
    /// </summary>
    internal void Deselect()
    {
        // Logic to visually indicate the tab is deselected
        Debug.Log($"{name} deselected");
        SetBackgroundImage(tabGroup.IdleTabSprite);
        associatedPage.DeactivatePage();
    }

    /// <summary>
    /// Called when the tab is hovered over. Updates visual state.
    /// </summary>
    internal void Hover()
    {
        // Logic to visually indicate the tab is hovered
        Debug.Log($"{name} hovered");
        SetBackgroundImage(tabGroup.HoverTabSprite);
    }

    /// <summary>
    /// Sets the background image sprite for the tab.
    /// </summary>
    /// <param name="sprite">The sprite to set as the background.</param>
    public void SetBackgroundImage(Sprite sprite)
    {
        if (backgroundImage != null)
        {
            backgroundImage.sprite = sprite;
        }
        else
        {
            Debug.LogError("Background image is not assigned for " + name);
        }
    }

    /// <summary>
    /// Unity event handler for when the pointer exits the tab area.
    /// </summary>
    /// <param name="eventData">Pointer event data.</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExited(this);
    }

    /// <summary>
    /// Unity event handler for when the tab is clicked.
    /// </summary>
    /// <param name="eventData">Pointer event data.</param>
    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    /// <summary>
    /// Unity event handler for when the pointer enters the tab area.
    /// </summary>
    /// <param name="eventData">Pointer event data.</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEntered(this);
    }
}
