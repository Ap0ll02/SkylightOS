using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using TMPro;
using UnityEngine;

public class NorthStarAdvancedMode : MonoBehaviour
{
    public TypewriterByCharacter tw;
    public TMP_Text nsText;
    public string currentText;
    public string[] startDialogueLines;
    
    // Start is called before the first frame update
    public IEnumerator PlayDialogueLine(string dialogueLines, float t = 1f)
    {
        tw.ShowText(dialogueLines);
        yield return new WaitUntil(() => !tw.isShowingText);
        yield return new WaitForSeconds(t);
        // tw.StartDisappearingText();
        // yield return new WaitUntil(() => !tw.isHidingText);
        yield return new WaitForSeconds(t);
        currentText = string.Empty;
    }

    public void Turnoff()
    {
        gameObject.SetActive(false);
    }
}
