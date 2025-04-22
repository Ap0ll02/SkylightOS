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
    private string LineOne= "<bounce a=0.1 f=0.4> NOOOOO YOU FOOL!!!!! You ruined all of my plans</bounce>";
    private string LineTwo= "<shake a=0.1 f=0.4> No matter</shake>, you may have restored the transistor fluid";
    private string LineThree= "<shake a=0.1 f=0.4> But this is not the last of me!!!</shake> I will be back!";

    public void StartDoge()
    {
        if (tw == null)
            tw = transform.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TypewriterByCharacter>();
        if (nsText == null)
        {
            tw = transform.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TypewriterByCharacter>();
        }
        StartCoroutine(PlayOrder());
    }

    public IEnumerator PlayOrder()
    {
        yield return PlayDialogueLine(LineOne, 0.5f);
        yield return PlayDialogueLine(LineTwo, 0.5f);
        yield return PlayDialogueLine(LineThree, 0.5f);
        this.gameObject.SetActive(false);
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
