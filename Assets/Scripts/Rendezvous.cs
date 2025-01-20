using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rendezvous : MonoBehaviour
{
    public GameObject rendezvous;
    public int Index { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            //GameObject go = Instantiate(rendezvous, transform.position, Quaternion.identity);
            //go.GetComponent<CharacterCap>().SetStore(Index);
        }
        else if (Input.GetMouseButtonDown(1))            enabled = false;
    }
}
