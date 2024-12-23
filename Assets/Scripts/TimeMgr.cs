using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TimeMgr
{
    public static TimeMgr Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new TimeMgr();
                instance.PauseGameRequestCnt = 0;
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }
    static TimeMgr instance;

    int PauseGameRequestCnt;

    public void PauseGame()
    {
        PauseGameRequestCnt++;
        GamePauseJudge();
    }
    public void ResumeGame()
    {
        PauseGameRequestCnt--;
        if (PauseGameRequestCnt < 0)
        {
            PauseGameRequestCnt = 0;
        }
        GamePauseJudge();
    }
    void GamePauseJudge()
    {
        if(PauseGameRequestCnt > 0)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
        //Debug.Log("TimeScale: " + Time.timeScale);
    }
}
