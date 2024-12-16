using System.Collections;
using UnityEngine;
using System.Text.RegularExpressions;
using System;
using TMPro;
using System.Collections.Generic;
using UnityEngine.InputSystem;
//using TMPro.EditorUtilities;
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
    /// @var twinName The name of the terminal window, change here if you did change your window name.
    string twinName = "TerminalWindow";

    public GameObject usrInput;

    /// @var TInstructionTxt The text that covers the terminal that can be used to show instructions or text.
    [SerializeField] TMP_Text TInstructionTxt;

    /// @var TWindow The terminal window variant, so it may be inactive upon start.
    public GameObject TWindow;

    /// @var TInputHeading Standard Terminal Header Prior To Input (ex: user@computer >)
    [SerializeField] TMP_Text TInputHeading;
    Coroutine lbp;

    /// @var IntroText A String to be displayed upoon start for terminal. 
    private string FirstText = "Welcome to ClearSky Console.\n We are testing multi-line editing tbh.";
    public TMP_Text autoFill;

    /// @var OnAVPressed event and Action delegate variable setup.
    public static event Action OnAVPressed;
    public static event Action OnNMAPPressed;
    public static event Action OnLSPressed;
    public int iterCount;
    TMP_Text caret;
    public void Awake()
    {
        TWindow = GameObject.Find(twinName);
        TWindow.SetActive(true);
        caret = GameObject.Find("Caret").GetComponent<TMP_Text>();
        autoFill = GameObject.Find("AutoFill").GetComponentInChildren<TMP_Text>();
    }

    public void Start()
    {
        TInstructionTxt.horizontalAlignment = HorizontalAlignmentOptions.Center; 
        TInstructionTxt.text = FirstText;
        TWindow.SetActive(false);
    }

    public void ListFilesExec()
    {
        try {
            StopCoroutine(lbp);
        } catch (Exception e) when (e is NullReferenceException) {
            Debug.Log("Coroutine Finished. Now Null");
        }
        lbp = null;
        TInstructionTxt.horizontalAlignment = HorizontalAlignmentOptions.Center; 
        // All code above the invoke line is what happens if the terminal task does not override.
        TInstructionTxt.text = "Files: \n";

        // Allows the TerminalTask to modify terminal commands if Task State is On
        OnLSPressed?.Invoke();
    }

    public void AntiVirusExec()
    {
        try {
            StopCoroutine(lbp);

        } catch (Exception e) when (e is NullReferenceException) {
            Debug.Log("Coroutine Finished. Now Null");
        }
        lbp = null;
        TInstructionTxt.horizontalAlignment = HorizontalAlignmentOptions.Center; 
        // All code above the invoke line is what happens if the terminal task does not override.
        TInstructionTxt.text = "AntiVirus: \n";

        // Allows the TerminalTask to modify terminal commands if Task State is On
        OnAVPressed?.Invoke();
    }
    int count = 0;
    public void NMapExec()
    {
        count = 0;
        TInstructionTxt.horizontalAlignment = HorizontalAlignmentOptions.Center; 
        // All code above the invoke line is what happens if the terminal task does not override.
        TInstructionTxt.text = "Mapping Ports on Local Network\n";
        iterCount = UnityEngine.Random.Range(1, 5);
        lbp ??= StartCoroutine(TerminalLoading());
        // Allows the TerminalTask to modify terminal commands if Task State is On
        OnNMAPPressed?.Invoke();
    }

    bool firstCharacter = true;
    Stack inpHist = new();
    int lCount = 0;
    int sCount = 0;
    int nCount = 0;

    public void CheckInput() {
        string nmapPattern = @"^(?:nmap)(\s+)(\w+)$";
        TMP_InputField uTxt = usrInput.GetComponent<TMP_InputField>();

        if(uTxt.text.ToLower().Trim() == "ls") {
            ListFilesExec();
        }
        else if(Regex.IsMatch(uTxt.text.ToLower(), nmapPattern)) {
            NMapExec();
        }
        else if(uTxt.text.ToLower().Trim() == "solar -i antivirus") {
            AntiVirusExec();
        }
        else if(uTxt.text.ToLower().Trim() == "ls") {
            
        }
        else {
            uTxt.text = "";
            uTxt.placeholder.GetComponent<TMP_Text>().text = "Unable To Recognize Command. Try Again.";
            return;
        }

        uTxt.text = "";
        uTxt.placeholder.GetComponent<TMP_Text>().text = "Enter Command";
        firstCharacter = true;
        lCount = 0;
        sCount = 0;
        nCount = 0;
        autoFill.text = "";
    }

    public void HandleUserInput(string input)
    {
        TMP_InputField uTxt = usrInput.GetComponent<TMP_InputField>();
        uTxt.text = input;
        canTab = true;
        CheckInput();
    }

    public void HandleCtrlC() {
        // TMP_InputField uTxt = usrInput.GetComponent<TMP_InputField>();
        // uTxt.text = "";
        // uTxt.placeholder.GetComponent<TMP_Text>().text = "Enter Command";
        // firstCharacter = true;
        // lCount = 0;
        // sCount = 0;
        // nCount = 0;
        // autoFill.text = "";
        // canTab = true;
    }
    bool canTab = true;
    public void HandleTab() {
        // TODO: Fix tab entering like 4 times
        TMP_InputField uTxt = usrInput.GetComponent<TMP_InputField>();
        if (canTab) {
            canTab = false;
            uTxt.text = tabCompTxt;
            uTxt.caretPosition = tabCompTxt.Length + 1; 
        }
    }

    string tabCompTxt;
    public void AutoCorrect(string input = null) {
        string solarPattern1 = @"^(?:solar)(\s+)$";
        string solarPattern2 = @"^(?:solar)(\s+)(?:-i)(\s+)$";
        TMP_InputField uTxt = usrInput.GetComponent<TMP_InputField>();

        caret.text = "";
        for (int i = 0; i < uTxt.caretPosition; i++) {
            caret.text += " ";
        }
        caret.text += "|";

        if(input == "s") {
            autoFill.text = "solar";
            tabCompTxt = "solar";
        }
        else if(Regex.IsMatch(uTxt.text, solarPattern1)) {
            autoFill.text = "-i";
            tabCompTxt = "solar -i";
        }
        else if(Regex.IsMatch(uTxt.text, solarPattern2)) {
            autoFill.text = "antivirus";
            tabCompTxt = "solar -i antivirus";
        }
        canTab = true;
    }

    public IEnumerator TerminalLoading() {
        while(count < 20 * iterCount) {
            count++;
            TInstructionTxt.text += "*";
            if(count % 20 == 0) {
                TInstructionTxt.text = "Mapping Ports on Local Network\n";
                TInstructionTxt.text += "*";
            }
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.005f, 0.65f));
        }
        TInstructionTxt.horizontalAlignment = HorizontalAlignmentOptions.Left; 
        TInstructionTxt.text = "Starting Nmap on Your Local Network:\n";
        TInstructionTxt.text += "Ports Scanned:\n";
        TInstructionTxt.text += "PORT       STATE     SERVICE\n";
        TInstructionTxt.text += "21/tcp     closed    ftp\n";
        TInstructionTxt.text += "80/tcp     open      http\n";
        TInstructionTxt.text += "121/tcp    open      SkyFile\n";
        TInstructionTxt.text += "145/tcp    closed    Normal Service :D\n";
        TInstructionTxt.text += "456/tcp    closed    CompanyLunch Server\n";
        TInstructionTxt.text += "3421/tcp   open      Ok\n";
        lbp = null;
    }
}


        // TMP_InputField uTxt = usrInput.GetComponent<TMP_InputField>();
        // autoFill.text = "";
        // List<List<string>> commandList = new();
        // List<string> l = new()
        // {
        //     "ls"
        // };
        // List<string> s = new()
        // {
        //     "solar",
        //     "solar -i",
        //     "solar -i antivirus"
        // };
        // List<string> n = new()
        // {
        //     "nmap"
        // };
        // commandList.Add(l);
        // commandList.Add(s);
        // commandList.Add(n);
        // string solarPattern = @"^(?:solar)(\s+)$";
        // if(input == "l") {
        //     autoFill.text = l[0];
        //     lCount++;
        //     inpHist.Push("l");
        // }
        // else if(input == "s") {
        //     autoFill.text = s[sCount];
        //     inpHist.Push("s");
        // }
        // else if(input == "n") {
        //     autoFill.text = n[0];
        //     nCount++;
        //     inpHist.Push("n");
        // }
        // else if(input == "\b") {
        //     lCount = 0;
        //     sCount = 0;
        //     nCount = 0;
        //     inpHist.Pop();
        // }
        // else if(input == null){
        //     foreach (var list in commandList) {
        //         autoFill.text += list[0] + "\n";
        //     }
        //     return;
        // }
        // else if(Regex.IsMatch(uTxt.text, solarPattern)) {
        //     sCount++;
        //     autoFill.text += s[sCount];
        // }
        // firstCharacter = false;
        // canTab = true;