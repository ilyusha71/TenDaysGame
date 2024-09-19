using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateSelectable : MonoBehaviour
{
    public RectTransform range;
    private Rect selectedRange;
    private Vector3 initialPosition;
    private bool drag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            initialPosition=Input.mousePosition;
            drag = true;
        }
        if (Input.GetMouseButtonUp(1))
            drag = false;
        if (drag)
        {
            range.sizeDelta = new Vector2(Mathf.Abs(Input.mousePosition.x - initialPosition.x), Mathf.Abs(Input.mousePosition.y - initialPosition.y));
            range.position = (Input.mousePosition + initialPosition) * 0.5f;
        }
        else
            range.sizeDelta = Vector2.zero;
    }
}
