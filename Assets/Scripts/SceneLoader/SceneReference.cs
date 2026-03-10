
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class SceneReference
{
    [SerializeField] string scenePath;

#if UNITY_EDITOR
    public SceneAsset SceneAsset
    {
        get => AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
        set => scenePath = AssetDatabase.GetAssetPath(value);
    }
#endif

    public string ScenePath => scenePath;
}