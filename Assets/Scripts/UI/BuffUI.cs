using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuffUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI myText;
    public string BuffName;
    public string lastTime = "";
    EventSystem eventSystem;
    bool MouseStay = false;
    private void Awake()
    {
        myText.transform.localScale = Vector3.zero;
    }
    private void Start()
    {
        eventSystem = EventSystem.current;
    }
    public void SetBuffName(string _buffName)
    {
        BuffName = _buffName;
        myText.text = BuffName;
    }
    private void Update()
    {
        List<RaycastResult> results = new List<RaycastResult>();
        // 执行射线检测
        GraphicRaycaster graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        graphicRaycaster.Raycast(pointerEventData, results);
        // 检查是否检测到了特定的UI元素
        bool isOverMe = false;
        foreach (var result in results)
        {
            if (result.gameObject == this.gameObject)
            {
                isOverMe = true;
                break;
            }
        }
        // 输出检测结果
        if (isOverMe)
        {
            if(!MouseStay)
            {
                MouseStay = true;
                MyMouseEnter();
            }
            else
            {
                MyMouseStay();
            }
        }
        else if (MouseStay)
        {
            MouseStay = false;
            MyMouseExit();
        }
    }

    private void MyMouseExit()
    {
        myText.transform.localScale = Vector3.zero;
    }
    private void MyMouseStay()
    {
        myText.transform.position = Input.mousePosition + new Vector3(0,20,0);
        myText.text = BuffName + lastTime;
    }
    private void MyMouseEnter()
    {
        myText.transform.localScale = Vector3.one;
    }

}
