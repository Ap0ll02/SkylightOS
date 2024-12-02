using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float destroyTimer = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,destroyTimer);
    }

}
