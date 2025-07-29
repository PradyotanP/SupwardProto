using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ClosetManager : MonoBehaviour
{
    public static ClosetManager Instance;

    private List<ClothingItem> clothingItems = new List<ClothingItem>();
    private string imageSavePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            imageSavePath = Path.Combine(Application.persistentDataPath, "Clothes");
            Directory.CreateDirectory(imageSavePath);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveClothingItem(Texture2D image)
    {
        string fileName = "cloth_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string filePath = Path.Combine(imageSavePath, fileName);

        byte[] pngData = image.EncodeToPNG();
        File.WriteAllBytes(filePath, pngData);

        var newItem = new ClothingItem(fileName);
        newItem.color = ImageUtils.GetDominantColor(image); // <- Set detected color here
        clothingItems.Add(newItem);

        Debug.Log($"Saved clothing item: {fileName} at {filePath} with color {newItem.color}");
    }

    public void SaveClothingItemWithMetadata(Texture2D image, ClothingCategory category, string color, string brand, string description)
    {
        string fileName = "cloth_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string filePath = Path.Combine(imageSavePath, fileName);
        File.WriteAllBytes(filePath, image.EncodeToPNG());

        var newItem = new ClothingItem(fileName)
        {
            category = category,
            color = color,
            brand = brand,
            description = description
        };

        clothingItems.Add(newItem);
        Debug.Log($"Saved clothing item with metadata: {fileName}");
    }


    public List<ClothingItem> GetAllItems()
    {
        return clothingItems;
    }
}
