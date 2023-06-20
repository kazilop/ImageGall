using UnityEngine;
using UnityEngine.SceneManagement;

public class NativeInputHandler : MonoBehaviour
{
#if UNITY_ANDROID
    // ���������� ������� ������� ������ Back ��� Android
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoBack();
        }
    }
#endif

#if UNITY_IOS
    // ���������� ������� swipe ��� iOS

    private void Start()
    {
        NativeSwipe.OnSwipeLeft += HandleSwipeLeft;
        NativeSwipe.OnSwipeRight += HandleSwipeRight;
        NativeSwipe.OnSwipeUp += HandleSwipeUp;
        NativeSwipe.OnSwipeDown += HandleSwipeDown;
    }

    // ���������� ������ �����
    private void HandleSwipeLeft()
    {
        GoBack();
    }

    // ���������� ������ ������
    private void HandleSwipeRight()
    {
        
    }

    // ���������� ������ �����
    private void HandleSwipeUp()
    {
        
    }

    // ���������� ������ ����
    private void HandleSwipeDown()
    {
        
    }
#endif


    public void GoBack()
    {
        SceneManager.LoadScene("GalleryScene");
    }
}
