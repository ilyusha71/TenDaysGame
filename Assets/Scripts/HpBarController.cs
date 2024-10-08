using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarController : MonoBehaviour
{
    public Image bar;
    public InputField input;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            input.gameObject.SetActive(!input.gameObject.activeSelf);
    }

    public void UpdateBar()
    {
        try { bar.fillAmount = (int.Parse(input.text) / 20) * 0.2f; }
        catch { }
    }
    public void UpdateManaBar()
    {
        try { bar.fillAmount = ((int.Parse(input.text) / 20) + 1) * 0.2f; }
        catch { }
    }
}
