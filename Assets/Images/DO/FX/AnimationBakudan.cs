using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBakudan : MonoBehaviour
{
    public float zero = 27f, one=16f;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(zero, zero, 0);
        transform.DOScale(new Vector3(one, one, 0), 0.5f).SetLoops(-1,LoopType.Yoyo);
    }
}
