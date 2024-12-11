using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    static CardManager instance;
    public static CardManager Instance=>instance;
    [SerializeField]List<CardProto> InitCards;
    List<CardProto> DrawPileCards = new List<CardProto>();
    List<CardProto> DiscardPileCards = new List<CardProto>();


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
    public Action<int> OnEnergyChange;
    //my var
    List<CardProto> handCards = new List<CardProto>();//not used yet
    CardProto ChosenCard;
    CardProto CardGonnaChoose;
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

        foreach (CardProto card in InitCards)
        {
            CardProto c = Instantiate(card,transform);
            DrawPileCards.Add(c);
        }

        for (int i = 0; i < MaxHandCnt; i++)
        {
            if (!DrawCard())
                break;            
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
                ChosenCard?.Unpick();

                ChosenCard = CardGonnaChoose;

                ChosenCard?.OnPick();
            }
            else
            {
                CardGonnaChoose = null;
                ChosenCard?.Unpick();
                ChosenCard=null;
            }
        }

    }
    public void Choose(CardProto card)
    {
        Debug.Log("Choose" + card.name);
        if (card == ChosenCard)
        {
            if(EnergyCnt < card.GetCost())
            {
                Debug.Log("·ÑÓÃ²»×ã");
            }
            else
                Use(card);
            CardGonnaChoose = null;
        }
        else
        {
            CardGonnaChoose = card;
        }
    }
    private void Use(CardProto card)
    {
        EnergyCnt -= card.GetCost();

        card.OnUse();
        //Discard
        Discard(card);
        //Draw
        DrawCard();
    }
    void Discard(CardProto card)
    {
        card.transform.SetParent(transform, false);
        DiscardPileCards.Add(card);
    }
    bool DrawCard()
    {
        if (DrawPileCards.Count == 0)
        {
            if (DrawFromDiscardPile() == false)
                return false;
        }
        CardProto card = DrawPileCards[DrawPileCards.Count - 1];
        DrawPileCards.Remove(card);
        card.transform.SetParent(HandCardZone, false);

        return true;
    }

    private bool DrawFromDiscardPile()
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
            yield return new WaitForSeconds(EnergyRegenerateRate);
            EnergyCnt = Math.Clamp(EnergyCnt + 1, 0, MaxEnergyLimit);
        }
    }
}
