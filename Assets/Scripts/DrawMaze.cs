using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System;

public class DrawMaze : AbstractMinigame
{
    public PlayerInput pInput;
    public TMP_Text terminalText;
    public string[] dirNames = new string[] {"Documents", "Pictures", "Movies", "SkylightOS", "NorthstarAI", 
    "Homework", "Emails", "Passwords", "Birds", "Shopping Lists", "Batteries", "Grandchildren", "Fruit", "Browsers", "Money", "Football", "School", "College", "Reward",
    "Work", "Risk", "Portrait", "Refrigerator", "Home", "Repairs", "Warranty Information", "Cleaning Supplies", "Share", "Cool Apps", "Extra Folder", "Songs", "Music", "Dubstep",
    "Country", "Banana", "Aurora", "Cascade", "Nebula", "Eclipse", "Horizon", "Vortex", "Zephyr", "Solace", "Mirage", "Nimbus", 
    "Tempest", "Luminous", "Serenity", "Ethereal", "Haven", "Harbor", "Glacier", "Echo", "Nova", "Celestial", 
    "Radiant", "Verdant", "Aurorae", "Halcyon", "Ember", "Quasar", "Aether", "Boreal", "Frost", "Prism", 
    "Torrent", "Zenith", "Chroma", "Lumina", "Cascade2", "Wanderer", "Eclipse2", "Sequoia", "Sylvan", 
    "Equinox", "Eldritch", "Drift", "Summit", "Verdure", "Crystalline", "Solstice", "Vale", "Mariner", "Nocturne"};

    public enum MazeProg {
        Begin, 
        A1, A2, A3, A4, A5,
        B1, B2, B3, B4, B5, 
        C1, C2, C3, C4, C5,
        D1, D2, D3, D4, D5,
        Ev, End
    }
    MazeProg mp = MazeProg.Begin;
    public List<char> path = new();
    public int[,] entryMatrix = {{2, 4, 1, 1, 1, 1, 4, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0}, 
                                {3, 2, 3, 2, 1, 1, 1, 1, 4, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1}, 
                                {4, 3, 1, 2, 1, 1, 1, 1, 3, 4, 1, 1, 1, 1, 1, 1, 1, 0, 0}};
    
    public void Awake() {
        //path.Add('X');
        pInput = new PlayerInput();
        terminalText = GameObject.Find("TInstructionTxt").GetComponent<TMP_Text>();
    }

    public void  Start() {
        gameObject.SetActive(false);
    }

