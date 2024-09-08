using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PhantomThiefManager : MonoBehaviour
{
    public GameObject character;
    public GameObject detective;
    public GameObject thief;
    public GameObject precious;
    public GameObject footprint;
    public GameObject barking;
    public GameObject bakudan;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            Switch(character);
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Switch(detective);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Switch(thief);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            Switch(precious);
        //if (Input.GetKeyDown(KeyCode.M))
        //    Switch(footprint);
        if (Input.GetKeyDown(KeyCode.B))
            barking.SetActive(!barking.activeSelf);
        if (Input.GetKeyDown(KeyCode.V))
            bakudan.SetActive(!bakudan.activeSelf);
    }
    void Switch(GameObject go)
    {
        go.SetActive(!go.activeSelf);
        if (EventSystem.current.currentSelectedGameObject)
            EventSystem.current.currentSelectedGameObject.transform.SetParent(go.transform);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
