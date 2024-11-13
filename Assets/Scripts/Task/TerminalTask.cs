using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/**
 * @Author Jack Ratermann
 * @Date 11/03/2024
 * @Version 0.1
 * @Brief This is the terminal task class that will be used to create tasks for the terminal to execute.
 * @Note If this breaks, the likely cause is "TerminalWindow" in the scene wasn't renamed and kept its' original "TerminalWindow Variant" name.
 */

public class TerminalTask : AbstractTask 
{
    /// @var State Enum that has on and off state for the task.
    private enum State
    {
        On,
        Off
    }

    /// @var Terminal Variables that get references in awake() to the scene's terminal
    public GameObject terminal;
    public TMP_Text terminalText;
    public GameObject lsBtn;
    public GameObject avBtn;
    public GameObject nmapBtn;
    private State termState;


    /// @brief Assign all of the terminal objects in the scene.
    public void Awake() {
        terminal = GameObject.Find("WindowCanvas/TerminalWindow/TerminalPanel");
        terminalText = GameObject.Find("WindowCanvas/TerminalWindow/TerminalPanel/TInstructionTxt").GetComponent<TMP_Text>();
        lsBtn = GameObject.Find("WindowCanvas/TerminalWindow/TerminalPanel/LSButton");
        avBtn = GameObject.Find("WindowCanvas/TerminalWindow/TerminalPanel/antiviralBtn");
        nmapBtn = GameObject.Find("WindowCanvas/TerminalWindow/TerminalPanel/nmapBtn");
    }

    public void Start()
    {
        this.gameObject.SetActive(false);
    }

    public override void checkHazards()
    {
        Debug.Log("This Exists");
    }

    public override void startTask()
    {
        string termText = "Welcome To The Console, Let's get you started installing some software\n"
            + "The AntiVirus toolkit is a helpful addition for getting rid of pesky malware!\n";
        termState = State.On;
        terminalText.text = termText;
    }

    public override void stopHazards()
    {
        Debug.Log("This Exists");
    }

    public override void startHazards()
    {
        Debug.Log("This Exists");
    }

    public void AVDownload()
    {

    }
}