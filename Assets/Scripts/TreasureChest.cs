using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TreasureChest : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public Image chest;
    public Sprite[] img;
    public DragChess gold;
    public Text textGold;
    private int num;
    public void NewGame()
    {
        chest.sprite = img[0];
        gold = null;
        num = 0;
        textGold.text = num.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        if (gold && (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)))
        {
            gold.transform.position = new Vector3(0,0,-10000);
            num += int.Parse(gold.GetComponentInChildren<Text>().text);
            textGold.text = num.ToString();
            gold=null;
            chest.sprite=img[1];
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {       
        if (EventSystem.current.currentSelectedGameObject)
        {
            if (EventSystem.current.currentSelectedGameObject.name == "Gold")
                gold = EventSystem.current.currentSelectedGameObject.GetComponent<DragChess>();
        }      
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        gold = null;
    }
}
