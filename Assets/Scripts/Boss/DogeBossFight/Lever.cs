using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    AlpinePlayer player;
    public GameObject KeyPrompt;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindDistanceToPlayer();
    }

    public void FindDistanceToPlayer()
    {
        Vector3 Distance = player.transform.position - transform.position;
        if (Distance.magnitude < 5)
        {
            KeyPrompt.SetActive(true);
        }
        else
        {
            KeyPrompt.SetActive(false);
        }
    }
}
