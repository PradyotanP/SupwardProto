//Author: Ayush Bhardwaj
//Date Created: August 12, 2025
//Description: This script represents a Page associated with a tab in a tab group UI.
//Handles activation and deactivation of the page.
//email: ayushb.developer@gmail.com

using UnityEngine;

/// <summary>
/// Represents a Page associated with a tab in a tab group UI.
/// Handles activation and deactivation of the page.
/// </summary>
public class TabPage : MonoBehaviour
{
    /// <summary>
    /// The tab group that this page belongs to
    /// </summary>
    [Tooltip("The tab group that this page belongs to")]
    [SerializeField] private TabGroup tabGroup;

    /// <summary>
    /// Activates the page and sets the associated tab as selected
    /// </summary>
    public void ActivatePage()
    {
        gameObject.SetActive(true);

    }

    /// <summary>
    /// Deactivates the page
    /// </summary>
    public void DeactivatePage()
    {
        gameObject.SetActive(false);
    }
}
