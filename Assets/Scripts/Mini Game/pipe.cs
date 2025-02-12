using System;
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

    public HashSet<PipeType> top_connectables;
    public HashSet<PipeType> bottom_connectables;
    public HashSet<PipeType> left_connectables;
    public HashSet<PipeType> right_connectables;

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
        top_connectables = new HashSet<PipeType>();
        bottom_connectables = new HashSet<PipeType>();
        left_connectables = new HashSet<PipeType>();
        right_connectables = new HashSet<PipeType>();

        // Updated to switch for better performance and readability
        switch (PipeStyle)
        {
            case PipeType.StraightUp:
                InitializeStraightUp();
                break;
            case PipeType.StraightLaying:
                InitializeStraightLaying();
                break;
            case PipeType.TopRight:
                InitializeTopRight();
                break;
            case PipeType.TopLeft:
                InitializeTopLeft();
                break;
            case PipeType.BottomLeft:
                InitializeBottomLeft();
                break;
            case PipeType.BottomRight:
                InitializeBottomRight();
                break;
            case PipeType.FourWay:
                InitializeFourWay();
                break;
        }
        

    }

    // The Following Initialization Methods were generated with AI (Github COPILOT) to
    // speed up the monotonous process of extremely similar code.

    private void InitializeFourWay()
    {
        left_connectables.UnionWith(new[]
        { PipeType.TopLeft, PipeType.BottomLeft, PipeType.StraightLaying, PipeType.FourWay });
        top_connectables.UnionWith(new[]
        { PipeType.TopLeft, PipeType.TopRight, PipeType.StraightUp, PipeType.FourWay });
        right_connectables.UnionWith(new[]
        { PipeType.TopRight, PipeType.BottomRight, PipeType.StraightLaying, PipeType.FourWay });
        bottom_connectables.UnionWith(new[]
        { PipeType.StraightUp, PipeType.BottomLeft, PipeType.BottomRight, PipeType.FourWay });
    }

    void InitializeStraightLaying()
    {
        left_connectables.UnionWith(new[]
        { PipeType.TopLeft, PipeType.BottomLeft, PipeType.StraightLaying, PipeType.FourWay });
        right_connectables.UnionWith(new[]
        { PipeType.TopRight, PipeType.BottomRight, PipeType.StraightLaying, PipeType.FourWay });
    }

    void InitializeStraightUp() {
        top_connectables.UnionWith(new[]
        { PipeType.TopLeft, PipeType.TopRight, PipeType.StraightUp, PipeType.FourWay});
        bottom_connectables.UnionWith(new[]
        { PipeType.StraightUp, PipeType.BottomLeft, PipeType.BottomRight, PipeType.FourWay});
    }

    void InitializeTopRight()
    {
        left_connectables.UnionWith(new[]
        { PipeType.TopLeft, PipeType.BottomLeft, PipeType.StraightLaying, PipeType.FourWay });
        bottom_connectables.UnionWith(new[]
        { PipeType.StraightUp, PipeType.BottomLeft, PipeType.BottomRight, PipeType.FourWay });
    }

    void InitializeTopLeft()
    {
        right_connectables.UnionWith(new[]
        { PipeType.TopRight, PipeType.BottomRight, PipeType.StraightLaying, PipeType.FourWay });
        bottom_connectables.UnionWith(new[]
        { PipeType.StraightUp, PipeType.BottomLeft, PipeType.BottomRight, PipeType.FourWay });
    }

    void InitializeBottomLeft()
    {
        right_connectables.UnionWith(new[]
        { PipeType.TopRight, PipeType.BottomRight, PipeType.StraightLaying, PipeType.FourWay });
        top_connectables.UnionWith(new[]
        { PipeType.TopLeft, PipeType.TopRight, PipeType.StraightUp, PipeType.FourWay });
    }

    void InitializeBottomRight()
    {
        left_connectables.UnionWith(new[]
        { PipeType.TopLeft, PipeType.BottomLeft, PipeType.StraightLaying, PipeType.FourWay });
        top_connectables.UnionWith(new[]
        { PipeType.TopLeft, PipeType.TopRight, PipeType.StraightUp, PipeType.FourWay });
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
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pd, results);

        foreach(var result in results){
            if(result.gameObject == gameObject){
                return true;
            }
        }
        return false;
    }

    void NextPipeStyle() {

        switch(PipeStyle){
            case PipeType.StraightUp:
                PipeStyle = PipeType.StraightLaying;
                break;
            case PipeType.StraightLaying:
                PipeStyle = PipeType.StraightUp;
                break;
            case PipeType.BottomLeft:
                PipeStyle = PipeType.BottomRight;
                break;
            case PipeType.BottomRight:
                PipeStyle = PipeType.TopRight;
                break;
            case PipeType.TopLeft:
                PipeStyle = PipeType.BottomLeft;
                break;
            case PipeType.TopRight:
                PipeStyle = PipeType.TopLeft;
                break;
        }
    }

    void OnEnable(){
        rotAction?.Enable();
    }
    void OnDisable(){
        rotAction?.Disable();
    }
}
