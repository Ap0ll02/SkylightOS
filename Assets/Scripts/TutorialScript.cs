using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    CanvasGroup cg;
    Coroutine arrowCrts = null;
    void Start()
    {
        cg = gameObject.GetComponent<CanvasGroup>();
        StartCoroutine(Timer(23f)); 
    }

    public IEnumerator Timer(float s) {
        yield return new WaitForSeconds(s);
        arrowCrts = StartCoroutine(BlinkArrow());
    }

    public IEnumerator TimerTwo(float s) {
        yield return new WaitForSeconds(s);
        arrowCrts = StartCoroutine(UpArrowToo());
    }

    public IEnumerator BlinkArrow() {
        cg.alpha = 0;
        float threshhold = 0.1f;
        int blinkCount = 0;
        while(blinkCount < 120) {
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
        StartCoroutine(TimerTwo(2f));
    }

    public IEnumerator UpArrowToo() { 
        float threshhold = 0.1f;
        cg.alpha = 1;
        int blinkCount = 0;
        RectTransform tr = GetComponent<RectTransform>();
        tr.anchoredPosition += new Vector2(-80, 300);
        tr.rotation = Quaternion.Euler(0, 0, 300);
        while(blinkCount < 100) {
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
