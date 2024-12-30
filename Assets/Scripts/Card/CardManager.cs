using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    static CardManager instance;
    public static CardManager Instance=>instance;

    [SerializeField]List<CardProto> CardDeck;
    List<CardProto> DrawPileCards = new List<CardProto>();
    List<CardProto> DiscardPileCards = new List<CardProto>();
    List<CardProto> ConsumePileCards = new List<CardProto>();

    [SerializeField] Transform HandCardZone;
    //SO
    
    [SerializeField] int MaxHandCnt = 6;
    //energy
    [SerializeField] int MaxEnergyLimit = 4;
    [SerializeField] int InitEnergy = 3;
    [SerializeField] float EnergyRegenerateRate = 2f;
    private int energyCnt;
    public int EnergyCnt
    {
        get
        {
            return energyCnt;
        }
        private set
        {
            energyCnt = value;
            OnEnergyChange?.Invoke(energyCnt);
        }
    }
    //Events
    public Action<int> OnEnergyChange;
    public Action<CardProto> OnUseSpeedUpCard;
    //my var
    List<CardProto> handCards = new List<CardProto>();//not used yet
    CardProto ChosenCard;
    CardProto CardGonnaChoose;
    //bool useCardTrigger;//will Trigger on use card



    private void Awake()
    {
        ChosenCard = null;
        CardGonnaChoose = null;
        instance = this;
        //SO Initialization
        EnergyCnt = InitEnergy;
    }

    private void Start()
    {
        StartCoroutine(GenerateEnergyNaturally());

        SpawnAllCardsInDeck();

        LevelMgr.Instance.OnLevelEnd += RemoveAllCards;
        LevelMgr.Instance.OnLevelBegin += SpawnAllCardsInDeck;
    }
    void SpawnAllCardsInDeck()
    {
        foreach (CardProto card in CardDeck)
        {
            CardProto c = Instantiate(card, transform);
            DrawPileCards.Add(c);
            c.SetHandZoneBehavior();
        }
    }
    public void AddNewCardToDeck(CardProto card)
    {
        CardDeck.Add(card);
    }
    public void RemoveAllCards()
    {
        foreach(CardProto card in DrawPileCards) {
            Destroy(card.gameObject);
        }
        foreach (CardProto card in DiscardPileCards)
        {
            Destroy(card.gameObject);
        }
        foreach(Transform cardGo in HandCardZone.transform)
        {
            Destroy(cardGo.gameObject);
        }
        foreach(CardProto cardGo in ConsumePileCards)
        {
            Destroy(cardGo.gameObject);
        }
        DrawPileCards = new List<CardProto>();
        DiscardPileCards = new List<CardProto>();
        ConsumePileCards = new List<CardProto>();
    }
    private void Update()
    {
        if(HandCardZone.childCount  == 0)//手牌打完了
        {
            DrawCard();
        }
    }
    private void LateUpdate()
    {
        UpdateChosenCard();
    }
    void UpdateChosenCard()
    {
        if (Input.GetMouseButtonUp(0))//TODO: Or use keyboard to choose card(any key not ESC pressed but no change happen) 
        {
            if (ChosenCard != CardGonnaChoose)
            {
                if (ChosenCard != null)
                ChosenCard.Unpick();

                ChosenCard = CardGonnaChoose;

                if (ChosenCard != null)
                ChosenCard.OnPick();

                TimeMgr.Instance.BulletTime();
            }
            //else if(useCardTrigger && ChosenCard.cardTypes.Contains(CardType.Comsume))//使用了消耗卡，则卡已被销毁
            //{
            //    CardGonnaChoose = null;
            //    ChosenCard = null;
            //}
            else
            {
                CardGonnaChoose = null;
                if(ChosenCard != null)
                    ChosenCard.Unpick();
                ChosenCard = null;

                TimeMgr.Instance.EndBulletTime();
            }
        }

    }
    public void Choose(CardProto card)
    {
        CardGonnaChoose = card;
        //Debug.Log("Choose " + card.name);
        if (card == ChosenCard)
        {
            if(EnergyCnt < card.GetCost())
            {
                Debug.Log("费用不足");
            }
            else
                Use(card);
        }
    }
    private void Use(CardProto card)
    {
        EnergyCnt -= card.GetCost();

        if(card.SpeedUpValue > 0)
        {
            OnUseSpeedUpCard?.Invoke(card);
        }

        card.OnUse();
        if (card.cardTypes.Contains(CardType.Consume))//消耗类型
        {
            //DestroyImmediate(card.gameObject);
            Consume(card);
        }
        else
        {
            Discard(card);
        }

        //Debug cardPile information
        //StringBuilder stringBuilder = new StringBuilder();
        //foreach (CardProto c in DrawPileCards)
        //{
        //    stringBuilder.Append(c.ToString()+" ");
        //}
        //Debug.Log("DrawPileCard:" + stringBuilder);
        //stringBuilder = new StringBuilder();
        //foreach (CardProto c in DiscardPileCards)
        //{
        //    stringBuilder.Append(c.ToString() + " ");
        //}
        //Debug.Log("DiscardPileCards:" + stringBuilder);
        //stringBuilder = new StringBuilder();
    }

    private void Consume(CardProto card)
    {
        card.transform.SetParent(transform, false);
        ConsumePileCards.Add(card);
    }

    void Discard(CardProto card)
    {
        card.transform.SetParent(transform, false);
        DiscardPileCards.Add(card);
    }
    void DrawCard()
    {
        for (int i = 0; i < MaxHandCnt; i++)
        {
            if (!DrawOneCard())
                break;
        }
    }
    public bool DrawOneCard()
    {
        if (DrawPileCards.Count == 0)
        {
            if (Shuffle() == false)
                return false;
        }
        CardProto card = DrawPileCards[DrawPileCards.Count - 1];
        DrawPileCards.Remove(card);
        card.transform.SetParent(HandCardZone, false);

        return true;
    }

    private bool Shuffle()//从弃牌堆洗到抽牌堆
    {
        if (DiscardPileCards.Count == 0)
            return false;

        while(DiscardPileCards.Count > 0)
        {
            int index = UnityEngine.Random.Range(0,DiscardPileCards.Count);
            DrawPileCards.Add(DiscardPileCards[index]);
            DiscardPileCards.RemoveAt(index);
        }
        return true;
    }
    IEnumerator GenerateEnergyNaturally()
    {
        while (true)
        {
            yield return new WaitForSeconds(1/EnergyRegenerateRate);
            EnergyCnt = Math.Clamp(EnergyCnt + 1, 0, MaxEnergyLimit);
        }
    }
}
