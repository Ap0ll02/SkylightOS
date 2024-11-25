using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

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
        ArrowGameOn,
        ArrowGameOff,
        MazeGameOn,
        MazeGameOff,
        StageTwo
    }

    /// @var Terminal Variables that get references in awake() to the scene's terminal
    public GameObject terminal;
    private List<bool> tasksDone = new List<bool>();
    public TMP_Text terminalText;
    public GameObject lsBtn;
    public GameObject hGroup;
    public GameObject avBtn;
    public GameObject nmapBtn;
    private State termState;
    public GameObject terminalPanel;

    // References to the arrow game to control its start and stop from the task
    public GameObject arrowGame;
    public Arrowgame ag;

    // Draw the maze for the LS Maze Game
    public GameObject drawMaze;
    public DrawMaze dm;

    // Audio source reference on TerminalWindow to play music on games.
    public AudioSource musicAG;


    /// @brief Assign all of the terminal objects in the scene.
    public void Awake() {
        arrowGame = FindObjectOfType<Arrowgame>().gameObject;
        terminal = FindObjectOfType<Terminal>().gameObject.GetComponentInParent<BasicWindow>().gameObject;
        terminalPanel = FindObjectOfType<Terminal>().gameObject;
        terminalText = terminalPanel.GetComponentInChildren<TMP_Text>();
        hGroup = FindObjectOfType<Terminal>().gameObject.GetComponentInChildren<HorizontalLayoutGroup>().gameObject;
        // lsBtn = GameObject.Find("LSBtn");
        // avBtn = GameObject.Find("antiviralBtn");
        // nmapBtn = GameObject.Find("nmapBtn");
        termState = State.Off;
        ag = arrowGame.GetComponent<Arrowgame>();
        drawMaze = FindObjectOfType<DrawMaze>().gameObject;
        dm = drawMaze.GetComponent<DrawMaze>();
        musicAG = FindObjectOfType<Terminal>().GetComponentInParent<AudioSource>();
        tasksDone.Add(false);
        tasksDone.Add(false);
    }

    public new void Start() {
        base.Start();
        gameObject.SetActive(false);
    }

    /// @brief Subscription handling to all 3 button events from the terminal.
    public void OnEnable()
    {
        Arrowgame.OnArrowPress += checkHazards;
        Arrowgame.OnGameEnd += AVTaskP2;
        Terminal.OnAVPressed += AVTask;
        Terminal.OnLSPressed += LSTask;
        Terminal.OnNMAPPressed += NMAPTask;
    }

    public void OnDisable()
    {
        Arrowgame.OnArrowPress -= checkHazards;
        Arrowgame.OnGameEnd -= AVTaskP2;
        Terminal.OnAVPressed -= AVTask;
        Terminal.OnLSPressed -= LSTask;
        Terminal.OnNMAPPressed -= NMAPTask;
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
        // TODO: Rename so maze game is the install file and arrow game is the download.

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

    /// @brief 3 Functions To Handle When The Terminal Buttons Are Pressed.
    public void AVTask()
    {
        /// @var termLoadBar will be used for arrow game, how the loading progresses. 
        /// Different process and visual than the prefab loading bar.
        if (termState == State.On)
        {
            terminalText.text = "";
            termState = State.ArrowGameOn;
            arrowGame.SetActive(true);
            startHazards();
            ag.StartGame();
            if(!musicAG.isPlaying) {
                musicAG.Play();
            }
        }
        else if (termState == State.ArrowGameOn) {
            terminalText.text = "";
        }
        else if (termState == State.MazeGameOn) {
            terminalText.text = terminalText.text;
        }
    }

    public void AVTaskP2() {
        StartCoroutine(FadeOut(5));
        stopHazards();
        termState = State.ArrowGameOff;
        // TODO: Ensure LS Task works
    }

    public void LSTask()
    {
        Debug.Log(termState);
        if(termState == State.On || termState == State.ArrowGameOff)
        {
            termState = State.MazeGameOn;
            terminalText.text = "Scanning Files: Maze Game Initiate!";
            terminalText.text += "Click A, B, C, or D for your options.";
            StartCoroutine(Timer(7f));
            terminalText.text = "---| A |---| B |---| C |---|D|---\n";
            terminalText.text += "Example Maze Stage, The Paths Correspond To A, B, C & D.\n";
            terminalText.text += "Please Press Any Key To Continue Into The Maze\n";
            terminalText.text += "-----------------------------------\n";
            hGroup.SetActive(false);
            dm.StartGame();
        }
        else if (termState == State.ArrowGameOn) {
            terminalText.text = "";
        }
        else if (termState == State.MazeGameOn) {
            terminalText.text = terminalText.text;
        }
        // TODO: Make hgroup buttons active again AFTER mazegame ends.
        // TODO: Do it with event like the Arrowgame does to signify end.
    }

    void NMAPTask()
    {
        if (termState == State.MazeGameOn) {
            terminalText.text = terminalText.text;
        }
        else if (termState == State.ArrowGameOn) {
            terminalText.text = "";
        }
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