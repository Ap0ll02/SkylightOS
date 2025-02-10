using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class pipe : MonoBehaviour
{
    public enum PipeType {
        StraightUp,
        StraightLaying,
        TopRight,
        TopLeft,
        BottomLeft,
        BottomRight,
        FourWay
    }

    public List<PipeType> top_connectables;
    public List<PipeType> bottom_connectables;
    public List<PipeType> left_connectables;
    public List<PipeType> right_connectables;

    public PipeType PipeStyle;
    public InputAction rotAction;
    void Awake(){
        if(rotAction != null){
            rotAction.performed += OnRotate;
        }
    }

    void Start() {
        CreateLists();
    }
    void CreateLists() {
        top_connectables = new();
        bottom_connectables = new();
        right_connectables = new();
        left_connectables = new();

        if(PipeStyle == PipeType.StraightUp){
            top_connectables.Add(PipeType.TopLeft);
            top_connectables.Add(PipeType.TopRight);
            top_connectables.Add(PipeType.StraightUp);
            top_connectables.Add(PipeType.FourWay);
            
            bottom_connectables.Add(PipeType.StraightUp);       
            bottom_connectables.Add(PipeType.BottomLeft);
            bottom_connectables.Add(PipeType.BottomRight);
            bottom_connectables.Add(PipeType.FourWay);
        }
        else if(PipeStyle == PipeType.StraightLaying){
            left_connectables.Add(PipeType.TopLeft);
            left_connectables.Add(PipeType.BottomLeft);
            left_connectables.Add(PipeType.StraightLaying);
            left_connectables.Add(PipeType.FourWay);

            right_connectables.Add(PipeType.TopRight);
            right_connectables.Add(PipeType.BottomRight);
            right_connectables.Add(PipeType.StraightLaying);
            right_connectables.Add(PipeType.FourWay);
        }
        else if(PipeStyle == PipeType.TopRight){
            left_connectables.Add(PipeType.TopLeft);
            left_connectables.Add(PipeType.BottomLeft);
            left_connectables.Add(PipeType.StraightLaying);
            left_connectables.Add(PipeType.FourWay);

            bottom_connectables.Add(PipeType.StraightUp);       
            bottom_connectables.Add(PipeType.BottomLeft);
            bottom_connectables.Add(PipeType.BottomRight);
            bottom_connectables.Add(PipeType.FourWay);
        }
        else if(PipeStyle == PipeType.TopLeft){
            right_connectables.Add(PipeType.TopRight);
            right_connectables.Add(PipeType.BottomRight);
            right_connectables.Add(PipeType.StraightLaying);
            right_connectables.Add(PipeType.FourWay);

            bottom_connectables.Add(PipeType.StraightUp);       
            bottom_connectables.Add(PipeType.BottomLeft);
            bottom_connectables.Add(PipeType.BottomRight);
            bottom_connectables.Add(PipeType.FourWay);
        }
        else if(PipeStyle == PipeType.BottomLeft){  
            right_connectables.Add(PipeType.TopRight);
            right_connectables.Add(PipeType.BottomRight);
            right_connectables.Add(PipeType.StraightLaying);
            right_connectables.Add(PipeType.FourWay);

            top_connectables.Add(PipeType.TopLeft);
            top_connectables.Add(PipeType.TopRight);
            top_connectables.Add(PipeType.StraightUp);
            top_connectables.Add(PipeType.FourWay);
        }
        else if(PipeStyle == PipeType.BottomRight){
            left_connectables.Add(PipeType.TopLeft);
            left_connectables.Add(PipeType.BottomLeft);
            left_connectables.Add(PipeType.StraightLaying);
            left_connectables.Add(PipeType.FourWay);

            top_connectables.Add(PipeType.TopLeft);
            top_connectables.Add(PipeType.TopRight);
            top_connectables.Add(PipeType.StraightUp);
            top_connectables.Add(PipeType.FourWay);
        }
        else if(PipeStyle == PipeType.FourWay){
            left_connectables.Add(PipeType.TopLeft);
            left_connectables.Add(PipeType.BottomLeft);
            left_connectables.Add(PipeType.StraightLaying);
            left_connectables.Add(PipeType.FourWay);

            top_connectables.Add(PipeType.TopLeft);
            top_connectables.Add(PipeType.TopRight);
            top_connectables.Add(PipeType.StraightUp);
            top_connectables.Add(PipeType.FourWay);

            right_connectables.Add(PipeType.TopRight);
            right_connectables.Add(PipeType.BottomRight);
            right_connectables.Add(PipeType.StraightLaying);
            right_connectables.Add(PipeType.FourWay);

            bottom_connectables.Add(PipeType.StraightUp);       
            bottom_connectables.Add(PipeType.BottomLeft);
            bottom_connectables.Add(PipeType.BottomRight);
            bottom_connectables.Add(PipeType.FourWay);
        }

    }

    // Rotate the given pipe 90 degrees
    private void OnRotate(InputAction.CallbackContext context)
    {
        if(!MouseOverPipe()) return;
        RectTransform rt = GetComponent<RectTransform>();
        Vector3 newRotation = rt.eulerAngles;
        newRotation.z += 90;
        newRotation.z %= 360;
        NextPipeStyle();
        CreateLists();
        rt.rotation = Quaternion.Euler(newRotation);
    }
    // Raycast results to ensure specific pipe is targeted.
    bool MouseOverPipe(){
        PointerEventData pd = new PointerEventData(EventSystem.current)
        {
            position = Mouse.current.position.ReadValue()
        };
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(pd, results);

        foreach(var result in results){
            if(result.gameObject == gameObject){
                return true;
            }
        }
        return false;
    }

    void NextPipeStyle() {
        if(PipeStyle == PipeType.StraightUp) PipeStyle = PipeType.StraightLaying;
        else if(PipeStyle == PipeType.StraightLaying) PipeStyle = PipeType.StraightUp;
        else if(PipeStyle == PipeType.BottomLeft) PipeStyle = PipeType.BottomRight;
        else if(PipeStyle == PipeType.BottomRight) PipeStyle = PipeType.TopRight;
        else if(PipeStyle == PipeType.TopLeft) PipeStyle = PipeType.BottomLeft;
        else if(PipeStyle == PipeType.TopRight) PipeStyle = PipeType.TopLeft;
        Debug.Log("CURRENT: " + PipeStyle);
    }

    void OnEnable(){
        rotAction?.Enable();
    }
    void OnDisable(){
        rotAction?.Disable();
    }
}
