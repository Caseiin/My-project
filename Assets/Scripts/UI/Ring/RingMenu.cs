using System;
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

    void Start()
    {
        stepLength = 360f/ data.Elements.Length;
        var iconDist = Vector3.Distance(ringSlicePrefab.Icon.transform.position, ringSlicePrefab.CakeSlice.transform.position);

        slices = new RingSlice[data.Elements.Length];

        for(int i =0 ; i < data.Elements.Length; i++)
        {
            slices[i] = Instantiate(ringSlicePrefab,transform);

            //Set root element
            slices[i].transform.localPosition = Vector3.zero;
            slices[i].transform.localRotation = Quaternion.identity;

            // Set cake piece 
            slices[i].CakeSlice.fillAmount = 1f/ data.Elements.Length - gapWidth/360f;
            slices[i].CakeSlice.transform.localPosition = Vector3.zero;
            slices[i].CakeSlice.transform.localRotation = Quaternion.Euler(0,0, stepLength/ 2f + gapWidth/ 2f + i* stepLength);
            slices[i].CakeSlice.color = new Color(1f,1f,1f,.5f);

            // Set Icon
            slices[i].Icon.transform.localPosition = slices[i].CakeSlice.transform.localPosition + Quaternion.AngleAxis(i * stepLength, Vector3.forward) * Vector3.up * iconDist;
            slices[i].Icon.sprite = data.Elements[i].Icon;

        }
    }


    float NormalizeAngle(float angle)=> (angle + 360f) % 360f;

    public void  FindMouseAngle(Vector2 direction){
        var mouseAngle = NormalizeAngle(Vector3.SignedAngle(Vector3.up, direction, Vector3.forward) + stepLength/ 2f);
        int selectedIndex = Mathf.FloorToInt(mouseAngle / stepLength) % slices.Length;
        Debug.Log($"Angle: {mouseAngle:F1}  →  Slice: {selectedIndex}");
    }
    
}
