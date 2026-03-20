using UnityEngine;

public class UIScreens : MonoBehaviour
{
    public ScreenType ScreenType;
    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);
}
