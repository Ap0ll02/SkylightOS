using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Febucci.UI; // For the text animator
using Febucci.UI.Core; // For core typewriter functionalitySSS
/// <summary>
/// Author Quinn Contaldi
/// This script handles the starting dialogue for the boss fight using Febucci animations and typewriter effects.
/// It instantiates the Nyan Cat and text box prefabs, and plays a sequence of dialogue lines with typewriter effects.
/// After the dialogue is complete, it activates the NyanceNyanceSingleton and destroys the instantiated objects.
/// </summary>
public class Dialogue : MonoBehaviour
{
    public GameObject NyanceNyanceSingleton;
    public GameObject nyanCatPrefab;
    public GameObject nyanCat;
    public GameObject textBoxPrefab;
    public GameObject textBox;
    public TextAnimator_TMP text; 

    //public string textOne = "<rainb><shake a=0.5>MUHAHAHAHA!</rainb></shake><waitfor=0.5> I have been here since 2008. You know how many IT persons have tried... and <incr>failed!!!</incr> to remove me ";
    public string textOne = "a";
    //public string textTwo = "Seriously<waitfor=0.5>, You know how rude it is to try and close a process! The <rainb>Rainbow Sprinkle Gall</rainb> of you people. YOU shall face my <bounce a=0.05>revenge!</bounce>";
    public string textTwo = "b";
    //public string textThree = "..... AND YOU WILL FACE MY REVENGE!";
    public string textThree = "C";
    //public string textFour = "....Really? come on... Why are the LAZERS NOT TURNING ON ...";
    public string textFour = "D";
    //public string textFive = "... THERE WE GO!... AS I WAS SAYING MUHAHAHA AND YOU WILL FACE MY REVENGE";
    public string textFive = "E";

    public TypewriterByCharacter typewriter; 
    public string[] dialogueLines = new string[5];
    // Start is called before the first frame update

    void Awake()
    {
        nyanCat = Instantiate(nyanCatPrefab);
        textBox = Instantiate(textBoxPrefab);
    }
    void Start()
    {
        dialogueLines = new string[] {textOne,textTwo,textThree,textFour,textFive };
        text = GetComponent<TextAnimator_TMP>();
        if (text == null)
        {
            Debug.Log("put the text box on the Dialogue");
        }
        else
        {
            Debug.LogError("TextBox GameObject is not assigned or missing a TextAnimator_TMP component!");
        }

        if (typewriter == null)
        {
            typewriter = GetComponent<TypewriterByCharacter>();
            if (typewriter == null)
            {
                Debug.LogError("TypewriterByCharacter component is not assigned or missing, put it on Dialogue object!");
            }
        }
        StartCoroutine(playDialogue());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Create a coroutine that will be used to itterate through each dialogue line 
    IEnumerator playDialogue()
    {
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
        Instantiate(NyanceNyanceSingleton);
        TurnOff();
    }

    public void TurnOff()
    {
        Destroy(nyanCat);
        Destroy(textBox);
        Destroy(gameObject);
    }
}
