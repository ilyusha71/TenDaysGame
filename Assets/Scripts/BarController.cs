using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BarController : MonoBehaviour, IPointerDownHandler
{
    public bool hp;
    public RectTransform shell, frame;
    public Image bar;
    public InputField input;
    public virtual void OnPointerDown(PointerEventData data)
    {
        if (hp)
        {
            if (Input.GetMouseButtonDown(0))
                bar.fillAmount -= 0.142857f;
            if (Input.GetMouseButtonDown(1))
                bar.fillAmount += 0.142857f;
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
                bar.fillAmount -= 0.2f;
            if (Input.GetMouseButtonDown(1))
                bar.fillAmount += 0.2f;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && input)
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
    public void ShowValue()
    {
        input.gameObject.SetActive(!input.gameObject.activeSelf);
    }
    public void AddBarLimit()
    {
        
    }
}
