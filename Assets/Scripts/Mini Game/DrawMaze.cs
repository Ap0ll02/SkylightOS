using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System;
using System.Linq;
/// <summary>
/// Jack Ratermann
/// Creates a maze drawn based on the filesystem. 
/// Depends on the file system, inode, the terminal, or area to draw it in.
/// See further comments to learn how files could be displayed in terminal
/// </summary>
public class DrawMaze : AbstractMinigame
{
    public static event Action OnGameEnd;
    public InputSystem_Actions pInput;
    // 0 = Not Allowed, 1 = Allowed, 2+ = Not Allowed Except Backspace
    public int inputAllowed = 0;
    public int curNode = 0;
    public FileSystem fs;
    public List<Inode> iMap;
    public TMP_Text terminalText;
    public GameObject evidenceImage;

    public enum MazeProg {
        Confirm, Begin, 
        A1, A2, A3, A4, A5,
        B1, B2, B3, B4, B5, 
        C1, C2, C3, C4, C5,
        D1, D2, D3, D4, D5,
        Ev, End
    }
    MazeProg mp = MazeProg.Confirm;
    
    public void Awake() {
        pInput = new InputSystem_Actions();
        terminalText = GameObject.Find("TInstructionTxt").GetComponent<TMP_Text>();
        fs = FindObjectOfType<FileSystem>();
        evidenceImage = GameObject.Find("TermImage");
        iMap = fs.inodeTable;
    }

    public void  Start() {
        gameObject.SetActive(false);
        evidenceImage.SetActive(false);
    }

    public override void StartGame(){
        gameObject.SetActive(true);
        inputAllowed = 1;
    }
    // Final Nodes So We Can Lock Input
    public List<int> finalNodes = new List<int>() {5, 6, 7, 8, 10, 11, 12, 13,
                    24, 25, 26, 27, 28, 29, 
                    30, 31, 32, 33, 34, 35, 36, 37, 
                    41, 42, 43, 44, 47, 48, 49, 
                    50, 51, 52, 53, 54};
    public string DrawLevel() {
        evidenceImage.SetActive(false);
        string level = "--- SKYLIGHT FILES ---\n\n\n\n";
        int counter = 0;
        char[] selector = new char[] {'A', 'B', 'C', 'D'};
        if(curNode == 29) {
            level = iMap[curNode].iName + "\n\n\n\n\n\n\n";
            evidenceImage.SetActive(true);
        }
        else if(iMap[curNode].numEntries == 0) {
            inputAllowed = 2;
            level += iMap[curNode].iName;
            if(curNode == 53) {
                OnGameEnd?.Invoke();
            }
        }
        else { 
            inputAllowed = 1;
            foreach(var child in iMap[curNode].iChildren) {
                level += "-|";
                level += selector[counter % 4] + ": " + child.iName;
                level += "|-";
                level += "\n";
                counter++;
            }
        }
        return level;
    }

    public void HandleA(InputAction.CallbackContext context) {
        if(inputAllowed == 1) {
            if(context.phase == InputActionPhase.Performed) {
                Debug.Log("INPUT: A " + curNode + " " + mp);
                mp = CheckProgress(curNode, 'a');
                Draw();
            }
        }
        
    }

    public void HandleB(InputAction.CallbackContext context) {
        if(inputAllowed == 1) {
            if(context.phase == InputActionPhase.Performed) {
                mp = CheckProgress(curNode, 'b');
                Draw(); 
            }
        }
    }

    public void HandleC(InputAction.CallbackContext context) {
        if(inputAllowed == 1) {
            if(context.phase == InputActionPhase.Performed) {
                mp = CheckProgress(curNode, 'c');
                Draw();
            }
        }
    }
    public void HandleD(InputAction.CallbackContext context) {
        if(inputAllowed == 1) {
            if(context.phase == InputActionPhase.Performed) {
                mp = CheckProgress(curNode, 'd');
                Draw();
            }
        }
    }

    public void HandleBack(InputAction.CallbackContext context) {
        if(inputAllowed >= 1) {
            if(context.phase == InputActionPhase.Performed) {
                if(iMap[curNode].iParent != null){ 
                    Debug.Log("Current iNode Parent: " + curNode);
                    curNode = iMap.IndexOf(iMap[curNode].iParent);
                    Debug.Log("New iNode Parent: " + curNode);
                }
                else curNode = 0;
                CheckProgress(curNode, 'x');
                Draw();
            }
        }
    }

