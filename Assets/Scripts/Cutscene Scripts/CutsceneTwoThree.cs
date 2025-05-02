using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTwoThree : MonoBehaviour
{
    AudioSource mySfx;
    // Start is called before the first frame update
    void Start()
    {
        mySfx = GetComponent<AudioSource>();
        newMsg.gameObject.SetActive(false);
        message.gameObject.SetActive(false);
        StartCoroutine(FirstPart(9f, 1));
    }

    public IEnumerator FirstPart(float seconds, int x)
    {
        yield return new WaitForSeconds(seconds);
        Handler(x);
    }

    public void Handler(int route)
    {
        switch (route)
        {
            case 1:
                NewMessage();
                break;
            case 2:
                DisplayMessage();
                break;
            case 3:
                EndScene();
                break;
            default:
                break;
        }
    }

    public TMPro.TMP_Text newMsg;

    public void NewMessage()
    {
        newMsg.gameObject.SetActive(true);
        mySfx.Play();
        StartCoroutine(FirstPart(4f, 2));
    }

    public TMPro.TMP_Text message;

    public void DisplayMessage()
    {
        newMsg.gameObject.SetActive(false);
        message.gameObject.SetActive(true);
        StartCoroutine(FirstPart(10f, 3));
    }

    public SceneChange sc;

    public void EndScene()
    {
        sc.LoadLevel3();
    }
}
