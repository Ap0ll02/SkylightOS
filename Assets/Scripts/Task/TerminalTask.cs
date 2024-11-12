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
    /// @var Terminal Variables that get references in awake() to the scene's terminal
    public GameObject terminal;
    public TMP_Text terminalText;
    public GameObject lsBtn;
    public GameObject avBtn;
    public GameObject tBtn3;
    private State termState; 

    /// @brief Assign all of the terminal objects in the scene.
    public void awake()
    {
        terminal = GameObject.Find("WindowCanvas/TerminalWindow/TerminalPanel");
        terminalText = GameObject.Find("WindowCanvas/TerminalWindow/TerminalPanel/TInstructionTxt");
        lsBtn = GameObject.Find("WindowCanvas/TerminalWindow/TerminalPanel/LSButton");
        avBtn = GameObject.Find("WindowCanvas/TerminalWindow/TerminalPanel/antiviralBtn");
        nmapBtn = GameObject.Find("WindowCanvas/TerminalWindow/TerminalPanel/nmapBtn");
    }
    
    private enum State
    {
        On,
        Off
    }

    // initiate our task
    public override void startTask()
    {
        private string termStartText = "Welcome IT User, This Console Is Your Secret Weapon In Virus Defense.\n We have a package waiting for you to use, simply click on the installation button below to retrieve it.\n";
        termState = State.On;
        terminalText.text = termStartText;
    }

    public override void checkHazards()
    {

    }

    public override void stopHazards()
    {

    }

    public override void startHazards()
    {

    }

}
