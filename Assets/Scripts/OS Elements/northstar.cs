using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Febucci.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// @Author Jack Ratermann
/// @brief Northstar helper, allows tasks to call on northstar to write out help to the user!
/// Depends on febucci animator, and needs to get the task list for anything automated.
/// Use style enum for different levels of effects when using WriteHint(),
/// cold is straightforward and easiest to read.

public class Northstar : MonoBehaviour
{
    public GameObject persona;
    public GameObject icon;
    public List<AbstractTask> taskList = new();
    public GameObject osmanager;

    public TypewriterByCharacter tw;
    public TMP_Text nsText;
    public bool canClose = true;
    public void Awake() {
        RawImage[] riList = GetComponentsInChildren<RawImage>();
        persona = riList[0].gameObject.name == "NSPersona" ? riList[0].gameObject : riList[1].gameObject;
        icon = riList[1].gameObject.name == "NSPersona" ? riList[0].gameObject : riList[1].gameObject;
        AbstractTask[] tasks = GetComponents<AbstractTask>();
        taskList = tasks.ToList();
        tw = GetComponentInChildren<TypewriterByCharacter>();
        nsText = GetComponentInChildren<TMP_Text>();
        // osmanager = GetComponent<OSManager>().gameObject;
    }

    public void Start() {
        persona.SetActive(false);
    }

    public void OnUserSummon() {
        tw.StartShowingText();
        canClose = false;
    }

    public void OnAutoSummon() {
        tw.StartShowingText();
        canClose = true;
    }
    public enum Style {
        cold, chilly, mid, warm, hot
    }

    public void WriteHint(string hint, Style s = Style.cold) {
        canClose = false;
        switch (s) {
            case Style.chilly: {
                nsText.text = "<bounce a=0.1 f=0.4>" + hint + "</bounce>";
                break;
            }
            case Style.cold: {
                nsText.text = hint;
                break;
            }
            case Style.mid: {
                nsText.text = "<pend a=0.5 f=0.8>" + hint + "</pend>";
                break;
            }
            case Style.warm: {
                nsText.text = "<fade d=4>" + hint + "</fade>";
                break;
            }
            case Style.hot: {
                nsText.text = "<wave a=1 f=0.4><rainb>" + hint + "</rainb></wave>";
                break;
            }
            default: break;
        }
        OnAutoSummon();
    }

    public Coroutine isAct = null;
    public void OnClick() {
        
        if(persona.activeSelf == false) {
            persona.SetActive(true);
            OnUserSummon();
        }
        else if(canClose == true && persona.activeSelf == true){
            tw.StopShowingText();
            tw.StartDisappearingText();

            if (isAct == null) {
                try {
                    StartCoroutine(Timer(3f));
                } catch {
                    isAct = null;
                }
                
            }
            persona.SetActive(false);
        }
    }

    private IEnumerator Timer(float x = 2f) {
        yield return new WaitForSeconds(x);
    }

    IEnumerator PlayDialogue(string[] dialogueLines)
    {
        // Go into each string in the dialogueLines array
        foreach (var line in dialogueLines)
        {
            // check if the line in the array is null 
            if (line != null)
            {
                // Have our typewriter start typing our each word
                tw.ShowText(line);
                // This function will continue and wait until the type writer is done showing text
                // It is asking its self are we still typing? if so continue the function
                // once it reaches the end it will return false and break our statement 
                yield return new WaitUntil(()=> tw.isShowingText == false);
                // We then wait for a couple of seconds before loading in the next string
                yield return new WaitForSeconds(0.5f);
            }

        }
        canClose = true;
    }
}