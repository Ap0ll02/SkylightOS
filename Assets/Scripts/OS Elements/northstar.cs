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
    public ParticleSystem compEffect;

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
        compEffect = compEffect.GetComponent<ParticleSystem>();
    }
    Coroutine pd;

    Coroutine nsNullTimer = null;
    public void Start() {
        persona.SetActive(true);
        canClose = false;
        string[] dialogue = new string[] {"Welcome To Skylight OS!", "I am your AI assistant Northstar! I am here to help at any time!", 
                      "Lets start getting to work, click my compass when you need me. Now, start by selecting a task!"};

        if(pd == null) {
            pd = StartCoroutine(PlayDialogue(dialogue, 2f));
        }
        //nsNullTimer ??= StartCoroutine(AutoCloseUpdate());
    }

    public void OnUserSummon() {
        tw.StartShowingText();
        canClose = true;
    }

    IEnumerator NullTimer() {
        yield return new WaitForSeconds(3f);
        persona.SetActive(false);
    }

    public void OnAutoSummon() {
        if(persona.activeSelf == true) {
            tw.StartShowingText();
        }
        canClose = true;
    }
    public enum Style {
        cold, chilly, mid, warm, hot
    }
    public Coroutine twh;

    public void WriteHint(string hint, Style s = Style.cold, bool autoSpeak = false) {
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
        if(autoSpeak) {
            persona.SetActive(true);
        } else {
            twh ??= StartCoroutine(Timer(2f, true));
            compEffect.Stop();
        }
        OnAutoSummon();
    }

    public Coroutine isAct = null;
    public void OnClick() {
        
        if(persona.activeSelf == false) {
            persona.SetActive(true);
            transform.SetAsLastSibling();
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

    private IEnumerator Timer(float x = 2f, bool effect = false) {
        if (effect) {
            compEffect.Play();
        }
        yield return new WaitForSeconds(x);
    }

    IEnumerator PlayDialogue(string[] dialogueLines, float t = 1f)
    {
        int count = 0;
        // Go into each string in the dialogueLines array
        foreach (var line in dialogueLines)
        {
            count++;
            // check if the line in the array is null 
            if (line != null)
            {
                // Have our typewriter start typing our each word
                tw.ShowText(line);
                if(count == 3) compEffect.Play();
                // This function will continue and wait until the type writer is done showing text
                // It is asking its self are we still typing? if so continue the function
                // once it reaches the end it will return false and break our statement 
                yield return new WaitUntil(()=> tw.isShowingText == false);
                // We then wait for a couple of seconds before loading in the next string
                yield return new WaitForSeconds(t);
                if(count == 3){
                    yield return new WaitForSeconds(2f);
                    compEffect.Stop();
                } 
            }
        }
        tw.StartDisappearingText();
        canClose = true;
        nsText.text = " ";
    }

 //   public IEnumerator AutoCloseUpdate() {
        //while(true) {
          //  yield return new WaitForSeconds(3f);
            //if(tw.isHidingText || nsText.text == null || nsText.text.Trim() == "<noparse></noparse>") {
              //  Debug.Log("Hiding Her");
                //persona.SetActive(false);
            //}
        //}
    //}
}