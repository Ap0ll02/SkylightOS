using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/**
 * @author Jack Ratermann
 * @date 11/03/2024
 * @version 0.1
 * @brief This is the terminal class that parses, executes and helps show output. This can be used in conjunction 
 * with the terminal task. 
 * @note This class will not have all actual terminal commands.
 */

/// Commands Available:

public class Terminal : AbstractTask
{
    /// @var TInstructionTxt The text that covers the terminal that can be used to show instructions or text.
    [SerializeField] TMP_Text TInstructionTxt;

    /// @var TInputHeading Standard Terminal Header Prior To Input (ex: user@computer >)
    [SerializeField] TMP_Text TInputHeading;

    /// @var TInput The input field to get user input.
    [SerializeField] GameObject TInput;

    /// @var IntroText A String to be displayed upoon start for terminal. 
    private string FirstText = "Welcome to ClearSky Console.\n We are testing multi-line editing tbh.";
    void Start()
    {
        TInstructionTxt.text = FirstText;
    }

    public override void startTask()
    {

    }

    public override void startHazards()
    {

    }

    public override void stopHazards()
    {

    }

    public override void checkHazards()
    {

    }
}
