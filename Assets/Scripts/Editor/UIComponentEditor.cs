using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UIComponent),true)]
public class UIComponentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        UIComponent uIComponent = (UIComponent)target;        // Cast to concrete type first
        IThemedUI themed = uIComponent as IThemedUI;          // Interface cast from concrete type works

        if (themed != null && GUILayout.Button("Apply Theme"))
        {
            themed.ApplyTheme(uIComponent.Theme);
        }

        GUI.enabled = true;
    }
}
