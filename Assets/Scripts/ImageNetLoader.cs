using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ImageNetLoader : MonoBehaviour
{
    public GridLayoutGroup gridLayout;
    public Scrollbar scrollbar;
    public int photoQnty = 66;
    public float sensitivity = 0.1f;
    public int imagesPerPage = 10;
    public string[] imageUrls;



    private int currentPage = 0;
    private int totalPages = 0;
    private int loadedImages = 0;
    [SerializeField] GameObject imagePrefab;
    [SerializeField] private RectTransform contentRectTransform;


    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        CreateFilename();
        scrollbar = FindAnyObjectByType<Scrollbar>();
        contentRectTransform = gridLayout.GetComponent<RectTransform>();
        CalculateScrollbarSize();
        LoadImagesOnPage(currentPage);
        scrollbar.onValueChanged.AddListener(OnScrollbarValueChanged);
    }


    private void CreateFilename()
    {
        imageUrls = new string[photoQnty];

        for (int i = 0; i < photoQnty; i++)
        {
            imageUrls[i] = "http://data.ikppbb.com/test-task-unity-data/pics/" + (i + 1) + ".jpg";

        }
    }


    private void CalculateScrollbarSize()
    {
        float pageSize = Mathf.Ceil(imageUrls.Length / (float)imagesPerPage);
        totalPages = Mathf.Max((int)pageSize, 1);
        scrollbar.size = 1f / totalPages;
        Debug.Log("Pages " + totalPages);
    }

    private void LoadImagesOnPage(int page)
    {
        int startIndex = loadedImages;
        int endIndex = Mathf.Min(startIndex + imagesPerPage, imageUrls.Length);

        if (startIndex > photoQnty) 
        {
            StopAllCoroutines();
            return;
        }

        for (int i = startIndex; i < endIndex; i++)
        {
            StartCoroutine(LoadImage(imageUrls[i], i));
        }

        loadedImages += imagesPerPage;
    }

    private IEnumerator LoadImage(string url, int index)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;

            GameObject imageObject = Instantiate(imagePrefab, contentRectTransform);
            imageObject.transform.SetParent(contentRectTransform, false);
            imageObject.name = index.ToString();
            
            Image image = imageObject.GetComponentInChildren<ImgPref>().GetComponent<Image>();
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

            Button button = imageObject.GetComponentInChildren<Button>();
            button.GetComponent<OnPress>().picUrl = imageUrls[index];  //"http://data.ikppbb.com/test-task-unity-data/pics/" + (index + 1) + ".jpg";


            LayoutRebuilder.ForceRebuildLayoutImmediate(contentRectTransform);
        }
        else
        {
            Debug.Log("Failed to load image: " + request.error);
        }
    }

    private void OnScrollbarValueChanged(float value)
    {
        int targetPage = Mathf.FloorToInt(value / scrollbar.size);
        int pageDiff = targetPage - currentPage;

        if (Mathf.Abs(pageDiff) >= Mathf.Abs(sensitivity))
        {
            currentPage = targetPage;
            LoadImagesOnPage(currentPage);

            // Scroll other images to the top
            int imagesToScroll = pageDiff > 0 ? imagesPerPage : -imagesPerPage;
            contentRectTransform.anchoredPosition += new Vector2(0f, gridLayout.cellSize.y * imagesToScroll);
        }
    }
}
