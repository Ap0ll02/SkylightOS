using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popups : Hazards
{
    public BasicWindow window;
    // Start is called before the first frame update
    void Start()
    {
        window = GetComponent<BasicWindow>();
        StartCoroutine(SetAsLastChildCoroutine());
    }

    // This is the only instance where it is acceptable to disable the game objects when closing a window
    private void OnEnable()
    {
        window.OnWindowClose += ClosePopup;
    }

    private void OnDisable()
    {
        window.OnWindowClose -= ClosePopup;
    }

    private IEnumerator SetAsLastChildCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            transform.SetAsLastSibling();
        }
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
