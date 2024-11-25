using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DrawMaze : AbstractMinigame
{
    public PlayerInput pInput;
    public TMP_Text terminalText;

    public enum MazeProg {
        Begin, 
        A1, A2, A3, A4, A5,
        B1, B2, B3, B4, B5, 
        C1, C2, C3, C4, C5,
        D1, D2, D3, D4, D5,
        Ev, End
    }
    MazeProg mp = MazeProg.Begin;
    
    public void Awake() {
        pInput = new PlayerInput();
        terminalText = GameObject.Find("TInstructionTxt").GetComponent<TMP_Text>();
    }

    public override void StartGame(){
        gameObject.SetActive(true);
    }
    public string DrawA() {
        Debug.Log("A got pressed");
        return "Nothing happened uwu";
    }

    public string DrawB() {
        return "--------------\n";
    }

    public void HandleA(InputAction.CallbackContext context) {
        if(context.phase == InputActionPhase.Performed) {
            Draw('a');
        }
    }

    public void HandleB(InputAction.CallbackContext context) {
        if(context.phase == InputActionPhase.Performed) {
            Draw('b');
        }
    }

    public void HandleC(InputAction.CallbackContext context) {
        if(context.phase == InputActionPhase.Performed) {
            Draw('c');
        }
    }
    public void HandleD(InputAction.CallbackContext context) {
        if(context.phase == InputActionPhase.Performed) {
            Draw('d');
        }
    }

    public void Draw(char input) {
        switch(mp) {
            case MazeProg.Begin: {
                terminalText.text = DrawA();
                mp = MazeProg.A1;
                break;
            }
            case MazeProg.A1: {
                break;
            }
            case MazeProg.A2: {
                break;
            }
            case MazeProg.A3: {
                break;
            }
            case MazeProg.A4: {
                break;
            }
            case MazeProg.B1: {
                break;
            }
            case MazeProg.B2: {
                break;
            }
            case MazeProg.B3: {
                break;
            }
        }
    }


}