    public MazeProg CheckProgress(int cNode, char c) {
        (int, MazeProg) checkNodeA(int cNode) =>
            cNode switch
            {
                0 => (cNode = 1, MazeProg.A1),
                1 => (cNode = 4, MazeProg.A2),
                4 => (cNode = 6, MazeProg.A3),
                6 => (cNode = 6, MazeProg.A3),
                9 => (cNode = 10, MazeProg.A4),
                2 => (cNode = 14, MazeProg.A2),
                14 => (cNode = 17, MazeProg.A3),
                15 => (cNode = 19, MazeProg.A3),
                17 => (cNode = 24, MazeProg.A4),
                18 => (cNode = 25, MazeProg.B4),
                19 => (cNode = 26, MazeProg.A4),
                20 => (cNode = 28, MazeProg.A4),
                21 => (cNode = 29, MazeProg.A4),
                29 => (cNode = 29, MazeProg.A4),
                16 => (cNode = 22, MazeProg.A3),
                22 => (cNode = 33, MazeProg.A4),
                3 => (cNode = 38, MazeProg.A2),
                30 => (cNode = 30, MazeProg.B4),
                31 => (cNode = 31, MazeProg.C4),
                32 => (cNode = 32, MazeProg.D4),
                38 => (cNode = 41, MazeProg.A3),
                40 => (cNode = 45, MazeProg.A3),
                45 => (cNode = 48, MazeProg.A4),
                46 => (cNode = 51, MazeProg.A4),
                _ => (cNode = 0, MazeProg.Begin)
            };
        (int, MazeProg) checkNodeB(int cNode) =>
            cNode switch
            {
                0 => (cNode = 2, MazeProg.B1),
                1 => (cNode = 5, MazeProg.B2),
                5 => (cNode = 5, MazeProg.B2),
                4 => (cNode = 7, MazeProg.B3),
                7 => (cNode = 7, MazeProg.B3),
                9 => (cNode = 11, MazeProg.B4),
                11 => (cNode = 11, MazeProg.B4),
                2 => (cNode = 15, MazeProg.B2),
                14 => (cNode = 18, MazeProg.B3),
                15 => (cNode = 20, MazeProg.B3),
                19 => (cNode = 27, MazeProg.B4),
                21 => (cNode = 30, MazeProg.B4),
                30 => (cNode = 30, MazeProg.B4),
                16 => (cNode = 23, MazeProg.B3),
                22 => (cNode = 34, MazeProg.B4),
                3 => (cNode = 39, MazeProg.B2),
                38 => (cNode = 42, MazeProg.B3),
                39 => (cNode = 44, MazeProg.B3),
                40 => (cNode = 46, MazeProg.B3),
                45 => (cNode = 49, MazeProg.B4),
                46 => (cNode = 52, MazeProg.B4),
                _ => (cNode = 0, MazeProg.Begin)
            };
        (int, MazeProg) checkNodeC(int cNode) =>
            cNode switch
            {
                0 => (cNode = 3, MazeProg.C1),
                4 => (cNode = 8, MazeProg.C3),
                8 => (cNode = 8, MazeProg.C3),
                9 => (cNode = 12, MazeProg.C4),
                12 => (cNode = 12, MazeProg.C4),
                2 => (cNode = 16, MazeProg.C2),
                15 => (cNode = 21, MazeProg.C3),
                21 => (cNode = 31, MazeProg.C4),
                31 => (cNode = 31, MazeProg.C4),
                22 => (cNode = 35, MazeProg.C4),
                3 => (cNode = 40, MazeProg.C2),
                38 => (cNode = 43, MazeProg.C3),
                45 => (cNode = 50, MazeProg.C4),
                46 => (cNode = 53, MazeProg.C4),
                _ => (cNode = 0, MazeProg.Begin)
            };
        (int, MazeProg) checkNodeD(int cNode) =>
            cNode switch
            {
                4 => (cNode = 9, MazeProg.D3),
                9 => (cNode = 13, MazeProg.D4),
                21 => (cNode = 32, MazeProg.D4),
                32 => (cNode = 32, MazeProg.D4),
                22 => (cNode = 36, MazeProg.D4),
                3 => (cNode = 41, MazeProg.D2),
                46 => (cNode = 54, MazeProg.D4),
                41 => (cNode = 47, MazeProg.D3),
                _ => (cNode = 0, MazeProg.Begin)
            };
        if(mp == MazeProg.Confirm) return MazeProg.Begin;
        else if(c == 'a') {
            (curNode, mp) = checkNodeA(cNode);
        }
        else if(c == 'b') {
            (curNode, mp) = checkNodeB(cNode);
        }
        else if(c == 'c') {
            (curNode, mp) = checkNodeC(cNode);
        }
        else if(c == 'd') {
            (curNode, mp) = checkNodeD(cNode);
        }

        return mp;
    }

    public void Draw() {
        // Determine maze progress before sending draw function request
        // Request a level to be drawn depending on the level and direction, or MazeProg
        terminalText.text = DrawLevel();
    }
}