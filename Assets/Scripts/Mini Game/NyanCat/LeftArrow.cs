using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author Quinn Contaldi
/// We want this child object to just focus on Right Arrow specific logic
/// </summary>
public class LeftArrow : Arrows
{
    public GameObject explosionPrefab;
    public Animator explosionAnimator;
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
                Debug.LogError("AnimatorController is not assigned to the explosionAnimator.");
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
                : explosionAnimator.SetBool("Perfect",true );
                NyanceNyanceRevolutionSingleton.playerScore += 110;
                DestroyArrow();
                break;
            // We need the range below and above 
            case float y when (y > 4.2 & y <= 4.4)
                : explosionAnimator.SetBool("Great",true ); 
                NyanceNyanceRevolutionSingleton.playerScore += 60;
                DestroyArrow();
                break;
            case float y when (y > 4.0 & y <= 4.2)
                : explosionAnimator.SetBool("Good",true );
                NyanceNyanceRevolutionSingleton.playerScore += 20;
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
