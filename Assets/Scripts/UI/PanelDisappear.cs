using System.Collections;
using UnityEngine;

public class PanelDisappear : MonoBehaviour
{
    [SerializeField] GameObject _panel;

    void Start()
    {
        InvokeRepeating(nameof(DisappearPanel),0f, 10f);
    }

    void DisappearPanel()
    {
        StartCoroutine(PanelDisappearRoutine());
    }

    IEnumerator PanelDisappearRoutine()
    {
        _panel.SetActive(true);
        yield return new WaitForSeconds(20f);
        _panel.SetActive(false);
    }
}