    public override void StartGame(){
        gameObject.SetActive(true);
    }
    public int fileNameCounter = 0;
    public string DrawLevel(MazeProg lvl, int numPaths) {
        string level= "";
        char[] selector = new char[] {'A', 'B', 'C', 'D'};
        // TODO: If above is good route path to evidence and password help
        // TODO: Route Other Paths To Dead Ends
        // TODO: Ensure Going Backwards Works
        Debug.Log("Previous: " + lvl);
        Debug.Log("Current: " + mp);
        if(lvl == MazeProg.Begin && mp == MazeProg.Begin) {
            for(int i = 0; i < 3; i++) {
                level += "-|";
                level += selector[i % 4] + ": " + dirNames[i];
                level += "|-";
                fileNameCounter++;
            }
            level += "\n";
        }
        // Path To I
        else if(mp == MazeProg.C1 && lvl == MazeProg.Begin) {
            for(int i = 0; i < entryMatrix[2,0]; i++) {
                level += "-|";
                level += selector[i % 4] + ": " + dirNames[i+fileNameCounter];
                level += "|-";
                fileNameCounter++;
            }
            level += "\n";
        }
        else if(mp == MazeProg.C2 && lvl == MazeProg.C1) {
            for(int i = 0; i < entryMatrix[2,3]; i++) {
                fileNameCounter++;
                level += "-|";
                level += selector[i % 4] + ": " + dirNames[i+fileNameCounter];
                level += "|-";
                fileNameCounter++;
            }
        }
        else if(mp == MazeProg.B3 && lvl == MazeProg.C2) {
            for(int i = 0; i < entryMatrix[2,9]; i++) {
                fileNameCounter++;
                level += "-|";
                level += selector[i % 4] + ": " + dirNames[i+fileNameCounter];
                level += "|-";
                fileNameCounter++;
            }
        }
        else if(mp == MazeProg.C4 && lvl == MazeProg.B3) {
            level = "-----|";
            level += "AntiVirus Installer Application";
            level += "|-----";
        }

        // Path To E
        else if(mp == MazeProg.B1 && lvl == MazeProg.Begin) {
            for(int i = 0; i < entryMatrix[1,0]; i++) {
                fileNameCounter++;
                level += "-|";
                level += selector[i % 4] + ": " + dirNames[i+fileNameCounter];
                level += "|-";
                fileNameCounter++;
            }
        }
        else if(mp == MazeProg.B2 && lvl == MazeProg.B1) {
            for(int i = 0; i < entryMatrix[1,2]; i++) {
                fileNameCounter++;
                level += "-|";
                level += selector[i % 4] + ": " + dirNames[i+fileNameCounter];
                level += "|-";
                fileNameCounter++;
            }
        }
        else if(mp == MazeProg.C3 && lvl == MazeProg.B2) {
            for(int i = 0; i < entryMatrix[1,8]; i++) {
                fileNameCounter++;
                level += "-|";
                level += selector[i % 4] + ": " + dirNames[i+fileNameCounter];
                level += "|-";
                fileNameCounter++;
            }
        }
        else if(mp == MazeProg.A4 && lvl == MazeProg.C3) {
            level = "-----|";
            level += "Evidence File Here, TBD";
            level += "|-----";
        }

        // Path to P
        else if(mp == MazeProg.B1 && lvl == MazeProg.Begin) {
            for(int i = 0; i < entryMatrix[1,0]; i++) {
                fileNameCounter++;
                level += "-|";
                level += selector[i % 4] + ": " + dirNames[i+fileNameCounter];
                level += "|-";
                fileNameCounter++;
            }
        }
        else if(mp == MazeProg.C2 && lvl == MazeProg.B1) {
            for(int i = 0; i < entryMatrix[1,3]; i++) {
                fileNameCounter++;
                level += "-|";
                level += selector[i % 4] + ": " + dirNames[i+fileNameCounter];
                level += "|-";
                fileNameCounter++;
            }
        }
        else if(mp == MazeProg.A3 && lvl == MazeProg.C2) {
            for(int i = 0; i < entryMatrix[1,9]; i++) {
                fileNameCounter++;
                level += "-|";
                level += selector[i % 4] + ": " + dirNames[i+fileNameCounter];
                level += "|-";
                fileNameCounter++;
            }
        }
        else if(mp == MazeProg.D4 && lvl == MazeProg.A3) {
            level = "-----|";
            level += "Password Help Here.";
            level += "|-----";
        }
        return level;
    }

    public void HandleA(InputAction.CallbackContext context) {
        if(context.phase == InputActionPhase.Performed) {
            Debug.Log("INPUT: A");
            path.Add('a');
            Draw();
        }
    }

    public void HandleB(InputAction.CallbackContext context) {
        if(context.phase == InputActionPhase.Performed) {
             Debug.Log("INPUT: B");
            path.Add('b');
            Draw();
        }
    }

    public void HandleC(InputAction.CallbackContext context) {
        if(context.phase == InputActionPhase.Performed) {
             Debug.Log("INPUT: C");
            path.Add('c');
            Draw();
        }
    }
    public void HandleD(InputAction.CallbackContext context) {
        if(context.phase == InputActionPhase.Performed) {
             Debug.Log("INPUT: D");
            path.Add('d');
            Draw();
        }
    }
    public int CalcPathEntries(MazeProg[] mpArr) {
        return 0;
    }
    // public MazeProg Draw(int preLvl, char preDir, int curLvl, char curDir) {
    //     // TODO: Find a way to get the previous and current MazeProg returned as a tuple or out to handleBack
    //     return CheckProgress(preLvl, preDir);
    // }
    public void HandleBack(InputAction.CallbackContext context) {
        if(context.phase == InputActionPhase.Performed) {
            Debug.Log("INPUT: BACK");
            if(path.Count - 1 > 1) path.RemoveAt(path.Count - 1);
            // int preLvl;
            // char preDir;
            // int curLvl;
            // char curDir;
            // Draw(preLvl, preDir, curLvl, curDir);
            MazeProg[] backUpProg = new MazeProg[] {MazeProg.Begin, MazeProg.Begin};
            fileNameCounter -= CalcPathEntries(backUpProg);
        }
    }

