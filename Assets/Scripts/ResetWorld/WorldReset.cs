using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldReset : MonoBehaviour
{
    [SerializeField] SceneReference scene;
    [SerializeField] InputReader _input;

    void OnEnable()
    {
        _input.OnResetTabTrigger += ResetWorld;
    }

    void OnDisable()
    {
        _input.OnResetTabTrigger -= ResetWorld;
    }

    void ResetWorld()
    {
        SceneManager.LoadScene(scene.ScenePath);
    }
}
