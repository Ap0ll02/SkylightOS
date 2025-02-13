using TMPro;
using UnityEngine;

public class Biosupdater : MonoBehaviour
{
    [SerializeField] TMP_Dropdown val;

    public BasicWindow window;
    public void Start() {
        window.CloseWindow();
        val.value = 0;
        val.SetValueWithoutNotify(0);
    }
    public void OnEnable() {
        PerformanceThiefManager.PThiefStarted += InitValue;
    }
    public void OnDisable() {
        PerformanceThiefManager.PThiefStarted -= InitValue;
    }

    void InitValue() {
        val.value = 0;
        val.SetValueWithoutNotify(0);
    }
}
