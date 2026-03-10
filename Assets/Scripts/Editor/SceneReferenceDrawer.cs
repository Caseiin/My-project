using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SceneReference))]
public class SceneReferenceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var pathProp = property.FindPropertyRelative("scenePath");

        SceneAsset scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathProp.stringValue);

        scene = (SceneAsset)EditorGUI.ObjectField(
            position,
            label,
            scene,
            typeof(SceneAsset),
            false);

        pathProp.stringValue = AssetDatabase.GetAssetPath(scene);
    }
}
