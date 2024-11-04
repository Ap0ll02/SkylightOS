using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// Jack Ratermann
// Switches scene to Level1, using the animation to designate when.
// Depends on the animation to call the function.
public class SceneChange : MonoBehaviour
{
    private void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
}
