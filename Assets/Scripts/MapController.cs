using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static MapController Instance { get; private set; }
    public CanvasScaler scaler;
    Vector2 resolution = new Vector2(Screen.width, Screen.height);
    Vector3 centre = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
    public GameObject grid;
    public GameObject crosshair;
    public CanvasGroup canvasGroup;
    [Header("Map")]
    public RectTransform map;
    public RectTransform[] maps;
    CharacterCap[] npc;
    Vector4 bounding;
    Vector3 mouseDirection = Vector3.zero; //鼠标移动方向
    float sensitivity = 1.0f; //鼠标移动灵敏度
    int edgeThreshold = 30; //边界阈值
    int speed = 700; //地图移动速度px/sec
    float segment = 0; //地图拉近挡位
    float canvasScale = 1.0f; //缩放比例
    float mapScale = 5.0f; // 5m/grid
    float maxScale;
    bool unlimited;

    [Header("Select")]
    public Transform pivot;
    public RectTransform selectionBox;
    public GameObject[] group;
    private CharacterCap[] items;
    private List<CharacterCap> onSelectedList = new List<CharacterCap>();

    [Header("Measure")]
    public GameObject rangefinder;
    public RectTransform line;
    public Transform[] flag;
    public TextMeshProUGUI textDistance;

    [Header("Deploy")]
    public GameObject menu;
    public RectTransform[] portals;
    public GameObject respawnPoint;
    public GameObject characterCao;
    public RectTransform[] team;
    Vector3[] posPortals;
    int indexPotal;

    private MouseControlState state = MouseControlState.None;
    public enum MouseControlState
    {
        None = -1,
        Measure = 0,
        Select = 1,
        Deploy = 2
    }

    // Start is called before the first frame update
    void Start()
    {
        SetMap(0);
        posPortals = new Vector3[portals.Length];
        for (int i = 0; i < posPortals.Length; i++)
        {
            posPortals[i] = portals[i].position;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && EventSystem.current.currentSelectedGameObject)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponent<CharacterCap>())
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                    EventSystem.current.currentSelectedGameObject.transform.SetParent(group[1].transform, true);
                if (Input.GetKeyDown(KeyCode.Alpha2))
                    EventSystem.current.currentSelectedGameObject.transform.SetParent(group[2].transform, true);
                if (Input.GetKeyDown(KeyCode.Alpha3))
                    EventSystem.current.currentSelectedGameObject.transform.SetParent(group[3].transform, true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 destination = transform.position - (Input.mousePosition - centre);
            if (EventSystem.current.currentSelectedGameObject)
            {
                if (EventSystem.current.currentSelectedGameObject.GetComponent<CharacterCap>())
                    destination = transform.position - (EventSystem.current.currentSelectedGameObject.transform.position - centre);
            }
            if (!unlimited) destination = new Vector3(Mathf.Clamp(destination.x, bounding.x, bounding.y), Mathf.Clamp(destination.y, bounding.z, bounding.w), 0);
            transform.DOMove(destination, 0.3f).SetEase(Ease.OutQuad);
            canvasGroup.blocksRaycasts = true;
        }
        else if (Input.GetMouseButtonDown(3)) ZoomIn(0.5f);
        else if (Input.GetMouseButtonDown(4)) ZoomIn(-0.5f);
        else if (Input.GetKeyDown(KeyCode.F1)) group[1].SetActive(!group[1].activeSelf);
        else if (Input.GetKeyDown(KeyCode.F2)) group[2].SetActive(!group[2].activeSelf);
        else if (Input.GetKeyDown(KeyCode.F3)) Move(2f);
        else
        {
            if (Input.mousePosition.x > resolution.x - edgeThreshold) mouseDirection.x = -sensitivity;
            else if (Input.mousePosition.x < edgeThreshold) mouseDirection.x = sensitivity;
            else mouseDirection.x = 0;
            if (Input.mousePosition.y > resolution.y - edgeThreshold) mouseDirection.y = -sensitivity;
            else if (Input.mousePosition.y < edgeThreshold) mouseDirection.y = sensitivity;
            else mouseDirection.y = 0;
            if (Input.GetKey(KeyCode.KeypadPlus) || Input.GetKey(KeyCode.E)) canvasScale *= (1 - Time.deltaTime);
            else if (Input.GetKey(KeyCode.KeypadMinus) || Input.GetKey(KeyCode.Q)) canvasScale *= (1 + Time.deltaTime);
            else if (Input.GetKey(KeyCode.Keypad0)) canvasScale = 1;
            else if (Input.GetKeyDown(KeyCode.BackQuote)) canvasScale = maxScale;
            else if (Input.GetKey(KeyCode.Keypad8)) transform.position -= new Vector3(0, 10000, 0);
            else if (Input.GetKey(KeyCode.Keypad2)) transform.position += new Vector3(0, 10000, 0);
            canvasScale *= (1 - Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 300);
            SwitchZoom();
            transform.position += ((mouseDirection - new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0)) * Time.deltaTime * speed);
            if (!unlimited) transform.position = new Vector3(Mathf.Clamp(transform.position.x, bounding.x, bounding.y), Mathf.Clamp(transform.position.y, bounding.z, bounding.w), 0);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            //print("0: " + transform.position + "B: " + bounding.x);
            //transform.position = new Vector3(Mathf.Clamp(transform.position.x, bounding.x, bounding.y), Mathf.Clamp(transform.position.y, bounding.z, bounding.w), 0);
            //print("1: " + transform.position + "B: " + bounding.x);
            print(transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) menu.SetActive(!menu.activeSelf);
        else if (Input.GetKeyDown(KeyCode.F5)) grid.SetActive(!grid.activeSelf);
        else if (Input.GetKeyDown(KeyCode.F6)) crosshair.SetActive(!crosshair.activeSelf);
        else if (Input.GetKeyDown(KeyCode.F7)) unlimited = !unlimited;
        if (state == MouseControlState.Deploy)
        {
            portals[indexPotal].position = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                if (indexPotal == 0)
                {
                    GameObject go = Instantiate(characterCao, portals[0].position, Quaternion.identity, team[0]);
                    go.GetComponent<CharacterCap>().SetStore(group[3].transform);
                }
                else
                    Instantiate(team[indexPotal].gameObject, portals[indexPotal].position, Quaternion.identity, team[0]);
                //team[indexPotal].position = portals[indexPotal].position;
                portals[indexPotal].position = posPortals[indexPotal];
                state = MouseControlState.None;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                portals[indexPotal].position = posPortals[indexPotal];
                state = MouseControlState.None;
            }
        }
    }
    void SwitchZoom()
    {
        if (!unlimited) canvasScale = Mathf.Min(canvasScale, maxScale);
        scaler.referenceResolution = resolution * canvasScale;
        CalculateBounding();
    }
    void ZoomIn(float value)
    {
        segment += value;
        segment = Mathf.Repeat(segment, 3f);
        canvasScale = 1 / (segment + 1);
        DOTween.To(() => scaler.referenceResolution, x => scaler.referenceResolution = x, resolution * canvasScale, 0.3f);
        CalculateBounding();
    }
    void Move(float times)
    {
        segment = times - 1;
        canvasScale = 1 / times;
        SwitchZoom();
        transform.position -= (Input.mousePosition - centre);
    }
    void CalculateBounding()
    {
        float offsetX = (map.rect.size.x - scaler.referenceResolution.x) * 0.5f / canvasScale;
        float offsetY = (map.rect.size.y - scaler.referenceResolution.y) * 0.5f / canvasScale;
        bounding = new Vector4(resolution.x * 0.5f - offsetX, resolution.x * 0.5f + offsetX, resolution.y * 0.5f - offsetY, resolution.y * 0.5f + offsetY);
    }
    public void OnPointerDown(PointerEventData data)
    {
        if (Input.GetMouseButton(0) && state == MouseControlState.Measure) EndMeasure();
        for (int i = 0; i < onSelectedList.Count; i++)
        {
            if (onSelectedList[i]) onSelectedList[i].Unhighlight();
        }
        onSelectedList.Clear();
    }
    public void OnBeginDrag(PointerEventData data)
    {
        if (state == MouseControlState.None)
        {
            if (Input.GetMouseButton(0)) BeginSelect();
            else if (Input.GetMouseButton(1)) BeginMeasure();
            canvasGroup.blocksRaycasts = false;
        }
        else if (state == MouseControlState.Measure)
            BeginMeasure();
    }
    public void OnDrag(PointerEventData data)
    {
        if (state == MouseControlState.Measure)
        {
            rangefinder.SetActive(true);
            flag[1].position = Input.mousePosition;
            Vector3 vec = Input.mousePosition - flag[0].position;
            float distance = (vec.magnitude) * canvasScale;
            float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
            flag[0].rotation = Quaternion.Euler(0, 0, angle);
            line.sizeDelta = new Vector2(distance, 2);
            line.position = flag[0].position + vec * 0.5f;
            line.rotation = Quaternion.Euler(0, 0, angle);
            textDistance.text = Mathf.Round(distance * mapScale *0.01f).ToString() + "m";
            textDistance.transform.position = Input.mousePosition + vec.normalized * 55 / canvasScale;
        }
        else if (state == MouseControlState.Select)
        {
            selectionBox.position = (Input.mousePosition + pivot.position) * 0.5f;
            selectionBox.sizeDelta = new Vector2(Mathf.Abs(Input.mousePosition.x - pivot.position.x), Mathf.Abs(Input.mousePosition.y - pivot.position.y)) * canvasScale;
        }
    }
    public void OnEndDrag(PointerEventData data)
    {
        canvasGroup.blocksRaycasts = true;
        if (state == MouseControlState.Select) EndSelect();
    }
    /* Function */
    void BeginSelect()
    {
        pivot.position = Input.mousePosition;
        selectionBox.position = Input.mousePosition;
        selectionBox.sizeDelta = Vector2.zero;
        state = MouseControlState.Select;
    }
    void EndSelect()
    {
        items = group[0].GetComponentsInChildren<CharacterCap>();
        Rect selectionRect = new Rect(
                  Mathf.Min(pivot.position.x, Input.mousePosition.x),
                  Mathf.Min(pivot.position.y, Input.mousePosition.y),
                  Mathf.Abs(Input.mousePosition.x - pivot.position.x),
                  Mathf.Abs(Input.mousePosition.y - pivot.position.y)
              );
        foreach (var item in items)
        {
            if (selectionRect.Contains(item.transform.position))
            {
                item.Highlight();
                onSelectedList.Add(item);
            }
        }
        selectionBox.sizeDelta = Vector2.zero;
        state = MouseControlState.None;
    }
    void BeginMeasure()
    {
        flag[0].position = Input.mousePosition;
        rangefinder.SetActive(false);
        state = MouseControlState.Measure;
    }
    void EndMeasure()
    {
        rangefinder.SetActive(false);
        state = MouseControlState.None;
    }
    public void BeginDeploy(int index)
    {
        indexPotal = index;
        state = MouseControlState.Deploy;
    }
    public void SetMapScale(InputField input)=> mapScale = (input.text == "") ? 0.1f:Mathf.Max(0.1f, float.Parse(input.text));
    public void SetMap(int index)
    {
        npc = group[4].GetComponentsInChildren<CharacterCap>();
        for (int i = 0; i < npc.Length; i++)
        {
            npc[i].transform.SetParent(map.transform, true);
        }
        map.gameObject.SetActive(false);
        map = maps[index];
        map.gameObject.SetActive(true);
        maxScale = map.rect.size.x / resolution.x;
        CalculateBounding();
        //grid.transform.SetParent(map, true);
        //team[0].transform.SetParent(map, true);
        npc = map.GetComponentsInChildren<CharacterCap>();
        for (int i = 0; i < npc.Length; i++)
        {
            npc[i].transform.SetParent(group[4].transform, true);
        }
    }
}
