using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PeanutsCounter : MonoBehaviour, IPointerDownHandler
{
    public Text count;
    private int initial;
    private void Awake()
    {
        initial = int.Parse(count.text);
    }
    public void OnPointerDown(PointerEventData eventData)
    {        
        int i = int.Parse(count.text);
        if (Input.GetMouseButton(0))
            i--;
        else if (Input.GetMouseButtonDown(1))
            i++;
        count.text = i.ToString();
    }
    public void Initialize() => count.text = initial.ToString();
}
