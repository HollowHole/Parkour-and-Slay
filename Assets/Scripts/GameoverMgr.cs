using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverMgr : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<Player>().OnHpChange += GameoverJudge;
    }
    void GameoverJudge(float playerHp,float _)
    {
        if (playerHp < 0)
        {
            Debug.Log("GamgOver!");
        }
    }
}
