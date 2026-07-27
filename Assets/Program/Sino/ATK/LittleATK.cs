using System.Collections;
using UnityEngine;

public class LittleATK : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator WaitTest()
    {
        Debug.Log("待ちます");
        yield return new WaitForSeconds(1);
    }
}
