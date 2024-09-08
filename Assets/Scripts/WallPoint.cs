using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WallPoint : MonoBehaviour,IPointerDownHandler
{
    public Sprite[] sprites;
    public SpriteRenderer center;
    public GameObject wallCol;
    public GameObject wallRow;
    bool red=false;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(1))
        {
            center.enabled = false;
            red = !red;
            center.sprite = sprites[red ? 1 : 0];
            wallCol.SetActive(false); 
            wallRow.SetActive(false);
        }
        else
        {
            center.enabled = true;            
            if (!wallCol.activeSelf)
            {
                wallCol.SetActive(true);
                wallRow.SetActive(false);
            }
            else
            {
                wallCol.SetActive(!wallCol.activeSelf);
                wallRow.SetActive(!wallRow.activeSelf);
            }
        }
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
