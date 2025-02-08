using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipe : MonoBehaviour
{
    public enum PipeType {
        StraightUp,
        StraightLaying,
        TopRight,
        TopLeft,
        BottomLeft,
        BottomRight
    }

    public List<PipeType> connectables;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
