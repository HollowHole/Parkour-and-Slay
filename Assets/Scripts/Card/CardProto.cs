using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

using UnityEngine.UI;

public class CardProto : MonoBehaviour
{
    //[Header("Highlight")]
    //HighlightableObject highlightableObject;
    //public Color highLightColor = Color.white;

    [Header("CoolDown")]
    RectTransform CDGrey;
    RectTransform myRectTransform;
    //SO
    [SerializeField] protected CardProtoSO cardSO;
    
    private float coolDownTimer = 0;//0表示冷却完毕

    protected virtual void Awake()
    {
        myRectTransform = GetComponent<RectTransform>();
        CDGrey = transform.Find("CDGrey").GetComponent<RectTransform>();
        //highlightableObject = GetComponent<HighlightableObject>();
        
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    protected virtual void Start()
    {
        
    }
    private void FixedUpdate()
    {
        if( coolDownTimer >0)
        {
            coolDownTimer -= Time.deltaTime;
        }
    }
    public void OnClick()
    {
        if (CDReady())
        {
            CardManager.Instance.Choose(this);
        }
    }
    public void OnPick()
    {
        //修改Scale和position
        Vector3 targetScale = new Vector3(1.3f, 1.3f, 1.3f);
        myRectTransform.localScale = targetScale;
        //highlightableObject.ConstantOnImmediate(highLightColor);
    }
    public void Unpick()
    {
        //Scale和position改回去
        myRectTransform.localScale = new Vector3(1,1,1);
        //highlightableObject.ConstantOffImmediate();
    }
    public virtual void OnUse()
    {
        //CDStart();
    }
    public virtual int GetCost()
    {
        return cardSO.EnergyCost;
    }
    private void CDStart()
    {
        coolDownTimer = cardSO.CD;
    }
    private bool CDReady()
    {
        return coolDownTimer <=0;
    }
    private void Update()
    {
        //调整CD时灰色部分的大小

        float height = coolDownTimer / cardSO.CD * myRectTransform.rect.height;
        float width = CDGrey.rect.width;
        Vector2 targetSize = new Vector2(width,height);
        CDGrey.sizeDelta = targetSize;
    }
}
