using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerGameManager : MonoBehaviour
{
    public Image expandPanel;
    public Transform[] expand;
    public virtual void LoadPlayer()
    {
        LoadPlayerAvatar.Instance.transform.SetParent(transform);
        LoadPlayerAvatar.Instance.transform.SetSiblingIndex(1);
        PeanutsManager.Instance.transform.SetParent(transform);
        PeanutsManager.Instance.transform.SetSiblingIndex(99);
    }
    public virtual void Expande(int index) { }

}
