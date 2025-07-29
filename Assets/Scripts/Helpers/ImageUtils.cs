using UnityEngine;

public static class ImageUtils
{
    public static Texture2D ResizeTexture(Texture2D source, int maxSize = 512)
    {
        float scale = Mathf.Min((float)maxSize / source.width, (float)maxSize / source.height);
        int newWidth = Mathf.RoundToInt(source.width * scale);
        int newHeight = Mathf.RoundToInt(source.height * scale);

        RenderTexture rt = RenderTexture.GetTemporary(newWidth, newHeight);
        Graphics.Blit(source, rt);

        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = rt;

        Texture2D result = new Texture2D(newWidth, newHeight);
        result.ReadPixels(new Rect(0, 0, newWidth, newHeight), 0, 0);
        result.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(rt);

        return result;
    }

    public static byte[] CompressToJPG(Texture2D texture, int quality = 75)
    {
        return texture.EncodeToJPG(quality);
    }

    public static string GetDominantColor(Texture2D tex)
    {
        Color32[] pixels = tex.GetPixels32();
        int r = 0, g = 0, b = 0;

        foreach (Color32 color in pixels)
        {
            r += color.r;
            g += color.g;
            b += color.b;
        }

        int total = pixels.Length;
        Color avg = new Color(r / (float)total / 255f, g / (float)total / 255f, b / (float)total / 255f);

        return ColorUtility.ToHtmlStringRGB(avg); // Hex RGB format like "FFB3A1"
    }
}
