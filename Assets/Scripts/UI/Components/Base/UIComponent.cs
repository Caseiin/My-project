using System;
using UnityEngine;

public class UIComponent : MonoBehaviour, IThemedUI
{
    public UIThemes Theme;
    public virtual void ApplyTheme(UIThemes theme)
    {
        Debug.Log("Theme is applied");
    }
}
