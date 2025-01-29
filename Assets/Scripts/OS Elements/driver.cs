using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class driver : MonoBehaviour
{
    public GameObject system_menu;
    public TMP_Text driver_desc;

    void Awake() {
        // FIXME When Garrett updates this menu the reference will need to be reacquired
        system_menu = GetComponentInParent<SystemResourcesWindow>().gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

}
