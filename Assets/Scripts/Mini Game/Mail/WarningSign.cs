using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSign : AbstractMail
{
    // Start is called before the first frame update
    RectTransform mailRect;
    void Update()
    {
        if (mailRect.anchoredPosition.y < -1881)
        {
            Destroy(gameObject);
        }
    }
    new void Start()
    {
        mailRect = GetComponent<RectTransform>();
        type = "Warning";
        score = -50;
        scoreManager = FindObjectOfType<UpdateGameScoreManager>();
        if(scoreManager == null)
        {
            Debug.Log("Score Manager not found");
        }
        base.Start();
    }

}
