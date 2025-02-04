using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractMail : MonoBehaviour
{
    public string type;
    public int score;
    public UpdateGameScoreManager scoreManager;
    public GameObject mailSound;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            HandleCollision(collision);
        }
    }

    private void HandleCollision(Collision2D collision)
    {
        if (scoreManager != null)
        {
            scoreManager.AddScore(score);
        }
        else
        {
            Debug.Log("Score Manager is null");
        }
        Instantiate(mailSound);
        Destroy(gameObject);
    }

}
