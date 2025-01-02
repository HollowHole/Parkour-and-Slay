using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverMgr : MonoBehaviour
{
    
    private void Start()
    {
        transform.localScale = Vector3.zero;
        FindObjectOfType<Player>().OnHpChange += GameoverJudge;
    }
    void GameoverJudge(float playerHp,float _, float __)
    {
        if (playerHp <= 0)
        {
            TimeMgr.Instance.PauseGame();
            transform.localScale = Vector3.one;
        }
    }
}
