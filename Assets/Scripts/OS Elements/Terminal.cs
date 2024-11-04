using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * @author Jack Ratermann
 * @date 11/03/2024
 * @version 0.1
 * @brief This is the terminal class that parses, executes and helps show output. This can be used in conjunction 
 * with the terminal task. 
 * @note This class will not have all actual terminal commands.
 */

/// Commands Available:
/// @code {ls} - Lists all files in the current directory
/// @code {cd} - Changes the current directory
/// @code {cat} - Displays the contents of a file
/// @code {clear} - Clears the terminal
/// @code {help} - Displays all available commands

public class Terminal : MonoBehaviour
{
    void Start()
    {
        /// Prints first console line
        Debug.Log("user@computer:~$");
    }

    /// @brief This function will parse the input from the user
    /// @param input The users input to be parsed
    /// @return 0 if successful
    int ParseInput(string input)
    {
        Debug.Log("user@computer:~$ " + input);
        return 0;
    }

    /// @brief This function will run the command given by the user <summary>
    /// @param command The command without arguments
    /// @param args The argument array for the command
    /// @return 0 if successful
    int RunCommand(string command, string[] args)
    {
        if (args.Length == 0)
        {
            Debug.Log("No arguments provided.");
        }

        Debug.Log("user@computer:~$ " + command + args[0]);
        return 0;
    }
}
