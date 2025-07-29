using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MetadataEntryUI : MonoBehaviour
{
    public TMP_Dropdown categoryDropdown;
    public TMP_InputField brandInput;
    public TMP_InputField descriptionInput;
    public Button saveButton;

    private Texture2D image;
    private string detectedColor;

    private void Start()
    {
        categoryDropdown.ClearOptions();
        categoryDropdown.AddOptions(Enum.GetNames(typeof(ClothingCategory)).ToList());

        saveButton.onClick.AddListener(SaveMetadata);
        gameObject.SetActive(false);
    }

    public void Show(Texture2D processedImage, string dominantColor)
    {
        image = processedImage;
        detectedColor = dominantColor;
        brandInput.text = "";
        descriptionInput.text = "";
        categoryDropdown.value = 0;
        gameObject.SetActive(true);
    }

    private void SaveMetadata()
    {
        var category = (ClothingCategory)categoryDropdown.value;
        string brand = brandInput.text;
        string description = descriptionInput.text;

        ClosetManager.Instance.SaveClothingItemWithMetadata(image, category, detectedColor, brand, description);
        gameObject.SetActive(false);
    }
}
