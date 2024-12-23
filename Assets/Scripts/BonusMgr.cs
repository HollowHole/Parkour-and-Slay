using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMgr : MonoBehaviour
{
    public static BonusMgr Instance;
    [SerializeField] GameObject BonusMenu;

    [SerializeField] List<CardProto> AllBonusCards;

    CardProto CardGonnaChoose;
    private void Awake()
    {
        Instance = this;

        CloseBonusMenu();
    }
    private void Start()
    {
        LevelMgr.Instance.OnLevelEnd += GenerateBonusCards;
        LevelMgr.Instance.OnLevelEnd += OpenBonusMenu;
        LevelMgr.Instance.OnLevelEnd += () => { Time.timeScale = 0; };
    }
    void GenerateBonusCards()
    {
        //clear old cards
        //generate new cards
    }
    public void Choose(CardProto card)
    {
        CardGonnaChoose = card;
    }
    public void Comfirm()
    {
        if (CardGonnaChoose == null)
            return;

        //add to card deck
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
        BonusMenu.transform.localScale = Vector3.zero;
        Time.timeScale = 1;
    }
}
