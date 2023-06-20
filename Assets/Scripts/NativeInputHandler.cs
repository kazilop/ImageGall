using UnityEngine;
using UnityEngine.SceneManagement;

public class NativeInputHandler : MonoBehaviour
{
#if UNITY_ANDROID
    // Обработчик события нажатия кнопки Back для Android
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoBack();
        }
    }
#endif

#if UNITY_IOS
    // Обработчик событий swipe для iOS

    private void Start()
    {
        NativeSwipe.OnSwipeLeft += HandleSwipeLeft;
        NativeSwipe.OnSwipeRight += HandleSwipeRight;
        NativeSwipe.OnSwipeUp += HandleSwipeUp;
        NativeSwipe.OnSwipeDown += HandleSwipeDown;
    }

    // Обработчик свайпа влево
    private void HandleSwipeLeft()
    {
        GoBack();
    }

    // Обработчик свайпа вправо
    private void HandleSwipeRight()
    {
        
    }

    // Обработчик свайпа вверх
    private void HandleSwipeUp()
    {
        
    }

    // Обработчик свайпа вниз
    private void HandleSwipeDown()
    {
        
    }
#endif


    public void GoBack()
    {
        SceneManager.LoadScene("GalleryScene");
    }
}
