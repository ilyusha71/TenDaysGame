using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DoraemonController : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public bool allowIdle=true;
    Animator anim;
    SpriteRenderer sr;
    Vector3 pos;
    float v,h;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        anim = GetComponent<Animator>();
        anim.SetFloat("V", -1);
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.gameObject)
            if (EventSystem.current.currentSelectedGameObject == gameObject)
            {
                v = Input.GetAxisRaw("Vertical");
                h = Input.GetAxisRaw("Horizontal");
                if (v == 0 && h == 0 && !allowIdle)
                    anim.speed = 0;
                else
                {
                    anim.speed = 1;
                    anim.SetFloat("V", v);
                    anim.SetFloat("H", h);
                    transform.position += new Vector3(h, v, 0) * Time.deltaTime * 30;
                }
            }
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        //pos = Camera.main.WorldToScreenPoint(transform.position) - Input.mousePosition;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //pos = Camera.main.WorldToScreenPoint(transform.position) - Input.mousePosition;
        //print(pos);
    }
    public void OnDrag(PointerEventData data)
    {
        //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + pos);
    }
}
