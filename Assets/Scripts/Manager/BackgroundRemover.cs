using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class BackgroundRemover : MonoBehaviour
{
    public static BackgroundRemover Instance;

    [SerializeField] private string removeBgAPIKey = "ESZ2gxx5NbRh2ze49KFMMJUn";

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RemoveBackground(Texture2D originalImage, Action<Texture2D> onComplete)
    {
        StartCoroutine(RemoveBackgroundCoroutine(originalImage, onComplete));
    }

    private IEnumerator RemoveBackgroundCoroutine(Texture2D image, Action<Texture2D> onComplete)
    {
        byte[] imageBytes = image.EncodeToPNG();

        WWWForm form = new WWWForm();
        form.AddBinaryData("image_file", imageBytes, "clothing.png", "image/png");

        UnityWebRequest request = UnityWebRequest.Post("https://api.remove.bg/v1.0/removebg", form);
        request.SetRequestHeader("X-Api-Key", removeBgAPIKey);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            byte[] result = request.downloadHandler.data;
            Texture2D transparentImage = new Texture2D(2, 2);
            transparentImage.LoadImage(result);
            onComplete?.Invoke(transparentImage);
        }
        else
        {
            Debug.LogError("Background removal failed: " + request.error);
            Debug.LogError("Response: " + request.downloadHandler.text);
            onComplete?.Invoke(null);
        }
    }
}
