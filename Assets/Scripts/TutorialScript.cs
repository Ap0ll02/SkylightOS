using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    CanvasGroup cg;
    Coroutine arrowCrts;
    void Start()
    {
        cg = gameObject.GetComponent<CanvasGroup>();
        if (arrowCrts == null) {
            StartCoroutine(Timer(21.5f));
        } 
    }

    public IEnumerator Timer(float s) {
        yield return new WaitForSeconds(s);
        StartCoroutine(BlinkArrow());
    }
    public IEnumerator BlinkArrow() {
        float threshhold = 0.1f;
        int blinkCount = 0;
        while(blinkCount < 10) {
            yield return new WaitForSeconds(0.05f);
            if(cg.alpha > threshhold){
                cg.alpha -= 0.05f;
            }
            else if(cg.alpha == 1) {
                threshhold = 0.1f;
            }
            else {
                threshhold = 1f;
                cg.alpha += 0.05f;
            }
            blinkCount++;
        }
        cg.alpha = 0;
    }
}
