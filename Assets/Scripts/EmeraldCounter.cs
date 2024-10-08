using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EmeraldCounter : MonoBehaviour, IPointerDownHandler
{
    public Text count;

    public void OnPointerDown(PointerEventData eventData)
    {        
        int i = int.Parse(count.text);
        if (Input.GetMouseButton(0))
            i++;
        else if (Input.GetMouseButtonDown(1))
            i--;
        count.text = i.ToString();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
