using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] float delay = 1f;

    [SerializeField] GameObject galleryButton;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider progressBar;
    [SerializeField] TextMeshProUGUI progressText;

    private bool isLoading = false;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        galleryButton.SetActive(true);
        loadingScreen.SetActive(false);
    }


    public void LoadGalleryScene()
    {
        if (!isLoading)
        {
            isLoading = true;
            galleryButton.SetActive(false);
            loadingScreen.SetActive(true);
            StartCoroutine(LoadSceneAsync());
        }
    }


    private System.Collections.IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("GalleryScene");
        while (!operation.isDone)
        {
            yield return new WaitForSeconds(delay);
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;
            progressText.text = $"{(int)(progress * 100)}%";

            yield return null;
        }

        loadingScreen.SetActive(false);
        galleryButton.SetActive(true);
        isLoading = false;
    }
}
