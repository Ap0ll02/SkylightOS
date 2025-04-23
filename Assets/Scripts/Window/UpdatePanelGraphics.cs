using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// Author: Quinn Joseph Contaldi
/// File Description:
/// </summary>
public class UpdatePanel : MonoBehaviour
{


    public GameObject ButtonOneObject;
    public GameObject ButtonTwoObject;
    public GameObject WarningObject;
    public GameObject GreenCheckMark;
    public TextMeshProUGUI versionText;
    public TextMeshProUGUI publisherText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI mainTitleText;
    public TextMeshProUGUI buttonOneText;
    public TextMeshProUGUI buttonTwoText;
    public TMP_FontAsset alienFont;
    public TMP_FontAsset regularFont;


    // Start is called before the first frame update
    void Awake()
    {
        ChangeState(UpdateState.Working);
    }

    public enum UpdateState
    {
        Working,
        NotWorking,
        NotWorkingInteractable
    }

    public void ChangeState(UpdateState state)
    {
        switch (state)
        {
            case UpdateState.Working:
                ButtonOneObject.SetActive(false);
                ButtonTwoObject.SetActive(false);
                WarningObject.SetActive(false);
                GreenCheckMark.SetActive(true);
                mainTitleText.font = regularFont;
                nameText.font = regularFont;
                publisherText.font = regularFont;
                versionText.font = regularFont;
                buttonOneText.font = regularFont;
                buttonTwoText.font = regularFont;
                break;
            case UpdateState.NotWorking:
                ButtonOneObject.SetActive(false);
                ButtonTwoObject.SetActive(false);
                WarningObject.SetActive(true);
                GreenCheckMark.SetActive(false);
                mainTitleText.font = alienFont;
                nameText.font = alienFont;
                publisherText.font = alienFont;
                versionText.font = alienFont;
                buttonOneText.font = alienFont;
                buttonTwoText.font = alienFont;
                break;
            case UpdateState.NotWorkingInteractable:
                ButtonOneObject.SetActive(true);
                ButtonTwoObject.SetActive(true);
                WarningObject.SetActive(true);
                GreenCheckMark.SetActive(false);
                mainTitleText.font = alienFont;
                nameText.font = alienFont;
                publisherText.font = alienFont;
                versionText.font = alienFont;
                buttonOneText.font = alienFont;
                buttonTwoText.font = alienFont;
                break;
            default:
                break;
        }
    }

}
