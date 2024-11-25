using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private NyanceNyanceRevolution NyanceNyanceRevolutionSingelton;
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        NyanceNyanceRevolutionSingelton = NyanceNyanceRevolution.GetInstance();
        scoreText.text = NyanceNyanceRevolutionSingelton.playerScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = NyanceNyanceRevolutionSingelton.playerScore.ToString();
    }


}
