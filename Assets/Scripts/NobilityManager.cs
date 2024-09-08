using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NobilityManager : MonoBehaviour
{
    public GameObject[] nobility;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            for (int i = 0; i < nobility.Length; i++)
            {
                if (i < 6)
                    nobility[i].transform.position = new Vector3(Random.Range(100, 500), Random.Range(100, 700), 0);
                else
                    nobility[i].transform.position = new Vector3(Random.Range(1400, 1800), Random.Range(100, 900), 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            nobility[0].SetActive(!nobility[0].activeSelf);
            nobility[5].SetActive(!nobility[5].activeSelf);
        }
    }
}
