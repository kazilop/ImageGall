using System;
using UnityEngine;
using UnityEngine.UI;

public class PhotoGallery : MonoBehaviour
{
    [SerializeField] int photoQunty;

    public string[] photoURLs;
    public RawImage displayImage;
    public Button previousButton;
    public Button nextButton;

    private int currentPhotoIndex = 0;

    void Start()
    {
        CreateFilename();
        DisplayPhoto(currentPhotoIndex);
    }

    private void CreateFilename()
    {
        photoURLs = new string[photoQunty];

        for (int i = 0; i < photoQunty;  i++)
        {
            photoURLs[i] = "http://data.ikppbb.com/test-task-unity-data/pics/" + (i+1) + ".jpg";

        }
    }

    public void PreviousPhoto()
    {
        currentPhotoIndex--;
        if (currentPhotoIndex < 0)
        {
            currentPhotoIndex = photoURLs.Length - 1;
        }
        DisplayPhoto(currentPhotoIndex);
    }

    public void NextPhoto()
    {
        currentPhotoIndex++;
        if (currentPhotoIndex >= photoURLs.Length)
        {
            currentPhotoIndex = 0;
        }
        DisplayPhoto(currentPhotoIndex);
    }

    private void DisplayPhoto(int index)
    {
        string photoURL = photoURLs[index];
        Debug.Log(photoURL);

        StartCoroutine(LoadPhotoCoroutine(photoURL));
      //  previousButton.interactable = (index != 0);
      //  nextButton.interactable = (index != photoURLs.Length - 1);
    }

    private System.Collections.IEnumerator LoadPhotoCoroutine(string url)
    {
        using (UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityEngine.Networking.UnityWebRequest.Result.ConnectionError || www.result == UnityEngine.Networking.UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error loading photo: " + www.error);
            }
            else
            {
                Texture2D photoTexture = ((UnityEngine.Networking.DownloadHandlerTexture)www.downloadHandler).texture;
                displayImage.texture = photoTexture;
            }
        }
    }
}
