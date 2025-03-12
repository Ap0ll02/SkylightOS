using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// Jack Ratermann
// Switches scene to Level1, using the animation to designate when.
// Depends on the animation to call the function.
public class SceneChange : MonoBehaviour
{
    public void LoadIntro(){ 
        SceneManager.LoadScene("Level0");
    }                
    public void LoadTransition12(){
        SceneManager.LoadScene("1To2Cutscene");
    }
    public void LoadTransition23(){
        SceneManager.LoadScene("2To3Cutscene");
    }
    public void LoadTransitionEnd(){
        SceneManager.LoadScene("EndingCutscene");
    }
    // Load the levels
    public void LoadLevel1(){
        SceneManager.LoadScene("Level1");
    }
    public void LoadLevel2(){
        SceneManager.LoadScene("Level2");
    }
    public void LoadLevel3(){
        SceneManager.LoadScene("Level3");
    }

}
