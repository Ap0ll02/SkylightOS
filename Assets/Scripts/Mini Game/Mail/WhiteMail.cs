using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : AbstractMail
{
    // https://www.youtube.com/watch?v=rfqAkUXKT5Y
        RectTransform mailRect;
        void Update()
        {
            if (mailRect.anchoredPosition.y < -1881)
            {
                Destroy(gameObject);
            }
        }
        void Start()
        {
            mailRect = GetComponent<RectTransform>();
            type = "White";
            score = 50;
            scoreManager = FindObjectOfType<UpdateGameScoreManager>();
            if(scoreManager == null)
            {
                Debug.Log("Score Manager not found");
            }
        base.Start();
        }

}
