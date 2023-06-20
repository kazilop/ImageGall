using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnPress : MonoBehaviour
{
    public string picUrl;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => LoadViewScene(button.GetComponent<OnPress>().picUrl));
    }

    
    public void LoadViewScene(string url)
    {
        Debug.Log(picUrl);
        PicParametr.picURL = picUrl;
        SceneManager.LoadSceneAsync("View");
    }

}
