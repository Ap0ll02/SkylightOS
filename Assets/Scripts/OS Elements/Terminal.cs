using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
//using TMPro.EditorUtilities;
/**
 * @author Jack Ratermann
 * @date 11/03/2024
 * @version 0.1
 * @brief This is the terminal class that parses, executes and helps show output. This can be used in conjunction 
 * with the terminal task. 
 * @note This class will not have all actual terminal commands.
 */

/// Commands Available: LS, apt-install, NMAP

public class Terminal : MonoBehaviour
{
    /// @var twinName The name of the terminal window, change here if you did change your window name.
    string twinName = "TerminalWindow";

    public GameObject usrInput;

    /// @var TInstructionTxt The text that covers the terminal that can be used to show instructions or text.
    [SerializeField] TMP_Text TInstructionTxt;

    /// @var TWindow The terminal window variant, so it may be inactive upon start.
    public GameObject TWindow;

    /// @var TInputHeading Standard Terminal Header Prior To Input (ex: user@computer >)
    [SerializeField] TMP_Text TInputHeading;
    Coroutine lbp;

    /// @var IntroText A String to be displayed upoon start for terminal. 
    private string FirstText = "Welcome to ClearSky Console.\n We are testing multi-line editing tbh.";

    /// @var OnAVPressed event and Action delegate variable setup.
    public static event Action OnAVPressed;
    public static event Action OnNMAPPressed;
    public static event Action OnLSPressed;
    public int iterCount;
    public void Awake()
    {
        TWindow = GameObject.Find(twinName);
        TWindow.SetActive(true);
        usrInput = GameObject.Find(ConsoleInput);
    }

    public void Start()
    {
        TInstructionTxt.horizontalAlignment = HorizontalAlignmentOptions.Center; 
        TInstructionTxt.text = FirstText;
        TWindow.SetActive(false);
    }

    public void ListFilesExec()
    {
        try {
            StopCoroutine(lbp);
        } catch (Exception e) when (e is NullReferenceException) {
            Debug.Log("Coroutine Finished. Now Null");
        }
        lbp = null;
        TInstructionTxt.horizontalAlignment = HorizontalAlignmentOptions.Center; 
        // All code above the invoke line is what happens if the terminal task does not override.
        TInstructionTxt.text = "Files: \n";

        // Allows the TerminalTask to modify terminal commands if Task State is On
        OnLSPressed?.Invoke();
    }

    public void AntiVirusExec()
    {
        try {
            StopCoroutine(lbp);

        } catch (Exception e) when (e is NullReferenceException) {
            Debug.Log("Coroutine Finished. Now Null");
        }
        lbp = null;
        TInstructionTxt.horizontalAlignment = HorizontalAlignmentOptions.Center; 
        // All code above the invoke line is what happens if the terminal task does not override.
        TInstructionTxt.text = "AntiVirus: \n";

        // Allows the TerminalTask to modify terminal commands if Task State is On
        OnAVPressed?.Invoke();
    }
    int count = 0;
    public void NMapExec()
    {
        count = 0;
        TInstructionTxt.horizontalAlignment = HorizontalAlignmentOptions.Center; 
        // All code above the invoke line is what happens if the terminal task does not override.
        TInstructionTxt.text = "Mapping Ports on Local Network\n";
        iterCount = UnityEngine.Random.Range(1, 5);
        lbp ??= StartCoroutine(TerminalLoading());
        // Allows the TerminalTask to modify terminal commands if Task State is On
        OnNMAPPressed?.Invoke();
    }

    public void HandleUserInput()
    {
        if (usrInput != null)
        {
            Debug.Log("Lol I love this input");
        }
    }

    public IEnumerator TerminalLoading() {
        while(count < 20 * iterCount) {
            count++;
            TInstructionTxt.text += "*";
            if(count % 20 == 0) {
                TInstructionTxt.text = "Mapping Ports on Local Network\n";
                TInstructionTxt.text += "*";
            }
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.005f, 0.65f));
        }
        TInstructionTxt.horizontalAlignment = HorizontalAlignmentOptions.Left; 
        TInstructionTxt.text = "Starting Nmap on Your Local Network:\n";
        TInstructionTxt.text += "Ports Scanned:\n";
        TInstructionTxt.text += "PORT       STATE     SERVICE\n";
        TInstructionTxt.text += "21/tcp     closed    ftp\n";
        TInstructionTxt.text += "80/tcp     open      http\n";
        TInstructionTxt.text += "121/tcp    open      SkyFile\n";
        TInstructionTxt.text += "145/tcp    closed    Normal Service :D\n";
        TInstructionTxt.text += "456/tcp    closed    CompanyLunch Server\n";
        TInstructionTxt.text += "3421/tcp   open      Ok\n";
        lbp = null;
    }
}
