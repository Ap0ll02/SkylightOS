using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LoadingScriptTextSO", menuName = "ScriptableObjects/LoadingScriptTextSO", order = 1)]
public class LoadingScriptTextSO : ScriptableObject
{
    public List<string> loadingTexts;
}
