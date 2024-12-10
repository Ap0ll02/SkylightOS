using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    // public List<Data> iData = new();
    public enum DataType {
        text, video, audio, picture, spreadsheet, hybrid
    }
    public string textContent;
}
