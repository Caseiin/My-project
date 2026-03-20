using UnityEngine;

public class UIScreens : MonoBehaviour
{
    public ScreenType ID;
    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);

}
