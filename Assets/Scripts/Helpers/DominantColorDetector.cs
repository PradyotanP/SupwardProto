using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class DominantColorDetector
{
    private static Dictionary<string, Color> colorMap = new Dictionary<string, Color>()
    {
        { "Red", Color.red },
        { "Green", Color.green },
        { "Blue", Color.blue },
        { "Yellow", Color.yellow },
        { "Cyan", Color.cyan },
        { "Magenta", Color.magenta },
        { "Black", Color.black },
        { "White", Color.white },
        { "Gray", Color.gray },
        { "Orange", new Color(1f, 0.5f, 0f) },
        { "Brown", new Color(0.6f, 0.3f, 0.1f) },
        { "Purple", new Color(0.5f, 0f, 0.5f) },
        { "Pink", new Color(1f, 0.75f, 0.8f) }
    };

    public static string GetDominantColorName(Texture2D texture)
    {
        if (texture == null) return "Unknown";

        Color[] pixels = texture.GetPixels();
        Dictionary<string, int> colorCounts = new Dictionary<string, int>();

        foreach (var pixel in pixels)
        {
            if (pixel.a < 0.5f) continue; // Skip transparent pixels

            string closestName = GetClosestColorName(pixel);

            if (!colorCounts.ContainsKey(closestName))
                colorCounts[closestName] = 0;

            colorCounts[closestName]++;
        }

        return colorCounts.OrderByDescending(kv => kv.Value).FirstOrDefault().Key ?? "Unknown";
    }

    private static string GetClosestColorName(Color target)
    {
        float minDistance = float.MaxValue;
        string closestColorName = "Unknown";

        foreach (var kvp in colorMap)
        {
            float dist = ColorDistance(target, kvp.Value);
            if (dist < minDistance)
            {
                minDistance = dist;
                closestColorName = kvp.Key;
            }
        }

        return closestColorName;
    }

    private static float ColorDistance(Color a, Color b)
    {
        return Mathf.Sqrt(
            Mathf.Pow(a.r - b.r, 2) +
            Mathf.Pow(a.g - b.g, 2) +
            Mathf.Pow(a.b - b.b, 2)
        );
    }
}
