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
        TWindow.SetActive(true);
    }

    public void Start()
    {
        TInstructionTxt.text = FirstText;
        TWindow.SetActive(false);
    }

    public void ListFilesExec()
    {
        TInstructionTxt.text = "Files: \n";
        OnLSPressed?.Invoke();
    }

    public void AntiVirusExec()
    {
        TInstructionTxt.text = "AntiVirus: \n";
        OnAVPressed?.Invoke();
    }

    public void NMapExec()
    {
        TInstructionTxt.text = "Mapping Ports : -----\n";
        OnNMAPPressed?.Invoke();
    }
}
