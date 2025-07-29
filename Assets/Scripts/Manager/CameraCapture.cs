using UnityEngine;
using UnityEngine.UI;

public class CameraCapture : MonoBehaviour
{
    public RawImage cameraPreview;
    public Button captureButton;

    private WebCamTexture webcamTexture;

    public MetadataEntryUI metadataUI;

    void Start()
    {
        webcamTexture = new WebCamTexture();
        cameraPreview.texture = webcamTexture;
        cameraPreview.material.mainTexture = webcamTexture;
        webcamTexture.Play();

        captureButton.onClick.AddListener(CaptureImage);
    }

    void CaptureImage()
    {
        Texture2D snap = new Texture2D(webcamTexture.width, webcamTexture.height);
        snap.SetPixels(webcamTexture.GetPixels());
        snap.Apply();

        Texture2D resized = ImageUtils.ResizeTexture(snap, 512);

        BackgroundRemover.Instance.RemoveBackground(resized, (processedImage) =>
        {
            Texture2D imageToUse = processedImage ?? snap;
            string color = DominantColorDetector.GetDominantColorName(imageToUse); // assumes this util exists
            metadataUI.Show(imageToUse, color);
        });
    }
}
