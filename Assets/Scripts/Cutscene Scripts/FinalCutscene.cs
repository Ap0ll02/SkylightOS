using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCutscene : MonoBehaviour
{
    void Start() {
        int evAmt = SaveLoad.GetEvidence();
        switch(evAmt) {
            case 0:
            case 1:
                Debug.Log("You have insufficient evidence");
                break;
            case 2:
            case 3:
                Debug.Log("You have a fair amount of evidence");
                break;
            case 4:
            case 5:
                Debug.Log("You have a lot of evidence");
                break;
            case 6:
                Debug.Log("You Got ALL The Evidence");
                break;
            default:
                break;
                
        }
    }
}
