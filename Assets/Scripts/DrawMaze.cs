using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System;
/// <summary>
/// Jack Ratermann
/// Creates a maze drawn based on the filesystem. 
/// Depends on the file system, inode, the terminal, or area to draw it in.
/// See further comments to learn how files could be displayed in terminal
/// </summary>
public class DrawMaze : AbstractMinigame
{
    public PlayerInput pInput;
    public FileSystem fs;
    public List<Inode> iMap;
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
    
    public void Awake() {
        //path.Add('X');
        pInput = new PlayerInput();
        terminalText = GameObject.Find("TInstructionTxt").GetComponent<TMP_Text>();
        fs = FindObjectOfType<FileSystem>();
        iMap = fs.inodeTable;
    }

    public void  Start() {
        gameObject.SetActive(false);
    }

    public override void StartGame(){
        gameObject.SetActive(true);
    }
    public int fileNameCounter = 0;
    public string DrawLevel(MazeProg lvl) {
        string level= "";
        char[] selector = new char[] {'A', 'B', 'C', 'D'};
        // TODO: If above is good route path to evidence and password help
        // TODO: Route Other Paths To Dead Ends
        // TODO: Ensure Going Backwards Works
        Debug.Log("Previous: " + lvl);
        Debug.Log("Current: " + mp);
        if(lvl == MazeProg.Begin && mp == MazeProg.Begin) {
            for(int i = 0; i < iMap[0].numEntries; i++) {
                level += "-|";
                level += selector[i % 4] + ": " + iMap[0].iChildren[i].iName;
                level += "|-";
                fileNameCounter++;
            }
            level += "\n";
        }
        // Path To I
        else if(mp == MazeProg.C1 && lvl == MazeProg.Begin) {
            for(int i = 0; i < iMap[3].numEntries; i++) {
                level += "-|";
                level += selector[i % 4] + ": " + iMap[3].iChildren[i].iName;
                level += "|-";
                fileNameCounter++;
            }
            level += "\n";
        }
        else if(mp == MazeProg.C2 && lvl == MazeProg.C1) {
            for(int i = 0; i < iMap[40].numEntries; i++) {
                level += "-|";
                level += selector[i % 4] + ": " + iMap[40].iChildren[i].iName;
                level += "|-";
                fileNameCounter++;
            }
            level += "\n";
        }
        else if(mp == MazeProg.B3 && lvl == MazeProg.C2) {
            for(int i = 0; i < iMap[46].numEntries; i++) {
                level += "-|";
                level += selector[i % 4] + ": " + iMap[46].iChildren[i].iName;
                level += "|-";
                fileNameCounter++;
            }
            level += "\n";
        }
        // ANY: else if that has this form (No Loop) could have the level += iMap[ind].iName changed.
        // This is a lone file page so it could choose to display the file instead. This is the general idea
        // to update to once the routing has been finished.
        // TODO: Update All These File Displays Once Routing Is Finished
        else if(mp == MazeProg.C4 && lvl == MazeProg.B3) {
            level = "-----|";
            level += iMap[53].iName;
            level += "|-----";
        }

        // Path To E
        else if(mp == MazeProg.B1 && lvl == MazeProg.Begin) {
            for(int i = 0; i < iMap[2].numEntries; i++) {
                level += "-|";
                level += selector[i % 4] + ": " + iMap[2].iChildren[i].iName;
                level += "|-";
                fileNameCounter++;
            }
            level += "\n";
        }
        else if(mp == MazeProg.B2 && lvl == MazeProg.B1) {
            for(int i = 0; i < iMap[15].numEntries; i++) {
                level += "-|";
                level += selector[i % 4] + ": " + iMap[15].iChildren[i].iName;
                level += "|-";
                fileNameCounter++;
            }
            level += "\n";
        }
        else if(mp == MazeProg.C3 && lvl == MazeProg.B2) {
            for(int i = 0; i < iMap[21].numEntries; i++) {
                level += "-|";
                level += selector[i % 4] + ": " + iMap[21].iChildren[i].iName;
                level += "|-";
                fileNameCounter++;
            }
            level += "\n";
        }
        else if(mp == MazeProg.A4 && lvl == MazeProg.C3) {
            level = "-----|";
            level += iMap[29].iName;
            level += "|-----";
        }
        else if(mp == MazeProg.B4 && lvl == MazeProg.C3) {
            level = "-----|";
            level += iMap[30].iName;
            level += "|-----";
        }
        else if(mp == MazeProg.C4 && lvl == MazeProg.C3) {
            level = "-----|";
            level += iMap[31].iName;
            level += "|-----";
        }
        else if(mp == MazeProg.D4 && lvl == MazeProg.C3) {
            level = "-----|";
            level += iMap[32].iName;
            level += "|-----";
        }

        // Path to P
        else if(mp == MazeProg.B1 && lvl == MazeProg.Begin) {
            for(int i = 0; i < iMap[2].numEntries; i++) {
                level += "-|";
                level += selector[i % 4] + ": " + iMap[2].iChildren[i].iName;
                level += "|-";
                fileNameCounter++;
            }
            level += "\n";
        }
        else if(mp == MazeProg.C2 && lvl == MazeProg.B1) {
            for(int i = 0; i < iMap[16].numEntries; i++) {
                level += "-|";
                level += selector[i % 4] + ": " + iMap[16].iChildren[i].iName;
                level += "|-";
                fileNameCounter++;
            }
            level += "\n";
        }
        else if(mp == MazeProg.B3 && lvl == MazeProg.C2) {
            level = "-----|";
            level += iMap[23].iName;
            level += "|-----";
        }
        else if(mp == MazeProg.A3 && lvl == MazeProg.C2) {
            for(int i = 0; i < iMap[22].numEntries; i++) {
                level += "-|";
                level += selector[i % 4] + ": " + iMap[22].iChildren[i].iName;
                level += "|-";
                fileNameCounter++;
            }
            level += "\n";
        }
        else if(mp == MazeProg.A4 && lvl == MazeProg.A3) {
            level = "-----|";
            level += iMap[33].iName;
            level += "|-----";
        }
        else if(mp == MazeProg.B4 && lvl == MazeProg.A3) {
            level = "-----|";
            level += iMap[34].iName;
            level += "|-----";
        }
        else if(mp == MazeProg.C4 && lvl == MazeProg.A3) {
            level = "-----|";
            level += iMap[35].iName;
            level += "|-----";
        }
        else if(mp == MazeProg.D4 && lvl == MazeProg.A3) {
            level = "-----|";
            level += iMap[36].iName;
            level += "|-----";
        }
        // Other Paths. Starting With A: User
        else if(mp == MazeProg.A1 && lvl == MazeProg.Begin) {
            for(int i = 0; i < iMap[0].numEntries; i++) {
                level += "-|";
                level += selector[i % 4] + ": " + iMap[0].iChildren[i].iName;
                level += "|-";
                fileNameCounter++;
            }
            level += "\n";
        }
        // Settings & Theme
        else if(mp == MazeProg.A2 && lvl == MazeProg.A1) {
            for(int i = 0; i < iMap[4].numEntries; i++) {
                level += "-|";
                level += selector[i % 4] + ": " + iMap[4].iChildren[i].iName;
                level += "|-";
                fileNameCounter++;
            }
            level += "\n";
        }
        else if(mp == MazeProg.B2 && lvl == MazeProg.A1) {
            level = "-----|";
            level += iMap[5].iName;
            level += "|-----";
        }
        // Cursors, Languages, Keyboard, Network
        else if(mp == MazeProg.A3 && lvl == MazeProg.A2) {
            level = "-----|";
            level += iMap[6].iName;
            level += "|-----";
        }
        else if(mp == MazeProg.B3 && lvl == MazeProg.A2) {
            level = "-----|";
            level += iMap[7].iName;
            level += "|-----";
        }
        else if(mp == MazeProg.C3 && lvl == MazeProg.A2) {
            level = "-----|";
            level += iMap[8].iName;
            level += "|-----";
        }
        else if(mp == MazeProg.D3 && lvl == MazeProg.A2) {
            for(int i = 0; i < iMap[9].numEntries; i++) {
                level += "-|";
                level += selector[i % 4] + ": " + iMap[9].iChildren[i].iName;
                level += "|-";
                fileNameCounter++;
            }
            level += "\n";
        }
        // Servers, Wi-FI, Ethernet, Advanced
        else if(mp == MazeProg.A4 && lvl == MazeProg.D3) {
            level = "-----|";
            level += iMap[10].iName;
            level += "|-----";
        }
        else if(mp == MazeProg.B4 && lvl == MazeProg.D3) {
            level = "-----|";
            level += iMap[11].iName;
            level += "|-----";
        }
        else if(mp == MazeProg.C4 && lvl == MazeProg.D3) {
            level = "-----|";
            level += iMap[12].iName;
            level += "|-----";
        }
        else if(mp == MazeProg.D4 && lvl == MazeProg.D3) {
            level = "-----|";
            level += iMap[13].iName;
            level += "|-----";
        }
        // Perry Music {Classical, Dubstep}
        else if(mp == MazeProg.A2 && lvl == MazeProg.B1) {
            for(int i = 0; i < iMap[14].numEntries; i++) {
                level += "-|";
                level += selector[i % 4] + ": " + iMap[14].iChildren[i].iName;
                level += "|-";
                fileNameCounter++;
            }
            level += "\n";
        }
        else if(mp == MazeProg.A3 && lvl == MazeProg.A2) {
            level = "-----|";
            level += iMap[17].iName;
            level += "|-----";
        }
        else if(mp == MazeProg.B3 && lvl == MazeProg.A2) {
            level = "-----|";
            level += iMap[18].iName;
            level += "|-----";
        }
        // Perry: Taxes, Venture Ideas
        else if(mp == MazeProg.A3 && lvl == MazeProg.B2) {
            for(int i = 0; i < iMap[19].numEntries; i++) {
                level += "-|";
                level += selector[i % 4] + ": " + iMap[19].iChildren[i].iName;
                level += "|-";
                fileNameCounter++;
            }
            level += "\n";
        }
        else if(mp == MazeProg.A4 && lvl == MazeProg.A3) {
            level = "-----|";
            level += iMap[26].iName;
            level += "|-----";
        }
        else if(mp == MazeProg.B4 && lvl == MazeProg.A3) {
            level = "-----|";
            level += iMap[27].iName;
            level += "|-----";
        }
        else if(mp == MazeProg.B3 && lvl == MazeProg.B2) {
            level = "-----|";
            level += iMap[28].iName;
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

    public void HandleBack(InputAction.CallbackContext context) {
        if(context.phase == InputActionPhase.Performed) {
            if(path.Count > 0) {
                path.Remove(path[^1]);
                Draw();
            }
            Draw();
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
                terminalText.text = DrawLevel(MazeProg.Begin);
                break;
            }
            case MazeProg.A1: {
                terminalText.text = DrawLevel(prevLvl);
                break;
            }
            case MazeProg.A2: {
                terminalText.text = DrawLevel(prevLvl);
                break;
            }
            case MazeProg.A3: {
                terminalText.text = DrawLevel(prevLvl);
                break;
            }
            case MazeProg.A4: {
                terminalText.text = DrawLevel(prevLvl);
                break;
            }
            case MazeProg.B1: {
                terminalText.text = DrawLevel(prevLvl);
                break;
            }
            case MazeProg.B2: {
                terminalText.text = DrawLevel(prevLvl);
                break;
            }
            case MazeProg.B3: {
                terminalText.text = DrawLevel(prevLvl);
                break;
            }
            case MazeProg.C1: {
                terminalText.text = DrawLevel(prevLvl);
                break;
            }
            case MazeProg.C2: {
                terminalText.text = DrawLevel(prevLvl);
                break;
            }
            case MazeProg.C3: {
                terminalText.text = DrawLevel(prevLvl);
                break;
            }
            case MazeProg.C4: {
                terminalText.text = DrawLevel(prevLvl);
                break;
            }
            case MazeProg.D1: {
                terminalText.text = DrawLevel(prevLvl);
                break;
            }
            case MazeProg.D2: {
                terminalText.text = DrawLevel(prevLvl);
                break;
            }
            case MazeProg.D3: {
                terminalText.text = DrawLevel(prevLvl);
                break;
            }
            case MazeProg.D4: {
                terminalText.text = DrawLevel(prevLvl);
                break;
            }
        }
    }
}