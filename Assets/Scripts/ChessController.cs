using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChessController : DraggableUI, ISelectHandler, IDeselectHandler
{
    public Image halo;
    public void OnSelect(BaseEventData eventData)
    {
        if (halo) halo.enabled = true;
    }
    public void OnDeselect(BaseEventData eventData)
    {
        if (halo) halo.enabled = false;
    }
}
