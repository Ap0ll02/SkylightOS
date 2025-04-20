using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Doge : MonoBehaviour
{
    public TypewriterByCharacter tw;
    public TMP_Text nsText;
    public string LineOne= "<bounce a=0.1 f=0.4> NOOOOO YOU FOOL!!!!! </bounce>";
    public string LineTwo= "<bounce a=0.1 f=0.4> You Restored the computer fluid</bounce>";

    public void StartDoge()
    {
        if (tw == null)
            tw = transform.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TypewriterByCharacter>();
        if (nsText == null)
        {
            tw = transform.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TypewriterByCharacter>();
        }
        PlayDialogueLine(LineOne, 2f);
        PlayDialogueLine(LineTwo, 2f);
    }
    private IEnumerator PlayDialogueLine(string dialogueLines, float t = 1f)
    {
        tw.ShowText(dialogueLines);
        yield return new WaitUntil(() => !tw.isShowingText);
        yield return new WaitForSeconds(t);
        tw.StartDisappearingText();
        yield return new WaitUntil(() => !tw.isHidingText);
        yield return new WaitForSeconds(t);
    }

}
