using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class northstar : MonoBehaviour
{
    public GameObject persona;
    public GameObject icon;
    public List<AbstractTask> taskList = new();
    public GameObject osmanager;
    public void Awake() {
        RawImage[] riList = GetComponentsInChildren<RawImage>();
        persona = riList[0].gameObject.name == "NSPersona" ? riList[0].gameObject : riList[1].gameObject;
        icon = riList[1].gameObject.name == "NSPersona" ? riList[0].gameObject : riList[1].gameObject;
        AbstractTask[] tasks = GetComponents<AbstractTask>();
        taskList = tasks.ToList();
        osmanager = GetComponent<OSManager>().gameObject;
    }

    public void Start() {

    }

    public void OnUserSummon() {
        
    }

    public void OnAutoSummon() {

    }
}
