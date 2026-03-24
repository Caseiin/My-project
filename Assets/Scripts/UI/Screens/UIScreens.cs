using UnityEngine;

public class UIScreens : MonoBehaviour
{
    public ScreenType ScreenType;
    public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
