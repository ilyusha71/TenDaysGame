using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinefieldWrapper : MonoBehaviour
{
    public GameObject wrapper;
    Text index;
    // Start is called before the first frame update
    void Start()
    {
        index = GetComponentInChildren<Text>();
    }
    public void Open()
    {     
        wrapper.SetActive( !wrapper.activeSelf);
        index.enabled = !index.enabled;
    }
}
