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
    public CanvasGroup personaCanvas;
    public GameObject icon;
    public GameObject speechBubble;
    public CanvasGroup speechBubbleCanvas;

    public bool personaOpen;
    public bool speechBubbleOpen;

    public List<AbstractTask> taskList = new();
    public GameObject osmanager;
    public ParticleSystem compEffect;

    public TypewriterByCharacter tw;
    public TMP_Text nsText;
    public string currentText;
    public bool canClose = true;
    private Coroutine pd;
    private Coroutine twh;
    private Coroutine isAct = null;
    private Coroutine autoCloseCoroutine = null;

    public void Awake()
    {
        personaOpen = true;
        speechBubbleOpen = true;
        RawImage[] riList = GetComponentsInChildren<RawImage>();
        persona = riList[0].gameObject.name == "NSPersona" ? riList[0].gameObject : riList[1].gameObject;
        icon = riList[1].gameObject.name == "NSPersona" ? riList[0].gameObject : riList[1].gameObject;
        AbstractTask[] tasks = GetComponents<AbstractTask>();
        taskList = tasks.ToList();
        tw = GetComponentInChildren<TypewriterByCharacter>();
        nsText = GetComponentInChildren<TMP_Text>();
        compEffect = compEffect.GetComponent<ParticleSystem>();
    }

    public void Start()
    {
        canClose = false;
        string[] dialogue = new string[] {"Welcome To Skylight OS!", "I am your AI assistant Northstar! I am here to help at any time!",
                          "Lets start getting to work, click my compass when you need me. Now, start by selecting a task!"};

        if (pd == null)
        {
            pd = StartCoroutine(PlayDialogue(dialogue, 2f));
        }
    }



    public enum Style
    {
        cold, chilly, mid, warm, hot
    }

    public void WriteHint(string hint, Style s = Style.cold, bool autoSpeak = false)
    {
        switch (s)
        {
            case Style.chilly:
                currentText = "<bounce a=0.1 f=0.4>" + hint + "</bounce>";
                break;
            case Style.cold:
                currentText = hint;
                break;
            case Style.mid:
                currentText = "<pend a=0.5 f=0.8>" + hint + "</pend>";
                break;
            case Style.warm:
                currentText = "<fade d=4>" + hint + "</fade>";
                break;
            case Style.hot:
                currentText = "<wave a=1 f=0.4><rainb>" + hint + "</rainb></wave>";
                break;
            default:
                break;
        }
        tw.ShowText(currentText);
        if (autoSpeak)
        {
            OnAutoSummon();
        }
        //else
        //{
        //    twh ??= StartCoroutine(Timer(2f, true));
        //    compEffect.Stop();
        //}
    }

    public void OnClick()
    {
        if (!personaOpen)
        {
            OnUserSummon();
        }
        else if (canClose && personaOpen)
        {
            ClosePersona();
            StopAllCoroutines();
        }
    }
    public void OnUserSummon()
    {
        transform.SetAsLastSibling();
        OpenPersona();
        if (currentText != string.Empty)
        {
            OpenSpeechBubble();
            StartCoroutine(PlayDialogueLine(currentText));
        }
    }

    public void OnAutoSummon()
    {
        transform.SetAsLastSibling();
        OpenPersona();
        OpenSpeechBubble();
        StartCoroutine(PlayDialogueLine(currentText));
    }

    private IEnumerator Timer(float x = 2f, bool effect = false)
    {
        if (effect)
        {
            compEffect.Play();
        }
        yield return new WaitForSeconds(x);
    }

    private IEnumerator PlayDialogue(string[] dialogueLines, float t = 1f)
    {
        canClose = false;
        int count = 0;
        foreach (var line in dialogueLines)
        {
            count++;
            if (line != null)
            {
                tw.ShowText(line);
                if (count == 3) compEffect.Play();
                yield return new WaitUntil(() => !tw.isShowingText);
                yield return new WaitForSeconds(t);
                if (count == 3)
                {
                    yield return new WaitForSeconds(2f);
                    compEffect.Stop();
                }
            }
        }
        tw.StartDisappearingText();
        yield return new WaitUntil(() => !tw.isHidingText);
        yield return new WaitForSeconds(t);
        CloseSpeechBubble();
        canClose = true;
        nsText.text = " ";
    }

    private IEnumerator PlayDialogueLine(string dialogueLines, float t = 1f)
    {
        canClose = false;
        tw.ShowText(dialogueLines);
        yield return new WaitUntil(() => !tw.isShowingText);
        yield return new WaitForSeconds(t);
        tw.StartDisappearingText();
        canClose = true;
        yield return new WaitUntil(() => !tw.isHidingText);
        yield return new WaitForSeconds(t);
        currentText = string.Empty;
        CloseSpeechBubble();
    }

    private IEnumerator AutoCloseUpdate()
    {
        yield return new WaitUntil(() => !tw.isShowingText);
        yield return new WaitForSeconds(2);
        tw.StartDisappearingText();
    }

    private void OpenPersona()
    {
        personaCanvas.alpha = 1;
        personaCanvas.blocksRaycasts = true;
        personaCanvas.interactable = true;
        personaOpen = true;
    }

    private void ClosePersona()
    {
        personaCanvas.alpha = 0;
        personaCanvas.blocksRaycasts = false;
        personaCanvas.interactable = false;
        personaOpen = false;
    }

    private void OpenSpeechBubble()
    {
        speechBubbleCanvas.alpha = 1;
        speechBubbleCanvas.blocksRaycasts = true;
        speechBubbleCanvas.interactable = true;
        speechBubbleOpen = true;
    }

    private void CloseSpeechBubble()
    {
        speechBubbleCanvas.alpha = 0;
        speechBubbleCanvas.blocksRaycasts = false;
        speechBubbleCanvas.interactable = false;
        speechBubbleOpen = false;
    }
}