    public MazeProg CheckProgress(int level, char direction) {
        if(direction == 'a') {
            if(level == 1) {
                return MazeProg.A1;
            }
            else if(level == 2) {
                return MazeProg.A2;
            }
            else if(level == 3) {
                return MazeProg.A3;
            }
            else if(level == 4) {
                return MazeProg.A4;
            }
            else {
                return MazeProg.Begin;
            }
        }
        else if(direction == 'b') {
            if(level == 1) {
                return MazeProg.B1;
            }
            else if(level == 2) {
                return MazeProg.B2;
            }
            else if(level == 3) {
                return MazeProg.B3;
            }
            else if(level == 4) {
                return MazeProg.B4;
            }
            else {
                return MazeProg.Begin;
            }
        }
        else if(direction == 'c') {
            if(level == 1) {
                return MazeProg.C1;
            }
            else if(level == 2) {
                return MazeProg.C2;
            }
            else if(level == 3) {
                return MazeProg.C3;
            }
            else if(level == 4) {
                return MazeProg.C4;
            }
            else {
                return MazeProg.Begin;
            }
        }
        else if(direction == 'd') {
            if(level == 1) {
                return MazeProg.D1;
            }
            else if(level == 2) {
                return MazeProg.D2;
            }
            else if(level == 3) {
                return MazeProg.D3;
            }
            else if(level == 4) {
                return MazeProg.D4;
            }
            else {
                return MazeProg.Begin;
            }
        }
        else {
            Debug.Log("didn't work right");
            return MazeProg.Begin;
        }
    }

    // NOTES: 
    // Evidence Located At B1-B2-C3-A4
    // Password Help At B1-C2-A3-D4
    // Install At C1-C2-B3-C4
    public void Draw() {
        MazeProg prevLvl;
        // Determine maze progress before sending draw function request
        int level = path.Count-1;
        char direction = path[path.Count - 1];
        int preLevel;
        char preDirection;
        try{
            preLevel = path.Count-2;
            preDirection = path[path.Count - 2];
        } catch (Exception e) when (e is ArgumentOutOfRangeException) {
            Debug.Log("First Time Around Here, Huh?");
            preLevel = 0;
            preDirection = '\0';
        }

        prevLvl = CheckProgress(preLevel, preDirection);
        mp = CheckProgress(level, direction);
        // Request a level to be drawn depending on the level and direction, or MazeProg
        switch(mp) {
            case MazeProg.Begin: {
                terminalText.text = DrawLevel(MazeProg.Begin, 3);
                break;
            }
            case MazeProg.A1: {
                terminalText.text = DrawLevel(prevLvl, 2);
                break;
            }
            case MazeProg.A2: {
                terminalText.text = DrawLevel(prevLvl, 4);
                break;
            }
            case MazeProg.A3: {
                terminalText.text = DrawLevel(prevLvl, 1);
                break;
            }
            case MazeProg.A4: {
                terminalText.text = DrawLevel(prevLvl, 1);
                break;
            }
            case MazeProg.B1: {
                terminalText.text = DrawLevel(prevLvl, 3);
                break;
            }
            case MazeProg.B2: {
                terminalText.text = DrawLevel(prevLvl, 3);
                break;
            }
            case MazeProg.B3: {
                terminalText.text = DrawLevel(prevLvl, 4);
                break;
            }
            case MazeProg.C1: {
                terminalText.text = DrawLevel(prevLvl, 4);
                break;
            }
            case MazeProg.C2: {
                terminalText.text = DrawLevel(prevLvl, 2);
                break;
            }
            case MazeProg.C3: {
                terminalText.text = DrawLevel(prevLvl, 4);
                break;
            }
            case MazeProg.C4: {
                terminalText.text = DrawLevel(prevLvl, 1);
                break;
            }
            case MazeProg.D1: {
                terminalText.text = DrawLevel(prevLvl, 0);
                break;
            }
            case MazeProg.D2: {
                terminalText.text = DrawLevel(prevLvl, 1);
                break;
            }
            case MazeProg.D3: {
                terminalText.text = DrawLevel(prevLvl, 4);
                break;
            }
            case MazeProg.D4: {
                terminalText.text = DrawLevel(prevLvl, 1);
                break;
            }
        }
    }
}