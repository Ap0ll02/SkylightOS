using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneOneTwo : MonoBehaviour
{
    bool onVar = true;

    // Start is called before the first frame update
    void Start()
    {
        newMsg.gameObject.SetActive(false);
        StartCoroutine(FirstPart());
    }

    public IEnumerator FirstPart()
    {
        while (onVar)
        {
            yield return new WaitForSeconds(6f);
            onVar = false;
            NewMessage();
        }
    }

    TMPro.TMP_Text newMsg;

    public void NewMessage()
    {
        newMsg.gameObject.SetActive(true);
    }
}
