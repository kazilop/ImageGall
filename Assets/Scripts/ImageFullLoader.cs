using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageFullLoader : MonoBehaviour
{
    public Image imageComponent;
    public string imageUrl;


    void Start()
    {
        imageUrl = PicParametr.picURL;
        StartCoroutine(LoadImage());
    }


    private IEnumerator LoadImage()
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl))
        {
            yield return www.SendWebRequest();
            
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Ошибка загрузки изображения: " + www.error);
                yield break;
            }

            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            imageComponent.sprite = sprite;
        }
    }
}
