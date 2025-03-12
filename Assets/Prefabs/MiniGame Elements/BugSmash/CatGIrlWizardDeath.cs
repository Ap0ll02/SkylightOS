using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatGIrlWizardDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timer());
    }

   public IEnumerator timer()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }


}
