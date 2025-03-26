using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicInsect : AbstractInsect
{
    public void Start()
    {
        BugSpeed = 0.5f;
        maxHealth = 5;
        currentHealth = maxHealth;
        pointValue = 100;
    }

}
