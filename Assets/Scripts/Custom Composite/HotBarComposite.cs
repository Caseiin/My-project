#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

[DisplayStringFormat("{slot1}/{slot2}/{slot3}/{slot4}/{slot5}")]
#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class HotbarComposite : InputBindingComposite<int>
{
    [InputControl(layout = "Button")] public int slot1;
    [InputControl(layout = "Button")] public int slot2;
    [InputControl(layout = "Button")] public int slot3;
    [InputControl(layout = "Button")] public int slot4;
    [InputControl(layout = "Button")] public int slot5;

    static HotbarComposite()
    {
        InputSystem.RegisterBindingComposite<HotbarComposite>();
    }

    // Ensures registration survives a build — editor static constructor alone won't
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init() { }

    public override int ReadValue(ref InputBindingCompositeContext context)
    {
        if (context.ReadValueAsButton(slot1)) return 0;
        if (context.ReadValueAsButton(slot2)) return 1;
        if (context.ReadValueAsButton(slot3)) return 2;
        if (context.ReadValueAsButton(slot4)) return 3;
        if (context.ReadValueAsButton(slot5)) return 4;
        return -1;
    }

    public override float EvaluateMagnitude(ref InputBindingCompositeContext context)
        => ReadValue(ref context) >= 0 ? 1f : 0f;
}