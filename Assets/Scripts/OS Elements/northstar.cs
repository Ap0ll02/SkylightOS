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

    public string[] startDialogueLines;
    public NorthstarDialogueSO startDialogueSO;

    private Coroutine hintCoroutine;

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

        if (startDialogueSO != null && startDialogueSO.dialogueLines.Length > 0)
        {
            if (pd == null)
            {
                pd = StartCoroutine(PlayMultipleLines(startDialogueSO.dialogueLines, 0.5f));
            }
        }
        else
        {
            Debug.LogWarning("No dialogue lines found in the assigned NorthstarDialogueSO.");
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
                currentText = "<wave a=0.3 f=0.4><rainb>" + hint + "</rainb></wave>";
                break;
            default:
                break;
        }
        tw.ShowText(currentText);
        if (autoSpeak)
        {
            OnAutoSummon();
        }
        else if(!personaOpen)
        {
            compEffect.Play();
        }
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
            //StopAllCoroutines();
        }
    }

    public void OnUserSummon()
    {
        transform.SetAsLastSibling();
        compEffect.Stop();
        OpenPersona();
        if (currentText != string.Empty)
        {
            OpenSpeechBubble();
            StartCoroutine(PlayLine(currentText));
        }
    }

    public void OnAutoSummon()
    {
        transform.SetAsLastSibling();
        OpenPersona();
        OpenSpeechBubble();
        StartCoroutine(PlayLine(currentText));
    }

    private IEnumerator Timer(float x = 2f, bool effect = false)
    {
        if (effect)
        {
            compEffect.Play();
        }
        yield return new WaitForSeconds(x);
    }

    private IEnumerator PlayMultipleLines(string[] dialogueLines, float t = 0.5f)
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
        canClose = true;
        yield return new WaitUntil(() => !tw.isHidingText);
        yield return new WaitForSeconds(t);
        nsText.text = string.Empty;
        CloseSpeechBubble();
    }

    private IEnumerator PlayLine(string dialogueLines, float t = 1f)
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
        yield return new WaitForSeconds(0.5f);
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

    public void StartHintCoroutine(string hint, float delay, Style s = Style.cold)
    {
        hintCoroutine = StartCoroutine(WaitForHint(hint, delay, s));
    }

    public void InterruptHintCoroutine()
    {
        if (hintCoroutine != null)
        {
            StopCoroutine(hintCoroutine);
            hintCoroutine = null;
        }
    }

    private IEnumerator WaitForHint(string hint, float delay, Style s = Style.cold)
    {
        yield return new WaitForSeconds(delay);
        CloseSpeechBubble();
        OpenSpeechBubble();
        WriteHint(hint, s, true);
    }

}
