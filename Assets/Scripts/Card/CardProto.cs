using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
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
    public CardProtoSO cardSO;
    [SerializeField] private GameObject BulletPrefab;

    protected List<GameObject> myBullets = new List<GameObject>();

    private float coolDownTimer = 0;//0表示冷却完毕

    int IDinBonusList;
    public List<CardType> cardTypes {  get; set; }
    public int EnergyCost{get;set;}
    public float SpeedUpValue {get;set;}
    public int DrawCardCnt { get;set;}
    public bool isPierce { get;set;}
    public float BulletSpeed { get; set; }
    public string TargetTag { get; set; }
    public float Damage { get; set; }
    public float GainArmor {  get; set; }

    protected virtual void Awake()
    {
        myRectTransform = GetComponent<RectTransform>();
        CDGrey = transform.Find("CDGrey").GetComponent<RectTransform>();

        ReadSO();
        //highlightableObject = GetComponent<HighlightableObject>();
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
        GainArmor = cardSO.GainArmor;

        Image img = transform.Find("Face").GetComponent<Image>();
        img.sprite = cardSO.CardFace;
        img.preserveAspect = true;
        transform.Find("Name").GetComponent<TextMeshProUGUI>().text = cardSO.CardName;
        transform.Find("Cost").GetComponent<TextMeshProUGUI>().text = cardSO.EnergyCost.ToString();
        transform.Find("Description").GetComponent<TextMeshProUGUI>().text = cardSO.CardDesc; 
        //Background
        RawImage bg = transform.Find("Background").GetComponent<RawImage>();
        switch (cardSO.rarity)
        {
            case Rarity.Common:
                bg.color = new Color(162f / 255, 162f / 255, 162f / 255);
                break;
            case Rarity.Rare:
                bg.color = new Color(0, 1, 120f / 255);
                break;
            case Rarity.Legendary:
                bg.color = new Color(1, 68f / 255, 0);
                break;
            default:
                bg.color = new Color(162f / 255, 162f / 255, 162f / 255);
                break;
        }
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
    public void SetBonusZoneBehavior(int _IDinBonusList)
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
        IDinBonusList = _IDinBonusList;
        GetComponent<Button>().onClick.AddListener(OnBonusZoneClick);
    }
    public void SetHandZoneBehavior()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(OnHandZoneClick);
    }
    public void OnBonusZoneClick()//在奖励界面中的点击事件
    {
        BonusMgr.Instance.Choose(this,IDinBonusList);
    }
    public void OnHandZoneClick()//在手牌中的点击事件
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
    public virtual bool IsUsable()
    {
        return CardManager.Instance.EnergyCnt >= EnergyCost;
    }
    public virtual void OnUse()
    {
        //抽卡
        for(int i = 0; i < DrawCardCnt; i++) 
            CardManager.Instance.DrawOneCard();
        //自我加速
        Player.Instance.GetComponent<ICanAffectSpeed>().AffectSpeed(SpeedUpValue);
        //起甲
        Player.Instance.Armor += GainArmor;
        //生成子弹
        SpawnBullets();
    }

    private void ApplyBasicAttriAndBuffAffect2Bullet()
    {
        foreach(GameObject bullet in myBullets)
        {
            BulletProto b = bullet.GetComponent<BulletProto>();
            Vector3 bulletV = transform.forward * BulletSpeed;

            float bulletDmg = Damage;
            if (Damage > 0)
                bulletDmg *= Player.Instance.DmgMagni;
            b.ApplyBasicAttri(TargetTag, bulletV, bulletDmg , 0,isPierce);

            b.OnHitTarget += ApplyMyBuffOnHit;
        }
    }
    protected virtual void ApplyMyBuffOnHit(Transform target) {
        
    }

    protected virtual void SpawnBullets()
    {
        myBullets.Clear();

        GameObject b = (Instantiate(BulletPrefab, GameObject.Find("AllBullets").transform, false));
        myBullets.Add(b);

        Vector3 bulletPos =  Player.Instance.transform.position;
        bulletPos.y += Player.BulletShootHeight;
        b.transform.position =  bulletPos;

        ApplyBasicAttriAndBuffAffect2Bullet();
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
