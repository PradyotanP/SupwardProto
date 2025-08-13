using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tab : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] private TabGroup tabGroup;
    [SerializeField] private Image backgroundImage;

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

    public void Select()
    {
        // Logic to visually indicate the tab is selected
        Debug.Log($"{name} selected");
        SetBackgroundImage(tabGroup.SelectedTabSprite);
    }

    internal void Deselect()
    {
        // Logic to visually indicate the tab is deselected
        Debug.Log($"{name} deselected");
        SetBackgroundImage(tabGroup.IdleTabSprite);
    }

    internal void Hover()
    {
        // Logic to visually indicate the tab is hovered
        Debug.Log($"{name} hovered");
        SetBackgroundImage(tabGroup.HoverTabSprite);
    }

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

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExited(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEntered(this);
    }
}
