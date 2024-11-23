using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Analytics;

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
        Off,
        StageOne,
        StageTwo
    }

    /// @var Terminal Variables that get references in awake() to the scene's terminal
    public GameObject terminal;
    public TMP_Text terminalText;
    public GameObject lsBtn;
    public GameObject avBtn;
    public GameObject nmapBtn;
    private State termState;
    public GameObject terminalPanel;

    // References to the arrow game to control its start and stop from the task
    public GameObject arrowGame;
    public Arrowgame ag;

    // Draw the maze for the LS Maze Game
    public DrawMaze drawMaze;

    // Audio source reference on TerminalWindow to play music on games.
    public AudioSource musicAG;


    /// @brief Assign all of the terminal objects in the scene.
    public void Awake() {
        arrowGame = FindObjectOfType<Arrowgame>().gameObject;
        terminal = GameObject.Find("WindowCanvas/TerminalWindow");
        terminalPanel = GameObject.Find("WindowCanvas/TerminalWindow/TerminalPanel");
        terminalText = GameObject.Find("WindowCanvas/TerminalWindow/TerminalPanel/TInstructionTxt").GetComponent<TMP_Text>();
        lsBtn = GameObject.Find("WindowCanvas/TerminalWindow/TerminalPanel/LSButton");
        avBtn = GameObject.Find("WindowCanvas/TerminalWindow/TerminalPanel/antiviralBtn");
        nmapBtn = GameObject.Find("WindowCanvas/TerminalWindow/TerminalPanel/nmapBtn");
        termState = State.Off;
        ag = arrowGame.GetComponent<Arrowgame>();
        drawMaze = GetComponent<DrawMaze>();
        musicAG = FindObjectOfType<Terminal>().GetComponentInParent<AudioSource>();
    }

    public new void Start() {
        base.Start();
        gameObject.SetActive(false);
    }

    // FIXME: Make this happen on input events instead. but another time I too tired now.
    public void Update() {
        checkHazards();
    }

    public override void checkHazards()
    {
        foreach (var hazardManager in hazardManagers)
        {
            if (!hazardManager.CanProgress())
            {
                ag.CanContinue = false;
            }
            else
            {
                ag.CanContinue = true;
            }
        }
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
        foreach (var hazardManager in hazardManagers) {
            hazardManager.StopHazard();
        }
    }

    public override void startHazards()
    {
        foreach (var hazardManager in hazardManagers) {
            hazardManager.StartHazard();
        }
    }

    /// @brief Subscription handling to all 3 button events from the terminal.
    void OnEnable()
    {
        Arrowgame.OnGameEnd += AVTaskP2;
        Terminal.OnAVPressed += AVTask;
        Terminal.OnLSPressed += LSTask;
        Terminal.OnNMAPPressed += NMAPTask;
    }

    void OnDisable()
    {
        Arrowgame.OnGameEnd -= AVTaskP2;
        Terminal.OnAVPressed -= AVTask;
        Terminal.OnLSPressed -= LSTask;
        Terminal.OnNMAPPressed -= NMAPTask;
    }

    /// @brief 3 Functions To Handle When The Terminal Buttons Are Pressed.
    void AVTask()
    {
        /// @var termLoadBar will be used for arrow game, how the loading progresses. 
        /// Different process and visual than the prefab loading bar.
        //string termLoadBar = "--------------------------------";
        if (termState == State.On)
        {
            terminalText.text = "";
            termState = State.StageOne;
            arrowGame.SetActive(true);
            ag.StartGame();
            if(!musicAG.isPlaying) {
                musicAG.Play();
            }
        }
        if (termState == State.StageOne) {
            terminalText.text = "";
        }
    }

    void AVTaskP2() {
        StartCoroutine(FadeOut(5));
        termState = State.On;
    }

    void LSTask()
    {
        if(termState == State.On)
        {
            termState = State.StageTwo;
            terminalText.text = "Scanning Files: Maze Game Initiate!";
            StartCoroutine(Timer(7));
            terminalText.text = "----| A |------| B |------| C |----\n";
            terminalText.text += "-----------------------------------\n";
            terminalText.text += "-----------------------------------\n";
            terminalText.text += "-----------------------------------\n";
            // TODO: Update maze game to work with input
            // Also have the else statement lead anywhere lol
            if(true) {
                terminalText.text = drawMaze.DrawA();
            }
            else {
                terminalText.text = drawMaze.DrawB();
            }
        }
        if (termState == State.StageOne) {
            terminalText.text = "";
        }
    }

    void NMAPTask()
    {

    }

    // Variable second timer
    private IEnumerator Timer(float x) {
        yield return new WaitForSeconds(x);
    }

    // Fades out music, passed in parameter is number of seconds to fadeout
    private IEnumerator FadeOut(float x) {
        while(musicAG.volume > 0) {
            musicAG.volume -= 1/(x*10);
            yield return new WaitForSeconds(0.1f);
        }
        if(musicAG.volume < 0) musicAG.Stop();  
    }
}