using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

[CustomPropertyDrawer(typeof(Effect), true)]
public class EffectDrawer : PropertyDrawer
{
    static Dictionary<string, Type> typeMap;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (typeMap == null)
            BuildTypeMap();

        // Draw dropdown for selecting effect type
        Rect typeRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        if (EditorGUI.DropdownButton(typeRect, new GUIContent(GetShortTypeName(property.managedReferenceFullTypename) ?? "Select Effect Type"), FocusType.Keyboard))
        {
            var menu = new GenericMenu();
            foreach (var kvp in typeMap)
            {
                var type = kvp.Value;
                menu.AddItem(new GUIContent(kvp.Key), type.FullName == property.managedReferenceFullTypename, () =>
                {
                    property.managedReferenceValue = Activator.CreateInstance(type); // Instantiate concrete effect
                    property.serializedObject.ApplyModifiedProperties();
                });
            }
            menu.ShowAsContext();
        }

        // Draw effect fields (healingAmount, etc.)
        if (property.managedReferenceValue != null)
        {
            Rect contentRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2,
                                        position.width,
                                        EditorGUI.GetPropertyHeight(property, true));
            EditorGUI.indentLevel++;
            EditorGUI.PropertyField(contentRect, property, GUIContent.none, true);
            EditorGUI.indentLevel--;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.managedReferenceValue == null)
            return EditorGUIUtility.singleLineHeight;
        return EditorGUIUtility.singleLineHeight + EditorGUI.GetPropertyHeight(property, true) + 2;
    }

    static void BuildTypeMap()
    {
        var baseType = typeof(Effect);
        typeMap = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => { try { return a.GetTypes(); } catch { return Type.EmptyTypes; } })
            .Where(t => !t.IsAbstract && baseType.IsAssignableFrom(t))
            .ToDictionary(t => ObjectNames.NicifyVariableName(t.Name), t => t);
    }

    static string GetShortTypeName(string fullTypeName)
    {
        if (string.IsNullOrEmpty(fullTypeName)) return null;
        var parts = fullTypeName.Split(' ');
        return parts.Length > 1 ? parts[1].Split('.').Last() : fullTypeName;
    }
}