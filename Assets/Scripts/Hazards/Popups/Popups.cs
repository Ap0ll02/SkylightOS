using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popups : Hazards
{
    public BasicWindow window;

    private void Awake()
    {
        window = GetComponent<BasicWindow>();
        window.isOpen = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetAsLastChildCoroutine());
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
