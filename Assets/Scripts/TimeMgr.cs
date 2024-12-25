using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TimeMgr
{
    bool isBulletTime = false;
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
        TimeJudge();
    }
    public void ResumeGame()
    {
        PauseGameRequestCnt--;
        if (PauseGameRequestCnt < 0)
        {
            PauseGameRequestCnt = 0;
        }
        TimeJudge();
    }
    public void BulletTime()
    {
        isBulletTime = true;
        TimeJudge() ;
    }
    public void EndBulletTime()
    {
        isBulletTime =false;
        Debug.Log("END Bullet Time");
        TimeJudge();
    }
    void TimeJudge()
    {
        if (PauseGameRequestCnt > 0)
            Time.timeScale = 0;
        else if (isBulletTime)
            Time.timeScale = 0.2f;
        else
            Time.timeScale = 1;
        //Debug.Log("TimeScale: " + Time.timeScale);
    }
}
