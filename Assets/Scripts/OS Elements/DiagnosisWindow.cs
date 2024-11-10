using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// Garrett Sharp
/// Diagnosis window
/// Script currently only needed so that reference can be grabbed.
/// Any unique behavior that is not like a window should be implemented here though
/// </summary>
public class DiagnosisWindow : MonoBehaviour
{
    // Starting disabled
    void Start()
    {
        gameObject.SetActive(false);
    }

    public static event Action OnDiagnosisWindowOpened;

    public void OpenWindow()
    {
        gameObject.SetActive(true);
        OnDiagnosisWindowOpened?.Invoke();
    }
}
