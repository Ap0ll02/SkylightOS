using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popups : Hazards
{
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
