using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushEnigmaManager: MonoBehaviour
{
    public GameObject[] faction;
    public GameObject visible;
    public GameObject mvp;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            visible.SetActive(!visible.activeSelf);
        if (Input.GetKeyDown(KeyCode.M))
            mvp.SetActive(!mvp.activeSelf);

        if (Input.GetKeyDown(KeyCode.Alpha1))
            faction[0].SetActive(!faction[0].activeSelf);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            faction[1].SetActive(!faction[1].activeSelf);
    }
}
