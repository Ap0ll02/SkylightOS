using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerDefenseRestart : MonoBehaviour
{
    public void RestartLevel() {
        SceneManager.LoadScene("TowerDefense");
    }
}
