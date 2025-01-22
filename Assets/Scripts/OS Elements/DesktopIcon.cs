using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopIcon : MonoBehaviour
{
    // Used to have more code but windows have this functionality built in already now
    public void ToggleVisible(BasicWindow window) {
        window.ToggleWindow();
    }

}
