using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class BonusMgr : MonoBehaviour
{
    public static BonusMgr Instance;
    [SerializeField] GameObject BonusMenu;



    [SerializeField] Transform BonusCardZone;
    [Tooltip("可选奖励卡牌的数量")]
    public int BonusCardCount = 3;


    [Header("掉率（百分制")]
    [SerializeField] float InitCommonDropRate = 90;
    [SerializeField] float InitRareDropRate = 10;
    [Tooltip("其实不参与计算")]
    [SerializeField] float InitLegendDropRate = 0;
    [SerializeField] float CommonDropRateChangeRate = -8;
    [SerializeField] float RareDropRateChangeRate =  5;
    [Tooltip("其实不参与计算")]
    [SerializeField] float LegendDropRateChangeRate = 3;

    int CommonCardsCnt;
    int RareCardsCnt;
    int LegendCardsCnt;
    List<CardProto> AllBonusCardsSortedByRarity;
    //

    CardProto ChosenCard;
    int ChosenCardID;

    private void Awake()
    {
        Instance = this;

        CloseBonusMenu();

        InitBonusCardList();
    }

    private void InitBonusCardList()
    {
        List<CardProto> CommonCards = new List<CardProto>();
        List<CardProto> RareCards = new List<CardProto>();
        List<CardProto> LegendCards = new List<CardProto>();
        string path = "Prefabs/Cards"; // 相对于 Resources 文件夹的路径
        GameObject[] prefabs = Resources.LoadAll<GameObject>(path);

        foreach (GameObject prefab in prefabs)
        {
            CardProto card = prefab.GetComponent<CardProto>();
            if(card == null) continue;
            CardProtoSO cardSO = card.cardSO;
            if(cardSO == null) continue;

            switch (cardSO.rarity)
            {
                case Rarity.Common:
                    CommonCards.Add(card);
                    break;
                case Rarity.Rare:
                    RareCards.Add(card);
                    break;
                case Rarity.Legendary:
                    LegendCards.Add(card);
                    break;
                default:
                    break;
            }
        }
        CommonCardsCnt = CommonCards.Count;
        RareCardsCnt = RareCards.Count;
        LegendCardsCnt = LegendCards.Count;
        AllBonusCardsSortedByRarity = CommonCards.Concat(RareCards).Concat(LegendCards).ToList();
        //log
        //StringBuilder stringBuilder = new StringBuilder();
        //foreach (CardProto c in AllBonusCardsSortedByRarity)
        //{
        //    stringBuilder.Append(c.name + " ,");
        //}
        //Debug.Log(stringBuilder.ToString());
    }

    private void Start()
    {
        LevelMgr.Instance.OnLevelEnd += GenerateBonusCards;
        LevelMgr.Instance.OnLevelEnd += OpenBonusMenu;
        LevelMgr.Instance.OnLevelEnd += () => { TimeMgr.Instance.PauseGame(); };
    }
    void GenerateBonusCards()
    {
        int levelCnt = LevelMgr.Instance.CurLevelCnt;
        List<int> bonusCardsIndex = new List<int>();
        //generate new cards
        string DebugStr = "Generating ";
        while(bonusCardsIndex.Count < BonusCardCount)
        {
            float commonDropRate = (InitCommonDropRate + (levelCnt - 1) * CommonDropRateChangeRate) / 100;
            commonDropRate = Mathf.Clamp(commonDropRate, 0f, 1f);
            float rareDropRate = (InitRareDropRate + (levelCnt - 1) * RareDropRateChangeRate) / 100;
            rareDropRate = Mathf.Clamp(rareDropRate, 0f, 1f);
            //float legendDropRate = InitLegendDropRate + levelCnt * LegendDropRateChangeRate;
            float ran = Random.value;

            int index;
            if (ran < commonDropRate)
            {//Common
                index = Random.Range(0, CommonCardsCnt);
                if (bonusCardsIndex.Contains(index)) continue;
                DebugStr += "Common, ";
            }
            else if(ran < commonDropRate + rareDropRate)
            {//rare
                index = Random.Range(CommonCardsCnt, CommonCardsCnt + RareCardsCnt);
                if (bonusCardsIndex.Contains(index)) continue;
                DebugStr += "Rare, ";
            }
            else
            {//legend
                index = Random.Range(CommonCardsCnt + RareCardsCnt, CommonCardsCnt + RareCardsCnt + LegendCardsCnt);
                if (bonusCardsIndex.Contains(index)) continue;
                DebugStr += "Legend, ";
            }
            bonusCardsIndex.Add(index);
        }
        Debug.Log(DebugStr);
        foreach(int i in bonusCardsIndex)
        {
            CardProto BonusCard = Instantiate(AllBonusCardsSortedByRarity[i]);
            BonusCard.transform.SetParent(BonusCardZone,false);
            BonusCard.SetBonusZoneBehavior(i);
        }

    }
    public void Choose(CardProto card,int cardIDinBonusList)
    {
        ChosenCardID = cardIDinBonusList;
        if (ChosenCard != null)
            ChosenCard.Unpick();
        ChosenCard = card;
        ChosenCard.OnPick();
    }
    public void Comfirm()
    {
        if (ChosenCard == null)
            return;

        //add to card deck
        CardManager.Instance.AddNewCardToDeck(AllBonusCardsSortedByRarity[ChosenCardID]);
        //
        CloseBonusMenu();
    }
    public void Skip()
    {
        CloseBonusMenu();
    }
    void OpenBonusMenu()
    {
        BonusMenu.transform.localScale = Vector3.one;
    }
    void CloseBonusMenu()
    {
        //clear old cards
        int childCount = BonusCardZone.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(BonusCardZone.GetChild(i).gameObject);
        }

        BonusMenu.transform.localScale = Vector3.zero;
        TimeMgr.Instance.ResumeGame();
    }
}
