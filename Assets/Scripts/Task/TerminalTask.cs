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
    public GameObject terminalPanel;
    public GameObject arrowGame;
    public Arrowgame ag;


    /// @brief Assign all of the terminal objects in the scene.
    public void Awake() {
        arrowGame = GameObject.Find("ArrowGame");
        terminal = GameObject.Find("WindowCanvas/TerminalWindow");
        terminalPanel = GameObject.Find("WindowCanvas/TerminalWindow/TerminalPanel");
        terminalText = GameObject.Find("WindowCanvas/TerminalWindow/TerminalPanel/TInstructionTxt").GetComponent<TMP_Text>();
        lsBtn = GameObject.Find("WindowCanvas/TerminalWindow/TerminalPanel/LSButton");
        avBtn = GameObject.Find("WindowCanvas/TerminalWindow/TerminalPanel/antiviralBtn");
        nmapBtn = GameObject.Find("WindowCanvas/TerminalWindow/TerminalPanel/nmapBtn");
        termState = State.Off;
        ag = arrowGame.GetComponent<Arrowgame>();
    }

    public new void Start() {
        base.Start();
        gameObject.SetActive(false);
    }

    public override void checkHazards()
    {
        Debug.Log("This Exists");
    }

    /// @brief Changes terminal information prompt and terminal state.
    public override void startTask()
    {
        // Terminal Task Start, Prompts User To Use The AntiVirus installation tool. Changes terminal state to On.
        string termText = "Welcome To The Console, Let's get you started installing some software\n"
            + "The AntiVirus toolkit is a helpful addition for getting rid of pesky malware!\n" + "Click On The Anti-Virus Installation to setup the connection address.";
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

    /// @brief Subscription handling to all 3 button events from the terminal.
    void OnEnable()
    {
        Terminal.OnAVPressed += AVTask;
        Terminal.OnLSPressed += LSTask;
        Terminal.OnNMAPPressed += NMAPTask;
    }

    void OnDisable()
    {
        Terminal.OnAVPressed -= AVTask;
        Terminal.OnLSPressed -= LSTask;
        Terminal.OnNMAPPressed -= NMAPTask;
    }

    /// @brief 3 Functions To Handle When The Terminal Buttons Are Pressed.
    void AVTask()
    {
        /// @var termLoadBar will be used for arrow game, how the loading progresses. 
        /// Different process and visual than the prefab loading bar.
        string termLoadBar = "--------------------------------";
        if (termState == State.On)
        {
            terminalText.text = termLoadBar;
            arrowGame.SetActive(true);
            ag.StartGame();
        }
    }

    void LSTask()
    {
        if(termState == State.On)
        {
            terminalText.text = "Scanning Files: Maze Game Initiate!";
        }
    }

    void NMAPTask()
    {

    }
}