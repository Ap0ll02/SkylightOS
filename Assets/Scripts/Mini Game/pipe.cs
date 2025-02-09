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

    // Rotate the given pipe 90 degrees
    private void OnRotate(InputAction.CallbackContext context)
    {
        if(!MouseOverPipe()) return;
        RectTransform rt = GetComponent<RectTransform>();
        Vector3 newRotation = rt.eulerAngles;
        newRotation.z += 90;
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

    void OnEnable(){
        rotAction?.Enable();
    }
    void OnDisable(){
        rotAction?.Disable();
    }
}
