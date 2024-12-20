using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


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
    [SerializeField] private GameObject BulletPrefab;

    protected List<GameObject> myBullets = new List<GameObject>();

    private float coolDownTimer = 0;//0表示冷却完毕

    public List<CardType> cardTypes {  get; set; }
    public int EnergyCost{get;set;}
    public float SpeedUpValue {get;set;}
    public int DrawCardCnt { get;set;}
    public bool isPierce { get;set;}
    public float BulletSpeed { get; set; }
    public string TargetTag { get; set; }
    public float Damage { get; set; }

    protected virtual void Awake()
    {
        myRectTransform = GetComponent<RectTransform>();
        CDGrey = transform.Find("CDGrey").GetComponent<RectTransform>();

        ReadSO();
        //highlightableObject = GetComponent<HighlightableObject>();
        
        GetComponent<Button>().onClick.AddListener(OnClick);


    }

    private void ReadSO()
    {
        EnergyCost = cardSO.EnergyCost;
        SpeedUpValue = cardSO.SpeedUpValue;
        DrawCardCnt = cardSO.DrawCardCnt;
        isPierce = cardSO.isPierce;
        BulletSpeed = cardSO.BulletSpeed;
        Damage = cardSO.Damage;
        TargetTag = cardSO.TargetTag;
        cardTypes = cardSO.CardTypes;
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
        myRectTransform.localScale= new Vector3(1,1,1);
        //highlightableObject.ConstantOffImmediate();
    }
    public virtual void OnUse()
    {
        //抽卡
        for(int i = 0; i < DrawCardCnt; i++) 
            CardManager.Instance.DrawOneCard();
        //自我加速
        Player.Instance.GetComponent<ICanAffectSpeed>().AffectSpeed(SpeedUpValue);
        //生成子弹
        myBullets.Clear();
        
        SpawnBullets();

        ApplyBasicAttriAndBuffAffect2Bullet();
    }

    private void ApplyBasicAttriAndBuffAffect2Bullet()
    {
        foreach(GameObject bullet in myBullets)
        {
            BulletProto b = bullet.GetComponent<BulletProto>();
            b.ApplyBasicAttri(TargetTag, BulletSpeed,  Damage, isPierce);
            b.OnHitTarget += ApplyMyBuffOnHit;
        }
    }
    protected virtual void ApplyMyBuffOnHit(Collider collider) { }

    protected virtual void SpawnBullets()
    {
        myBullets.Add(Instantiate(BulletPrefab, Player.Instance.transform, false));
    }
    public virtual int GetCost()
    {
        return EnergyCost;
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
