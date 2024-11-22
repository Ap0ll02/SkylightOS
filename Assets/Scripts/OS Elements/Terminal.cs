using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
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
    string twinName = "TerminalWindow2 Variant";

    /// @var TInstructionTxt The text that covers the terminal that can be used to show instructions or text.
    [SerializeField] TMP_Text TInstructionTxt;

    /// @var TWindow The terminal window variant, so it may be inactive upon start.
    public GameObject TWindow;

    /// @var TInputHeading Standard Terminal Header Prior To Input (ex: user@computer >)
    [SerializeField] TMP_Text TInputHeading;

    /// @var IntroText A String to be displayed upoon start for terminal. 
    private string FirstText = "Welcome to ClearSky Console.\n We are testing multi-line editing tbh.";

    /// @var OnAVPressed event and Action delegate variable setup.
    public static event Action OnAVPressed;
    public static event Action OnNMAPPressed;
    public static event Action OnLSPressed;

    public void Awake()
    {
        TWindow = GameObject.Find(twinName);
        TWindow.SetActive(true);
    }

    public void Start()
    {
        TInstructionTxt.text = FirstText;
        TWindow.SetActive(false);
    }

    public void ListFilesExec()
    {
        // All code above the invoke line is what happens if the terminal task does not override.
        TInstructionTxt.text = "Files: \n";

        // Allows the TerminalTask to modify terminal commands if Task State is On
        OnLSPressed?.Invoke();
    }

    public void AntiVirusExec()
    {
        // All code above the invoke line is what happens if the terminal task does not override.
        TInstructionTxt.text = "AntiVirus: \n";

        // Allows the TerminalTask to modify terminal commands if Task State is On
        OnAVPressed?.Invoke();
    }

    public void NMapExec()
    {
        // All code above the invoke line is what happens if the terminal task does not override.
        TInstructionTxt.text = "Mapping Ports : -----\n";

        // Allows the TerminalTask to modify terminal commands if Task State is On
        OnNMAPPressed?.Invoke();
    }
}
