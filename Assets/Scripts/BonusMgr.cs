using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMgr : MonoBehaviour
{
    public static BonusMgr Instance;
    [SerializeField] GameObject BonusMenu;

    [SerializeField] List<CardProto> AllBonusCards;

    [SerializeField] Transform BonusCardZone;
    [Tooltip("可选奖励卡牌的数量")]
    public int BonusCardCount = 3;
    CardProto ChosenCard;
    int ChosenCardID;

    private void Awake()
    {
        Instance = this;

        CloseBonusMenu();
    }
    private void Start()
    {
        LevelMgr.Instance.OnLevelEnd += GenerateBonusCards;
        LevelMgr.Instance.OnLevelEnd += OpenBonusMenu;
        LevelMgr.Instance.OnLevelEnd += () => { TimeMgr.Instance.PauseGame(); };
    }
    void GenerateBonusCards()
    {
        
        //generate new cards
        for (int i = 0; i < BonusCardCount; i++)
        {
            int index = Random.Range(0, AllBonusCards.Count);
            CardProto BonusCard = Instantiate(AllBonusCards[index]);//等权随机选一个
            BonusCard.transform.SetParent(BonusCardZone,false);
            BonusCard.SetBonusZoneBehavior(index);
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
        CardManager.Instance.AddNewCardToDeck(AllBonusCards[ChosenCardID]);
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
