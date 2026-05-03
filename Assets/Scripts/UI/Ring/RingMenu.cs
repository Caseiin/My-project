using System;
using UnityEditor.UI;
using UnityEngine;

public class RingMenu : MonoBehaviour
{
    [Header("Ring requirements")]
    [SerializeField] RingSO data;
    [SerializeField] RingSlice ringSlicePrefab;
    [SerializeField] float  gapWidth = 1f;
    protected RingMenu Parent;
    protected RingSlice[] slices;
    float stepLength;
    int selectedIndex;
    public static Action<AbilitySO> onAbiiltySelected;
    public event Action onSelectedAbility;

    void Awake()
    {
        stepLength = 360f/ data.Elements.Length;
        var iconDist = Vector3.Distance(ringSlicePrefab.Icon.transform.position, ringSlicePrefab.CakeSlice.transform.position);

        slices = new RingSlice[data.Elements.Length];

        for(int i =0 ; i < data.Elements.Length; i++)
        {
            slices[i] = Instantiate(ringSlicePrefab,transform);
            slices[i].CakeSlice.raycastTarget = false;
            slices[i].Icon.raycastTarget = false;

            //Set root element
            slices[i].transform.localPosition = Vector3.zero;
            slices[i].transform.localRotation = Quaternion.identity;

            // Set cake piece 
            slices[i].CakeSlice.fillAmount = 1f/ data.Elements.Length - gapWidth/360f;
            slices[i].CakeSlice.transform.localPosition = Vector3.zero;
            slices[i].CakeSlice.transform.localRotation = Quaternion.Euler(0,0, stepLength/ 2f + gapWidth/ 2f + i* stepLength);
            slices[i].CakeSlice.color = new Color(.1f,.1f,.1f,.5f);

            // Set Icon
            slices[i].Icon.transform.localPosition = slices[i].CakeSlice.transform.localPosition + Quaternion.AngleAxis(i * stepLength, Vector3.forward) * Vector3.up * iconDist;
            slices[i].Icon.sprite = data.Elements[i].Icon;

        }
    }



    // public void  FindMouseAngle(Vector2 direction){
    //     var mouseAngle = NormalizeAngle(Vector3.SignedAngle(Vector3.up, direction, Vector3.forward) + stepLength/ 2f);
    //     selectedIndex = Mathf.FloorToInt(mouseAngle / stepLength) % slices.Length;
    //     SelectActiveElement(selectedIndex);
    //     // Debug.Log($"Angle: {mouseAngle:F1}  →  Slice: {selectedIndex}");
    // }


    public void StepSelection(int step){
        selectedIndex += step;
        if (selectedIndex < 0){
            selectedIndex = slices.Length -1;
        }
        else if (selectedIndex >= slices.Length)
            selectedIndex = 0;

        SelectActiveElement(selectedIndex);
    }
    
    float NormalizeAngle(float angle)=> (angle + 360f) % 360f;
    void SelectActiveElement(int elementidx){
        for(int i = 0; i < data.Elements.Length; i++){
            if(i == elementidx)
            {
                slices[i].CakeSlice.color = new Color(1f,1f,.97f,.75f);
            }
            else 
                slices[i].CakeSlice.color = new Color(.1f,.1f,.1f,.5f);

        }
    }

    public void SelectAbilityElement()
    {
        var selectedAbility = data.Elements[selectedIndex].Ability;
        onAbiiltySelected?.Invoke(selectedAbility);
        onSelectedAbility?.Invoke();
        Debug.Log($" Selected Ability: {selectedAbility}");
    }
}
