using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RevolverController : MonoBehaviour, IPointerDownHandler
{
    public Sprite[] color;
    private Image cylinder;
    public GameObject seal;
    int index=0;
    bool rotate;
    private void Awake()
    {
        cylinder=GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) seal.SetActive(!seal.activeSelf);
        if (Input.GetKeyDown(KeyCode.R)) Rotate();
        if (Input.GetKeyDown(KeyCode.E)) Stop();
        if (Input.GetKeyDown(KeyCode.Alpha1)) Clockwise(1);
        if (Input.GetKeyDown(KeyCode.Alpha2)) Clockwise(-1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) Clockwise(3);
    }   
    public void OnPointerDown(PointerEventData data)
    {
        index++;
        index=(int)Mathf.Repeat(index,3);
        cylinder.sprite = color[index];
    }
    public void Clockwise(int i)
    {
        if (!rotate)
        {            
            transform.DOKill(false);
            rotate = true;
            transform.DOLocalRotate(new Vector3(0, 0, 60 * i), 0.3f, RotateMode.LocalAxisAdd).SetEase(Ease.OutCubic).
                OnComplete(() => rotate = false);
        }
    }
    public void Rotate()
    {
        if (!rotate)
        {
            transform.DOKill(false);
            rotate = true;
            transform.DOLocalRotate(new Vector3(0, 0, transform.rotation.eulerAngles.z+360), 0.3f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);  
        }
    }
    public void Stop()
    {
        if (rotate)
        {
            transform.DOKill(false);
            float t = Mathf.Abs(360+Random.Range(0, 6) * 60 - transform.rotation.eulerAngles.z);
            transform.DOLocalRotate(new Vector3(0, 0, t), 1.0f, RotateMode.LocalAxisAdd).SetEase(Ease.OutCubic);
            rotate = false;
        }
    }
}
