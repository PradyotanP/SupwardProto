using UnityEngine;
using System;

[Serializable]
public class ClothingItem
{
    public string id;
    public string fileName;
    public ClothingCategory category = ClothingCategory.Uncategorized;
    public string color = "Unknown";
    public string brand = "";
    public string description = "";

    public ClothingItem(string fileName)
    {
        this.id = Guid.NewGuid().ToString();
        this.fileName = fileName;
    }
}

