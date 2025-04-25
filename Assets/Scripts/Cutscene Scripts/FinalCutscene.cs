using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCutscene : MonoBehaviour
{
    public List<GameObject> endScene = new();
    void Start() {
        // ===============================================
        // Switch Based On Evidence For End Cutscene/Image
        // ===============================================
        int evAmt = SaveLoad.GetEvidence();
        switch(evAmt) {
            case 0:
            case 1:
                Debug.Log("You have insufficient evidence");
                endScene[0].SetActive(true);
                break;
            case 2:
            case 3:
                Debug.Log("You have a fair amount of evidence");
                endScene[1].SetActive(true);
                break;
            case 4:
            case 5:
                Debug.Log("You have a lot of evidence");
                endScene[2].SetActive(true);
                break;
            case 6:
                Debug.Log("You Got ALL The Evidence");
                endScene[3].SetActive(true);
                break;
            default:
                break;                
        }
    }
}
