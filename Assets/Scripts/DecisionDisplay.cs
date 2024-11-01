using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionDisplay : MonoBehaviour
{
    public Transform final; 
    public Transform[] main;
    public Transform[] dark;   
    public Transform[] slots;
    public Transform[] movements;
    int x=2, y=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Move(int index)
    {
        if (index < 3)
        {
            x = index;
            main[0].localPosition = slots[index].localPosition;
        }
        else
        {
            y = (index - 3);
            dark[0].localPosition = slots[index].localPosition;
        }         
        final.localPosition = new Vector3(main[0].localPosition.x,dark[0].localPosition.y, 0);

        if (x + y == 0)
        {
            main[1].localPosition = movements[4].localPosition;
            dark[1].localPosition = movements[4].localPosition;
        }
        else if (x + y == 1)
        {
            main[1].localPosition = movements[3].localPosition;
            dark[1].localPosition = movements[3].localPosition;
        }
        else if (x + y == 3)
        {
            main[1].localPosition = movements[1].localPosition;
            dark[1].localPosition = movements[1].localPosition;
        }
        else if (x * y == 4)
        {
            main[1].localPosition = movements[0].localPosition;
            dark[1].localPosition = movements[0].localPosition;
        }
        else
        {
            main[1].localPosition = movements[2].localPosition;
            dark[1].localPosition = movements[2].localPosition;
        }
    }
}
