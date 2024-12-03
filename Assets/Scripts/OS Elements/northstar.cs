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
    public bool canClose = true;
    public void Awake() {
        RawImage[] riList = GetComponentsInChildren<RawImage>();
        persona = riList[0].gameObject.name == "NSPersona" ? riList[0].gameObject : riList[1].gameObject;
        icon = riList[1].gameObject.name == "NSPersona" ? riList[0].gameObject : riList[1].gameObject;
        AbstractTask[] tasks = GetComponents<AbstractTask>();
        taskList = tasks.ToList();
        // osmanager = GetComponent<OSManager>().gameObject;
    }

    public void Start() {
        persona.SetActive(false);
    }

    public void OnUserSummon() {
        
    }

    public void OnAutoSummon() {

    }

    public void OnClick() {
        if(persona.activeSelf == false) {
            persona.SetActive(true);
            OnUserSummon();
        }
        else if(canClose == true && persona.activeSelf == true){
            persona.SetActive(false);
        }
    }
}
