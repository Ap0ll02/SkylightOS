using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Febucci.UI;
using Febucci.UI.Core;
using Random = UnityEngine.Random;

public class NyanCatStageOne : AbstractBossStage
{
    // We got to have a refference of the canvas to spawn
    public GameObject canvas;
    // The current text that is going to be played and animated
    public TextAnimator_TMP text;
    // These are the five lines that Nyan cat will be yapping about during the first stages.
    public string[] dialogueLines = new string[5];
    public string textOne = "<rainb><shake a=0.5>MUHAHAHAHA!</rainb></shake><waitfor=0.5> I have been here since 2008. You know how many IT persons have tried... and <incr>failed!!!</incr> to remove me ";
    public string textTwo = "Seriously<waitfor=0.5>, You know how rude it is to try and close a process! The <rainb>Rainbow Sprinkle Gall</rainb> of you people. YOU shall face my <bounce a=0.05>revenge!</bounce>";
    public string textThree = "..... AND YOU WILL FACE MY REVENGE!";
    public string textFour = "....Really? come on... Why are the LAZERS NOT TURNING ON ...";
    public string textFive = "... THERE WE GO!... AS I WAS SAYING MUHAHAHA AND YOU WILL FACE MY REVENGE";
    // This is the textbox prefab that spawns on the face of nyan cat
    public GameObject textBoxPrefab;
    private GameObject textBox;
    public GameObject bossTextPrefab;
    private GameObject bossText;
    // This is the nyan cat character including his animation
    public GameObject nyanCatIdlePrefab;
    private GameObject nyanCatIdle;
    // The typewriter refference used to control the typewriter
    public TypewriterByCharacter typewriter;
    // We want our wonderful AI to provide commentary
    public GameObject NorthStar;
    public Northstar northstar;

    private void Start()
    {
        // Get the boss manager from the scene because each stage should have a reference to the boss
        bossManager = GameObject.Find("NyanCatBossManager").GetComponent<BossManager>();
    }

    public override void BossStartStage()
    {
        northstar.WriteHint("Oh no... this is not good!!! You have to stop him",Northstar.Style.warm,true);
        bossText = Instantiate(bossTextPrefab, canvas.transform);
        if(bossText == null)
            Debug.LogError("Cant instantiate bossText");
        if (nyanCatIdle == null)
            nyanCatIdle = Instantiate(nyanCatIdlePrefab);
        if(nyanCatIdle == null)
            Debug.LogError("cant instantiate NyanCatIdle");
        text = bossText.GetComponent<TextAnimator_TMP>();
        if (text == null)
            Debug.Log("put the text box prefab on NyanCatBossFight Prefab");
        textBox = Instantiate(textBoxPrefab);
        if (textBox == null)
            Debug.LogError("Cant instantiate textBox");
        if (typewriter == null)
        {
            typewriter = bossText.GetComponent<TypewriterByCharacter>();
            if (typewriter == null)
            {
                Debug.LogError("TypewriterByCharacter component is not assigned or missing, put it on Dialogue object!");
            }
        }
        StartCoroutine(playDialogue());
    }

    IEnumerator playDialogue()
    {
        dialogueLines = new string[] { textOne, textTwo, textThree, textFour, textFive };
        // Go into each string in the dialogueLines array
        foreach (var line in dialogueLines)
        {
            // check if the line in the array is null
            if (line != null)
            {
                // Have our typewriter start typing our each word
                typewriter.ShowText(line);
                // This function will continue and wait until the type writer is done showing text
                // It is asking its self are we still typing? if so continue the function
                // once it reaches the end it will return false and break our statement
                yield return new WaitUntil(()=> typewriter.isShowingText == false);
                // We then wait for a couple of seconds before loading in the next string
                yield return new WaitForSeconds(0.5f);
            }
        }
        // Once we are done with the dialogue we will invoke the next stage of the boss fight
        BossEndStage();
    }
    public override void BossEndStage()
    {
        Destroy(bossText);
        Destroy(textBox);
        Destroy(nyanCatIdle);
        bossManager.NextStage();
        gameObject.SetActive(false);
    }
}
