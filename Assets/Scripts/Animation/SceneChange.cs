using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// Jack Ratermann
// Switches scene to Level1, using the animation to designate when.
// Depends on the animation to call the function.
public class SceneChange : MonoBehaviour
{
    private void LoadIntro(){ 
        SceneManager.LoadScene("Level0");
    }                
    private void LoadTransition12(){
        SceneManager.LoadScene("1To2Cutscene");
    }
    private void LoadTransition23(){
        SceneManager.LoadScene("2To3Cutscene");
    }
    private void LoadTransitionEnd(){
        SceneManager.LoadScene("EndingCutscene");
    }
    // Load the levels
    private void LoadLevel1(){
        SceneManager.LoadScene("Level1");
    }
    private void LoadLevel2(){
        SceneManager.LoadScene("Level2");
    }
    private void LoadLevel3(){
        SceneManager.LoadScene("Level3");
    }

}
