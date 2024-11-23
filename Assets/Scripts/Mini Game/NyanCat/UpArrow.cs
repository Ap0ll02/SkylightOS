using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author Quinn Contaldi
/// Don't yell at me for all the similarities between these classes; the object-oriented programmer in me needs to make these separate.
/// The reason being is that I want to define different graphics based on different arrows.
/// So making different arrows have their own class and be extendable is important.
/// Anyways, enough yapping, this is the up arrow class.
/// We want this child object to just focus on Right Arrow specific logic
/// </summary>
public class ArrowUp : Arrows
{
    public GameObject explosionPrefab;
    public RuntimeAnimatorController rightArrowAnimatorController;
    private Animator explosionAnimator; // Add this line to declare explosionAnimator

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Ensure the explosionAnimator is assigned
        if (explosionPrefab != null)
        {
            explosionAnimator = explosionPrefab.GetComponent<Animator>();
            if (explosionAnimator.runtimeAnimatorController == null)
            {
                if (rightArrowAnimatorController != null)
                {
                    explosionAnimator.runtimeAnimatorController = rightArrowAnimatorController;
                }
                else
                {
                    Debug.LogError("AnimatorController is not assigned to the explosionAnimator.");
                }
            }
        }
        else
        {
            Debug.LogError("Explosion prefab is not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // This is keeping track of the 
    public override void ScoreCheck()
    {
        switch (transform.position.y)
        {
            case float y when (y > 4.4 & y <= 4.6)
                :
                explosionAnimator.SetBool("PerfectArrow", true);
                NyanceNyanceRevolutionSingleton.playerScore += 105;
                DestroyArrow();
                break;
            // We need the range below and above 
            case float y when (y > 4.2 & y <= 4.4)
                :
                explosionAnimator.SetBool("GreatArrow", true);
                NyanceNyanceRevolutionSingleton.playerScore += 70;
                DestroyArrow();
                break;
            case float y when (y > 4.0 & y <= 4.2)
                :
                explosionAnimator.SetBool("GoodArrow", true);
                NyanceNyanceRevolutionSingleton.playerScore += 10;
                DestroyArrow();
                break;
            case float y when (y > outOfBounds)
                :
                DestroyArrow();
                break;
            default
                :
                break;
        }
    }

}
