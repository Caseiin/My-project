using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    [SerializeField] SceneReference scene;
    public void OnStartClick()
    {
        SceneManager.LoadScene(scene.ScenePath);
    }

    public void OnExitClick()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif 
        Application.Quit();
    }
}
