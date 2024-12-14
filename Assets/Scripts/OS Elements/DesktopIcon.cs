using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopIcon : MonoBehaviour
{
    public bool visible;
    public void ToggleVisible(GameObject app) {
        var appGroup = app.GetComponent<CanvasGroup>();
        if(appGroup == null)
        {
            appGroup = app.AddComponent<CanvasGroup>();
        }
        // visible = appGroup.alpha > 0;
        if(!app.activeSelf) {
            app.SetActive(true);
            visible = false;
        }
        if (visible) {
            appGroup.alpha = 0;
            appGroup.interactable = false;
            appGroup.blocksRaycasts = false;
            visible = false;
        }
        else {
            appGroup.alpha = 1;
            appGroup.interactable = true;
            appGroup.blocksRaycasts = true;
            visible = true;
        }
    }

}
