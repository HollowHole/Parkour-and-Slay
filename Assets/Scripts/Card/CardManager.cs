using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardManager : MonoBehaviour
{

    static CardManager instance;
    public static CardManager Instance=>instance;
    List<CardProto> cards;
    CardProto ChosenCard;
    
    private void Start()
    {
        ChosenCard = null;
        cards = new List<CardProto>();
        instance = this;
        
    }
    private void Update()
    {
        
    }
    public void Choose(CardProto card)
    {
        //CardProto card = cardGo.GetComponent<CardProto>();
        //Debug.Log("click"+card.name);
        if (card == ChosenCard)
        {
            card.OnUse();
            card.Unpick();
            ChosenCard = null;
        }
        else
        {
            if(ChosenCard !=null)
            { 
                ChosenCard.Unpick();
            }
            card.OnPick();
            ChosenCard = card;
        }
    }
    //�������߼�����������ð�ť�Ļ���ֻ����Я�����ж�����ǲ��ǵ��������ط�
    IEnumerable  MouseClickInspector()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0)) { 
                
            }
            yield return null;
        }
    }
}
